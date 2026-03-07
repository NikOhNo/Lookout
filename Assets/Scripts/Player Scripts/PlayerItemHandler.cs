using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{

    //Reference Vars
    private ReferenceManager referenceManager;
    private TaskManager taskManager;
    [SerializeField] private float itemRotationSpeed;
    public Vector3 heldItemOffset;

    //Runtime Vars
    public ItemBase heldItem;
    public bool nearPainting;

    private void Start()
    {
        referenceManager = ReferenceManager.Instance;
        taskManager = referenceManager.taskManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GossipPainting"))
        {
            taskManager.UpdateGossipCount(1);
            Debug.Log("meowww");
        }


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
