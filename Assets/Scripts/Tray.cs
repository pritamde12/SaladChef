using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tray : Interactable
{
    public override void OnInteract(MasterChef chef)
    {
      if(transform.childCount == 0)
        {
            Debug.Log("check if hand is empty");
            //take food
            if(!chef.IsHandEmpty())
            {
                chef.GetFoodFromHand(this);
            }
            else
                Debug.Log("hand is empty");
        }
      else
        {
            //give food
            Food foodOnTray = GetComponentInChildren<Food>();
            foodOnTray.transform.SetParent(chef.transform);
            foodOnTray.transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() => { chef.AddItemToHand(foodOnTray); Destroy(foodOnTray.gameObject); });


        }
    }
}
