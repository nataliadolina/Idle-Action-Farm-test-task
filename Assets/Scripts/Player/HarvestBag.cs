using UnityEngine;
using Zenject;
using Environment;
using System.Collections.Generic;
using Utilities.Configuration;

namespace Player
{
    internal class HarvestBag : MonoBehaviour
    {
        [SerializeField]
        private Vector3 maxStackScale;
        [SerializeField]
        private Transform stackFixingPoint;
        [SerializeField]
        private Transform blocksStackTransform;

        private GameObject _blocksStackGameObject;
        private int _blocksCount = 0;

        private void Start()
        {
            _blocksStackGameObject = blocksStackTransform.gameObject;
            _blocksStackGameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

        private void AddWheatBlockToStack()
        {
            _blocksCount++;
            SetStackScale();
        }

        private void SetStackScale()
        {
            if (!_blocksStackGameObject.activeSelf)
            {
                _blocksStackGameObject.SetActive(true);
            }

            Vector3 scale = new Vector3(maxStackScale.x, maxStackScale.y * _blocksCount / _stackSize, maxStackScale.z);
            blocksStackTransform.localScale = scale;
            blocksStackTransform.position = stackFixingPoint.position + Vector3.up * scale.y / 2;
        }

#region Dependencies

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
                wheatBlock.onAddWheatBlockToStack += AddWheatBlockToStack;
            }
        }

        private void ClearSubscriptions()
        {
            foreach (var wheatBlock in _wheatBlocks)
            {
                wheatBlock.onAddWheatBlockToStack -= AddWheatBlockToStack;
            }
        }

#endregion
    }
}