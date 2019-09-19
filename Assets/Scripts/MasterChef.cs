using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MasterChef : MonoBehaviour
{
    public PLAYER playerType;
    Vector2 moveVector;
    float speed = 5;
    Vector2 heightBounds;
    Vector2 sideBounds;
    private void OnEnable()
    {
        if(playerType == PLAYER.PLAYER_1)
        {
            PlayerInput.movePlayer_1 += OnMove;
        }
        else
        {
            PlayerInput.movePlayer_2 += OnMove;
        }

        heightBounds = GameManager.instance.heightBounds;
        sideBounds = GameManager.instance.sideBounds;
    }

    private void OnDisable()
    {
        if (playerType == PLAYER.PLAYER_1)
        {
            PlayerInput.movePlayer_1 -= OnMove;
        }
        else
        {
            PlayerInput.movePlayer_2 -= OnMove;
        }
    }

    private void OnMove(Vector2 move)
    {
        moveVector = move;

        

        Debug.Log("Moving");
    }

    //Update position and then constrain it
    private void Update()
    {
        transform.position += new Vector3(moveVector.x * Time.deltaTime * speed, moveVector.y * Time.deltaTime * speed, 0);
        Vector3 currentPosition = transform.position;


        if(currentPosition.x > sideBounds.y)
           currentPosition.x = sideBounds.y;
       
        else if(currentPosition.x < sideBounds.x)
           currentPosition.x = sideBounds.x;
       
        if(currentPosition.y > heightBounds.y)
          currentPosition.y = heightBounds.y;
       
        else if(currentPosition.y < heightBounds.x)
          currentPosition.y = heightBounds.x;
       

        transform.position = currentPosition;
    }
}
