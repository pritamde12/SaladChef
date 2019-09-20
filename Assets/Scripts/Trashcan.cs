using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Interactable
{
    public override void OnInteract(MasterChef chef)
    {
        if(!chef.IsHandEmpty())
        {
            chef.GetFoodFromHand(this, true);
        }

        else if(chef.HasSalad())
        {

        }
    }
}
