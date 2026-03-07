using NUnit.Framework.Internal.Commands;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

/*
 * The execution order of what happens when you talk to a painting goes as follows:
 * Interact -> UpdateState -> DialogueManager logic runs -> DialogueEnded
 */

public class PaintingBase : MonoBehaviour, IInteractable
{
    public enum PaintingState
    {
        IDLE, //When you talk to a painting while they have no task to give. Pulls from random dialogue
        HASTASKTOGIVE, //When you talk to a painting and they give you a task
        WAITINGFORTASKCOMPLETION, //When you talk to a painting and they've given you a task that
                                  //you haven't completed
        WAITINGFORRESPONSE, //When you're in the middle of answering a multiple choice quiz from a painting
        PISSEDOFF, //When you talk to a painting after you didn't complete their task on time.
    }
    public Interactor Interactor { get; set; }
    public string InteractText => "Talk with " + paintingName;

    //Refs
    public DialogueScriptableObject[] dialogue;
    protected ReferenceManager referenceManager;
    protected TaskManager taskManager;
    protected DialogueManager dialogueManager;
    protected GameManager gameManager;

    //Tuning Vars
    [SerializeField] protected string paintingName;
    [SerializeField] protected float timeBetweenTasks;
    [SerializeField] protected float timeBeforePissedOff;
    [TextArea(3, 20)]
    [SerializeField] private List<string> randomDialogue = new();

    //Runtime Vars
    public int currentDialogueIndex;
    protected string nextDialogueToBeShown;
    public PaintingState paintingState;
    private List<string> randomDialogueGraveyard = new();
    protected float timeThatIllGetPissedOffAt = 9999;

    //This interact function just displays nextDialogueToBeShown, which is updated elsewhere through
    //UpdateState or some other means
    public virtual void Interact()
    {
        Debug.Log("Current State: " + paintingState);
        if (currentDialogueIndex >= dialogue.Length)
        {
            GetRandomDialogue();
            return;

        }
        if (!dialogueManager.IsDialogueRunning)
        {
            UpdateState();
            dialogueManager.SetupDialogue(nextDialogueToBeShown, this);
        }
            
        else
            dialogueManager.DisplayNextDialogue();
        // Interactor?.Notifier.ShowInteract(InteractText);
    }

    //For any generic behavior when states are updated, put it in this switch statement. 
    //For any SPECIAL behavior do NOT put it in this method unless you want to override the entire switch.
    public virtual void UpdateState(PaintingState newState, string nextDialogueToShow)
    {
        paintingState = newState;
        nextDialogueToBeShown = nextDialogueToShow;
    }

    public virtual void UpdateState(PaintingState newState)
    {
        paintingState = newState;
    }

    protected virtual void UpdateState()
    {
        switch (paintingState)
        {
            case PaintingState.IDLE:
            {
                nextDialogueToBeShown = GetRandomDialogue();
                break;
            }
            case PaintingState.HASTASKTOGIVE:
            {
                paintingState = PaintingState.WAITINGFORTASKCOMPLETION;
                StartPissedOffTimer();
                nextDialogueToBeShown = dialogue[currentDialogueIndex].giveTaskDialogue;
                break;        
            }
            case PaintingState.WAITINGFORTASKCOMPLETION:
            {
                break;
            }
            case PaintingState.PISSEDOFF:
            {
                gameManager.updateNumberOfPissedOffPaintings(-1);
                paintingState = PaintingState.IDLE;
                break;
            }
        }
    }

    protected string GetRandomDialogue()
    {
        if (randomDialogue.Count <= 0)
        {
            randomDialogue = new List<string>(randomDialogueGraveyard);
            randomDialogueGraveyard.Clear();
        }

        string dialogueChosen = randomDialogue[Random.Range(0, randomDialogue.Count)];
        randomDialogue.Remove(dialogueChosen);
        randomDialogueGraveyard.Add(dialogueChosen);
        Debug.Log("Playing Random Dialogue Index: " + randomDialogue.Count);
        return dialogueChosen;
    }

    public virtual void TaskGive()
    {

    }


    //Specific paintings can have special behavior for each of these with their overrides.
    //BY DEFAULT: This sets the next dialogue to CompleteTaskDialogue and then plays it immediately
    //If there isn't any more special behavior beyond this, you don't need to override this method
    protected virtual void TaskComplete() //Recieves TaskComplete events from TaskManager
    {
        UpdateState(PaintingState.IDLE, dialogue[currentDialogueIndex].completeTaskDialogue);
        nextDialogueToBeShown = dialogue[currentDialogueIndex].completeTaskDialogue;
        if (currentDialogueIndex <= dialogue.Length)
        {
            currentDialogueIndex++;
        }
        else
            currentDialogueIndex = 0;
        StartCoroutine(TimeBetweenTasksTimer());
    }

    //Any specific things that happen when Dialogue Ends can be overriden (VFX, SFX, animations, etc.)
    public virtual void DialogueEnded()
    {

    }

    public void StartPissedOffTimer()
    {
        timeThatIllGetPissedOffAt = gameManager.globalTimer 
            + (timeBeforePissedOff + gameManager.GetRandomTimeToGetPissedOffModifier());
        Debug.Log("Started " + paintingName +  "'s Pissed Off Timer. Current Time: " + Time.deltaTime
            + ", Time When Pissed Off: " + timeThatIllGetPissedOffAt);
    }

    protected virtual IEnumerator TimeBetweenTasksTimer()
    {
        yield return new WaitForSeconds(timeBetweenTasks);
        StartPissedOffTimer();
        UpdateState(PaintingState.HASTASKTOGIVE, dialogue[currentDialogueIndex].giveTaskDialogue);
    }

    //Specific paintings subscribe to TaskManager's events in their Start override.
    protected virtual void Start()
    {
        referenceManager = ReferenceManager.Instance;
        this.taskManager = referenceManager.taskManager;
        this.dialogueManager = referenceManager.dialogueManager;
        this.gameManager = referenceManager.gameManager;
    }

    protected virtual void FixedUpdate()
    {
        if (gameManager.globalTimer >= timeThatIllGetPissedOffAt && paintingState != PaintingState.PISSEDOFF)
        {
            Debug.Log(paintingName + " is PISSED OFF");
            UpdateState(PaintingState.PISSEDOFF, dialogue[currentDialogueIndex].failTaskDialogue);
            gameManager.updateNumberOfPissedOffPaintings(1);
        }
        
    }
}
