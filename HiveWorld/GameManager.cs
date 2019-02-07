using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    public int year;
    public int money;
    [HideInInspector]public int pop; //Population
    [HideInInspector]public float gt; //Global Tension

    public float mPC; //Money per capita
    public float popPY; //Population growth per year in percentatge;

    public float expensiveProportion;
    public float cheapProportion;

    public int expensiveCost;
    public int cheapCost;


    public Country[] countryList;
    public int selectedCountry;
    public bool isSelected;

    public int idCost;
    public int iddone;
    public float idProportion;



    public enum GameStates { tutorial, playing, pause, gameOver, win }
    public GameStates GameState;


    // Use this for initialization
    void Awake() {
        GameState = GameStates.tutorial;

        gameManager = this;
        year = 2050;
        iddone = 0;
              
        isSelected = false;
        selectedCountry = 50;


        
    }
    void Start()
    {
        NextYear();

        foreach (Country country in countryList) {
            country.localPopulation = 666;
        }

    }
    // Update is called once per frame
    void Update() {
        if (gt >= 100) GameState = GameStates.gameOver;
        else if (iddone >= 100) GameState = GameStates.win;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameState == GameStates.tutorial)
            {
                GameState = GameStates.pause;
            }
            else if (GameState == GameStates.playing)
            {
                GameState = GameStates.pause;
            }
            else if (GameState == GameStates.pause)
            {
                GameState = GameStates.playing;
            }
            else if (GameState == GameStates.gameOver || GameState == GameStates.win)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    public void SetSelected(int number)
    {

        if(number == selectedCountry)
        {
            isSelected = false;
            selectedCountry = 50;
        }
        else
        {
            isSelected = true;
            selectedCountry = number;
        }

        if (isSelected)
        {
            expensiveCost = (int)(countryList[selectedCountry].localPopulation * expensiveProportion);
            cheapCost = (int)(countryList[selectedCountry].localPopulation * cheapProportion);
        }

    }

    public void Deselect()
    {
        isSelected = false;
        selectedCountry = 50;
    }

    public void InvestID() {
        if(money >= idCost)
        {
            money -= idCost;
            iddone += 10;
        }

        
    }

    public void NextYear()
    {

        if (GameState == GameStates.playing || GameState == GameStates.tutorial)
        {
            Debug.Log("##################  Year " + year + "  ###################");

            year++;

            int newPop = 0;
            float newGT = 0;

            foreach (Country country in countryList)
            {
                country.NewYearNewEvents();

                country.localPopulation += (int)(country.localPopulation * popPY);
                newPop += country.localPopulation;
                newGT += country.localTension;

            }
            pop = newPop;
            gt = newGT;

            //money += (int)(pop * mPC);
            money += 500;

            //idCost = (int)(pop * (1 + idProportion));
            idCost = 1000;
        }
    }

    public void DoAction(Crisis.actions action)
    {
        switch (action)
        {
            case Crisis.actions.expensive:
                if (money >= expensiveCost)
                {
                    money -= expensiveCost;
                    countryList[selectedCountry].DoAction(Crisis.actions.expensive);
                }              
                break;

            case Crisis.actions.cheap:
                if (money >= cheapCost)
                {
                    money -= cheapCost;
                    countryList[selectedCountry].DoAction(Crisis.actions.cheap);
                }
                break;

            case Crisis.actions.nothing:
                countryList[selectedCountry].DoAction(Crisis.actions.nothing);
                break;
        }
    }

    public void UpdateTension() {
        float newGT = 0;

        foreach (Country country in countryList)
        {
            newGT += country.localTension;
        }

        gt = newGT;
    }

    

}
