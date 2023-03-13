using UnityEngine;
using UI.Abstract;
using UI.Interfaces;
using System;

namespace UI
{
    internal class CutInput : ClickableBase, ICutInput
    {
        public event Action onCutButtonPressed;

        private protected override void OnClick()
        {
            onCutButtonPressed?.Invoke();
        }
    }
}