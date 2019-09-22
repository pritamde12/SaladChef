using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class MasterChef : MonoBehaviour
{
    public PLAYER playerType;
    Vector2 moveVector;
    float speed = 5;
    Vector2 heightBounds;
    Vector2 sideBounds;
    Vector3 foodTargetScale;
    //shows if pressing the Interact button is valid
    public GameObject interactIndicator;
    public Image item_1;
    public Image item_2;
    public Image saladInHandIndicator; //shows if player is carrying salad
    public Transform saladHolder;

    bool canInteract=false;
    bool canMove =true;
    Interactable currentInteractable = null;

    Queue<string> foodsInHand = new Queue<string>();



    private void OnEnable()
    {        
        if (playerType == PLAYER.PLAYER_1)
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
        if (canMove == false)
            return;
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
        if (HasSalad())
            return true;
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

    public bool HasSalad()
    {
        if (saladInHandIndicator.gameObject.activeSelf)
            return true;
        else
            return false;
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

    public void SetMoveState(bool state)
    {
        canMove = state;
    }
    //deactivates/activates interaction feature
    public void SetInteractionState(bool state)
    {
        canInteract = state;
        interactIndicator.SetActive(state);
    }

    public void SaladInHandIndicator(bool state, Vector3 originalScale)
    {
        foodTargetScale = originalScale;
        saladInHandIndicator.gameObject.SetActive(state);

       
        if(state)
        {
            Food[] foods = GetComponentsInChildren<Food>();
            for (int i = 0; i < foods.Length; i++)
            {
                foodsInHand.Enqueue(foods[i].KEY);
            }
           
        }
        else
        {
            foodsInHand.Clear();
        }
        
        
    }

    public void SaladInHandIndicator(bool state)
    {
        SaladInHandIndicator(state, Vector3.zero);
    }

    public void Serve(Transform target, List<string> currentOrder, UnityAction<bool> callback )
    {
        Transform salad = saladHolder.GetChild(0);
        salad.SetParent(target);
      


        bool result = true;



        for (int i = 0; i < currentOrder.Count; i++)
        {
            if(!foodsInHand.Contains(currentOrder[i]))
            {
                Debug.Log(currentOrder[i] + " not found");
                result =  false;
            }
        }


        SaladInHandIndicator(false);


        salad.transform.localScale = foodTargetScale;


        Sequence sequence = DOTween.Sequence();
        sequence.Append(salad.DOLocalMove(Vector3.zero, 0.3f));
        sequence.Append(salad.transform.DOScale(Vector3.zero, 0.3f)).OnComplete(() => { callback?.Invoke(result); Destroy(salad.gameObject); });

                   
       

    }
}
