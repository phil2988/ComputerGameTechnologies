using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuBoxTemplate : MonoBehaviour
{
    private GameObject menuBox;
    private Vector3 _scaleVector;
    public MenuBoxTemplate()
    {
        Console.WriteLine("Constructor");
        var loadedPrefabResource = LoadPrefabFromFile();
        menuBox = (GameObject)Instantiate(loadedPrefabResource, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private UnityEngine.Object LoadPrefabFromFile()
    {
        string filename = "Assets/Resources/BorderPrefab/BorderPrefab2";
        Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
        var loadedObject = Resources.Load(filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return loadedObject;
    }

    public void SetSize(Vector3 scaleVector)
    {
        Console.WriteLine("SetSize");
        menuBox.transform.localScale = scaleVector;
    }

    public GameObject GetMenuBox()
    {
        return menuBox;
    }

    public void Destroy()
    {
        Destroy(menuBox);
    }
}