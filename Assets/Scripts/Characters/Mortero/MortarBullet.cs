using UnityEngine;

public class MortarBullet : MonoBehaviour
{
    public float vida = 5f;

    void Start()
    {
        Destroy(gameObject, vida);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemigo"))
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }
}
