using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractNotifier : MonoBehaviour
{
    TMP_Text interactText;
    Animator animator;

    void Awake()
    {
        interactText = GetComponentInChildren<TMP_Text>();
        animator = GetComponent<Animator>();
        animator.SetBool("visible", false);
    }

    public void ShowInteract(string text)
    {
        interactText.text = text;
        animator.SetBool("visible", true);
    }

    public void HideInteract()
    {
        animator.SetBool("visible", false);
    }
}
