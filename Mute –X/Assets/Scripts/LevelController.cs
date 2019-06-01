using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public Objective[] Objectives;
    public string currentObjective { get; set; }
    public bool SomeoneDied { get; set; }
    public int EnemeiesAlive { get; set; }
    public int EnemeiesKilled { get; set; }
    public int AmmoGatherd { get; set; }
    public int zombiesKilled { get; set; }
    public int guardsKilled { get; set; }
   
}

public class Objective
{
    public string Text { get; set; }
    public string SubText { get; set; }
    public bool Done { get; set; }

    public Objective(string text)
    {
        Text = text;
    }

}
