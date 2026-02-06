using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    //Singleton Stuff
    public static ReferenceManager Instance { get; private set; }
    //Refs
    public DialogueManager dialogueManager;
    public TaskManager taskManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
