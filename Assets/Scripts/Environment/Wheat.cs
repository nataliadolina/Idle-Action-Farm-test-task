using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Environment
{
    internal class Wheat : MonoBehaviour
    {
        internal event Action<WheatBlock, Transform, Rigidbody> onAddWheatBlockToStack;

        [SerializeField]
        private WheatColliderCutReceiver wheatColliderCutReveiver;

        internal WheatColliderCutReceiver WheatColliderCutReveiver => wheatColliderCutReveiver;

        internal void SendAddToStackEvent(WheatBlock wheatBlock, Transform wheatTransform, Rigidbody rigidBody)
        {
            onAddWheatBlockToStack?.Invoke(wheatBlock, wheatTransform, rigidBody);
        }
    }
}
