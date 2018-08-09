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
    private bool Enabled { get; set; }

    public SwipeInputManager(ISteering steering)
    {
        _steering = steering;
    }

    public void Initialize ()
    {
        _screenSize = new Vector2(Screen.width, Screen.height);
        _prevRatio = _touchStart = Vector2.zero;
        Enabled = false;
    }
	
    public void Enable () {
        Enabled = true;
    }
    
    public void Reset () {
        _touchStart = _prevRatio = Vector2.zero;
        Enabled = false;
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
            ratio.x = ((drag.x / _screenSize.x) * 4);
        }
        
        if (_steering.Grounded)
		{
            ratio.y = Mathf.Min(1, _prevRatio.y + yAcc);
			airTime = airSlowDuration;
		}
		else
		{
			airTime -= yAcc;
			ratio.y = Mathf.Lerp(0.0f, _prevRatio.y, airTime / airSlowDuration);
		}
        
        _steering.Move(ratio.x, ratio.y);
        _prevRatio = ratio;
    }
}