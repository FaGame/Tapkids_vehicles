﻿using UnityEngine;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using Vectrosity;

public class GestureLineDrawing : GestureDrawing
{
	public float lineWidth = 4f;
	public Material lineMaterial;
	public bool useEndCap = false;
	public Texture2D capTex;
	public Material capMaterial;

	private List<VectorLine> lineList;
	private VectorLine currentLine;
	private Vector2 previousPosition;
	private int sqrMinPixelMove;
	private bool canDraw = false;


	void Awake ()
	{
		if (useEndCap) {
			VectorLine.SetEndCap ("capLine", EndCap.Mirror, capMaterial, capTex);
			lineMaterial = capMaterial;
		}

		lineList = new List<VectorLine> ();

		for (int i = 0; i < recognizer.MaxStrokeNumber; i++) {
			AddStroke (i);
		}

		// Used for .sqrMagnitude, which is faster than .magnitude
		sqrMinPixelMove = minPixelMove * minPixelMove;
	}

	void Start()
	{
		GameObject vestrosityGO = GameObject.Find ("VectorCanvas");
		vestrosityGO.AddComponent<DestroyOnLoad> ();
	}

	private void AddStroke (int index)
	{
		VectorLine line = new VectorLine ("GestureStroke" + index, new List<Vector2> (), lineMaterial, lineWidth, LineType.Continuous, Joins.Weld);

		// Optimization for updating only the last point of the currentLine, and the rest is not re-computed
		line.endPointsUpdate = 1;

		if (useEndCap) {
			line.endCap = "capLine";
		}

		lineList.Add (line);
	}

	protected override void StrokeStart (Lean.LeanFinger finger)
	{
		if (!canDraw)
			return;
		
		ChangeToNextStroke ();
	}

	protected override void StrokeDrag (Lean.LeanFinger finger)
	{
		if (!canDraw)
			return;
		
		if ((finger.ScreenPosition - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw) {	

			previousPosition = finger.ScreenPosition;
			currentLine.points2.Add (finger.ScreenPosition);

			if (currentLine.points2.Count >= maxPoints)
				canDraw = false;

			currentLine.Draw ();
		}
	}	

	protected override void StrokeEnd (Lean.LeanFinger finger)
	{
		
	}

	public void ResetStroke ()
	{
		for (int i = 0; i < lineList.Count; i++) {
			lineList [i].points2.Clear ();
			lineList [i].Draw ();
		}

		canDraw = false;
	}

	/// <summary>
	/// Changes to next stroke. use to split losing focus stroke
	/// </summary>
	public void ChangeToNextStroke ()
	{
		if (recognizer.IsReachMaxStroke)
			return;
		
		recognizer.ChangeToNextStroke ();

		currentLine = lineList [recognizer.CurrentStrokeID + 1];

		previousPosition = Input.mousePosition;
		currentLine.points2.Add (Input.mousePosition);
		canDraw = true;
	}

	public void SetDrawing(bool isActive)
	{
		canDraw = isActive;
	}
}
