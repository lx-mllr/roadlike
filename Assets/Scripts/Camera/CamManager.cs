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
    FollowCam _followMode;
    TrackingCam _trackingMode;

    public static Camera activeCamera;

    public CamManager(CamSettings settings, FollowCam fCam, TrackingCam tCam)
    {
        _settings = settings;
        _followMode = fCam;
        _trackingMode = tCam;

        activeCamera = _settings.uiCam;
    }

    public void onStartButton() {
        _settings.uiCam.enabled = false;
        _settings.gameplayCam.enabled = true;           
        _settings.gameplayCam.transform.position = _settings.uiCam.transform.position;
        _settings.gameplayCam.transform.rotation = _settings.uiCam.transform.rotation;

        _trackingMode.enabled = false;
        _followMode.enabled = true; 
        activeCamera = _settings.gameplayCam;
    }

    public void onShowMainScreen() {
        
        _settings.gameplayCam.enabled = false;
        _settings.uiCam.enabled = true;
        activeCamera = _settings.uiCam;
    }

    public void onEnableTracking(EnableTrackingCamSignal signal) {
        _followMode.enabled = false;
        _trackingMode.enabled = true;
        _trackingMode.targetPosition = signal.trackingPosition;
    }

    public void onDisableTracking(DisableTrackingCamSignal signal) {
        _followMode.enabled = true;
        _trackingMode.enabled = false;
    }
}