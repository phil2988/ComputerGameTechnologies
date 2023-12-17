using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] public Button respawnButton;
    [SerializeField] public Button mainMenuButton;
    
    public PlayerStats playerStats;
    
    
    // Start is called before the first frame update
    void Start()
    {
        respawnButton.onClick.AddListener(OnClickRespawnFunction);
        mainMenuButton.onClick.AddListener(OnClickMainMenuFunction);
    }
    
    void OnClickRespawnFunction()
    {
        playerStats.respawnPlayer();
    }
    void OnClickMainMenuFunction()
    {
        SceneManager.LoadSceneAsync(0);
    }
    
}
