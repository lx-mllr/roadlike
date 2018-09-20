using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameHUDController : MonoBehaviour {

	[Inject] IInputManager _inputManager;
	[Inject] SignalBus _signalBus;

	public RawImage leftIcon;
	public RawImage rightIcon;

	// Use this for initialization
	void Start () {
		_signalBus.Subscribe<GameEndSignal>(DestroyScreen);

		leftIcon.material = Instantiate(leftIcon.material);
		rightIcon.material = Instantiate(rightIcon.material);
	}
	
	// Update is called once per frame
	void Update () {
		RawImage turnDirection = _inputManager.inputRatio.x > 0 ? rightIcon : leftIcon;
		turnDirection.material.SetFloat("Vector1_CBC31853", Mathf.Abs(_inputManager.inputRatio.x));		
	}

	
    public void DestroyScreen () {
		_signalBus.Unsubscribe<GameEndSignal>(DestroyScreen);

        Destroy(this.gameObject);
    }
}
