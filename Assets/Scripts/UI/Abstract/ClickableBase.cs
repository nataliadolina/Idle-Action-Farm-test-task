using UnityEngine;
using UnityEngine.UI;

namespace UI.Abstract
{
    internal abstract class ClickableBase : MonoBehaviour
    {
        private protected Button _button;
        private protected GameObject _buttonGameObject;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonGameObject = _button.gameObject;
            _button.onClick.AddListener(OnClick);
        }

        private void Start()
        {
            OnStartInternal();
        }

        private protected virtual void OnStartInternal() { }

        private protected abstract void OnClick();
    }
}