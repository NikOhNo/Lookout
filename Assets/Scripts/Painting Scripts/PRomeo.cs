using System.Threading.Tasks.Sources;
using UnityEditor;
using UnityEngine;

public class PRomeo : PaintingBase, IInteractable
{

    //Refs
    [SerializeField] private PJuliet julietMyLove;

    //Runtime Vars
    public bool waitingForJulietsMessage = false;
    

    protected override void Start()
    {
        base.Start();
        
        taskManager.JulietTaskComplete += TaskComplete;
    }

    public void WaitingForJulietMessage()
    {
        UpdateState(PaintingState.WAITINGFORTASKCOMPLETION, dialogue[currentDialogueIndex].specialDialogue);
    }

    public override void Interact()
    {
        base.Interact();
    }

    protected override void TaskComplete()
    {
        base.TaskComplete();
    }

    protected override void UpdateState()
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
                    julietMyLove.WaitingForRomeoMessage();
                    paintingState = PaintingState.IDLE; //skip waiting if you're giving the task and go to idle
                    break;
                }
            case PaintingState.WAITINGFORTASKCOMPLETION:
                {
                    paintingState = PaintingState.PISSEDOFF;
                    break;
                }
            case PaintingState.PISSEDOFF:
                {
                    paintingState = PaintingState.IDLE;
                    break;
                }
        }
    }

    public override void DialogueEnded()
    {
        base.DialogueEnded();
    }

}
