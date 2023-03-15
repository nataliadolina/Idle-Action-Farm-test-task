using UnityEngine;
using EzySlice;
using Environment;

namespace Systems
{
    internal sealed class CutSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToCut;
        [SerializeField]
        private Material referenceMaterial;
        [SerializeField]
        private Transform cutLine;
        [SerializeField]
        private Vector3 sliceNormal;

        private Vector3 _objectToSliceStartPosition;
        private Transform _thisTransform;

        private GameObject _sliceGameObject;

        private WheatColliderCutReceiver _wheatColliderCutReceiver;
        private GrowingWheat _growingWheat;

        internal GameObject SliceGameObject { get => _sliceGameObject; }

#region MonoBehaviour

        private void Awake()
        {
            Wheat parentContainer = GetComponentInParent<Wheat>();

            _wheatColliderCutReceiver = parentContainer.WheatColliderCutReveiver;
            _growingWheat = parentContainer.GrowingWheat;

            _objectToSliceStartPosition = objectToCut.transform.position;
            _thisTransform = transform;
            
            Cut();
            _sliceGameObject.SetActive(false);

            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void ShowSlice()
        {
            _sliceGameObject.SetActive(true);
        }

        private void HideSlice(bool isWheatGrowing)
        {
            if (!isWheatGrowing)
            {
                return;
            }

            _sliceGameObject.SetActive(false);
        }

        private void Cut()
        {
            GameObject[] slices = SliceInstantiate(cutLine.position, sliceNormal);

            foreach(GameObject slice in slices)
            {
                if (slice.name == "Upper_Hull")
                {
                    Destroy(slice);
                }
                else
                {
                    Transform sliceTransform = slice.transform;
                    sliceTransform.parent = _thisTransform;
                    sliceTransform.position = _objectToSliceStartPosition;
                    MeshRenderer meshRenderer = slice.GetComponent<MeshRenderer>();
                    meshRenderer.materials[1] = referenceMaterial;
                    meshRenderer.materials[1].shader = referenceMaterial.shader;
                    meshRenderer.materials[1].color = referenceMaterial.color;

                    _sliceGameObject = slice.gameObject;
                    _sliceGameObject.SetActive(false);
                }
            }

            Destroy(objectToCut);
        }

        private GameObject[] SliceInstantiate(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
        {
            return objectToCut.SliceInstantiate(planeWorldPosition, planeWorldDirection);
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            _wheatColliderCutReceiver.onWheatCut += ShowSlice;
            _growingWheat.onWheatIsGrowing += HideSlice;
        }

        private void ClearSubscriptions()
        {
            _wheatColliderCutReceiver.onWheatCut -= ShowSlice;
            _growingWheat.onWheatIsGrowing -= HideSlice;
        }

#endregion

    }
}
