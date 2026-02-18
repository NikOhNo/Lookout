using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCatFood : ItemBase
{

    protected override void Start()
    {
        reciever = typeof(PKevin);
        base.Start();
    }

    public override void DeliverItem(PaintingBase painting)
    {
        base.DeliverItem(painting);
        taskManager.KevinFoodDelivered();
        Destroy(gameObject);
    }

}
