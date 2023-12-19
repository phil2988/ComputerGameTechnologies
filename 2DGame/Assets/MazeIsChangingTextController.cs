using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeIsChangingTextController : MonoBehaviour
{
    public GameObject text;

    public void UpdateActive()
    {
        Debug.Log("Toggling Text!");

        text.SetActive(!text.activeSelf);
    }
}
