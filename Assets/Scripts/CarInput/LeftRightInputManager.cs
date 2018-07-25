using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LeftRightInputManager : IInputManager {

	private ISteering _steering;

	public float rotPadding = 0.5f;
	public float yAcc = 0.002f;

	private Vector2 screenSize;
	private Vector2 prevRatio = Vector2.zero;

	public LeftRightInputManager (ISteering steering)
	{
		_steering = steering;
	}

	// Use this for initialization
	public void Initialize () 
	{
		screenSize = new Vector2(Screen.width, Screen.height);
	}
	
	public void Tick ()
	{
		Vector2 touchPos, ratio = touchPos = Vector2.zero;

		if (Input.touchCount > 0)
		{
			touchPos = Input.GetTouch(0).position;
			ratio.x = -1 + ((touchPos.x / screenSize.x) * 2);
		}
		ratio.y = Mathf.Min(1, prevRatio.y + yAcc);

		ratio = Vector2.Lerp(prevRatio, ratio, rotPadding);
		_steering.move(ratio.x, ratio.y);

		prevRatio = ratio;
	}
}
