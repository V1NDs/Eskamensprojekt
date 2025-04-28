using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider slider;
    public bool reverted = false;
    public bool canLavaDamage = true;
    public AudioSource lavaAudio;

    // Start is called once before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 40)
        {
            GameObject.Find("pistol_1").GetComponent<Gun>().extraDamage = 1.5f;
        } else
        {
            GameObject.Find("pistol_1").GetComponent<Gun>().extraDamage = 1.0f;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Death Screen");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == 4 && canLavaDamage)
        {
            reverted = false;
            GameObject.Find("Player").GetComponent<FPSController>().walkSpeed = 2f;
            GameObject.Find("Player").GetComponent<FPSController>().runSpeed = 3f;
            TakeDamage(10);
            canLavaDamage = false;

            Invoke("RestartLavaDamage", 1);
            lavaAudio.Play();
        } else
        {
            GameObject.Find("Player").GetComponent<FPSController>().walkSpeed = 4f;
            GameObject.Find("Player").GetComponent<FPSController>().runSpeed = 6f;
        }
    }

    void RestartLavaDamage()
    {
        canLavaDamage = true;
    }
}