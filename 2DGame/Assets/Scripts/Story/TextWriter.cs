using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private static TextWriter _instance;
    private List<TextWriterSingle> _textWriterSingleList;

    private void Awake()
    {
        _instance = this;
        _textWriterSingleList = new List<TextWriterSingle>();
    }

    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,
        bool invisibleCharacters, bool removeWriterBeforeAdd)
    {
        if (removeWriterBeforeAdd)
        {
            _instance.RemoveWriter(uiText);
        }
        return _instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters);
    }
    private TextWriterSingle AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        TextWriterSingle textWriterSingle =
            new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters);
        _textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TextMeshProUGUI uiText)
    {
        _instance.RemoveWriter(uiText);
    }
    private void RemoveWriter(TextMeshProUGUI uiText)
    {
        for (int i = 0; i < _textWriterSingleList.Count; i++)
        {
            if (_textWriterSingleList[i].GetUIText() == uiText)
            {
                _textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _textWriterSingleList.Count; i++)
        {
            bool destroyInstance = _textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                _textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }


    //Represents a single textwriter instance
    public class TextWriterSingle
    {
        private TextMeshProUGUI _uiText;
        private string _textToWrite;
        private int _characterIndex;
        private float _timePerCharacter;
        private float _timer;
        private bool _invisibleCharacters;

        public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,
            bool invisibleCharacters)
        {
            _uiText = uiText;
            _textToWrite = textToWrite;
            _timePerCharacter = timePerCharacter;
            _invisibleCharacters = invisibleCharacters;
            _characterIndex = 0;
        }

        //Return true on isComplete
        public bool Update()
        {
            _timer -= Time.deltaTime;
            while (_timer <= 0f)
            {
                //display next character
                _timer += _timePerCharacter;
                _characterIndex++;
                string text = _textToWrite.Substring(0, _characterIndex);
                if (_invisibleCharacters)
                {
                    text += "<color=#00000000>" + _textToWrite.Substring(_characterIndex) + "</color";
                }

                _uiText.text = text;

                if (_characterIndex >= _textToWrite.Length)
                {
                    return true;
                }
            }
            return false;
        }

        public TextMeshProUGUI GetUIText()
        {
            return _uiText;
        }

        public bool IsActive()
        {
            return _characterIndex < _textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            _uiText.text = _textToWrite;
            _characterIndex = _textToWrite.Length;
            TextWriter.RemoveWriter_Static(_uiText);
            
        }
    }
}
