using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PKevin : PaintingBase
{
    //Refs
    [SerializeField] private AudioClip[] meowSFX;
    
    //Tuning Vars
    [SerializeField] private float maxTimeBetweenMeows;
    [SerializeField] private float minTimeBetweenMeows;
    [SerializeField] private float vibrationIntensity;
    [SerializeField] private float maxVibrationIntensity;
    private AudioSource audioSource;

    //Runtime Vars
    private float timePassedSinceHungry = 0;
    private Vector3 originPoint;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TimeBetweenTasksTimer());
        taskManager.KevinTaskComplete += TaskComplete;
        originPoint = transform.position;
    }

    public override void Interact()
    {
        //Just get a random meow
        nextDialogueToBeShown = GetRandomDialogue();
        base.Interact();
    }


    private void StartTask()
    {
        StartCoroutine(PlayRandomMeow());
        StartPissedOffTimer();
    }

    protected override IEnumerator TimeBetweenTasksTimer()
    {
        yield return new WaitForSeconds(timeBetweenTasks);
        StartTask();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (paintingState == PaintingState.PISSEDOFF)
        {
            timePassedSinceHungry += Time.deltaTime;
            float currentVibrationAmount = 
                Math.Clamp(timePassedSinceHungry * vibrationIntensity, 0, maxVibrationIntensity);
            transform.position = originPoint + 
                new Vector3(UnityEngine.Random.Range(-1, 2) * currentVibrationAmount,
                UnityEngine.Random.Range(-1, 2) * currentVibrationAmount, 0);
        }
    }

    protected override void TaskComplete()
    {
        timeThatIllGetPissedOffAt = 9999;
        StopAllCoroutines();
        if (paintingState == PaintingState.PISSEDOFF)
        {
            gameManager.updateNumberOfPissedOffPaintings(-1);
            UpdateState(PaintingState.IDLE);
            transform.position = originPoint;
        }
        timePassedSinceHungry = 0;
        StartCoroutine(TimeBetweenTasksTimer());
    }



    private IEnumerator PlayRandomMeow()
    {
        audioSource.PlayOneShot(meowSFX[UnityEngine.Random.Range(0, meowSFX.Length - 1)]);
        if (timeThatIllGetPissedOffAt - Time.deltaTime > 0)
        {
            yield return new WaitForSeconds(((timeThatIllGetPissedOffAt - Time.deltaTime) / timeThatIllGetPissedOffAt)
            * (maxTimeBetweenMeows - minTimeBetweenMeows));
        }
        else
        {
            yield return new WaitForSeconds(minTimeBetweenMeows);
        }
        StartCoroutine(PlayRandomMeow());
    }


}
