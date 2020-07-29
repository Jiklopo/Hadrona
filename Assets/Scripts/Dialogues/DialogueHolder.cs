using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogue;

    [SerializeField] private UnityEvent dialogueEndEvent;

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue, dialogueEndEvent);
    }
}