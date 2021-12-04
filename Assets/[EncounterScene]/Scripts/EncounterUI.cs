using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Will prompt the user what to do, and enable/disable UI elements at different times
public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    EncounterInstance encounter;

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject abilityPanel;

    public GameObject player;
    public GameObject enemy;

    //These are buttons
    GameObject move1, move2;

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

        
        encounter.onEnemyTurnBegin.AddListener(DisablePlayerUI);

        
        encounter.onEnemyTurnEnd.AddListener(EnablePlayerUI);





        abilityPanel.SetActive(false);
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
        mainPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        mainPanel.SetActive(false);
        abilityPanel.SetActive(false);
    }


    //public void ResetPlayerTurn(ICharacter characterTurn)
    //{
    //    animateTextCoroutineRef = AnimateTextCoroutine("What will " + characterTurn.name + " do?");
    //}




    ////---------------------------------------------------------------------- Panel Control via MAIN BUTTONS --------------------------------------------------
    public void OnFightButtonPressed(ICharacter characterTurn)
    {
        StopCoroutine((animateTextCoroutineRef));
        animateTextCoroutineRef = AnimateTextCoroutine("What will " + characterTurn.name + " do?");
        StartCoroutine(animateTextCoroutineRef);
        abilityPanel.SetActive(true);
        mainPanel.SetActive(false);

        GameObject[] allButtons = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allButtons)
        {
            if (go.name == "Move1")
            {
                move1 = go;
                move1.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[0].name;
            }

            else if (go.name == "Move2")
            {
                move2 = go;
                move2.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[1].name;
            }
        }


        move1.GetComponent<Button>().onClick.AddListener(delegate { OnAttackButtonPressed(player.GetComponent<EncounterPlayerCharacter>(), 0, 
        move1.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text); });

        move2.GetComponent<Button>().onClick.AddListener(delegate { OnAttackButtonPressed(player.GetComponent<EncounterPlayerCharacter>(), 1, 
        move2.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text); });
    }

    //----------------------------------------------------------------------- Panel Control via ABILITY BUTTONS --------------------------------------------------

    public void OnAttackButtonPressed(ICharacter characterTurn, int move, string moveName)
    {
        StopCoroutine((animateTextCoroutineRef));
        player.GetComponent<EncounterPlayerCharacter>().UseAbility(move);
        animateTextCoroutineRef = AnimateTextCoroutine(characterTurn.name + " used " + moveName + "!");
        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);
        StartCoroutine(animateTextCoroutineRef);

        //StartCoroutine(DelayNextTurn());

        //play slap animation here



    }


    //IEnumerator DelayNextTurn()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    encounter.AdvanceTurns();
    //}


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
