using UnityEngine;

public class GridCreate : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    void Start()
    {
        for(int i = 0; i < 81; i++)
        {
            Instantiate(cellPrefab, gridParent);
        }
    }
}
