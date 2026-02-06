using UnityEngine;
using UnityEngine.AI;

public class EnemigoBehavior : MonoBehaviour
{
    public Transform target;

    public int puntosPorMuerte;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Morir()
    { 
        PointSystem.instancia.AñadirPuntos(puntosPorMuerte); Destroy(gameObject); 
    }

}
