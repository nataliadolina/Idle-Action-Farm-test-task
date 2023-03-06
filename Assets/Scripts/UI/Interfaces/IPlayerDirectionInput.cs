using UnityEngine;
using System;

namespace UI.Interfaces
{
    internal interface IPlayerDirectionInput
    {
        event Action<Vector2> onCharacterDirectionChanged;
    }
}