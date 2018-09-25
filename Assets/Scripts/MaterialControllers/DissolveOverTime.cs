using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOverTime : MonoBehaviour {

	public string PropertyKey = "";
	public float duration = 1f;
	public bool invert = false;

	private Material _material;
	private IEnumerator _coroutine;

	// Use this for initialization
	void Start () { 
		_material = GetComponent<Renderer>().material;
		_coroutine = Dissolve();
		StartCoroutine(_coroutine);
	}

	void OnDestroy () {
		if (_coroutine != null) {
			StopCoroutine(_coroutine);
		}
	}
	
	IEnumerator Dissolve () {
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			_material.SetFloat(PropertyKey, (invert) ? 1 - elapsed : elapsed);
			yield return null;
		}
	}
}
