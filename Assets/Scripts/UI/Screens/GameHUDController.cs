﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameHUDController : MonoBehaviour {

	[Inject] IInputManager _inputManager;
	[Inject] SignalBus _signalBus;

	public RawImage leftIcon;
	public RawImage rightIcon;
	public float fillPadding = 0.5f;

	private float leftFill = 0f;
	private float rightFill = 0f;

	private const string FILL_VALUE_KEY = "Vector1_CBC31853";

	// Use this for initialization
	void Start () {
		_signalBus.Subscribe<GameEndSignal>(DestroyScreen);

		leftIcon.material = Instantiate(leftIcon.material);
		rightIcon.material = Instantiate(rightIcon.material);
	}
	
	// Update is called once per frame
	void Update () {
		leftFill = Mathf.Lerp(leftFill, Mathf.Max(_inputManager.inputRatio.x * -1, 0f), fillPadding);
		rightFill = Mathf.Lerp(rightFill, Mathf.Max(_inputManager.inputRatio.x, 0f), fillPadding);

		leftIcon.material.SetFloat(FILL_VALUE_KEY, leftFill);
		rightIcon.material.SetFloat(FILL_VALUE_KEY, rightFill);		
	}

	
    public void DestroyScreen () {
		_signalBus.Unsubscribe<GameEndSignal>(DestroyScreen);

        Destroy(this.gameObject);
    }
}
