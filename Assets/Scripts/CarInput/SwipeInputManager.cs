using UnityEngine;

public class SwipeInputManager : IInputManager {
    
    public bool Enabled { get; set; }
	public float yAcc = 0.002f;

    private ISteering _steering;
    private Vector2 _screenSize;

    private Vector2 _touchStart;
    private Vector2 _prevRatio;

    public SwipeInputManager(ISteering steering)
    {
        _steering = steering;
    }

    public void Initialize ()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
        _prevRatio = _touchStart = Vector2.zero;
    }

    public void Tick ()
    {
        if (!Enabled) {
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

        Vector2 ratio = Vector2.zero;
        
        if (_touchStart.sqrMagnitude > 0 && touchCurrent.sqrMagnitude > 0)
        {
            Vector2 drag = touchCurrent - _touchStart;
            ratio.x = ((drag.x / _screenSize.x) * 2);
        }
        ratio.y = Mathf.Min(1.0f, _prevRatio.y + yAcc);
        _steering.move(ratio.x, ratio.y);
        _prevRatio = ratio;
    }
}