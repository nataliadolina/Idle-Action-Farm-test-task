using UnityEngine;
using Utilities.Pool;
using UI.Animations;
using Zenject;
using Environment;
using System;

namespace UI
{
    internal class CoinsPool : MonoBehaviour
    {
        internal event Action onCoinReleased;

        [SerializeField]
        private int poolSize;
        [SerializeField]
        private bool autoExpand;
        [SerializeField]
        private string prefabLink = "Prefabs/UI/CoinImage";

        private PoolMono<CoinImageFlightAnimation> _poolMono;
        private void Start()
        {
            _poolMono = new PoolMono<CoinImageFlightAnimation>(Resources.Load<CoinImageFlightAnimation>(prefabLink), poolSize, transform, autoExpand);
        }

        private void GetFreeElement()
        {
            CoinImageFlightAnimation coinImageFlightAnimation = _poolMono.GetFreeELement();
            coinImageFlightAnimation.LaunchTween();
        }

        internal void SendCoinReleaseEvent()
        {
            onCoinReleased?.Invoke();
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Injections

        [Inject]
        private Wheat[] _allWheat;

        [Inject]
        private void OnConstruct()
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onStopSellProcessEvent += GetFreeElement;
            }
        }

        private void ClearSubscriptions()
        {
            foreach (var wheat in _allWheat)
            {
                wheat.onStopSellProcessEvent -= GetFreeElement;
            }
        }
        
#endregion
    }
}
