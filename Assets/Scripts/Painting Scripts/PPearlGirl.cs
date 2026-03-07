using UnityEngine;

public class PPearlGirl : PaintingBase
{

    [SerializeField] private int gossipNeeded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
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
                    paintingState = PaintingState.WAITINGFORTASKCOMPLETION;
                    StartPissedOffTimer();
                    nextDialogueToBeShown = dialogue[currentDialogueIndex].giveTaskDialogue;
                    break;
                }
            case PaintingState.WAITINGFORTASKCOMPLETION:
                {
                    if (taskManager.currentGossip >= gossipNeeded)
                    {
                        nextDialogueToBeShown = dialogue[currentDialogueIndex].completeTaskDialogue;
                        TaskComplete();
                    }
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


    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void TaskComplete()
    {
        base.TaskComplete();
        taskManager.currentGossip = 0;
    }

}
