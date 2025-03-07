using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this for UI manipulation
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public int health = 100;
    public Text healthText; // Assign this in the Inspector

    void Update()
    {
        if (health < 100)
        {
            health += 1; // Regenerate health
            UpdateHealthUI();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            health -= 5;
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
