using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class ServingPoint : Interactable
{
    
    public Customer customer;
    Collider2D interactionTrigger;
   
    Image[] foodIcons;
    HorizontalLayoutGroup layoutGroup;
    List<string> currentOrder = new List<string>();


    private UnityAction<bool> OnServed;



    private void Start()
    {
        foodIcons = GetComponentsInChildren<Image>(true);
        layoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();
        interactionTrigger = GetComponent<Collider2D>();
        interactionTrigger.enabled = false;
        OnServed = CheckResult;
    }

    bool isAvailable = true;

    public bool IsAvailable { get => isAvailable; set => isAvailable = value; }

    public override void OnInteract(MasterChef chef)
    {
       if(chef.saladInHandIndicator.gameObject.activeSelf)
        {
            
            chef.Serve(this.transform, currentOrder, OnServed);
        }
    }

    void CheckResult(bool result)
    {
        if(result)
        {
            //reward and leave
            customer.transform.DOLocalMoveY(4.37f, 2).OnComplete(()=> isAvailable = true);
            HideOrder();
        }
        else
        {
            //get Angry
        }
    }
   

    public void CallACustomer(List<string> order)
    {
        Debug.Log(order.Count);
        currentOrder = order;
        isAvailable = false;
        Vector3 spawnPoint = transform.position;
        spawnPoint.y += 5;
        customer.gameObject.SetActive(true);
        customer.transform.position = spawnPoint;
       
        customer.transform.DOLocalMoveY(1.37f, 2).OnComplete(()=> ShowOrder(order));
    }

    void HideOrder()
    {
        for (int i = 0; i < foodIcons.Length; i++)
        {
            foodIcons[i].gameObject.SetActive(false);
        }
    }

   void ShowOrder(List<string> order)
    {
        for (int i = 0; i < order.Count; i++)
        {
            foodIcons[i].color = DataManager.instance.GetFoodWithKey(order[i]).icon.color;
            foodIcons[i].gameObject.SetActive(true);
        }

        if (order.Count == 2)
            layoutGroup.spacing = -0.88f;
        else if (order.Count == 3)
            layoutGroup.spacing = -0.56f;

        interactionTrigger.enabled = true;
    }

    void ResetCustomer()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.y += 5;
        customer.transform.position = spawnPoint;
        
    }
}
