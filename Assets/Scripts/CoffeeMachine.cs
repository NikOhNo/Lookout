using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class CoffeeMachine : MonoBehaviour, IInteractable
{
    bool coffeeReady = false;
    bool brewing = false;
    float brewTime;
    float expendedTime = 0.0f;

    // quintenary expression
    public string InteractText => isClogged ? "Unclog" : coffeeReady ? "COFFEE" : brewing ? "Brewing..." : "Brew Coffee";

    public Interactor Interactor { get; set; }

    AudioSource audioSource;

    public enum CoffeeType {Risky, Normal, Powerful};

    public CoffeeMenuHandler coffeeMenuHandler; // ref to the ui object selecting coffee

    // boolean explosion
    private bool isWaitingForSelection = false;
    private bool clogChecked = false;
    private bool isClogged = false;

    private CoffeeRoastSO currentRoast;

    public TMP_Text cloggedIndicator;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // brewTime = audioSource.clip.length;
        Debug.Log(coffeeReady);
        coffeeMenuHandler.coffeeSelectorPressed.AddListener(OnCoffeeButtonPressed);
    }
    
    void Update()
    {
        if (!coffeeReady && brewing)
        {
            expendedTime += Time.deltaTime;
            // Finished brewing coffee
            if (expendedTime >= brewTime)
            {
                FinishBrewing();
            }
            if (expendedTime >= brewTime / 2.0f && !clogChecked)
            {
                float p = UnityEngine.Random.Range(0.0f, 1.0f);
                if (p <= currentRoast.clogChance)
                {
                    // cloggers
                    SetClogged(true);
                }
                clogChecked = true;
            }
        }
    }

    public void Interact()
    {
        if (coffeeReady)
        {
            GiveCoffee();
        }
        else if (!brewing && !isClogged)
        {
            // open coffee selection menu
            OpenCoffeeSelector();
            Debug.Log("Hello"); 
            // wait for player to select something
            // Start Brewing
            // StartBrewing();
        }
        else if (isClogged)
        {
            // TODO unclog minigame ? :) the witless
            SetClogged(false);
        }
    }

    private void ShowInteractText()
    {
        Interactor?.Notifier.ShowInteract(InteractText);
    }

    private void StartBrewing(CoffeeRoastSO coffeeRoast)
    {
        currentRoast = coffeeRoast;

        brewTime = coffeeRoast.brewTime;
        brewing = true;
        clogChecked = false; // set all my fucking bools
        ShowInteractText();
        expendedTime = 0.0f;
        audioSource.pitch = audioSource.clip.length / coffeeRoast.brewTime;
        audioSource.PlayOneShot(audioSource.clip);

        Debug.Log($"starting coffee brew of roast {coffeeRoast.name} and brew time {brewTime}");
    }
    
    private void FinishBrewing()
    {
        coffeeReady = true;
        brewing = false;
        ShowInteractText();

        Debug.Log("Coffee is ready broski");
    }

    private void GiveCoffee()
    {
        Interactor.GetComponentInParent<PlayerController>().ResetCoffee();
        coffeeReady = false;
        brewing = false;
        expendedTime = 0.0f;
        ShowInteractText();
    }

    private void OpenCoffeeSelector()
    {
        // coffeeMenuHandler.enabled = true;
        coffeeMenuHandler.gameObject.SetActive(true);
        isWaitingForSelection = true;
    }

    private void CloseCoffeeSelector()
    {
        coffeeMenuHandler.gameObject.SetActive(false);
        isWaitingForSelection = false;
    }

    private void OnCoffeeButtonPressed(CoffeeRoastSO coffeeRoast)
    {
        if (isWaitingForSelection)
        {
            StartBrewing(coffeeRoast);
            CloseCoffeeSelector();
        }
    }

    private void SetClogged(bool val)
    {
        cloggedIndicator.gameObject.SetActive(val);
        isClogged = val;
        brewing = !val;
        ShowInteractText();

        if (val)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
