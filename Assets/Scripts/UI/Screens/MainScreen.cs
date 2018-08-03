using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainScreen : BaseScreen {

    public Text coinCount;

    private User _user;

    [Inject]
    public void Init (User user) {
        _user = user;

        coinCount.text = "Coin Count " + _user.state.coinCount.ToString();
    }
}