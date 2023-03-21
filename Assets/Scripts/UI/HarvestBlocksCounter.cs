using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Utilities.Configuration;
using Player;
using TMPro;

namespace UI
{
    internal class HarvestBlocksCounter : MonoBehaviour
    {
        [SerializeField]
        private Image fillImage;

        [SerializeField]
        private TMP_Text currentHarvestBlocksAmountText;

        [SerializeField]
        private TMP_Text maxHarvestBlocksAmountText;

        private int _maxHarvestBlocksAmount;

        private void SetFillAmountAndText(int currentHarvestBlocksAmount)
        {
            currentHarvestBlocksAmountText.text = currentHarvestBlocksAmount.ToString();
            fillImage.fillAmount = (float)currentHarvestBlocksAmount / _maxHarvestBlocksAmount;
        }

#region MonoBehaviour

        private void Start()
        {
            SetFillAmountAndText(0);
        }
        
        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Injections

        [Inject]
        private PlayerSettingsConfig _playerSettingsConfig;

        [Inject]
        private HarvestStack _harvestStack;

        [Inject]
        private void OnConstruct()
        {
            _maxHarvestBlocksAmount = _playerSettingsConfig.HarvestStackSize;
            maxHarvestBlocksAmountText.text = _maxHarvestBlocksAmount.ToString();
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _harvestStack.onHarvestBlockAddToStack += SetFillAmountAndText;
        }

        private void ClearSubscriptions()
        {
            _harvestStack.onHarvestBlockAddToStack -= SetFillAmountAndText;
        }

#endregion
    }
}