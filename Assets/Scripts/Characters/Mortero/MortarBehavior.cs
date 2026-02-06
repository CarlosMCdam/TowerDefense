using UnityEngine;

public class MortarBehavior : MonoBehaviour
{
    public Transform firePoint;
    public GameObject balaPrefab;
    public float rango = 20f;
    public float fireRate = 1f;
    public float anguloDisparo = 45f; // ángulo de la parábola

    private float fireCountdown = 0f;
    private Transform objetivo;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo == null)
            return;

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
        GameObject bala = Instantiate(balaPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bala.GetComponent<Rigidbody>();

        // ESTA es la línea importante:
        Vector3 velocidad = CalcularVelocidadParabolica(objetivo.position, firePoint.position, anguloDisparo);

        rb.linearVelocity = velocidad;
    }

    Vector3 CalcularVelocidadParabolica(Vector3 destino, Vector3 origen, float anguloGrados)
    {
        float gravedad = Mathf.Abs(Physics.gravity.y);

        Vector3 planoXZ = new Vector3(destino.x - origen.x, 0, destino.z - origen.z);
        float distanciaXZ = planoXZ.magnitude;

        float altura = destino.y - origen.y;
        float anguloRad = anguloGrados * Mathf.Deg2Rad;

        float velocidad = Mathf.Sqrt(
            gravedad * distanciaXZ * distanciaXZ /
            (2 * (distanciaXZ * Mathf.Tan(anguloRad) - altura))
        );

        Vector3 dirXZ = planoXZ.normalized;

        Vector3 velocidadFinal =
            dirXZ * velocidad * Mathf.Cos(anguloRad) +
            Vector3.up * velocidad * Mathf.Sin(anguloRad);

        return velocidadFinal;
    }
}
