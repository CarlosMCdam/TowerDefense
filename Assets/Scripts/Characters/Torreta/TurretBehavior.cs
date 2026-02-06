using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public float rango = 10f;
    public float fireRate = 1f;
    public GameObject balaPrefab;
    public Transform firePoint;

    private float fireCountdown = 0f;
    private Transform objetivo;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo == null)
            return;

        transform.LookAt(objetivo);

        if (fireCountdown <= 0f)
        {
            Disparar();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void BuscarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        float distanciaMin = Mathf.Infinity;
        GameObject enemigoCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float dist = Vector3.Distance(transform.position, enemigo.transform.position);
            if (dist < distanciaMin)
            {
                distanciaMin = dist;
                enemigoCercano = enemigo;
            }
        }

        if (enemigoCercano != null && distanciaMin <= rango)
            objetivo = enemigoCercano.transform;
        else
            objetivo = null;
    }

    void Disparar()
    {
        Instantiate(balaPrefab, firePoint.position, firePoint.rotation);
    }
}
