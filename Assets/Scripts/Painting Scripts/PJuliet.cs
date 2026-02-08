using UnityEngine;

public class PJuliet : PaintingBase, IInteractable
{

    //Refs
    [SerializeField] private PRomeo romeoMyLove;

    protected override void Start()
    {
        base.Start();
        taskManager.RomeoTaskComplete += TaskComplete;
        UpdateState(PaintingState.HASTASKTOGIVE, dialogue[currentDialogueIndex].giveTaskDialogue);
    }

    public void WaitingForRomeoMessage()
    {
        UpdateState(PaintingState.WAITINGFORTASKCOMPLETION, dialogue[currentDialogueIndex].specialDialogue);
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
                    romeoMyLove.WaitingForJulietMessage();
                    paintingState = PaintingState.IDLE; //skip waiting if you're giving the task and go to idle
                    nextDialogueToBeShown = dialogue[currentDialogueIndex].giveTaskDialogue;
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

    protected override void TaskComplete()
    {
        
    }

    public override void DialogueEnded()
    {
        base.DialogueEnded();
    }

}
