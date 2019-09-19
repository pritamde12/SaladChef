using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    Food[] allFood;
    public GameObject foodObject;
    private Dictionary<string, Food> FOOD_ITEMS = new Dictionary<string, Food>();
    private void Awake()
    {
        instance = this;
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
}
