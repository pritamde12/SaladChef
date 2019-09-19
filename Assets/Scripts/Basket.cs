using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Basket : Interactable
{
    public override void OnInteract(MasterChef chef)
    {
        if (chef.IsHandFull())
            return;
        Food newFood = DataManager.instance.GetFoodWithKey(KEY);
        Food foodSpawned = Instantiate(newFood, transform.position, Quaternion.identity);
        foodSpawned.transform.SetParent(chef.transform);
        foodSpawned.transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(()=> { chef.AddItemToHand(foodSpawned); Destroy(foodSpawned.gameObject); });
    }
}
