﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialButton : MonoBehaviour {

	#region public members
	#endregion public members

	#region private members
	private Button pauseButton;
	#endregion private members

	#region Mono
	void Start () {
		pauseButton = GetComponent <Button> ();
		if(pauseButton != null) {
			pauseButton.onClick.AddListener (delegate {
				CarGameEventController.OnToggleTutorialPanel (true);
			});
		}
	}
	#endregion Mono
	#region public functions
	#endregion public functions

	#region private functions
	#endregion private functions

}