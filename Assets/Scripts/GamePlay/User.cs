using System;
using Zenject;

public class User : IInitializable {

    private string state_fileName = "UserState.dat";
    public UserState state { get { return _state; } }
    private UserState _state;

    [Inject]
    SaveManager _saveMangager;

    public User () {
    }

    public void Initialize () {
        _state = _saveMangager.Load<UserState>(state_fileName);
    }

    public void SaveState() {
        _saveMangager.Save(state_fileName, _state);
    }

    public void OnGameEnd(int roundScore) {
        _state.highScore = (roundScore > _state.highScore) ? roundScore : _state.highScore;
        SaveState();
    }

}

[Serializable]
public struct UserState {
    public int highScore;
}