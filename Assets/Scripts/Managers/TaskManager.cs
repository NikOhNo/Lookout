using UnityEngine;

/*
 * This is the BIIG global event bus that stores all the conditions for painting tasks being completed.
 * It invokes functions that paintings can subscribe to once these conditions have been met.
 * If we want any exceptions we can just make a new event here and subscribe it in a painting script's
 * Start or elsewhere during runtime it's quite flexible.
 */
public class TaskManager : MonoBehaviour
{
    //Events
    public delegate void EventHandler();
    public event EventHandler MonaLisaTaskComplete;
    public event EventHandler RomeoTaskComplete;
    public event EventHandler JulietTaskComplete;
    
    //Runtime Vars

    public void MonaLisaSmileCheckBoxMarked()
    {
        MonaLisaTaskComplete?.Invoke();
    }

    public void JulietMessageRecieved()
    { 
        
    }

    public void RomeoMessageRecieved()
    { 
        
    }


    void Start()
    {
        
    }



}
