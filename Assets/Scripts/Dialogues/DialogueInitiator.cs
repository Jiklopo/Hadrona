using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueHolder))]
public class DialogueInitiator : MonoBehaviour
{
    private DialogueHolder dialogue;
    void Start()
    {
        dialogue = GetComponent<DialogueHolder>();
        dialogue.StartDialogue();
    }
}
