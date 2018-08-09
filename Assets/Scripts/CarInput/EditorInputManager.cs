using UnityEngine;
using Zenject;

public class EditorInputManager : IInputManager
{
    public float airSlowDuration = 30.0f;
	public float yAcc = 0.0015f;

    private ISteering _steering;
	private Vector2 _prevRatio = Vector2.zero;
    private float airTime;
    private float speedTarget;
    private bool Enabled { get; set; }

    public EditorInputManager(ISteering steering)
    {
        _steering = steering;
    }

    public void Initialize () {
        Enabled = false;
    }

    public void Reset () {
        _prevRatio = Vector2.zero;
        Enabled = false;
    }

    public void Enable () {
        Enabled = true;
    }

    public void Tick ()
    {
        if (!Enabled) {
            return;
        }

        Vector2 inputRatio = Vector2.zero;
        inputRatio.x = Input.GetAxis("Horizontal");

		if (_steering.Grounded)
		{
            inputRatio.y = Mathf.Min(1, _prevRatio.y + yAcc);
			airTime = airSlowDuration;
		}
		else
		{
			airTime -= yAcc;
			inputRatio.y = Mathf.Lerp(0.0f, _prevRatio.y, airTime / airSlowDuration);
		}
        
        _steering.Move(inputRatio.x, inputRatio.y);
        _prevRatio = inputRatio;
    }
}