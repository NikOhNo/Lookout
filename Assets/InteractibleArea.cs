using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class InteractibleArea : MonoBehaviour
{
    [SerializeField] InteractNotifier interactNotifier;

    // this is just here to keep track of the interactibles that the player is overlapping. consider the closest one

    private List<GameObject> overlappingInteractibles = new();

    // look through the list to find the closest one to our position, then return its tag. so we can compare in the player against like "coffee machine" or "painting" or something
    public GameObject TryInteract()
    {
        if (overlappingInteractibles.Count == 0)
        {
            return null;
        }

        GameObject closestGO = overlappingInteractibles[0];
        foreach (GameObject go in overlappingInteractibles)
        {
            if ((transform.position - closestGO.transform.position).magnitude > (transform.position - go.transform.position).magnitude)
            {
                closestGO = go;
            }
        }

        return closestGO; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("interactibles"))
        {
            interactNotifier.ShowInteract("Coffee!");
            overlappingInteractibles.Add(collision.gameObject);
            Debug.Log(collision.gameObject.name);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("interactibles"))
        {
            interactNotifier.HideInteract();
            overlappingInteractibles.Remove(collision.gameObject); // lol. lmao
        }
    }
}
