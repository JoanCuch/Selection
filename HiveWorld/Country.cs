using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Country : MonoBehaviour {

    GameManager gameManager;

    public int countryNumber;

    public string[] countryNames = { "Canada", "EUA", "Mexico", "Venezuela", "Argentina", "Spain", "Morrocco", "Congo", "South Africa", "Poland", "Saudi Arabia", "India", "Russia", "China", "Australia" };
    public string countryName;

    public int localPopulation;
    public float localTension;

    public int crisisProvability;
    public bool inCrisis;
    [HideInInspector]public Crisis crisis;

    public GameObject[] myButton;

	// Use this for initialization
	void Start () {

        localPopulation = 666;

        inCrisis = false;
        gameManager = GameManager.gameManager;
	}
	
	// Update is called once per frame
	void Update () {

     

	}

    public void NewYearNewEvents()
    {
        if (inCrisis)
        {
            DoAction(Crisis.actions.nothing);
        }
        else 
        {
            //if (localTension > 0) localTension -= 0.1f;
            if (localTension < 0) localTension = 0;

            int alea = Random.Range(1, 5);
            if (alea == 1)
            {
                AddCrisis();
            }
        }


        foreach (GameObject button in myButton)
        {
            button.SetActive(inCrisis);
        }
        

    }

    public void SetSelected() {
        gameManager.SetSelected(countryNumber);
        countryName = countryNames[countryNumber];
    }

    #region crisis

    public void AddCrisis()
    {
        inCrisis = true;
        crisis = new Crisis();
        crisis.SetNumber();
        crisis.myCountry = this;
        
    }

    public void DoAction(Crisis.actions action)
    {
        switch (action)
        {
            case Crisis.actions.expensive:
                crisis.DoAction(Crisis.actions.expensive);
                break;

            case Crisis.actions.cheap:
                crisis.DoAction(Crisis.actions.cheap);
                break;

            case Crisis.actions.nothing:
                crisis.DoAction(Crisis.actions.nothing);
                break;
        }
    }

    public void EndCrisis() {
        Destroy(crisis);
        inCrisis = false;

        foreach (GameObject button in myButton)
        {
            button.SetActive(inCrisis);
        }

        gameManager.isSelected = false;
    }

    #endregion


}
