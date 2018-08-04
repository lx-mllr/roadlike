﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LeftRightInputManager : IInputManager {

	private ISteering _steering;

	public float padding = 0.5f;
	public float yAcc = 0.015f;
	public float dragSensitivity = 25;

	private Vector2 screenSize;
	private Vector2 _prevTouch;
	private Vector2 _prevRatio = Vector2.zero;
    private bool Enabled { get; set; }

	public LeftRightInputManager (ISteering steering)
	{
		_steering = steering;
	}

	// Use this for initialization
	public void Initialize () 
	{
		screenSize = new Vector2(Screen.width, Screen.height);
        Enabled = false;
	}
	
    public void Enable () {
        Enabled = true;
    }
	
    public void Reset () {
        _prevRatio = Vector2.zero;
        Enabled = false;
    }
	
	public void Tick ()
	{
		if (!Enabled) {
			return;
		}

		Vector2 touchPos = Vector2.zero;
		Vector2 ratio = Vector2.zero;
		Vector2 drag = Vector2.zero;

		if (Input.touchCount > 0)
		{
			touchPos = Input.GetTouch(0).position;
			// [-1, 1]
			float scaledX = -1 + ((touchPos.x / screenSize.x) * 2);
			drag = touchPos - _prevTouch;

			if (drag.sqrMagnitude > dragSensitivity)
			{
				if (Mathf.Sign(drag.x) != Mathf.Sign(scaledX))
				{
					scaledX *= -1;
				}
			}
			ratio.x = scaledX;
		}
		ratio.y = Mathf.Min(1, _prevRatio.y + yAcc);

		ratio = Vector2.Lerp(_prevRatio, ratio, padding);
		_steering.Move(ratio.x, ratio.y);

		_prevRatio = ratio;
		_prevTouch = touchPos;
	}
}
