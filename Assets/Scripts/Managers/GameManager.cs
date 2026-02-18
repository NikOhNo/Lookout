using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Refs
    ReferenceManager referenceManager;
    DialogueManager dialogueManager;

    //Runtime Vars
    public float globalTimer = 0;

    private int numberOfPissedOffPaintings = 0;
    private int maxNumberOfPissedOfPaintings;
    private int minPissedOffTimeModifier;
    private int maxPissedOfftimeModifier;

    //Tuning Vars
    public enum Difficulty { 
        EASY,
        MEDIUM,
        HARD
    }
    public Difficulty difficulty;
    [SerializeField] private float gameOverTimeSlowSpeed;
    [Header("Difficulty Tuning Vars")]
    [SerializeField] private int easyMaxNumOfPissedOffPaintings;
    [SerializeField] private int easyMinPissedOffTimeModifier;
    [SerializeField] private int easyMaxPissedOffTimeModifier;
    [SerializeField] private int mediumMaxNumOfPissedOffPaintings;
    [SerializeField] private int mediumMinPissedOffTimeModifier;
    [SerializeField] private int mediumMaxPissedOffTimeModifier;
    [SerializeField] private int hardMaxNumOfPissedOffPaintings;
    [SerializeField] private int hardMinPissedOffTimeModifier;
    [SerializeField] private int hardMaxPissedOffTimeModifier;
    private void Start()
    {
        ChangeDifficulty(difficulty);
        referenceManager = ReferenceManager.Instance;
        dialogueManager = referenceManager.dialogueManager;
    }

    public void ChangeDifficulty(Difficulty newDifficulty)
    {
        if (newDifficulty == Difficulty.EASY)
        {
            maxPissedOfftimeModifier = easyMaxPissedOffTimeModifier;
            minPissedOffTimeModifier = easyMinPissedOffTimeModifier;
            maxNumberOfPissedOfPaintings = easyMaxNumOfPissedOffPaintings;
        }
        else if (newDifficulty == Difficulty.MEDIUM)
        {
            maxPissedOfftimeModifier = mediumMaxPissedOffTimeModifier;
            minPissedOffTimeModifier = mediumMinPissedOffTimeModifier;
            maxNumberOfPissedOfPaintings = mediumMaxNumOfPissedOffPaintings;
        }
        else if (newDifficulty == Difficulty.HARD)
        { 
            maxPissedOfftimeModifier = hardMaxPissedOffTimeModifier;
            minPissedOffTimeModifier = hardMinPissedOffTimeModifier;
            maxNumberOfPissedOfPaintings = hardMaxNumOfPissedOffPaintings;
        }
    }

    public int GetRandomTimeToGetPissedOffModifier()
    {
        return Random.Range(minPissedOffTimeModifier, maxNumberOfPissedOfPaintings + 1);
    }


    public void updateNumberOfPissedOffPaintings(int increment)
    {
        numberOfPissedOffPaintings += increment;
        if (numberOfPissedOffPaintings == maxNumberOfPissedOfPaintings)
        {
            StartCoroutine(GameOver());
        }
        Debug.Log("Pissed off Paintings: " + numberOfPissedOffPaintings + "/" + maxNumberOfPissedOfPaintings);
    }

    private IEnumerator GameOver()
    {
        float currentTimeScale = Time.timeScale;
        Debug.Log("Game Over!");
        while (true) {
            yield return new WaitForEndOfFrame();
            currentTimeScale -= gameOverTimeSlowSpeed;
            if (currentTimeScale < 0)
            {
                Time.timeScale = 0;
                yield break;
            }
            else
                Time.timeScale = currentTimeScale;
        }
    }

    private void Update()
    {
        if (!dialogueManager.IsDialogueRunning)
        {
            globalTimer += Time.deltaTime;
            Debug.Log(globalTimer);
        }
    }


}
