using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Runtime Vars
    private int numberOfPissedOffPaintings = 0;

    //Tuning Vars
    public enum Difficulty { 
        EASY,
        MEDIUM,
        HARD
    }
    public Difficulty difficulty;
    private int maxNumberOfPissedOfPaintings;
    private int minTimeBetweenTasksModifier;
    private int maxTimeBetweenTasksModifier;

    public void ChangeDifficulty(Difficulty newDifficulty)
    {
        if (newDifficulty == Difficulty.EASY)
        {
            maxNumberOfPissedOfPaintings = 5;
            minTimeBetweenTasksModifier = 0;
            maxTimeBetweenTasksModifier = 10;
        }
        else if (newDifficulty == Difficulty.MEDIUM)
        {
            maxNumberOfPissedOfPaintings = 3;
            minTimeBetweenTasksModifier = -5;
            maxTimeBetweenTasksModifier = 5;
        }
        else if (newDifficulty == Difficulty.HARD)
        { 
            maxNumberOfPissedOfPaintings = 2;
            minTimeBetweenTasksModifier = 1;
            maxTimeBetweenTasksModifier = -10;
        }
    }

    public int GetRandomTimeBetweenTasksModifier()
    {
        return Random.Range(minTimeBetweenTasksModifier, maxTimeBetweenTasksModifier + 1);
    }


    public void updateNumberOfPissedOffPaintings(int increment)
    {
        numberOfPissedOffPaintings += increment;
        if (numberOfPissedOffPaintings == maxNumberOfPissedOfPaintings)
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        Debug.Log("Game Over!");
        while (Time.timeScale > 0) {
            yield return new WaitForEndOfFrame();
            Time.timeScale -= 0.1f;
        }
    }


    private void Start()
    {
        
    }

}
