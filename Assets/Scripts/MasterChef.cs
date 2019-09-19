using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MasterChef : MonoBehaviour
{
    public PLAYER playerType;
    Vector2 moveVector;
    float speed = 5;
    Vector2 heightBounds;
    Vector2 sideBounds;

    //shows if pressing the Interact button is valid
    public GameObject interactIndicator;
    public Image item_1;
    public Image item_2;

    bool canInteract;
    Interactable currentInteractable = null;

    Queue<string> foodsInHand = new Queue<string>();

    private void OnEnable()
    {
        if(playerType == PLAYER.PLAYER_1)
        {
            PlayerInput.movePlayer_1 += OnMove;
            PlayerInput.InteractPlayer_1 += OnTryInteract;
        }
        else
        {
            PlayerInput.movePlayer_2 += OnMove;
            PlayerInput.InteractPlayer_2 += OnTryInteract;
        }

        heightBounds = GameManager.instance.heightBounds;
        sideBounds = GameManager.instance.sideBounds;
    }

    private void OnDisable()
    {
        if (playerType == PLAYER.PLAYER_1)
        {
            PlayerInput.movePlayer_1 -= OnMove;
            PlayerInput.InteractPlayer_1 -= OnTryInteract;
        }
        else
        {
            PlayerInput.movePlayer_2 -= OnMove;
            PlayerInput.InteractPlayer_2 -= OnTryInteract;
        }
    }

    private void OnMove(Vector2 move)
    {
        moveVector = move;   
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

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        canInteract = true;
        interactIndicator.SetActive(true);
        currentInteractable = collision.GetComponent<Interactable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {       
        canInteract = false;
        interactIndicator.SetActive(false);
        currentInteractable = null;
    }

    void OnTryInteract()
    {
        if(canInteract)
        {
            currentInteractable.OnInteract(this);
        }
    }

    public void AddItemToHand(Food food)
    {       
        foodsInHand.Enqueue(food.KEY);
        if(foodsInHand.Count == 1)
        {
            item_1.gameObject.SetActive(true);
            item_1.color = food.icon.color;
        }
        else
        {
            item_2.gameObject.SetActive(true);
            item_2.color = food.icon.color;
        }
    }

    public bool IsHandFull()
    {
        if (foodsInHand.Count == 2)
            return true;
        else
            return false;
    }

    public bool IsHandEmpty()
    {
        if (foodsInHand.Count != 0)
            return false;
        else
            return true;
    }

    public void GetFoodFromHand(Interactable interactable, bool destroyOnTransfer = false)
    {
        Debug.Log("Getting food from hand");
        string foodName        =  foodsInHand.Dequeue();
        Food food = DataManager.instance.GetFoodWithKey(foodName);        
        Food spawnedFood =  Instantiate(food, transform.position, Quaternion.identity);
        spawnedFood.transform.SetParent(interactable.transform);
        spawnedFood.transform.DOLocalMove(Vector3.zero, 0.3f).OnComplete(()=> {
            if (destroyOnTransfer)
            {
                Destroy(spawnedFood.gameObject);
            }
        });
        UpdateIcon();


    }

   void UpdateIcon()
    {
        if(foodsInHand.Count == 0)
        {
            item_1.gameObject.SetActive(false);
            return;
        }
        string foodName = foodsInHand.Peek();
        item_2.gameObject.SetActive(false);

        item_1.gameObject.SetActive(true);
        item_1.color = DataManager.instance.GetFoodWithKey(foodName).icon.color;
    }
}
