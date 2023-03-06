using UnityEngine;
using System;
using UI.Abstract;
using UI.Interfaces;

namespace UI
{
    internal class PlayerDirectionInput : JoystickAreaHandler, IPlayerDirectionInput
    {
        /// <summary>
        /// Vector2: direction
        /// </summary>
        public event Action<Vector2> onCharacterDirectionChanged;

        private protected override void UpdatePlayerDirection(in Vector2 direction)
        {
            onCharacterDirectionChanged?.Invoke(direction);
        }
    }
}
