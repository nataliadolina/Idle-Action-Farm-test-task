using UnityEngine;
using UI.Abstract;
using UI.Interfaces;
using System;
using Systems;
using Zenject;

namespace UI
{
    internal class CutInput : ClickableBase, ICutInput
    {
        public event Action onCutButtonPressed;

        [SerializeField]
        private GameObject buttonGameObject;

        private void SetButtonGameObjectActive(bool value)
        {
            buttonGameObject.SetActive(value);
        }

        private protected override void OnClick()
        {
            onCutButtonPressed?.Invoke();
        }

#region MonoBehaviour

        private protected override void AwakeInternal()
        {
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Injections

        [Inject]
        private HarvestColliderCutReceiversContainer _harvestColliderCutReceiversContainer;

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _harvestColliderCutReceiversContainer.onIsThereAnyCuttableHarvestChanged += SetButtonGameObjectActive;
        }

        private void ClearSubscriptions()
        {
            _harvestColliderCutReceiversContainer.onIsThereAnyCuttableHarvestChanged -= SetButtonGameObjectActive;
        }

#endregion

    }
}