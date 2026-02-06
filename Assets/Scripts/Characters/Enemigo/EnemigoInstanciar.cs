using UnityEngine;

public class EnemigoInstanciar : MonoBehaviour
{
    public GameObject enemigoPrefab;

    public float spawnInterval = 0;
    public int xPos;
    public int yPos;
    public int zPos;
    public int xRotation;
    public int yRotation;
    public int zRotation;


    void Start()
    {
        if (spawnInterval == 0) SpawnEnemigo();
        else InvokeRepeating(nameof(SpawnEnemigo), 0f, spawnInterval);
    }

    void SpawnEnemigo()
    {
        Instantiate(enemigoPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRotation, yRotation, zRotation));
    }
}
