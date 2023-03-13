using UnityEngine;

public class HarvestBagTest : MonoBehaviour
{
    [SerializeField]
    private int testBlocksAmount;
    [SerializeField]
    private GameObject testPrefab;

    [SerializeField]
    private int blocksPerX;
    [SerializeField]
    private int blocksPerY;

    [SerializeField]
    private Vector3 blockInStackSize;
    [SerializeField]
    private Vector3 blockInStackSize1;

    private Vector3 _blockInStackStartPosition;
    private int _blocksCount = 0;

    private void Start()
    {
        _blockInStackStartPosition = new Vector3(blockInStackSize.x / 2, blockInStackSize.y / 2, -blockInStackSize.z / 2);
        for (int i = 0; i < testBlocksAmount; i++)
        {
            GameObject instance = Instantiate(testPrefab);
            AddBlockToStack(instance.GetComponent<Transform>(), instance.GetComponent<Rigidbody>());
        }
    }

    private void AddBlockToStack(Transform blockTransform, Rigidbody rigidbody)
    {
        rigidbody.isKinematic = true;

        blockTransform.parent = transform;
        blockTransform.localScale = blockInStackSize1;
        blockTransform.localRotation = Quaternion.identity;
        blockTransform.localPosition = GetPositionInStack();

        _blocksCount++;
    }

    private Vector3 GetPositionInStack()
    {
        float x = _blockInStackStartPosition.x + _blocksCount % blocksPerX * blockInStackSize.x;
        float y = _blockInStackStartPosition.y + _blocksCount % (blocksPerX * blocksPerY) / blocksPerX * blockInStackSize.y;
        float z = _blockInStackStartPosition.z - _blocksCount / (blocksPerX * blocksPerY) * blockInStackSize.z;
        return new Vector3(x, y, z);
    }
}
