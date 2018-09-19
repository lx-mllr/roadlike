using UnityEngine;

public class SwipeInputManager : IInputManager {
    
	public float yAcc = 0.002f;
    public float airSlowDuration = 5.0f;

    private ISteering _steering;
    private Vector2 _screenSize;

    private Vector2 _touchStart;
    private Vector2 _prevRatio;
    private float airTime;
    private float speedTarget;
    private bool _enabled;
    public bool Enabled { get { return _enabled; } }
    
	private Vector2 _inputRatio;
	public Vector2 inputRatio { get { return _inputRatio; } }

    public SwipeInputManager(ISteering steering)
    {
        _steering = steering;
    }

    public void Initialize ()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
        _prevRatio = _touchStart = Vector2.zero;
        _enabled = false;
    }
	
    public void Enable () {
        _enabled = true;
    }
    
    public void Reset () {
        _touchStart = _prevRatio = Vector2.zero;
        _enabled = false;
    }

    public void Tick ()
    {
        if (!_enabled) {
            return;
        }

        Vector2 touchCurrent = Vector2.zero;
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    _touchStart = Input.GetTouch(0).position;
                    break;
                case TouchPhase.Moved:
                    touchCurrent = Input.GetTouch(0).position;
                    break;
                case TouchPhase.Ended:
                    _touchStart = Vector2.zero;
                    break;
            }
        }

        _inputRatio = Vector2.zero;
        
        if (_touchStart.sqrMagnitude > 0 && touchCurrent.sqrMagnitude > 0)
        {
            Vector2 drag = touchCurrent - _touchStart;
            _inputRatio.x = ((drag.x / _screenSize.x) * 4);
        }
        

            _inputRatio.y = Mathf.Min(1, _prevRatio.y + yAcc);
        
        _steering.Move(_inputRatio.x, _inputRatio.y, 0.0f, 0.0f);
        _prevRatio = _inputRatio;
    }   
}