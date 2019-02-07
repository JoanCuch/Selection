using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crisis : MonoBehaviour {

  

    public enum type { food, water, space, epidemia}
    public type myType;
    public int crisisNumber;

    public enum actions { expensive, cheap, nothing}

    public Country myCountry;
    GameManager gameManager;


    [HideInInspector] public string[] InfoText = {
        "is on crisis! His farmlands hasn't produced enought to sustent all his population! There isn't enogh food to keep every citizen alive!",
        "is on crisis! In his rivers don't flow clean water, his dams are empty and his reserves of potable water are in critical level!",
        "is on crisis! There are too many people, and they have no more available space to build more houses. Their citizens are living in terrible conditions!",
        "is on crisis! There is an epidemic! Thousands of their citizens are dying. The corpses are being piled up in the streets! That's terrible!"
    };

    [HideInInspector] public string[] ExpensiveText = {
        "Oh, no! Send them ships and planes full of food!",
        "Oh, no! Send them ships and planes full of clean water!",
        "Oh, no! Send them expert builder teams!",
        "Oh, no! Create a full quarantine with expert doctors!"
    };
    [HideInInspector] public string[] CheapText = {
        "Well, Send them some ships with food",
        "Well, Send them some ships with water",
        "Well, Send them some money",
        "Well, Send them some doctors",
    };

    [HideInInspector] public string[] NothingText = {
        "Who cares? They are only one little country",
        "Who cares? They are only one little country",
        "Who cares? They are only one little country",
        "Who cares? They are only one little country",
    };


    // Use this for initialization
    void Start () {

        gameManager = GameManager.gameManager;
        Debug.Log("crisis number: " + crisisNumber);
        crisisNumber = Random.Range(1, 5);
        switch (crisisNumber)
        {
            case 1: myType = type.food; break;
            case 2: myType = type.water; break;
            case 3: myType = type.space; break;
            case 4: myType = type.epidemia; break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoAction(actions action)
    {
        switch(action) {
            case actions.expensive:
                EndCrisis();
                break;

            case actions.cheap:
                myCountry.localTension += 0.2f;
                GameManager.gameManager.UpdateTension();                
                EndCrisis();
                break;

            case actions.nothing:
                Debug.Log("nothing");
                myCountry.localTension += 3f;
                GameManager.gameManager.UpdateTension();

                int random = Random.Range(1, 5);
                switch (random)
                {
                    case 1:
                        EndCrisis();
                        break;

                    case 4:
                        //Debug.Log("super crisis");
                        //myCountry.localTension += 10;
                        EndCrisis();
                        break;

                    default:
                        break;
                }
                break;
        }

    }


    void EndCrisis()
    {
        myCountry.EndCrisis();
    }

    public void SetNumber()
    {

        crisisNumber = Random.Range(0, 4);
        switch (crisisNumber)
        {
            case 1: myType = type.food; break;
            case 2: myType = type.water; break;
            case 3: myType = type.space; break;
            case 0: myType = type.epidemia; break;
        }
    }


    public string ReturnExpensiveText() { return ExpensiveText[crisisNumber]; }
    public string ReturnCheapText() { return CheapText[crisisNumber]; }
    public string ReturnNothingText() { return NothingText[crisisNumber]; }
    public string ReturnInfoText() { return InfoText[crisisNumber]; }


}
