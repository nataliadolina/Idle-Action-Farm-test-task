using UnityEngine;
using Zenject;
using Environment;
using Utilities.Configuration;
using System;

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

        private void Start()
        {
            _blockInStackStartPosition = new Vector3(blockInStackSize.x/2, blockInStackSize.y / 2, -blockInStackSize.z / 2);
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

        private void AddBlockToStack(Transform blockTransform, Rigidbody rigidbody)
        {
            rigidbody.isKinematic = true;

            blockTransform.parent = transform;
            blockTransform.localScale = blockInStackSize;
            blockTransform.localRotation = Quaternion.identity;
            blockTransform.localPosition = GetPositionInStack();

            _blocksCount++;
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
        private WheatBlock[] _wheatBlocks;

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
            foreach (var wheatBlock in _wheatBlocks)
            {
                wheatBlock.onAddWheatBlockToStack += AddBlockToStack;
            }
        }

        private void ClearSubscriptions()
        {
            foreach (var wheatBlock in _wheatBlocks)
            {
                wheatBlock.onAddWheatBlockToStack -= AddBlockToStack;
            }
        }

#endregion
    }
}