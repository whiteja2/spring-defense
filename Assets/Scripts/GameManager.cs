using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject draggingUnit;
    public GameObject currentContainer;
    public GameObject gameUnit;
    public TMP_Text roundOverText;

    // currency variables
    public int currentCurrency = 0;
    public int startingCurrency = 100;
    public Text currencyText = null;

    public static GameManager instance;

    //round manager variables
    private RoundManager roundManager;

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

    void Update()
    {
        RoundOver();
    }

    public void PlaceObject()
    {
        if (draggingUnit != null && currentContainer != null && currentCurrency > 0)
        {
            Instantiate(draggingUnit.GetComponent<UnitDrag>().card.unitGame, currentContainer.transform);
            currentContainer.GetComponent<UnitContainer>().isFull = true;
            UpdateCurrency(-20);
        }
    }

    public void UpdateCurrency(int amount)
    {
        currentCurrency += amount;
        if (currencyText != null)
        {
            currencyText.text = "$ " + currentCurrency.ToString();
        }
    }

    public void RoundOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && roundManager != null && roundManager.GetRoundTimer() == 0f)
        {
            roundOverText.gameObject.SetActive(true);
            roundOverText.text = "Round Over!";
            StartCoroutine(LoadMainMenuAfterDelay(3f));
        }
    }

    private IEnumerator LoadMainMenuAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("MainMenu");
    }

}
