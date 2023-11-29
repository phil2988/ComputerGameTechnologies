using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
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
        string textFile = "Assets/Scripts/Story/Story.txt";

        // Read entire text file content in one string
        string text = File.ReadAllText(textFile);
        Debug.Log(text);
        _messageArray = text.Split("<next>");
        _messageCounter = _messageArray.Length;
        button.onClick.AddListener(OnClickFunction);
    }

    private void OnClickFunction()
    {
        if (_textWriterSingle != null && _textWriterSingle.IsActive())
        {
            //Currently active TextWriter
            _textWriterSingle.WriteAllAndDestroy();
        }
        if (_textWriterSingle != null && _messageCounter > _messageArray.Length)
        {
            string message = "End of Story";
            _textWriterSingle = TextWriter.AddWriter_Static(storyText, message, .05f, true, true);
        }
        else
        {
            Debug.Log(_messageArray.Length);
            string message = _messageArray[_messageCounter];
            Debug.Log(message);
            _textWriterSingle = TextWriter.AddWriter_Static(storyText, message, .05f, true, true);
            _messageCounter++;
        }
    }

    private void Start()
    {
        //TextWriter.AddWriter_Static(storyText, "Hello World!", .1f, true);
    }
}