using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f;
    public string sentence;
    public Animator animator;
    public PlayerMovement playerBig;
    public PlayerSmall playerSmall;

    private Coroutine typingCoroutine;

    // Trigger to start the dialogue
    public void StartDialogue(string sentence)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        this.sentence = sentence;
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    // Coroutine for typing effect
    IEnumerator TypeSentence()
    {
        textMeshPro.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
