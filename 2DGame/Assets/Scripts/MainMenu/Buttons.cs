using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] public Button playButton;
    [SerializeField] public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(OnClickPlayFunction);
        quitButton.onClick.AddListener(OnClickQuitFunction);
    }

    void OnClickPlayFunction()
    {
        SceneManager.LoadScene(1);
    }
    
    void OnClickQuitFunction()
    {
        Application.Quit();
    }
}
