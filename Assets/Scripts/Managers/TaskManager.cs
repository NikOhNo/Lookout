using UnityEngine;

public class TaskManager : MonoBehaviour
{
    //Events
    public delegate void EventHandler();
    public event EventHandler MonaLisaTaskComplete;
    
    //Runtime Vars

    public void MonaLisaSmileCheckBoxMarked()
    {
        MonaLisaTaskComplete?.Invoke();
    }


    void Start()
    {
        
    }



}
