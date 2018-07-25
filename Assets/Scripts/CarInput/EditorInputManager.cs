using UnityEngine;
using Zenject;

public class EditorInputManager : IInputManager
 {
	public float yAcc = 0.002f;

    private ISteering _steering;
	private Vector2 _prevRatio = Vector2.zero;

    public EditorInputManager(ISteering steering)
    {
        _steering = steering;
    }

    public void Initialize ()
    {
    }

    public void Tick ()
    {
        Vector2 inputRatio = new Vector2(Input.GetAxis("Horizontal"),  Mathf.Min(1, _prevRatio.y + yAcc));
        _steering.move(inputRatio.x, inputRatio.y);
        _prevRatio = inputRatio;
    }
}