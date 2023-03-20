using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment.Animations;
using Environment;

namespace Utilities.Utils
{
    internal struct WheatBlockArgs
    {
        internal WheatBlockFlightAnimation WheatBlockFlightAnimation;
        internal Transform BlockTransform;
        internal Rigidbody Rigidbody;

        internal WheatBlockArgs(WheatBlockFlightAnimation wheatBlockFlightAnimation, Transform blockTransform, Rigidbody rigidbody)
        {
            WheatBlockFlightAnimation = wheatBlockFlightAnimation;
            BlockTransform = blockTransform;
            Rigidbody = rigidbody;
        }
    }
}
