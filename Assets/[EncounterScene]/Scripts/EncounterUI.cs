using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    GameObject AIHealthBar;

    public Image blackFade;

    //These are buttons
    GameObject move1, move2;
    GameObject fightButton;
    GameObject runButton;


    [SerializeField]
    float timeBetweenCharacters = 0.1f;

    public BattleManager battleManager;
    GameObject PlayerHealthBar;

    public int chosenMove;
    public string chosenMoveName;

    public MonsterDatabase Mourntooth;
    AttackAnimationController attackAnimController;

    Vector2 moveLocation;


    //This is a reference to keep track of our coroutine
    private IEnumerator animateTextCoroutineRef = null;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayAndFadeFromBlack());

        //For fading from black on stage opening
        blackFade.canvasRenderer.SetAlpha(1.0f);

        // For Fading to black
        blackFade.canvasRenderer.SetAlpha(0.0f);

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
            else if(go.name == "RunButton")
            {
                runButton = go;
            }
        }

        fightButton.GetComponent<Button>().onClick.AddListener(OnFightButtonPressed);
        runButton.GetComponent<Button>().onClick.AddListener(OnRunButtonPressed);

        abilityPanel.SetActive(false);
        PlayerHealthBar = GameObject.Find("PlayerHealthBar");
        AIHealthBar = GameObject.Find("EnemyHealthBar");
        player.GetComponent<EncounterPlayerCharacter>().Mourntooth.CurrentHp = player.GetComponent<EncounterPlayerCharacter>().Mourntooth.TotalHp;

        Mourntooth = player.GetComponent<EncounterPlayerCharacter>().Mourntooth;
        attackAnimController = GameObject.Find("AttackAnimationController").GetComponent<AttackAnimationController>();



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
        move2.GetComponent<Button>().onClick.AddListener(delegate{OnAttackButtonPressed(1,move2.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text);});

       fightButton.GetComponent<Button>().onClick.RemoveAllListeners();

    }

    //----------------------------------------------------------------------- Panel Control via ABILITY BUTTONS --------------------------------------------------

    public void OnAttackButtonPressed(int move, string moveName)
    {
        chosenMove = move;
        chosenMoveName = moveName;
        battleManager.TurnStart(move, moveName);
        Debug.Log("button pressed");
    }

    public void OnRunButtonPressed()
    {
        StartCoroutine(Run());
    }

    public IEnumerator DoAttack(int move, string moveName)
    {
        StopCoroutine(animateTextCoroutineRef);
        animateTextCoroutineRef = AnimateTextCoroutine(player.GetComponent<EncounterPlayerCharacter>().Mourntooth.name + " used " + moveName + "!");
        //enemy.GetComponent<AICharacter>().TakeDamage(player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[move], player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[move].damage);

        int damageToDeal = battleManager.TakeDamage(Mourntooth.MonsterAbilities[move], Mourntooth, enemy.GetComponent<AICharacter>().ScendoMonster);
        enemy.GetComponent<AICharacter>().TakeDamage(damageToDeal);

        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);
        StartCoroutine(animateTextCoroutineRef);

        attackAnimController.OnAttackAnim(Mourntooth.MonsterAbilities[move], AIHealthBar.GetComponent<HealthBarScript>());

        if (battleManager.playerFaster == true)
        {
            yield return new WaitForSeconds(4.0f);
            //start opponents turn
            StartCoroutine(enemy.GetComponent<AICharacter>().DelayDecisionBetter());
        }

        else
        {
            yield return new WaitForSeconds(4.0f);
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
        Mourntooth.CurrentHp -= damageRecieved;
        //PlayerHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
    }


    public void fadeToBlack()
    {
        //This is changing the FadeIn/Out image to 1 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(1, 2.0f, false);
        Debug.Log("Fading from encounter...");

    }

    public void fadeFromBlack()
    {
        //This is changing the FadeIn/Out image to 0 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(0, 2.0f, false);
        Debug.Log("Fading into encounter...");
    }

    IEnumerator DelayAndFadeFromBlack()
    {
        fadeFromBlack();
        yield return new WaitForSeconds(1.6f);
    }

    IEnumerator Run()
    {
        animateTextCoroutineRef = AnimateTextCoroutine("Got away safely!");
        StartCoroutine(animateTextCoroutineRef);
        yield return new WaitForSeconds(1.0f);
        fadeToBlack();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Overworld");
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
