using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Refs
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private GameObject textBox;
    [SerializeField] private ReferenceManager referenceManager;
    [SerializeField] private TaskManager taskManager;

    //Runtime Vars
    public bool isTypeWriterRunning = false;
    public bool IsDialogueRunning = false;
    private List<string> subStrings = new();
    private int currentSubStringIndex = 0;
    private string stringCurrentlyBeingTypewritten;
    private PaintingBase currentPainting;
    private DialogueScriptableObject nextDialogueObject;

    //Tuning Vars
    [SerializeField] private float typeWriterDelayBetweenChars;

    private void Start()
    {
        referenceManager = ReferenceManager.Instance;
        taskManager = referenceManager.taskManager;
    }

    public void SetupDialogue(string dialogue, PaintingBase currentPainting, DialogueScriptableObject nextDialogue)
    {
        IsDialogueRunning = true;
        this.currentPainting = currentPainting;
        textBox.SetActive(true);
        CreateSubStrings(dialogue);
        DisplayNextDialogue();

        if (nextDialogue)
            nextDialogueObject = nextDialogue;
    }

    //Checks for special dialogue/response dialogue and automatically plays them
    private void SetupNextDialogue()
    {
        if (nextDialogueObject.isSpecialDialogue)
            SetupDialogue(nextDialogueObject.specialDialogue, currentPainting, nextDialogueObject.nextDialogue);
        else if (nextDialogueObject.hasResponseChoices)
        { 
            //Not Implemented: Response choices panel
        }
        currentPainting.currentDialogueIndex++;
    }

    public void DisplayNextDialogue()
    {
        if (currentSubStringIndex == subStrings.Count)
        {
            if (nextDialogueObject)
            {
                SetupNextDialogue();   
                return;
            }
            else
            {
                ResetDialogue();
                return;
            }
        }
        if (isTypeWriterRunning)
        {
            DisplayAllDialogueImmediately();
            return;
        }

        StartCoroutine(TypeWriter(subStrings[currentSubStringIndex]));
    }

    private void ResetDialogue()
    {
        IsDialogueRunning = false;
        currentPainting.DialogueEnded();
        currentPainting = null;
        textBox.SetActive(false);
        subStrings.Clear();
        currentSubStringIndex = 0;
        stringCurrentlyBeingTypewritten = string.Empty;
        isTypeWriterRunning = false;
    }


    // alvin did you test this
    //It works meow T_T -cralvin
    private void CreateSubStrings(string dialogue)
    {
        int lastIndex = 0;
        char[] charArray = dialogue.ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            char currentChar = charArray[i];
            if (i == charArray.Length - 1 || currentChar == '\n')
            {
                subStrings.Add(dialogue.Substring(lastIndex, i - lastIndex));
                Debug.Log(dialogue.Substring(lastIndex, i - lastIndex));
                lastIndex = i+1;
            }
        }
    }

    private IEnumerator TypeWriter(string dialogue)
    {
        isTypeWriterRunning = true;
        string stringToBuild = "";
        stringCurrentlyBeingTypewritten = dialogue;
        foreach (char character in dialogue)
        { 
            stringToBuild = stringToBuild + character;
            textField.text = stringToBuild;
            yield return new WaitForSeconds(typeWriterDelayBetweenChars);
        }
        isTypeWriterRunning = false;
        currentSubStringIndex++;
    }

    public void DisplayAllDialogueImmediately()
    {
        StopAllCoroutines();
        textField.text = stringCurrentlyBeingTypewritten;
        currentSubStringIndex++;
        isTypeWriterRunning = false;
    }


}
