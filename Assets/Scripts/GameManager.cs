using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject draggingUnit;
    public GameObject currentContainer;
    public GameObject gameUnit;

    // currency variables
    public int currentCurrency = 0;
    public int startingCurrency = 100;
    public Text currencyText = null;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        Debug.Log("instance assignment.");
    }

    void Start()    
    {
        currentCurrency = startingCurrency;
        currencyText = GameObject.Find("Currency").GetComponent<Text>();
        currencyText.text = "$ " + currentCurrency.ToString();   
    }

    public void PlaceObject()
    {
        if (draggingUnit != null && currentContainer != null)
        {
            Instantiate(draggingUnit.GetComponent<UnitDrag>().card.unitGame, currentContainer.transform);
            currentContainer.GetComponent<UnitContainer>().isFull = true;
            UpdateCurrency(-20);
        }
    }

    public void UpdateCurrency(int amount) {
        currentCurrency += amount;
        if (currencyText != null) {
            currencyText.text = "$ " + currentCurrency.ToString();
        }
    }
}
