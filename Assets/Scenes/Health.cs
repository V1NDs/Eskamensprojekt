using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called once before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //We're Dead
            //Play Death Anymation 
            //Show GameOver screen
        }
    }
}