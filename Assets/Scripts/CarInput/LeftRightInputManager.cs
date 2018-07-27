using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LeftRightInputManager : IInputManager {

    public bool Enabled { get; set; }
	private ISteering _steering;

	public float rotPadding = 0.5f;
	public float yAcc = 0.002f;

	private Vector2 screenSize;
	private Vector2 _prevRatio = Vector2.zero;

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
	
    public void Reset () {
        _prevRatio = Vector2.zero;
        Enabled = false;
    }
	
	public void Tick ()
	{
		if (!Enabled) {
			return;
		}

		Vector2 touchPos, ratio = touchPos = Vector2.zero;

		if (Input.touchCount > 0)
		{
			touchPos = Input.GetTouch(0).position;
			ratio.x = -1 + ((touchPos.x / screenSize.x) * 2);
		}
		ratio.y = Mathf.Min(1, _prevRatio.y + yAcc);

		ratio = Vector2.Lerp(_prevRatio, ratio, rotPadding);
		_steering.Move(ratio.x, ratio.y);

		_prevRatio = ratio;
	}
}
