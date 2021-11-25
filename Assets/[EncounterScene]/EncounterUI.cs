using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will prompt the user what to do, and enable/disable UI elements at different times
public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    EncounterInstance encounter;

    [SerializeField]
    EncounterPlayerCharacter attack;

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    private GameObject abilityPanel;

    [SerializeField]
    private GameObject fightPanel;



    [SerializeField]
    float timeBetweenCharacters = 0.1f;


    //This is a reference to keep track of our coroutine
    private IEnumerator animateTextCoroutineRef = null;
    
    // Start is called before the first frame update
    void Start()
    { 

        animateTextCoroutineRef = AnimateTextCoroutine("You have encountered an opponent!");
        //Animate some text to say what you encountered
        StartCoroutine(animateTextCoroutineRef);


        //StopCoroutine((animateTextCoroutineRef));
        //encounter.onCharacterTurnBegin.AddListener(AnnounceCharacterTurnBegin);

        //OnCharaterTurnBegin, announce whose turn it is now
        encounter.onPlayerTurnBegin.AddListener(EnablePlayerUI);

        //On player turn end, disable UI
        encounter.onPlayerTurnEnd.AddListener(DisablePlayerUI);


        //On player turn begin, enable UI


        fightPanel.SetActive(false);


    }

    //void AnnounceCharacterTurnBegin(ICharacter characterTurn)
    //{
    //    if (animateTextCoroutineRef != null)
    //    {
    //        StopCoroutine(animateTextCoroutineRef);
    //    }

    //    animateTextCoroutineRef = AnimateTextCoroutine("It is " + characterTurn.name + "'s turn.");
    //    StartCoroutine(animateTextCoroutineRef);
    //}

    void EnablePlayerUI(ICharacter characterTurn)
    {
        abilityPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        abilityPanel.SetActive(false);
        fightPanel.SetActive(false);
    }


    public void ResetPlayerTurn(ICharacter characterTurn)
    {
        animateTextCoroutineRef = AnimateTextCoroutine("What will " + characterTurn.name + " do?");
        StartCoroutine(animateTextCoroutineRef);
    }




    ////---------------------------------------------------------------------- Panel Control via ABILITY BUTTONS --------------------------------------------------
    public void OnFightButtonPressed(ICharacter characterTurn)
    {
        animateTextCoroutineRef = AnimateTextCoroutine("What will " + characterTurn.name + " do?");
        StartCoroutine(animateTextCoroutineRef);
        fightPanel.SetActive(true);
        abilityPanel.SetActive(false);
    }

    //-------------------------------------------------------------------------- Panel Control via FIGHT BUTTONS --------------------------------------------------

    public void OnSlapButtonPressed(ICharacter characterTurn)
    {
        animateTextCoroutineRef = AnimateTextCoroutine(characterTurn.name + " used Slap!"); //need to add attack argument here so any attack can be used
        StartCoroutine(animateTextCoroutineRef);
        fightPanel.SetActive(false);

        //play slap animation here
    }



    IEnumerator AnimateTextCoroutine(string message)
    {
        encounterText.text = "";
        for (int currentCharater = 0; currentCharater < message.Length; currentCharater++)
        {
            encounterText.text += message[currentCharater];
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }


}
