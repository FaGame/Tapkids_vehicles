﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverPanel : MonoBehaviour {

	#region public members
	#endregion public members

	#region private members
	private CanvasGroup mCanvasGroup;
	#endregion private members

	#region Mono
	void OnEnable () {

	}

	void OnDisable () {

	}

	void Start () {
		mCanvasGroup = GetComponent <CanvasGroup> ();
	}
	#endregion Mono

	#region public functions
	#endregion public functions

	#region private functions
	private void OnToggleGameOverPanel (bool _isToggled) {
		mCanvasGroup.alpha = _isToggled ? 1f : 0f;
		mCanvasGroup.interactable = _isToggled ? true : false;
		mCanvasGroup.blocksRaycasts = _isToggled ? true : false;
	}
	#endregion private functions

}