﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIVehicleNameText : MonoBehaviour {
	public Text vehicleNameText;

	void OnEnable () {
		Messenger.AddListener <Vehicle> (EventManager.GUI.UPDATEVEHICLE.ToString (), HandleUpdateVehicle);
	}

	void Start () {
		vehicleNameText = GetComponent <Text> ();
	}
	void OnDisable () {
		Messenger.RemoveListener <Vehicle> (EventManager.GUI.UPDATEVEHICLE.ToString (), HandleUpdateVehicle);
	}

	void HandleUpdateVehicle (Vehicle _vehicle) {
		vehicleNameText.text = _vehicle.name;
	}

}
