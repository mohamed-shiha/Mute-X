using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public Image fillImage;
    Slider healthBar;
    PlayerData data;

    
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        data = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    private void Update()
    {
        healthBar.value = (float)data.Health/100 ;
    }

    public void ChangeColour()
    {
        int value = data.Health;
        Color newColor;
        if (value <= 25)
        {
            newColor = Color.red;
        }
        else if (value <= 75)
        {
            newColor = new Color(236, 118, 0);
        }
        else newColor = new Color(0.09211465f, 0.8490566f, 0.2100936f, 0.3215686f);
        fillImage.color = newColor;
    }
}
