using NUnit.Framework.Internal.Commands;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingBase : MonoBehaviour, IInteractable
{
    //Refs
    public DialogueScriptableObject[] dialogue;
    protected ReferenceManager referenceManager;
    protected TaskManager taskManager;
    protected DialogueManager dialogueManager;

    //Tuning Vars
    [SerializeField] private string paintingName;

    public Interactor Interactor { get; set; }
    public string InteractText => "Talk with " + paintingName;
    [SerializeField] protected string placeHolder;

    //Runtime Vars
    protected int currentDialogueIndex;
    protected string nextDialogueToBeShownWhenInteractedWith;


    public void Interact()
    {
        if (!dialogueManager.IsDialogueRunning)
            dialogueManager.SetupDialogue(dialogue[currentDialogueIndex].giveTaskDialogue);
        else
            dialogueManager.DisplayNextDialogue();
        // Interactor?.Notifier.ShowInteract(InteractText);
    }

    protected virtual void GetNextDialogue()
    { 
        
    }

    protected virtual void UpdateStatus()
    { 
        
    }

    protected virtual void TaskComplete()
    {
        nextDialogueToBeShownWhenInteractedWith = dialogue[currentDialogueIndex].completeTaskDialogue;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        referenceManager = ReferenceManager.Instance;
        this.taskManager = referenceManager.taskManager;
        this.dialogueManager = referenceManager.dialogueManager;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
