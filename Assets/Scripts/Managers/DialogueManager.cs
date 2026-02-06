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

    //Runtime Vars
    public bool isTypeWriterRunning = false;
    public bool IsDialogueRunning => textBox.activeSelf;
    private List<string> subStrings = new();
    private int currentSubStringIndex = 0;
    private string stringCurrentlyBeingTypewritten;

    //Tuning Vars
    [SerializeField] private float typeWriterDelayBetweenChars;

    public void SetupDialogue(string dialogue)
    {
        textBox.SetActive(true);
        CreateSubStrings(dialogue);
        DisplayNextDialogue();
        
    }

    public void DisplayNextDialogue()
    {
        if (currentSubStringIndex == subStrings.Count)
        {
            ResetDialogue();
            return;
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
        subStrings = new List<string>();
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
