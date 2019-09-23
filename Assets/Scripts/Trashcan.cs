using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactable
{
    public override void OnInteract(MasterChef chef)
    {
        if (chef.HasSalad())
        {
            chef.ThrowSalad(this.transform);
        }
        else if (!chef.IsHandEmpty())
        {
            chef.GetFoodFromHand(this, true);
        }

       
    }
}
