using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameHUDController : MonoBehaviour {

	[Inject] IInputManager _inputManager;
	[Inject] SignalBus _signalBus;

	public Text scoreCounter;
	private int roundScore;

	public RawImage leftIcon;
	public RawImage rightIcon;
	public float fillPadding = 0.5f;

	private float leftFill = 0f;
	private float rightFill = 0f;

	private const string FILL_VALUE_KEY = "Vector1_CBC31853";

	// Use this for initialization
	void Start () {
		leftIcon.material = Instantiate(leftIcon.material);
		rightIcon.material = Instantiate(rightIcon.material);

		_signalBus.Subscribe<ScoreIncremented>(IncrementCounter);
	}

	void OnDestroy() {
		_signalBus.Unsubscribe<ScoreIncremented>(IncrementCounter);
	}
	
	void Update () {
		leftFill = Mathf.Lerp(leftFill, Mathf.Max(_inputManager.inputRatio.x * -1, 0f), fillPadding);
		rightFill = Mathf.Lerp(rightFill, Mathf.Max(_inputManager.inputRatio.x, 0f), fillPadding);

		leftIcon.material.SetFloat(FILL_VALUE_KEY, leftFill);
		rightIcon.material.SetFloat(FILL_VALUE_KEY, rightFill);
	}

	void IncrementCounter () {
		roundScore++;
		scoreCounter.text = roundScore.ToString();
	}
}
