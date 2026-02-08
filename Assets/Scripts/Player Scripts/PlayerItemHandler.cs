using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
    //Runtime Vars
    public ItemBase heldItem;
    public bool nearPainting;

    private void OnTriggerEnter(Collider other)
    {
        if (heldItem)
        {
            if (other.CompareTag("Painting"))
            {
                nearPainting = true;
            }
        }
    }
}
