using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeButtons : MonoBehaviour
{
    [SerializeField] public Button resumeButton;

    [SerializeField] public Button menuButton;

    [SerializeField] public GameObject escapeMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(OnClickResumeFunction);
        menuButton.onClick.AddListener(OnClickMenuFunction);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeMenu.SetActive(!escapeMenu.activeSelf);
        }
    }

    void OnClickResumeFunction()
    {
        escapeMenu.SetActive(false);
    }

    void OnClickMenuFunction()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
