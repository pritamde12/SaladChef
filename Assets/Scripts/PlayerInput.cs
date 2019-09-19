using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public static UnityAction<Vector2> movePlayer_1;
    public static UnityAction<Vector2> movePlayer_2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       

        float y_1 = Input.GetAxis("WASD_Vertical");
        float x_1 = Input.GetAxis("WASD_Horizontal");

        

        float y_2 = Input.GetAxis("Arrow_Vertical");
        float x_2 = Input.GetAxis("Arrow_Horizontal");


        movePlayer_1?.Invoke(new Vector2(x_1, y_1));
        movePlayer_2?.Invoke(new Vector2(x_2, y_2));


    }
}
