using UnityEngine;

public class Interactor : MonoBehaviour
{
    public InteractNotifier Notifier;
    IInteractable focusedInteractable = null;

    public void Interact()
    {
        focusedInteractable?.Interact();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out var interactable))
        {
            //Debug.Log($"Entered trigger with interactable: {interactable.InteractText}");
            if (focusedInteractable != null) focusedInteractable.Interactor = null;
            focusedInteractable = interactable;
            focusedInteractable.Interactor = this;
            Notifier.ShowInteract(interactable.InteractText);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out var interactable))
        {
            if (focusedInteractable == interactable)
            {
                focusedInteractable.Interactor = null;
                focusedInteractable = null;
                Notifier.HideInteract();
            }
        }
    }
}
