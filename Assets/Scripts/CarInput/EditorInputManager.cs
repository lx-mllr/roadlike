using UnityEngine;
using Zenject;

public class EditorInputManager : IInputManager
{
    public float airSlowDuration = 30.0f;
	public float yAcc = 0.05f;

    private ISteering _steering;
	private Vector2 _prevRatio = Vector2.zero;
    private float airTime;
    private float speedTarget;
    private bool _enabled;
    public bool Enabled { get { return _enabled; } }

    public EditorInputManager(ISteering steering)
    {
        _steering = steering;
    }

    public void Initialize () {
        _enabled = false;
    }

    public void Reset () {
        _prevRatio = Vector2.zero;
        _enabled = false;
    }

    public void Enable () {
        _enabled = true;
    }

    public void Tick ()
    {
        if (!_enabled) {
            return;
        }

        Vector2 inputRatio = Vector2.zero;
        inputRatio.x = Input.GetAxis("Horizontal");

		
        inputRatio.y = Mathf.Min(1, _prevRatio.y + yAcc);

        _steering.Move(inputRatio.x, inputRatio.y, 0.0f, 0.0f);
        _prevRatio = inputRatio;
    }
}