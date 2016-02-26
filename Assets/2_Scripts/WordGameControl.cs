﻿using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using System.Linq;

public class WordGameControl : MonoBehaviour {

	private Dictionary<string, DefaultTrackableEventHandlerFSM> dic_targetImages = new Dictionary<string, DefaultTrackableEventHandlerFSM>();
	private Dictionary<string, Transform> dic_playableTargets = new Dictionary<string, Transform> ();
    private PlayMakerFSM fsm;

	void Awake(){
        fsm = gameObject.GetComponent<PlayMakerFSM>();

        DefaultTrackableEventHandlerFSM[] imgTargs = FindObjectsOfType<DefaultTrackableEventHandlerFSM> ();
		for(int i = 0; i < imgTargs.Length; i++){
			dic_targetImages.Add (imgTargs[i].targetName, imgTargs[i]);
		}
	}

    void _GameOver() {
        dic_playableTargets.Clear();
    }

    void _InitGame(){
		List<string> playableLetters = GetPlayableLetters ();

		foreach(string letter in playableLetters){
			if(dic_targetImages.ContainsKey(letter)){
				DefaultTrackableEventHandlerFSM imageTarget;
				if ( dic_targetImages.TryGetValue (letter, out imageTarget) ) {
					//Set ready state for playable image target
					imageTarget.Ready ();
				}
			}
		}
	}

    public void _ResetGame() {
        fsm.Fsm.BroadcastEvent("ge_reset");
    }

    public void _ReadyGame() {
        fsm.Fsm.Event("ready");
    }

    void _AddPlayableTarget(string letter) {
        DefaultTrackableEventHandlerFSM imageTarget;
        if (dic_targetImages.TryGetValue(letter, out imageTarget))
        {
            dic_playableTargets.Add(letter, imageTarget.gameObject.transform);
        }
    }

    void _RemovePlayableTarget(string letter) {
        if (dic_targetImages.ContainsKey(letter))
        {
            dic_playableTargets.Remove(letter);
        }
    }

    List<string> GetPlayableLetters(){
		List<string> letters = new List<string> ();
		letters.Add ("M");
		letters.Add ("D");
		letters.Add ("N");
		return letters;
	}

    List<string> GetAnswersList() {
        List<string> answers = new List<string>();
        answers.Add("MD");
        answers.Add("MDN");
        return answers;
    }

	string _CheckingWordsOrder(){
		dic_playableTargets = dic_playableTargets.OrderBy(x=>x.Value.position.x).ToDictionary(x => x.Key, x => x.Value);
		string word = "";
		for(int i = 0; i < dic_playableTargets.Count; i++){
			word = word + dic_playableTargets.Keys.ElementAt (i);
		}
		Debug.Log (word);
		return word;
	}
}
