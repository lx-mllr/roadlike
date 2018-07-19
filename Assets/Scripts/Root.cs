using System;
using UnityEngine;
using strange.extensions.context.impl;

public class Root : ContextView
{

	void Awake()
	{
		context = new RootContext(this);
	}
}
