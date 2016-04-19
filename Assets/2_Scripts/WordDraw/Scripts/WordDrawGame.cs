﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PDollarGestureRecognizer;

namespace WordDraw
{
	public class WordDrawGame : MonoBehaviour
	{

		public GestureLineDrawing _drawer;
		public Text _statusText;
		public Text _resultText;
		public LetterSpawner _spawner;
		public WordDrawScore _wordDrawScore;

		void OnEnable ()
		{
			LeanGestureRecognizer.OnGestureReset += OnGestureReset;
			LetterSpawner.OnGameOver += OnGameOver;
			LetterSpawner.OnNoMatchResult += OnNoMatch;
			LeanGestureRecognizer.OnGestureDetected += OnResult;
			Messenger.AddListener<bool> (EventManager.GameState.PAUSEGAME.ToString(), Pause);
		}

		void OnDisable ()
		{
			LeanGestureRecognizer.OnGestureReset -= OnGestureReset;
			LetterSpawner.OnGameOver -= OnGameOver;
			LetterSpawner.OnNoMatchResult -= OnNoMatch;
			LeanGestureRecognizer.OnGestureDetected -= OnResult;
		}

		void Start()
		{
			StartCoroutine (GameCoroutine());
		}

		private void Pause(bool isPaused)
		{
			if (isPaused)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}

		public void Exit()
		{
			SceneController.Instance.LoadingSceneAsync (SceneController.SceneID.MENU);
		}

		public void Restart ()
		{
			_drawer.ResetStroke ();
			SceneController.Instance.ReloadCurrentScene ();
		}

		private void OnNoMatch()
		{
			_statusText.text = "More Carefully";
			StartCoroutine (ResetStatusTextCor());
		}

		private IEnumerator ResetStatusTextCor()
		{
			yield return new WaitForSeconds (2f);
			_statusText.text = "";
		}

		private IEnumerator GameCoroutine()
		{
			while(true)
			{
				if (_wordDrawScore.CurrentScore > _spawner.CurrentDifficulty.RequiredScore)
					_spawner.ChangeToMextDifficulty ();
				
				yield return null;
			}
		}

		private void OnGestureReset ()
		{
			_drawer.ResetStroke ();
		}

		private void OnGameOver()
		{
			_statusText.text = "GAMEOVER";
			_drawer.enabled = false;
			_wordDrawScore.enabled = false;
		}

		private void OnResult(Result result)
		{
			_resultText.text = result.GestureClass;
		}
	}
}
