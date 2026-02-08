using System;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoffeeMenuHandler : MonoBehaviour
{
    public UnityEvent<CoffeeRoastSO> coffeeSelectorPressed;

    public List<CoffeeSelectorButton> selectorButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (CoffeeSelectorButton button in selectorButtons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => {
                coffeeSelectorPressed?.Invoke(button.roast); 
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
