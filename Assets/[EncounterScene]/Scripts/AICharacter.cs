using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{

    int health;

    public MonsterDatabase ScendoMonster;
    Ability[] scendoAttacks;


    [SerializeField]
    private EncounterPlayerCharacter opponent;
    
    [SerializeField]
    private EncounterInstance myEncounter;

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    float timeBetweenCharacters = 0.1f;

    [SerializeField]
    public GameObject enemy;

    [SerializeField]
    public GameObject mainPanel;

    [SerializeField]
    public GameObject abilityPanel;

    int moveToUse = 0;


    GameObject AIHealthBar;
    public BattleManager battleManager;
    public EncounterUI encounterUI;


    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = ScendoMonster.FrontSprite;
        scendoAttacks = ScendoMonster.MonsterAbilities;
        ScendoMonster.CurrentHp = ScendoMonster.TotalHp;
        AIHealthBar = GameObject.Find("EnemyHealthBar");
    }

    private IEnumerator animateTextCoroutineRef = null;

    bool isAttacking = false;

    public override void TakeTurn(EncounterInstance encounter)
    {
        //myEncounter = encounter;
        //opponent = myEncounter.Player;
        //StartCoroutine(DelayDecision(myEncounter));

    }

    public void UseAbility(int slot)
    {

        scendoAttacks[slot].Cast(this, opponent);
        //scendoAttacks[slot].Cast(this, opponent);
        //myEncounter.AdvanceTurns();

    }

    public void TakeDamage(int damageRecieved)
    {
        ScendoMonster.CurrentHp -= damageRecieved;
        Debug.Log("damage taken: " + damageRecieved);
        Debug.Log("HP remaining: " + ScendoMonster.CurrentHp);
        AIHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
    
    }


    IEnumerator DelayDecision(EncounterInstance encounter)
    {
        //Choose what action to do
        //Cast some ability
        myEncounter.currentCharacterTurn = myEncounter.Enemy; // new
        
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Enemy taking turn");


        //if (health <= 50 && health >=26)
        //{
        //    int moveProbability = Random.Range(1, 5);


        //    if (moveProbability == 1)
        //    {
        //        UseAbility(0);
        //        moveToUse = 0;
        //    }
        //    else if(moveProbability == 2)
        //    {
        //        UseAbility(1);
        //        moveToUse = 1;
        //    }

        //    else if (moveProbability >= 3)
        //    {
        //        UseAbility(2);
        //        moveToUse = 2;
        //        health += 50;
        //    }

        //} else if(health <= 25)
        //{
        //    int moveProbability = Random.Range(1, 10);


        //    if (moveProbability == 1)
        //    {
        //        UseAbility(0);
        //        moveToUse = 0;
        //    }
        //    else if (moveProbability == 2)
        //    {
        //        UseAbility(1);
        //        moveToUse = 1;
        //    }

        //    else if (moveProbability >= 3)
        //    {
        //        UseAbility(2);
        //        moveToUse = 2;
        //        health += 50;
        //    }
        //}

        //else if(health > 50)
        //{
        //    int moveProbability = Random.Range(1, 10);


        //    if (moveProbability <= 4)
        //    {
        //        UseAbility(0);
        //        moveToUse = 0;
        //    }
        //    else if (moveProbability <= 9 && moveProbability >=5)
        //    {
        //        UseAbility(1);
        //        moveToUse = 1;
        //    }

        //    else if (moveProbability == 10)
        //    {
        //        UseAbility(2);
        //        moveToUse = 2;
        //        health += 50;
        //    }

            UseAbility(moveToUse);
        //}



        animateTextCoroutineRef = AnimateTextCoroutine( "Enemy " + ScendoMonster.name + " used " + scendoAttacks[moveToUse].name + "!");
        
        abilityPanel.SetActive(false);
        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);
        StartCoroutine(animateTextCoroutineRef);
        yield return new WaitForSeconds(5.0f);
        mainPanel.SetActive(true);

        myEncounter.currentCharacterTurn = myEncounter.Enemy; //new



        //Debug.Log("Enemy taking turn");
        //yield return new WaitForSeconds(5.0f);
        myEncounter.AdvanceTurns(); // recomment out if no work :\


    }

    public IEnumerator DelayDecisionBetter()
    {

        Debug.Log("Enemy taking turn");

        //UseAbility(moveToUse);



       
        animateTextCoroutineRef = AnimateTextCoroutine("Enemy " + ScendoMonster.name + " used " + scendoAttacks[moveToUse].name + "!");
        encounterUI.TakeDamage(ScendoMonster.MonsterAbilities[moveToUse].damage);
        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);

        StartCoroutine(animateTextCoroutineRef);


        if (battleManager.playerFaster == false)
        {
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(encounterUI.DoAttack(encounterUI.chosenMove, encounterUI.chosenMoveName));
        }
        
        else if(battleManager.playerFaster == true)
        {
            yield return new WaitForSeconds(2.0f);
            mainPanel.SetActive(true);
            encounterUI.ResetTurn();
        }


        
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
