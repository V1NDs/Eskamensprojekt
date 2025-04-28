using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class killcount : MonoBehaviour
{
    public TMP_Text counterText;
    int kills = 0;

    private void showkills() 
    {
        counterText.text = kills.ToString();
    }

    public void Addkill () 
    {
        kills++;
        showkills();
        
    } 
}
