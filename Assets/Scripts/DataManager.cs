using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    Food[] allFood;
    public GameObject foodObject;

    public Orderdata orderdata;

    private Dictionary<string, Food> FOOD_ITEMS = new Dictionary<string, Food>();
    private Dictionary<ORDER_TYPE, float> ORDER_TIME = new Dictionary<ORDER_TYPE, float>();
    private Dictionary<ORDER_TYPE, float> ORDER_PENALTY = new Dictionary<ORDER_TYPE, float>();
    private Dictionary<ORDER_TYPE, float> ORDER_REWARD = new Dictionary<ORDER_TYPE, float>();



    private void Awake()
    {
        instance = this;
        for (int i = 0; i < orderdata.allOrderType.Length; i++)
        {
            ORDER_TIME.Add(orderdata.allOrderType[i].type, orderdata.allOrderType[i].waitingTime);
            ORDER_PENALTY.Add(orderdata.allOrderType[i].type, orderdata.allOrderType[i].penalty);
            ORDER_REWARD.Add(orderdata.allOrderType[i].type, orderdata.allOrderType[i].reward);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        allFood = Resources.LoadAll<Food>("Food");

        for (int i = 0; i < allFood.Length; i++)
        {
            FOOD_ITEMS.Add(allFood[i].KEY, allFood[i]);
        }

        
    }

    public Food GetFoodWithKey(string key)
    {
        if (!FOOD_ITEMS.ContainsKey(key))
        {
            Debug.LogError("No Food Found with key : "+ key);
            return null;
        }
        return FOOD_ITEMS[key];
    }

    public float GetWaitingTime(ORDER_TYPE oRDER_TYPE)
    {
        return ORDER_TIME[oRDER_TYPE];
    }

    public float GetReward(ORDER_TYPE oRDER_TYPE)
    {
        return ORDER_REWARD[oRDER_TYPE];
    }

    public float GetPenally(ORDER_TYPE oRDER_TYPE)
    {
        return ORDER_PENALTY[oRDER_TYPE];
    }
}
