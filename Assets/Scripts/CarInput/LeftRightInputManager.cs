using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LeftRightInputManager : IInputManager {

	private ISteering _steering;

	public float yAcc = 0.05f;
	public float dragSensitivity = 5;

	private Vector2 screenSize;
	private Vector2 _prevTouch;
	private Vector2 _prevRatio = Vector2.zero;
    private bool _enabled;

	public LeftRightInputManager (ISteering steering)
	{
		_steering = steering;
	}

	// Use this for initialization
	public void Initialize () 
	{
		screenSize = new Vector2(Screen.width, Screen.height);
        _enabled = false;
	}
	
    public void Enable () {
        _enabled = true;
    }
	
    public void Reset () {
        _prevTouch = _prevRatio = Vector2.zero;
        _enabled = false;
    }
	
	public void Tick ()
	{
		if (!_enabled) {
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

		_steering.Move(ratio.x, ratio.y, 0.0f, 0.0f);

		_prevRatio = ratio;
		_prevTouch = touchPos;
	}
}
