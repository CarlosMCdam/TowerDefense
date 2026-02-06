using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float velocidad = 20f;
    public float vida = 3f;

    void Start()
    {
        Destroy(gameObject, vida);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            other.GetComponent<EnemigoBehavior>().Morir();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
