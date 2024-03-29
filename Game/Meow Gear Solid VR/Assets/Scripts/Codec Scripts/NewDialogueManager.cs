using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewDialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    // Dialogue Box animation
    public Animator dialogueAnimator;

    // Puts all sentences we are going to display into queue
    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue (Dialogue dialogue)
    {
        // Open dialogue box
        dialogueAnimator.SetBool("dialogueIsOpen", true);

        nameText.text = dialogue.callerName;

        // Clears any previous sentences
        sentences.Clear();

        // Loop through sentences and add to queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If there is no sentences in queue, end dialogue
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialogueAnimator.SetBool("dialogueIsOpen", false);
    }
}
