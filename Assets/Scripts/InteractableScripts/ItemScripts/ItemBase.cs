using System.Buffers;
using UnityEngine;

public class ItemBase : MonoBehaviour
{

    //Refs
    protected ReferenceManager referenceManager;
    protected PlayerItemHandler playerItemHandler;
    private BoxCollider col;

    //Runtime Vars
    public bool isItemHeld = false;

    private void Start()
    {
        referenceManager = ReferenceManager.Instance;
        playerItemHandler = referenceManager.playerItemHandler;
        col = GetComponent<BoxCollider>();
    }

    //Pickup Item
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isItemHeld = true;
            col.enabled = false;
            //hide sprite
            gameObject.transform.parent = other.transform;
        }
    }

    public void DropItem()
    {
        col.enabled = true;
        isItemHeld = false;
        gameObject.transform.parent = null;
    }


}
