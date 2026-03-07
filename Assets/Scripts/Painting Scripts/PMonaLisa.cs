using UnityEngine;

public class PMonaLisa : PaintingBase
{

    protected override void Start()
    {
        base.Start();
        taskManager.MonaLisaTaskComplete += TaskComplete;
        UpdateState(PaintingState.HASTASKTOGIVE);
    }

    protected override void TaskComplete()
    {
        base.TaskComplete();
        Interact();
    }

}
