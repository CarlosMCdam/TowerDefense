using UnityEngine;

public class Tilemap3D : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10;
    public int height = 10;
    public int depth = 1;
    public float cellSize = 1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Vector3 pos = new Vector3(x * cellSize, y * cellSize, z * cellSize);
                    Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}

