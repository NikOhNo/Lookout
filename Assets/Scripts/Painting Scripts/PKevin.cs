using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PKevin : PaintingBase
{
    [SerializeField] private AudioClip[] meowSFX;
    [SerializeField] private float timeUntilFail;
    [SerializeField] private float maxTimeBetweenMeows;
    [SerializeField] private float minTimeBetweenMeows;
    private AudioSource audioSource;

    //Runtime Vars
    private float currentTimeUntilFail;
        
    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentTimeUntilFail = timeUntilFail;
        StartCoroutine(PlayRandomMeow());
    }

    // Update is called once per frame
    protected override void Update()
    {
        currentTimeUntilFail -= Time.deltaTime;

    }

    private IEnumerator PlayRandomMeow()
    {
        audioSource.PlayOneShot(meowSFX[UnityEngine.Random.Range(0, meowSFX.Length - 1)]);
        yield return new WaitForSeconds((1 + (currentTimeUntilFail / timeUntilFail)) * (maxTimeBetweenMeows - minTimeBetweenMeows));
        StartCoroutine(PlayRandomMeow());
    }


}
