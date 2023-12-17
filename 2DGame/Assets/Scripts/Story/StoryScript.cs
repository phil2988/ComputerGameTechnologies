using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class StoryScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI storyText;
    [SerializeField] public Button button;

    private TextWriter.TextWriterSingle _textWriterSingle;

    private int _messageCounter = 1;
    private string[] _messageArray;

    private void Awake()
    {
        //string textFile = "Assets/Resources/Story/Story.txt";
        TextAsset textFileRosource = Resources.Load<TextAsset>("Story/Story");
        
        // Read entire text file content in one string
        //string text = File.ReadAllText(textFile);
        string text = textFileRosource.text;
        _messageArray = text.Split("<next>");
        button.onClick.AddListener(OnClickFunction);
        string message = _messageArray[0];
        Debug.Log(message);
        _textWriterSingle = TextWriter.AddWriter_Static(storyText, message, .05f, true, true);
    }

    private void OnClickFunction()
    {
        if (_textWriterSingle != null && _textWriterSingle.IsActive())
        {
            //Currently active TextWriter
            _textWriterSingle.WriteAllAndDestroy();
        } else 
        if (_textWriterSingle != null && _messageCounter >= _messageArray.Length)
        {
            //string message = "End of Story";
            //_textWriterSingle = TextWriter.AddWriter_Static(storyText, message, .05f, true, true);
            //SceneManager.LoadSceneAsync(2);
            LoadScene(2);

        }
        else
        {
            Debug.Log(_messageArray.Length);
            Debug.Log(_messageCounter);
            string message = _messageArray[_messageCounter];
            Debug.Log(message);
            _textWriterSingle = TextWriter.AddWriter_Static(storyText, message, .05f, true, true);
            _messageCounter++;
        }
    }
    
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progressValue);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}