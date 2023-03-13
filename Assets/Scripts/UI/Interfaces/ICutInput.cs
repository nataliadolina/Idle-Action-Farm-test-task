using UnityEngine;
using System;

namespace UI.Interfaces
{ 
    internal interface ICutInput
    {
        public event Action onCutButtonPressed;
    }
}
