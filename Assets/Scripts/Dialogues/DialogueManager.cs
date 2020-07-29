using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueWindow;
    [SerializeField]
    private TextMeshProUGUI nameField, textField;
    public static DialogueManager Instance { get; private set; }

    private Queue<Dialogue> dialogue;
    private UnityEvent dialogueEndEvent;
    public bool Started { get; private set; }

    private void Awake()
    {
        Instance = this;
        Started = false;
        dialogueWindow.SetActive(false);
    }

    public bool StartDialogue(Dialogue[] dialogue, UnityEvent dialogueEndEvent)
    {
        if (Started || dialogue.Length == 0)
        {
            Started = false;
            return false;
        }

        this.dialogueEndEvent = dialogueEndEvent;

        this.dialogue = new Queue<Dialogue>();
        dialogueWindow.SetActive(true);
        foreach (Dialogue d in dialogue)
        {
            this.dialogue.Enqueue(d);
        }
        this.dialogue.Enqueue(dialogue[dialogue.Length - 1]);
        Started = true;

        if (Timer.Instance is Timer)
            Timer.Instance.StopTimer();
        if (ControlButtons.Instance is ControlButtons)
            ControlButtons.Instance.moveable = false;
        if (LevelUI.Instance is LevelUI)
            LevelUI.Instance.HideUI();
        NextReplique();
        return true;
    }

    public void NextReplique()
    {
        if (!Started)
            return;
        Dialogue replique = dialogue.Dequeue();
        nameField.text = replique.name;
        textField.text = replique.text;

        if (dialogue.Count == 0)
            EndDialogue();
    }

    private void EndDialogue()
    {
        Started = false;
        dialogueWindow.SetActive(false);
        if (Timer.Instance is Timer)
            Timer.Instance.StartTimer();
        if (ControlButtons.Instance is ControlButtons)
            ControlButtons.Instance.moveable = true;
        if (LevelUI.Instance is LevelUI)
            LevelUI.Instance.ShowUI();
        dialogueEndEvent.Invoke();
    }
}
