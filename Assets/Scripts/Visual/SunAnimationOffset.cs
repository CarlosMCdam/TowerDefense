using UnityEngine;

public class SunAnimationOffset : MonoBehaviour
{
    public Animator sunAnimator;
    [Range(0f, 1f)] public float dayOffset = 0.25f; // 0 = amanecer, 0.5 = mediodía, 1 = medianoche

    void Start()
    {
        // Reproduce la animación desde el punto indicado del ciclo
        sunAnimator.Play("CicloDiaNoche", 0, dayOffset);
    }
}

