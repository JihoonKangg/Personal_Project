using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Sentence;

    public UnityAction done = null;

    Queue<string> sentences = new Queue<string>();

    public Animator anim;

    public void Begin(Dialogue info)
    {
        SceneData.Inst.NPC_Talking = true;
        anim.SetBool("IsOpen", true);
        sentences.Clear();

        Name.text = info.name;

        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Next();
    }

    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }

        Sentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach(var letter in sentence)
        {
            Sentence.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void End()
    {
        anim.SetBool("IsOpen", false);
        Sentence.text = string.Empty;
        SceneData.Inst.NPC_Talking = false;
        done?.Invoke();
        done = null;
    }
}
