using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData", order = 1)]
public class ScriptObjDialogue : ScriptableObject
{
    [TextArea]
    public string[] randomDialogues;








    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
