using UnityEngine;
using Zenject;

public class EditorInputManager : IInputManager
{
    public bool Enabled { get; set; }
	public float yAcc = 0.002f;

    private ISteering _steering;
	private Vector2 _prevRatio = Vector2.zero;

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

    public void Tick ()
    {
        if (!Enabled) {
            return;
        }

        Vector2 inputRatio = new Vector2(Input.GetAxis("Horizontal"),  Mathf.Min(1, _prevRatio.y + yAcc));
        _steering.Move(inputRatio.x, inputRatio.y);
        _prevRatio = inputRatio;
    }
}