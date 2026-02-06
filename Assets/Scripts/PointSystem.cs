using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static PointSystem instancia;

    public int puntos = 0;

    void Awake()
    {
        instancia = this;
    }

    public void AñadirPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntos: " + puntos);
    }

    public bool GastarPuntos(int cantidad)
    {
        if (puntos >= cantidad)
        {
            puntos -= cantidad;
            return true;
        }
        return false;
    }
}
