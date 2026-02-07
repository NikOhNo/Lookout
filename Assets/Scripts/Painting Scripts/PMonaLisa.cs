using UnityEngine;

public class PMonaLisa : PaintingBase
{

    protected override void Start()
    {
        base.Start();
        taskManager.MonaLisaTaskComplete += TaskComplete;
        nextDialogueToBeShown = dialogue[currentDialogueIndex].giveTaskDialogue;
    }

    protected override void TaskComplete()
    {
        base.TaskComplete();
    }

    protected override void GetNextDialogue()
    {
    }
}
