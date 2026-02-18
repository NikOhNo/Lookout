using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{

    //Reference Vars
    [SerializeField] private float itemRotationSpeed;
    public Vector3 heldItemOffset;

    //Runtime Vars
    public ItemBase heldItem;
    public bool nearPainting;


    private void OnTriggerEnter(Collider other)
    {
        if (heldItem)
        {
            if (other.TryGetComponent<PaintingBase>(out PaintingBase painting))
            {
                nearPainting = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nearPainting = false;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, itemRotationSpeed, 0));
    }

}
