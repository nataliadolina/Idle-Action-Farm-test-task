using UnityEngine;
using System;
using Utilities.Utils;

namespace Environment
{
    internal class Wheat : MonoBehaviour
    {
        internal event Action<WheatBlockArgs> onAddWheatBlockToStack;
        internal event Action onStopSellProcessEvent;

        [SerializeField]
        private WheatColliderCutReceiver wheatColliderCutReveiver;
        [SerializeField]
        private GrowingWheat growingWheat;

        internal WheatColliderCutReceiver WheatColliderCutReveiver => wheatColliderCutReveiver;
        internal GrowingWheat GrowingWheat => growingWheat;

        internal void SendAddToStackEvent(WheatBlockArgs wheatBlockArgs)
        {
            onAddWheatBlockToStack?.Invoke(wheatBlockArgs);
        }

        internal void SendStopSellProcessEvent()
        {
            onStopSellProcessEvent?.Invoke();
        }
    }
}
