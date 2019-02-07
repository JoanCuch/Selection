using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    GameManager gameManager;
    public Text money;
    public Text year;
    public Text population;
    public Text globalTension;

    public GameObject countryControls;

    public Text infoButton;
    public Text expensiveButton;
    public Text cheapButton;
    public Text nothingButton;

    public Text idCost;
    public Text idDone;

    public GameObject pause;
    public GameObject tutorial;
    public GameObject win;
    public GameObject gameOver;


    // Use this for initialization
    void Start () {

        


        gameManager = GameManager.gameManager;
        UpdateTexts();

    }
	
	// Update is called once per frame
	void Update () {
        UpdateTexts();

    }

    public void NextYear()
    {
        gameManager.NextYear();



    }

    public void UpdateTexts()
    {
        year.text = gameManager.year.ToString();
        money.text = gameManager.money.ToString() + " M $";
        globalTension.text = gameManager.gt.ToString("F1") + "%";
        population.text = gameManager.pop.ToString() + " M";

        countryControls.SetActive(gameManager.isSelected);
        infoButton.gameObject.SetActive(gameManager.isSelected);


        if (gameManager.isSelected)
        {
            string expensiveText = gameManager.countryList[gameManager.selectedCountry].crisis.ReturnExpensiveText();
            string cheapText = gameManager.countryList[gameManager.selectedCountry].crisis.ReturnCheapText();
            string nothingText = gameManager.countryList[gameManager.selectedCountry].crisis.ReturnNothingText();
            string infoText = gameManager.countryList[gameManager.selectedCountry].crisis.ReturnInfoText();

            string countryName = gameManager.countryList[gameManager.selectedCountry].countryName;

            expensiveButton.text = expensiveText + "\n <size=30> Invest: " + gameManager.expensiveCost + " M $</size>";
            cheapButton.text = cheapText + "\n <size=30> Invest: " + gameManager.cheapCost + " M $</size>";
            nothingButton.text = nothingText + "\n <size=30> Do nothing </size>";
            infoButton.text = countryName + " " + infoText;
        }

        pause.SetActive(gameManager.GameState == GameManager.GameStates.pause);
        tutorial.SetActive(gameManager.GameState == GameManager.GameStates.tutorial);
        win.SetActive(gameManager.GameState == GameManager.GameStates.win);
        gameOver.SetActive(gameManager.GameState == GameManager.GameStates.gameOver);

        idDone.text = gameManager.iddone + "%";
        idCost.text = gameManager.idCost + "M $";

    }

    public void ExpensiveAction() { gameManager.DoAction(Crisis.actions.expensive); }
    public void CheapAction() { gameManager.DoAction(Crisis.actions.cheap); }
    public void DoNothingAction() { gameManager.DoAction(Crisis.actions.nothing); }
    public void InvestID() { gameManager.InvestID(); }
    public void Deselect() { gameManager.Deselect(); }



}
