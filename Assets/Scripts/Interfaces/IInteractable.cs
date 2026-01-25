using UnityEngine;

public interface IInteractable
{
    public Interactor Interactor { get; set; }
    string InteractText { get; }
    public void Interact();
}
