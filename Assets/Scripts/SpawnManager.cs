using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public ServingPoint[] servingPoints;
    ServingPoint tempSP;

    public ORDER_TYPE ORDER_GROUP = ORDER_TYPE.EASY;
    public string[] allOrders;

    public static UnityAction OnCustomerLeft;

    int customerLeftCount = 0;

    private void Start()
    {
        Shuffle();
        SpawnACustomere();
        SpawnACustomere();
        OnCustomerLeft += SetOrderTypeAndSpawn;
    }
    public void Shuffle()
    {
        for (int i = 0; i < servingPoints.Length; i++)
        {
            int rnd = Random.Range(0, servingPoints.Length);
            tempSP = servingPoints[rnd];
            servingPoints[rnd] = servingPoints[i];
            servingPoints[i] = tempSP;
        }
    }

    public void SpawnACustomere()
    {
        for (int i = 0; i < servingPoints.Length; i++)
        {
            if(servingPoints[i].IsAvailable)
            {
                servingPoints[i].CallACustomer(PrepareOrder());
                break;
            }
        }

    }

    void SetOrderTypeAndSpawn()
    {
        customerLeftCount++;

        if (customerLeftCount < 5)
        {
            ORDER_GROUP = ORDER_TYPE.EASY;
        }
        else if (customerLeftCount < 10)
        {
            ORDER_GROUP = ORDER_TYPE.MEDIUM;
        }

        else if (customerLeftCount < 14)
        {
            ORDER_GROUP = ORDER_TYPE.EASY;
        }
        else
        {
            int i = Random.Range(2,4);
            ORDER_GROUP = (ORDER_TYPE)i;
        }


        SpawnACustomere();

    }

    public List<string> PrepareOrder()
    {

        int mode = (int)ORDER_GROUP;
        List<string> newOrder = new List<string>();
        
        for ( int i = 0; i < mode; i++)
        {
            string newKey = GetRandomOrder();

            while (newOrder.Contains(newKey))
            {
                newKey = GetRandomOrder();
            }

            newOrder.Add(newKey);
        }
        Debug.Log(">>>>"+ newOrder.Count);
        return newOrder;
    }
      
    public string GetRandomOrder()
    {
        return allOrders[Random.Range(0, allOrders.Length)]; 
    }
}
