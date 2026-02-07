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
    //Global Events

    //Runtime Vars
    public bool isCarryingRomeosMessageForJuliet = false;
    public bool isCarryingJulietsMessageForRomeo = false;
    public float globalTimer = 0;

    private void Start()
    {
        referenceManager = ReferenceManager.Instance;
        dialogueManager = referenceManager.dialogueManager;
    }

    private void Update()
    {
        if (!dialogueManager.IsDialogueRunning)
        {
            globalTimer += Time.deltaTime;
        }
    }


    #region Mona Lisa


    public void MonaLisaSmileCheckBoxMarked()
    {
        MonaLisaTaskComplete?.Invoke();
    }

    #endregion

    public void JulietMessageCorrect()
    { 
        JulietTaskComplete?.Invoke();
    }

    public void RomeoMessageCorrect()
    { 
        RomeoTaskComplete?.Invoke();
    }



}
