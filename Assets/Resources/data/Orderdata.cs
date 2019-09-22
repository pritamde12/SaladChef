using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orderdata : ScriptableObject
{
    [System.Serializable]
    public class OrderDetails
    {
        public ORDER_TYPE type;
        public float waitingTime;
        public float reward;
        public float penalty;
    }
    public OrderDetails[] allOrderType;
}
