using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SteeringInputManager : MonoBehaviour {

	public CarSteering steering;
	public Vector3 boxForward = Vector3.one;

	private Vector2 screenSize;

	// Use this for initialization
	void Start () 
	{
		screenSize = new Vector2(Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 touchPos, ratio = touchPos = Vector2.zero;
		if (Input.touchCount > 0)
		{
			touchPos = Input.GetTouch(0).position;
		}
		else if (Input.GetMouseButtonDown(0))
		{
			touchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.z);
		}
		ratio = new Vector2(touchPos.x / screenSize.x, touchPos.y / screenSize.y);

		ratio = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (ratio.sqrMagnitude > 0)
		{
			steering.move(ratio.x, ratio.y);
		}

		boxForward = transform.forward;
	}
}
