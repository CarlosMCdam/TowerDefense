using UnityEngine;
using UnityEngine.AI;

public class EnemigoBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
