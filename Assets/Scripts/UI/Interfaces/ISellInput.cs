using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI.Interfaces
{
    internal interface ISellInput
    {
        public event Action onSellButtonPressed;
    }
}