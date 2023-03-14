using UnityEngine;
using Zenject;
using Environment;
using Utilities.Configuration;
using System;
using System.Collections.Generic;

namespace Player
{
    internal class HarvestBag : MonoBehaviour
    {
        internal event Action<int> onHarvestBlockAddToStack;

        [SerializeField]
        private int blocksPerX;
        [SerializeField]
        private int blocksPerY;

        [SerializeField]
        private Vector3 blockInStackSize;

        private Vector3 _blockInStackStartPosition;
        private int _blocksCount = 0;

        private List<WheatBlock> _blocksInStack = new List<WheatBlock>();

        private void Start()
        {
            _blockInStackStartPosition = new Vector3(blockInStackSize.x/2, blockInStackSize.y / 2, -blockInStackSize.z / 2);
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

        private void AddBlockToStack(WheatBlock block, Transform blockTransform, Rigidbody rigidbody)
        {
            if (_blocksInStack.Contains(block))
            {
                return;
            }

            _blocksInStack.Add(block);
            rigidbody.isKinematic = true;

            blockTransform.parent = transform;
            blockTransform.localScale = blockInStackSize;
            blockTransform.localRotation = Quaternion.identity;
            blockTransform.localPosition = GetPositionInStack();

            _blocksCount++;
            onHarvestBlockAddToStack?.Invoke(_blocksCount);
            if (_blocksCount == _stackSize)
            {
                ClearSubscriptions();
                Debug.Log("Stack us full");
            }
        }

        private Vector3 GetPositionInStack()
        {
            float x = _blockInStackStartPosition.x + _blocksCount % blocksPerX * blockInStackSize.x;
            float y = _blockInStackStartPosition.y + _blocksCount % (blocksPerX * blocksPerY) / blocksPerX * blockInStackSize.y;
            float z = _blockInStackStartPosition.y - _blocksCount / (blocksPerX * blocksPerY) * blockInStackSize.z;
            return new Vector3(x, y, z);
        }

#region Injections

        [Inject]
        private Wheat[] _allWheat;

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
        }

        private void ClearSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onAddWheatBlockToStack -= AddBlockToStack;
            }
        }

#endregion
    }
}