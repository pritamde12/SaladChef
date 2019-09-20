using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CuttingBoard : Interactable
{
    public GameObject knifeObject;
    private MasterChef currentChef;
    public Sprite cutVeggies;

    List<string> itemsInCuttingBoard = new List<string>();

    public override void OnInteract(MasterChef chef)
    {
       if(!chef.IsHandEmpty())
        {
            currentChef = chef;
            chef.GetFoodFromHand(this);
            StartCutting();
        }
        else if(transform.GetComponentInChildren<Food>()  != null)
        {
            Food[] allCutFood = GetComponentsInChildren<Food>();
            for (int i = 0; i < allCutFood.Length; i++)
            {
                itemsInCuttingBoard.Add(allCutFood[i].KEY);
                if(i != 0)
                {
                    allCutFood[i].transform.SetParent(allCutFood[0].transform);
                }
            }
            allCutFood[0].transform.SetParent(chef.transform);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(allCutFood[0].transform.DOLocalMove(Vector3.zero, 0.3f));
            sequence.Append(allCutFood[0].transform.DOScale(Vector3.zero, 0.3f)).OnComplete(() => Destroy(allCutFood[0].gameObject));           
            chef.CollectSaladFromCuttingBoard(itemsInCuttingBoard);
        }
    }

    void StartCutting()
    {
        currentChef.SetMoveState(false);
        currentChef.SetInteractionState(false);
        knifeObject.SetActive(true);
        DOVirtual.DelayedCall(Constants.CUTTING_TIME, FinishedCutting);
    }

    void FinishedCutting()
    {
        int i = transform.childCount;
        Transform lastFood = transform.GetChild(i - 1);
        lastFood.localScale = Vector3.one;
        lastFood.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
        Food targetFood = lastFood.GetComponent<Food>();


        Color lastFoodColor = targetFood.icon.color;
        targetFood.icon.sprite = cutVeggies;
        targetFood.icon.color = lastFoodColor;

       currentChef.SetMoveState(true);
        currentChef.SetInteractionState(true);
        knifeObject.SetActive(false);
    }
}
