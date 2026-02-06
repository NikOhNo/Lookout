using UnityEngine;

public class PMonaLisa : PaintingBase
{

    protected override void Start()
    {
        base.Start();
        taskManager.MonaLisaTaskComplete += TaskComplete;
    }

    protected override void GetNextDialogue()
    {
    }
}
