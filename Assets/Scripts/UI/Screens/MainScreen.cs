using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainScreen : MonoBehaviour {

    public Text coinCount;

    [Inject] User _user;
    
    void Start () {
        coinCount.text = "Coin Count " + _user.state.coinCount.ToString();
    }
}