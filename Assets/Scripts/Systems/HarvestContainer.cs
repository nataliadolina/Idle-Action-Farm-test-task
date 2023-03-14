using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Environment;

namespace Systems
{
    internal class HarvestContainer : MonoBehaviour
    {
        internal event Action<bool> onIsThereAnyCuttableHarvestChanged;
        
        private List<WheatColliderCutReceiver> _cuttableHarvest = new List<WheatColliderCutReceiver>();

        private bool _isThereAnyCuttableHarvest = true;
        private bool IsThereAnyCuttableHarvest
        {
            get => _isThereAnyCuttableHarvest;
            set
            {
                if (value != _isThereAnyCuttableHarvest)
                {
                    _isThereAnyCuttableHarvest = value;
                    onIsThereAnyCuttableHarvestChanged?.Invoke(value);
                }

            }
        }

        private void OnAnyHarvestCanBeCutSetTrue(WheatColliderCutReceiver currentCutReceiver)
        {
            bool isExistingChest = false;
            foreach (var cutReceiver in _cuttableHarvest)
            {
                if (cutReceiver.Equals(currentCutReceiver))
                {
                    isExistingChest = true;
                    break;
                }
            }

            if (!isExistingChest)
            {
                _cuttableHarvest.Add(currentCutReceiver);
            }

            IsThereAnyCuttableHarvest = _cuttableHarvest.Count > 0;
        }

        private void OnAnyHarvestCanBeCutSetFalse(WheatColliderCutReceiver currentCutReceiver)
        {
            _cuttableHarvest.Remove(currentCutReceiver);
            IsThereAnyCuttableHarvest = _cuttableHarvest.Count > 0;
        }

        private void OnAnyHarvestCanBeCutChanged(WheatColliderCutReceiver currentCutReceiver, bool canBeCut)
        {
            switch (canBeCut)
            {
                case true:
                    OnAnyHarvestCanBeCutSetTrue(currentCutReceiver);
                    break;
                case false:
                    OnAnyHarvestCanBeCutSetFalse(currentCutReceiver);
                    break;
            }
        }

#region MonoBehaviour

        private void Start()
        {
            IsThereAnyCuttableHarvest = _cuttableHarvest.Count > 0;
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubstriptions();
        }

#endregion

#region Injections

        [Inject]
        private WheatColliderCutReceiver[] _allCutReceivers;

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            foreach (var cutReceiver in _allCutReceivers)
            {
                cutReceiver.onHarvestCanBeCutChanged += OnAnyHarvestCanBeCutChanged;
            }
        }

        private void ClearSubstriptions()
        {
            foreach (var cutReceiver in _allCutReceivers)
            {
                cutReceiver.onHarvestCanBeCutChanged -= OnAnyHarvestCanBeCutChanged;
            }
        }

#endregion
    }
}
