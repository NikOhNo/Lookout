using System;
using System.Buffers;
using Unity.VisualScripting;
using UnityEngine;


public class ItemBase : MonoBehaviour
{

    //Refs
    protected ReferenceManager referenceManager;
    protected PlayerItemHandler playerItemHandler;
    protected TaskManager taskManager;
    protected BoxCollider col;
    protected Rigidbody rb;
    protected Type reciever;

    //Runtime Vars
    public bool isItemHeld = false;

    protected virtual void Start()
    {
        if (reciever == null)
            Debug.LogWarning("Item reciever not set! Set a reciever in the Start method of Item child classes.");
        referenceManager = ReferenceManager.Instance;
        playerItemHandler = referenceManager.playerItemHandler;
        taskManager = referenceManager.taskManager;
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    //Pickup Item

    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isItemHeld = true;
            col.isTrigger = true;
            gameObject.transform.parent = playerItemHandler.gameObject.transform;
            transform.localPosition = Vector3.zero + playerItemHandler.heldItemOffset;
            playerItemHandler.heldItem = this;
            rb.isKinematic = true;
        }
        if (other.TryGetComponent<PaintingBase>(out PaintingBase painting))
        {
            if (painting.GetType() == reciever)
            {
                DeliverItem(painting);
                Debug.Log("Giving item " + gameObject.name + " to " + painting.gameObject.name);
            }
        }
        Debug.Log("Item" + gameObject.name + " collided with: " + other.gameObject.name);
    }

    public virtual void DeliverItem(PaintingBase painting)
    {
        col.enabled = false;
        playerItemHandler.heldItem = null;
        //You can add more special logic for other types of interactions here!
    }


    public void DropItem()
    {
        col.isTrigger = false;
        isItemHeld = false;
        gameObject.transform.parent = null;
        rb.isKinematic = false;
    }

    protected void ConsumeItem()
    {
        Destroy(gameObject);
    }


}
