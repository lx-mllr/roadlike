using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainScreen : MonoBehaviour {

    public Text highscore;

    [Inject] User _user;
    
    void Start () {
        highscore.text = "High Score " + _user.state.highScore.ToString();
    }
}