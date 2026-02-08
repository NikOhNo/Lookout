using UnityEngine;

public class ItemCatFood : ItemBase, IInteractable
{

    public string InteractText => isItemHeld ? "Drop Sashimi" : playerItemHandler.nearPainting
        ? "Use Sashimi" : "Pick Up Sashimi";

    public Interactor Interactor { get; set; }

    public void Interact()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
