using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTimer : MonoBehaviour {

	public float duration = .75f;
	public float maxFade = 5f;

	private const string DISSOLVE_FADE_KEY = "Vector1_72893C76";

	private Material material;
	private float startTime;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material;
		//this.material = Instantiate(material);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float ratio = (Time.time - startTime) / (startTime + duration);
		ratio = Mathf.Min(ratio * maxFade, maxFade);
		material.SetFloat(DISSOLVE_FADE_KEY, ratio); 
	}
}
