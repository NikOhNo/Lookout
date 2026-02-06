using UnityEngine;

public class PRomeo : PaintingBase, IInteractable
{
    protected override void Start()
    {
        base.Start();
        taskManager.JulietTaskComplete += TaskComplete;
    }
}
