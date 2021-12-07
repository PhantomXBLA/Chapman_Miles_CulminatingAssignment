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
    GameObject fightButton;

    [SerializeField]
    float timeBetweenCharacters = 0.1f;

    public BattleManager battleManager;
    GameObject PlayerHealthBar;

    public int chosenMove;
    public string chosenMoveName;


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

        GameObject[] allButtons = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allButtons)
        {
            if (go.name == "FightButton")
            {
                fightButton = go;
            }
        }

        fightButton.GetComponent<Button>().onClick.AddListener(OnFightButtonPressed);

        abilityPanel.SetActive(false);
        PlayerHealthBar = GameObject.Find("PlayerHealthBar");
        player.GetComponent<EncounterPlayerCharacter>().Mourntooth.CurrentHp = player.GetComponent<EncounterPlayerCharacter>().Mourntooth.TotalHp;


    }

    void EnablePlayerUI(ICharacter characterTurn)
    {
        mainPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        mainPanel.SetActive(false);
        abilityPanel.SetActive(false);
    }




    ////---------------------------------------------------------------------- Panel Control via MAIN BUTTONS --------------------------------------------------
    public void OnFightButtonPressed()
    {
        StopCoroutine((animateTextCoroutineRef));
        animateTextCoroutineRef = AnimateTextCoroutine("What will " + player.GetComponent<EncounterPlayerCharacter>().Mourntooth.name + " do?");
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

        move1.GetComponent<Button>().onClick.RemoveAllListeners();
        move1.GetComponent<Button>().onClick.AddListener(delegate{OnAttackButtonPressed(0,move1.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text);});

        move2.GetComponent<Button>().onClick.RemoveAllListeners();
        move2.GetComponent<Button>().onClick.AddListener(delegate{OnAttackButtonPressed(0,move2.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text);});

       fightButton.GetComponent<Button>().onClick.RemoveAllListeners();

    }

    //----------------------------------------------------------------------- Panel Control via ABILITY BUTTONS --------------------------------------------------

        public void OnAttackButtonPressed(int move, string moveName)
    {
        //StopCoroutine((animateTextCoroutineRef));
        //player.GetComponent<EncounterPlayerCharacter>().UseAbility(move);
        //animateTextCoroutineRef = AnimateTextCoroutine(characterTurn.name + " used " + moveName + "!");
        //enemy.GetComponent<AICharacter>().TakeDamage(player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[move].damage);
        //abilityPanel.SetActive(false);
        //mainPanel.SetActive(false);
        //StartCoroutine(animateTextCoroutineRef);

        //StartCoroutine(DelayNextTurn());

        //play slap animation here



        chosenMove = move;
        chosenMoveName = moveName;
        battleManager.TurnStart(move, moveName);

        Debug.Log("button pressed");

    }

    public IEnumerator DoAttack(int move, string moveName)
    {
        StopCoroutine(animateTextCoroutineRef);
        animateTextCoroutineRef = AnimateTextCoroutine(player.GetComponent<EncounterPlayerCharacter>().Mourntooth.name + " used " + moveName + "!");
        enemy.GetComponent<AICharacter>().TakeDamage(player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[move].damage);
        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);
        StartCoroutine(animateTextCoroutineRef);

        if (battleManager.playerFaster == true)
        {
            yield return new WaitForSeconds(2.0f);
            //start opponents turn
            StartCoroutine(enemy.GetComponent<AICharacter>().DelayDecisionBetter());
        }

        else
        {
            yield return new WaitForSeconds(2.0f);
            mainPanel.SetActive(true);
            //reset panel
            ResetTurn();
        }
    }

    public void ResetTurn()
    {
        fightButton.GetComponent<Button>().onClick.RemoveListener(OnFightButtonPressed);
        fightButton.GetComponent<Button>().onClick.AddListener(OnFightButtonPressed);
        Debug.Log("----------TURN RESET----------");

        animateTextCoroutineRef = AnimateTextCoroutine("What will " + player.GetComponent<EncounterPlayerCharacter>().Mourntooth.name + " do?");
        StartCoroutine(animateTextCoroutineRef);
    }

    public void TakeDamage(int damageRecieved)
    {
        player.GetComponent<EncounterPlayerCharacter>().Mourntooth.CurrentHp -= damageRecieved;
        Debug.Log("damage taken: " + damageRecieved);
        Debug.Log("HP remaining: " + player.GetComponent<EncounterPlayerCharacter>().Mourntooth.CurrentHp);
        PlayerHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
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
