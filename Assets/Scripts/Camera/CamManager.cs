using System;
using UnityEngine;
using Zenject;

public class CamManager {

    [Serializable]
    public struct CamSettings {
        public Camera gameplayCam;
        public Camera uiCam;
    }

    CamSettings _settings;

    public CamManager(CamSettings settings)
    {
        _settings = settings;
    }

    public void onStartButton() {
        Debug.Log("IN CAM MAN");
        _settings.uiCam.enabled = false;
        _settings.gameplayCam.enabled = true;
    }
}