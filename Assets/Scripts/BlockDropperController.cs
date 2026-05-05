using UnityEngine;

public class BlockDropperController : MonoBehaviour
{
    [Header("Block Setting")]
    [SerializeField] private GameObject blockPrefab;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(blockPrefab, transform.position, Quaternion.identity);
        }
    }
}