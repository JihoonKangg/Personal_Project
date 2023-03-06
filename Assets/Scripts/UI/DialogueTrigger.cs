using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;
    public Dialogue Successinfo;
    public Dialogue Alreadyinfo;

    public void Trigger(Dialogue d, UnityAction done = null)
    {
        var system = FindObjectOfType<DialogueSystem>();
        system.done = done;
        system.Begin(d);
    }
}
