using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoffeeMachine : MonoBehaviour, IInteractable
{
    bool coffeeReady = false;
    bool brewing = false;
    float brewTime;
    float expendedTime = 0.0f;

    public string InteractText => coffeeReady ? "COFFEE" : brewing ? "Brewing..." : "Brew Coffee";

    public Interactor Interactor { get; set; }

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        brewTime = audioSource.clip.length;
    }
    
    void Update()
    {
        if (!coffeeReady)
        {
            expendedTime += Time.deltaTime;
            // Finished brewing coffee
            if (expendedTime >= brewTime)
            {
                FinishBrewing();
            }
        }
    }

    public void Interact()
    {
        if (coffeeReady)
        {
            GiveCoffee();
        }
        else if (!brewing)
        {
            // Start Brewing
            StartBrewing();
        }
    }

    private void StartBrewing()
    {
        Interactor?.Notifier.ShowInteract("Brewing...");
        brewing = true;
        expendedTime = 0.0f;
        audioSource.PlayOneShot(audioSource.clip);
    }
    
    private void FinishBrewing()
    {
        Interactor?.Notifier.ShowInteract("COFFEE");
        coffeeReady = true;
        brewing = false;
    }

    private void GiveCoffee()
    {
        Interactor.GetComponentInParent<PlayerController>().ResetCoffee();
        coffeeReady = false;
        brewing = false;
        expendedTime = 0.0f;
        Interactor.Notifier.ShowInteract("Brew Coffee");
    }
}
