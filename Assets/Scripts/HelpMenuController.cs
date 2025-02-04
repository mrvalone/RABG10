using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenuController : MonoBehaviour {

    public void OnReturnClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
