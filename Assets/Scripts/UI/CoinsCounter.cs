using UnityEngine;
using TMPro;
using Zenject;
using UI.Animations;

namespace UI
{
    internal class CoinsCounter : MonoBehaviour
    {
        [SerializeField]
        private float oneCoinCost;

        [SerializeField]
        private TMP_Text balanceText;

        private float _currentBalance;
        private CoinImageAnimation _coinImageAnimation;

        private void UpdateBalance()
        {
            _currentBalance += oneCoinCost;
            balanceText.text = _currentBalance.ToString();
            _coinImageAnimation.LaunchAnimation();
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Injections

        [Inject]
        private CoinsPool _coinsPool;

        [Inject]
        private void OnConstruct()
        {
            _coinImageAnimation = GetComponentInChildren<CoinImageAnimation>();
            balanceText.text = "0";
            SetSubscriptions();
        }

#endregion

#region Subscruptions
        private void SetSubscriptions()
        {
            _coinsPool.onCoinReleased += UpdateBalance;
        }

        private void ClearSubscriptions()
        {
            _coinsPool.onCoinReleased -= UpdateBalance;
        }

#endregion

    }
}
