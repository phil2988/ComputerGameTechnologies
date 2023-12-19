using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAttackPeformed;

    public void TriggerAttack()
    {
        OnAttackPeformed?.Invoke();
    }
}
