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
        _settings.uiCam.enabled = false;
        _settings.gameplayCam.enabled = true;
    }

    public void onShowMainScreen() {
        _settings.gameplayCam.transform.position = _settings.uiCam.transform.position;
        _settings.gameplayCam.transform.rotation = _settings.uiCam.transform.rotation;
        
        _settings.gameplayCam.enabled = false;
        _settings.uiCam.enabled = true;
    }
}