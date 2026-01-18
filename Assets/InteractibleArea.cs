using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class InteractibleArea : MonoBehaviour
{
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

    void OnTriggerEnter(Collider col)
    {
        overlappingInteractibles.Add(col.gameObject);
        Debug.Log(col.gameObject.name);
    }

    void OnTriggerExit(Collider col)
    {
        overlappingInteractibles.Remove(col.gameObject); // lol. lmao
    }
}
