using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueSeries
{
    public string[] sentences;
}

public class DialogueTrigger : MonoBehaviour
{
    DialogueManager dialogueManager;
    public bool isNeedInteraction = true;
    public DialogueSeries[] dialogueSeries;
    private bool isPlayerInRange = false;
    private bool isFinishedSentence = true;
    private int currentSeriesIndex = 0;
    private int currentSentenceIndex = 0;


    private void Start()
    {
        dialogueManager = GameObject.Find("Dialogue").GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerInRange)
        {
            isPlayerInRange = true;
            if (!isNeedInteraction)
                StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        dialogueManager.animator.SetInteger("Show", 1);
        dialogueManager.playerBig.isSeeingDialogue = true;
        dialogueManager.playerSmall.isSeeingDialogue = true;
        if (dialogueManager != null)
        {
            if (currentSeriesIndex < dialogueSeries.Length)
            {
                if (currentSentenceIndex < dialogueSeries[currentSeriesIndex].sentences.Length)
                {
                    dialogueManager.StartDialogue(dialogueSeries[currentSeriesIndex].sentences[currentSentenceIndex]);
                    currentSentenceIndex++;
                }
                else
                {
                    dialogueManager.playerBig.isSeeingDialogue = false;
                    dialogueManager.playerSmall.isSeeingDialogue = false;
                    dialogueManager.animator.SetInteger("Show", 0);
                    currentSentenceIndex = 0;
                    currentSeriesIndex++;
                    
                }
            }
            else
            {
                Debug.Log("All dialogues played");
                dialogueManager.playerBig.isSeeingDialogue = false;
                dialogueManager.playerSmall.isSeeingDialogue = false;
                dialogueManager.animator.SetInteger("Show", 0);
                currentSeriesIndex = 0;
                currentSentenceIndex = 0;
                StartDialogue();
            }
        }
    }
}