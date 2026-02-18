using UnityEngine;

/*
 * This is the BIIG global event bus that stores all the conditions for painting tasks being completed.
 * It invokes functions that paintings can subscribe to once these conditions have been met.
 * If we want any exceptions we can just make a new event here and subscribe it in a painting script's
 * Start or elsewhere during runtime it's quite flexible.
 */
public class TaskManager : MonoBehaviour
{
    //Refs
    private ReferenceManager referenceManager;
    private DialogueManager dialogueManager;


    public delegate void EventHandler();
    //Painting Events
    public event EventHandler MonaLisaTaskComplete;
    public event EventHandler RomeoTaskComplete;
    public event EventHandler JulietTaskComplete;
    public event EventHandler KevinTaskComplete;
    //Global Events

    //Runtime Vars
    

    private void Start()
    {
        referenceManager = ReferenceManager.Instance;
        dialogueManager = referenceManager.dialogueManager;
    }

    private void Update()
    {
        
    }


    #region Mona Lisa

    public void MonaLisaSmileCheckBoxMarked()
    {
        DebugLogEventCall(MonaLisaTaskComplete.ToString());
        MonaLisaTaskComplete?.Invoke();
    }

    #endregion

    #region Kevin

    public void KevinFoodDelivered()
    {
        DebugLogEventCall(KevinTaskComplete.ToString());
        KevinTaskComplete?.Invoke();
    }

    #endregion

    private void DebugLogEventCall(string eventName)
    {
        Debug.Log("Invoked Event: " + eventName);
    }

}
