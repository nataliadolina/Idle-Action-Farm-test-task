using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Abstract;
using UI.Interfaces;
using System;
using Systems;
using Zenject;
using Environment;

namespace UI
{
    internal class SellInput : ClickableBase, ISellInput
    {
        public event Action onSellButtonPressed;

        [SerializeField]
        private GameObject buttonGameObject;

#region MonoBehaviour

        private protected override void AwakeInternal()
        {
            buttonGameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void SetButtonGameObjectActive(bool value)
        {
            buttonGameObject.SetActive(value);
        }

        private protected override void OnClick()
        {
            onSellButtonPressed?.Invoke();
        }

#region Injections

        [Inject]
        private SellManager _sellManager;

        [Inject]
        private void OnConstruct()
        {
            SetSubscriptions();
        }

#endregion


#region Subscriptions

        private void SetSubscriptions()
        {
            _sellManager.onPlayerCanSellHarvestChanged += SetButtonGameObjectActive;
        }

        private void ClearSubscriptions()
        {
            _sellManager.onPlayerCanSellHarvestChanged -= SetButtonGameObjectActive;
        }

#endregion

    }
}