using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AmmoUI : MonoBehaviour {

	public TextMeshProUGUI Ammo;
	public TextMeshProUGUI Magazine;
	public PlayerData PlayerData;
	public Weapons Weapon;

	void Update () {
		Ammo.text = "/"+PlayerData.Ammo;
		Magazine.text = ""+Weapon.Magazine;
	}
}
