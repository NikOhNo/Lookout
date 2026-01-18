using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image mask;
    [Range(0.0f, 1.0f)] public float fillAmount;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mask.fillAmount = Mathf.Clamp(fillAmount, 0.0f, 1.0f);
    }
}
