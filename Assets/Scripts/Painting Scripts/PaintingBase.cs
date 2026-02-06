using NUnit.Framework.Internal.Commands;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingBase : MonoBehaviour, IInteractable
{
    public enum PaintingState
    {
        IDLE, //When you talk to a painting while they have no task to give. Pulls from random dialogue
        HASTASKTOGIVE, //When you talk to a painting and they give you a task
        WAITINGFORTASKCOMPLETION, //When you talk to a painting and they've given you a task that
                                  //you haven't completed
        PISSEDOFF, //When you talk to a painting after you didn't complete their task on time.
    }
    public Interactor Interactor { get; set; }
    public string InteractText => "Talk with " + paintingName;
    [SerializeField] protected string placeHolder;

    //Refs
    public DialogueScriptableObject[] dialogue;
    protected ReferenceManager referenceManager;
    protected TaskManager taskManager;
    protected DialogueManager dialogueManager;

    //Tuning Vars
    [SerializeField] protected string paintingName;
    [SerializeField] protected int timeBetweenTasks;

    //Runtime Vars
    protected int currentDialogueIndex;
    protected string nextDialogueToBeShown;
    public PaintingState paintingState;

    //This interact function just displays nextDialogueToBeShown, which is updated elsewhere through
    //UpdateState or some other means
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

    //If we want special behavior when states are updated, put it in this switch statement. 
    //Otherwise, calling UpdateStatus will automatically set the state and dialogue accordingly.
    protected virtual void UpdateStatus(PaintingState newState)
    {
        paintingState = newState;
        switch (newState)
        {
            case PaintingState.HASTASKTOGIVE:
                nextDialogueToBeShown = dialogue[currentDialogueIndex].giveTaskDialogue;
                break;

        }
    }

    //Specific paintings can have special behavior for each of these with their overrides.
    protected virtual void TaskComplete() //Recieves TaskComplete events from TaskManager
    {
        nextDialogueToBeShown = dialogue[currentDialogueIndex].completeTaskDialogue;
    }

    protected IEnumerator TimeBetweenTasksTimer()
    { 
        yield return new WaitForSeconds(timeBetweenTasks);
        UpdateStatus(PaintingState.HASTASKTOGIVE);
    }

    //Specific paintings subscribe to TaskManager's events in their Start override.
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
