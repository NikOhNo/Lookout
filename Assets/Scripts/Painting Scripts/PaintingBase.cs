using Unity.VisualScripting;
using UnityEngine;

public class PaintingBase : MonoBehaviour, IInteractable
{
    //Refs
    public DialogueScriptableObject[] dialogue;

    //Tuning Vars
    [SerializeField] private string paintingName;

    public Interactor Interactor { get; set; }
    public string InteractText => "Talk with " + paintingName;
    [SerializeField] protected string placeHolder;

    public void Interact()
    {
        Debug.Log("yeah i'm interacting");
        GetNextDialogue();
        // Interactor?.Notifier.ShowInteract(InteractText);
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
