using UnityEngine;
using System;
using Systems;

namespace Environment
{
    internal sealed class WheatColliderCutReceiver : MonoBehaviour
    {
        internal event Action<WheatColliderCutReceiver, bool> onHarvestCanBeCutChanged;

        [SerializeField]
        private GrowingWheat growingWheat;
        [SerializeField]
        private DistanceToSubjectZoneProcessor cutDistanceToSubjectZoneProcessor;

        internal event Action onWheatCut;
        private bool _canBeCut = false;

        internal bool CanBeCut => _canBeCut;

        private bool _playerInCutZone;
        private bool _wheatIsRipe;

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
            growingWheat.onWheatIsGrowing += OnWheatIsGrowingChanged;
            cutDistanceToSubjectZoneProcessor.onAimEnterZone += OnPlayerEnteredCutZone;
            cutDistanceToSubjectZoneProcessor.onAimExitZone += OnPlayerExitedCutZone;
        }

        private void ClearSubscriptions()
        {
            growingWheat.onWheatIsGrowing -= OnWheatIsGrowingChanged;
            cutDistanceToSubjectZoneProcessor.onAimEnterZone -= OnPlayerEnteredCutZone;
            cutDistanceToSubjectZoneProcessor.onAimExitZone -= OnPlayerExitedCutZone;
        }

#endregion

    }
}
