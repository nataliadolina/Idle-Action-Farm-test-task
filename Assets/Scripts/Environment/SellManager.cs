using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Systems;
using UI.Interfaces;
using Utilities.Utils;

namespace Environment
{
    internal sealed class SellManager : MonoBehaviour
    {
        internal event Action<bool> onPlayerCanSellHarvestChanged;

        [SerializeField]
        private DistanceToPlayerZoneProcessor distanceToPlayerZoneProcessor;

        private bool _hasHarvestInStack = false;
        private bool _playerInSellZone = false;

#region MonoBehaviour

        private void Start()
        {
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void HarvestAddedToStack(WheatBlockArgs _)
        {
            _hasHarvestInStack = true;
            onPlayerCanSellHarvestChanged?.Invoke(_playerInSellZone);
        }

        private void PlayerEnteredSellZone()
        {
            _playerInSellZone = true;
            onPlayerCanSellHarvestChanged?.Invoke(_hasHarvestInStack);
        }

        private void PlayerExitedSellZone()
        {
            _playerInSellZone = false;
            onPlayerCanSellHarvestChanged?.Invoke(false);
        }

        private void SellAllHarvestInStack()
        {
            if (_hasHarvestInStack && _playerInSellZone)
            {
                _hasHarvestInStack = false;
                onPlayerCanSellHarvestChanged?.Invoke(false);
            }
        }

#region Injections

        [Inject]
        private Wheat[] _allWheat;

        [Inject]
        private ISellInput _sellInput;

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onAddWheatBlockToStack += HarvestAddedToStack;
            }
           
            _sellInput.onSellButtonPressed += SellAllHarvestInStack;
            distanceToPlayerZoneProcessor.onAimEnterZone += PlayerEnteredSellZone;
            distanceToPlayerZoneProcessor.onAimExitZone += PlayerExitedSellZone;
        }

        private void ClearSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onAddWheatBlockToStack -= HarvestAddedToStack;
            }

            _sellInput.onSellButtonPressed -= SellAllHarvestInStack;
            distanceToPlayerZoneProcessor.onAimEnterZone -= PlayerEnteredSellZone;
            distanceToPlayerZoneProcessor.onAimExitZone -= PlayerExitedSellZone;
        }

#endregion
    }
}
