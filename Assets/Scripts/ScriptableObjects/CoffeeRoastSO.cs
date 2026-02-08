using UnityEngine;


// making a class for this because FUCK YOU!!
[CreateAssetMenu(fileName = "CoffeeRoast", menuName = "ScriptableObjects/CoffeeRoasts", order = 2)]
public class CoffeeRoastSO : ScriptableObject {
    public string roastName;
    public float brewTime;
    public float speedBonus;
    public float staminaRegenBonus;

    [Range(0, 1.0f)]
    public float clogChance;

    // add any other stats or fields or meows
}