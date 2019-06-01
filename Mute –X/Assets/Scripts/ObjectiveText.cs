using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveText : MonoBehaviour {

	public LevelController level;
    public TextMeshProUGUI textMesh;


	void Update () {
        textMesh.text = level.currentObjective;
        if (level.currentObjective.Contains("All"))
        {
            textMesh.color = new Color(0.7f,0.67f,0.16f);
        }
    }
}
