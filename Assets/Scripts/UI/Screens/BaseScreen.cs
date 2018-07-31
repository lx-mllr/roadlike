using System;
using UnityEngine;
using Zenject;

public class BaseScreen : MonoBehaviour {
    
    public void DestroyScreen () {
        Destroy(this.gameObject);
    }
}