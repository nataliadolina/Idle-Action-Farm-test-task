using UnityEngine;
using UnityEngine.UI;

namespace UI.Abstract
{
    internal abstract class ClickableBase : MonoBehaviour
    {
        private protected Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
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