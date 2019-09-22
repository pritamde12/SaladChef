using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ServingPoint : Interactable
{
    
    public Customer customer;
    Collider2D interactionTrigger;
   
    Image[] foodIcons;
    HorizontalLayoutGroup layoutGroup;
    List<string> currentOrder = new List<string>();


    private UnityAction<bool> OnServed;
    public Transform iconParent;

    public GameObject timerObject;
   



    private void Start()
    {
        foodIcons = iconParent.GetComponentsInChildren<Image>(true);
        layoutGroup = GetComponentInChildren<HorizontalLayoutGroup>();
        interactionTrigger = GetComponent<Collider2D>();
        interactionTrigger.enabled = false;
        OnServed = CheckResult;
        customer.OnTimeUp += DeactivateTimer;
        DeactivateTimer(false);
    }
   

    private void DeactivateTimer(bool state)
    {
        timerObject.SetActive(state);
    }

    private void DeactivateTimer()
    {
        Debug.Log("Deactivating");
        timerObject.SetActive(false);
        customer.transform.DOLocalMoveY(4.37f, 2).OnComplete(() => isAvailable = true);
        HideOrder();
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
            customer.StopTimer();
            DeactivateTimer();
        }
        else
        {
            //get Angry
            customer.AngryModeOn();
            customer.transform.DOShakeScale(1, 0.4f);
        }
    }
   

    public void CallACustomer(List<string> order)
    {
        ORDER_TYPE oRDER_TYPE = (ORDER_TYPE)order.Count;
        Debug.Log(order.Count);
        currentOrder = order;
        isAvailable = false;
        Vector3 spawnPoint = transform.position;
        spawnPoint.y += 5;
        customer.gameObject.SetActive(true);
        customer.transform.position = spawnPoint;
        Debug.Log(DataManager.instance.GetWaitingTime(oRDER_TYPE));
        customer.SetTimer(DataManager.instance.GetWaitingTime(oRDER_TYPE));
        customer.transform.DOLocalMoveY(1.37f, 2).OnComplete(()=> { ShowOrder(order); DeactivateTimer(true); });
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

        customer.StartTimer();
    }

    void ResetCustomer()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.y += 5;
        customer.transform.position = spawnPoint;        
    }


    private void OnDisable()
    {
        OnServed = null;
        customer.OnTimeUp -= DeactivateTimer;
    }
}
