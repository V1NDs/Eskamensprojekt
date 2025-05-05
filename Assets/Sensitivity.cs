using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    public Slider sensSlider;
    private float sensitivity;
    public bool inGame = false;
    public GameObject player;

    private void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("sens");
        sensSlider.value = sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("sens", sensitivity);

        if (inGame)
        {
            player.GetComponent<FPSController>().lookSpeed = sensitivity;
        }
    }

    public void UpdateSens(float sens)
    {
        sensitivity = sens;
    }
}
