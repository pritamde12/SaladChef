using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public ServingPoint[] servingPoints;
    ServingPoint tempSP;

    public ORDER_TYPE ORDER_GROUP = ORDER_TYPE.EASY;
    public string[] allOrders;

    private void Start()
    {
        Shuffle();
        SpawnACustomere();
        SpawnACustomere();
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
