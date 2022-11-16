using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]private DialogueQueue _queue;
    [SerializeField] private DialogueSequence _sequence;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitSprite;
    [SerializeField] private GameObject dialoguePanel;
   
    private bool nextInput;
   private void Awake()
    {
        GrabDialogue(_sequence);
    }
   private void OnEnable()
    {
        EventsManager.Instance.StartGame += EndDialogue;
    }
    private void OnDisable()
    {
        EventsManager.Instance.StartGame -= EndDialogue;
    }
    private void GrabDialogue(DialogueSequence sequence)
    {
        _queue = new DialogueQueue(sequence);

        foreach (var phrase in sequence.Phrases)
        {
            _queue.Enqueue(phrase);
        }
        StartCoroutine(PlayDialogue());
    }
    private IEnumerator PlayDialogue()
    {
        //Todo: Add another coroutine to pop up dialogue box
        while (!_queue.IsEmpty())
        {
            var dia = _queue.Dequeue();
            nameText.text = dia.Character.Name;
            dialogueText.text = dia.Sentence;
            portraitSprite.sprite = dia.Character.Portrait;
            yield return StartCoroutine(WaitForButtonPress());
        }
        EventsManager.Instance.OnGameStart();
    }
    private IEnumerator WaitForButtonPress()
    {
        while (!nextInput)
        {
            yield return null;
        }
        nextInput = false;
        yield return new WaitForFixedUpdate();
    }
    public void NextDialogue()
    {
        nextInput = true;
        SoundEffectsHandler.Instance.PlayEffect("UI-Click");
    }
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
