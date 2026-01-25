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
    public bool isDialogueBoxActive = false;
    private List<string> subStrings;
    private int currentSubStringIndex = 0;
    private string stringCurrentlyBeingTypwritten;

    //Tuning Vars
    [SerializeField] private float typeWriterDelayBetweenChars;

    public void DisplayDialogue(string dialogue)
    {
        if (currentSubStringIndex == subStrings.Count)
        {
            ResetDialogue();
        }

        if (!isDialogueBoxActive)
        {
            CreateSubStrings(dialogue);
            StartCoroutine(TypeWriter(subStrings[currentSubStringIndex]));
        }
        else
            StartCoroutine(TypeWriter(subStrings[currentSubStringIndex]));

        currentSubStringIndex++;
    }

    private void ResetDialogue()
    {

        textBox.SetActive(false);

        subStrings.Clear();
        currentSubStringIndex = 0;
        stringCurrentlyBeingTypwritten = string.Empty;
        isTypeWriterRunning = false;
        isDialogueBoxActive = false;
    }


    private void CreateSubStrings(string dialogue)
    {
        subStrings = new List<string>();
        int lastIndex = 0;
        char[] charArray = dialogue.ToCharArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            lastIndex = i;
            char currentChar = charArray[i];
            if (currentChar == '\n' && charArray[i+1] != '\0')
            {
                subStrings.Add(dialogue.Substring(lastIndex, i));
                
            }
        }
    }

    private IEnumerator TypeWriter(string dialogue)
    {
        isTypeWriterRunning = true;
        string stringToBuild = "";
        dialogue = stringCurrentlyBeingTypwritten;
        foreach (char character in dialogue)
        { 
            stringToBuild = stringToBuild + character;
            textField.text = stringToBuild;
            yield return new WaitForSeconds(typeWriterDelayBetweenChars);
        }
        isTypeWriterRunning = false;
    }

    public void DisplayAllDialogueImmediately(string dialogue)
    {
        StopAllCoroutines();
        textField.text = stringCurrentlyBeingTypwritten;
    }


}
