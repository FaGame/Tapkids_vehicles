﻿using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

	void OnTriggerEnter () {
		CarGameEventController.OnGatherLetter();
	}

	// check valid word here?
}
