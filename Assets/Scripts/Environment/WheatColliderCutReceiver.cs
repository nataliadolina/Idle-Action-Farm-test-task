using UnityEngine;
using System;
using Systems;

namespace Environment
{
    internal sealed class WheatColliderCutReceiver : MonoBehaviour
    {
        internal event Action<WheatColliderCutReceiver, bool> onHarvestCanBeCutChanged;

        [SerializeField]
        private DistanceToPlayerZoneProcessor cutDistanceToPlayerZoneProcessor;

        internal event Action onWheatCut;
        private bool _canBeCut = false;

        internal bool CanBeCut => _canBeCut;

        private bool _playerInCutZone;
        private bool _wheatIsRipe;

        private GrowingWheat _growingWheat;
        private WheatColliderCutReceiver _wheatCutReceiver;

#region MonoBehaviour

        private void Start()
        {
            Wheat parentContainer = GetComponentInParent<Wheat>();
            _growingWheat = parentContainer.GrowingWheat;
            _wheatCutReceiver = parentContainer.WheatColliderCutReveiver;
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion
        
        private void OnPlayerEnteredCutZone()
        {
            _playerInCutZone = true;
            _canBeCut = _wheatIsRipe && _playerInCutZone;
            onHarvestCanBeCutChanged?.Invoke(this, _canBeCut);
        }

        private void OnPlayerExitedCutZone()
        {
            _playerInCutZone = false;
            _canBeCut = false;
            onHarvestCanBeCutChanged?.Invoke(this, false);
        }

        private void OnWheatIsGrowingChanged(bool isWheatInGrowProcess)
        {
            _wheatIsRipe = !isWheatInGrowProcess;
            _canBeCut = _wheatIsRipe && _playerInCutZone;
            onHarvestCanBeCutChanged?.Invoke(this, _canBeCut);
        }

        private void OnWheatCut()
        {
            OnWheatIsGrowingChanged(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_canBeCut && other.CompareTag("Instrument"))
            {
                onWheatCut?.Invoke();
            }
        }

#region Subsctriptions

        private void SetSubscriptions()
        {
            _growingWheat.onWheatIsGrowing += OnWheatIsGrowingChanged;
            cutDistanceToPlayerZoneProcessor.onAimEnterZone += OnPlayerEnteredCutZone;
            cutDistanceToPlayerZoneProcessor.onAimExitZone += OnPlayerExitedCutZone;
            _wheatCutReceiver.onWheatCut += OnWheatCut;
        }

        private void ClearSubscriptions()
        {
            _growingWheat.onWheatIsGrowing -= OnWheatIsGrowingChanged;
            cutDistanceToPlayerZoneProcessor.onAimEnterZone -= OnPlayerEnteredCutZone;
            cutDistanceToPlayerZoneProcessor.onAimExitZone -= OnPlayerExitedCutZone;
            _wheatCutReceiver.onWheatCut -= OnWheatCut;
        }

#endregion

    }
}
