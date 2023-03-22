using UnityEngine;
using Zenject;
using Environment.Animations;
using Environment;
using Utilities.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.Interfaces;
using Utilities.Utils;

namespace Player
{
    internal class HarvestStack : MonoBehaviour
    {
        internal event Action<int> onHarvestBlockAddToStack;
        internal event Action onStackIsFull;

        [SerializeField]
        private int blocksPerX;
        [SerializeField]
        private int blocksPerY;

        [SerializeField]
        private Vector3 blockInStackSize;

        private Vector3 _blockInStackStartPosition;
        private int _blocksCount = 0;

        private int _currentBlock = 0;

        private List<WheatBlockArgs> _blocksInStackArgs = new List<WheatBlockArgs>();

        private void Start()
        {
            _blockInStackStartPosition = new Vector3(blockInStackSize.x/2, blockInStackSize.y / 2, -blockInStackSize.z / 2);
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

        private void AddBlockToStack(WheatBlockArgs wheatBlockArgs)
        {
            if (_blocksInStackArgs.Contains(wheatBlockArgs))
            {
                return;
            }

            if (_blocksCount == _stackSize)
            {
                onStackIsFull?.Invoke();
                return;
            }

            Rigidbody rigidbody = wheatBlockArgs.Rigidbody;
            Transform blockTransform = wheatBlockArgs.BlockTransform;
            WheatBlockFlightAnimation wheatBlockFlightAnimation = wheatBlockArgs.WheatBlockFlightAnimation;

            _blocksInStackArgs.Add(wheatBlockArgs);
            rigidbody.isKinematic = true;

            blockTransform.parent = transform;
            blockTransform.localScale = blockInStackSize;
            blockTransform.localRotation = Quaternion.identity;
            Vector3 position = GetPositionInStack();
            blockTransform.localPosition = position;

            _blocksCount++;
            onHarvestBlockAddToStack?.Invoke(_blocksCount);
            if (_blocksCount == _stackSize)
            {
                onStackIsFull?.Invoke();
            }
        }

        private Vector3 GetPositionInStack()
        {
            float x = _blockInStackStartPosition.x + _blocksCount % blocksPerX * blockInStackSize.x;
            float y = _blockInStackStartPosition.y + _blocksCount % (blocksPerX * blocksPerY) / blocksPerX * blockInStackSize.y;
            float z = _blockInStackStartPosition.y - _blocksCount / (blocksPerX * blocksPerY) * blockInStackSize.z;
            return new Vector3(x, y, z);
        }

        private void ClearStack()
        {
            _currentBlock = _blocksCount - 1;
            StartCoroutine(WaitToStartBlockAnimation());
        }

        private IEnumerator WaitToStartBlockAnimation()
        {
            while (_currentBlock > -1)
            {
                WheatBlockArgs currentArgs = _blocksInStackArgs[_currentBlock];

                WheatBlockFlightAnimation wheatBlockArgs = currentArgs.WheatBlockFlightAnimation;
                Transform curentBlockTransform = currentArgs.BlockTransform;
                curentBlockTransform.parent = null;

                yield return new WaitForSeconds(0.05f);
                wheatBlockArgs.CreateAnimationSequenceAndPlay();
                _blocksInStackArgs.Remove(currentArgs);
                _currentBlock--;
                _blocksCount--;
                onHarvestBlockAddToStack?.Invoke(_blocksCount);
            }
        }

#region Injections

        [Inject]
        private Wheat[] _allWheat;

        [Inject]
        private ISellInput _sellInput;

        [Inject]
        private PlayerSettingsConfig _playerSettingsConfig;

        private int _stackSize;

        [Inject]
        private void OnConstruct()
        {
            _stackSize = _playerSettingsConfig.HarvestStackSize;
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onAddWheatBlockToStack += AddBlockToStack;
            }

            _sellInput.onSellButtonPressed += ClearStack;
        }

        private void ClearSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onAddWheatBlockToStack -= AddBlockToStack;
            }

            _sellInput.onSellButtonPressed -= ClearStack;
        }

#endregion
    }
}