using UnityEngine;

public class PJuliet : PaintingBase, IInteractable
{
    protected override void Start()
    {
        base.Start();
        taskManager.RomeoTaskComplete += TaskComplete;
    }
}
