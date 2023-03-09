using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

namespace Systems
{
    internal class SliceSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToSlice;
        [SerializeField]
        private Transform slicePosition;
        [SerializeField]
        private Vector3 sliceNormal;

        private Vector3 _objectToSliceStartPosition;

        private void Start()
        {
            _objectToSliceStartPosition = objectToSlice.transform.position;
        }

        public void PerformSlice()
        {
            GameObject[] slices = SliceInstantiate(slicePosition.position, sliceNormal);

            foreach(GameObject slice in slices)
            {
                slice.transform.position = _objectToSliceStartPosition;
                //Rigidbody sliceRigidbody = slice.AddComponent<Rigidbody>();
                //sliceRigidbody.useGravity = true;

                //slice.AddComponent<BoxCollider>();
            }

            Destroy(objectToSlice);
        }

        private SlicedHull Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
        {
            return objectToSlice.Slice(planeWorldPosition, planeWorldDirection);
        }

        public GameObject[] SliceInstantiate(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
        {
            return objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection);
        }
    }
}
