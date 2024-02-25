using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 initialScale;

    private void Start()
    {
        // Store the initial scale of the health bar
        initialScale = transform.localScale;
    }

    public void ChangeHealth(int health)
    {
        // Calculate the new X scale based on the player's health
        float newScaleX = Mathf.Clamp01((float)health / 3.0f) * initialScale.x;

        // Apply the new scale to the health bar
        transform.localScale = new Vector3(newScaleX, initialScale.y, initialScale.z);
    }

    public void resetScale()
    {
        transform.localScale = initialScale;
    }
}
