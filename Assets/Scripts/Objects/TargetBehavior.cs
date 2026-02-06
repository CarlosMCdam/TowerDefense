using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            other.GetComponent<EnemigoBehavior>().Morir();
            Destroy(other.gameObject); // destruye SOLO al enemigo
        }
    }

}
