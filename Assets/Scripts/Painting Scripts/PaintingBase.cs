using Unity.VisualScripting;
using UnityEngine;

public class PaintingBase : MonoBehaviour, IInteractable
{
    //Refs
    public DialogueScriptableObject[] dialogue;


    public Interactor Interactor { get; set; }
    public string InteractText => "";
    [SerializeField] protected string placeHolder;

    public void Interact()
    {
        Interactor?.Notifier.ShowInteract(InteractText);
    }

    protected virtual void GetNextDialogue()
    { 
        
    }

    protected virtual void UpdateStatus()
    { 
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
