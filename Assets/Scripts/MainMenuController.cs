using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void OnStartClick()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void OnTipClicked()
    {
        SceneManager.LoadScene("HELP");
    }


}
