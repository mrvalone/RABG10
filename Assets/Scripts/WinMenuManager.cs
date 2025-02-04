using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour {

    public void OnRestartClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
