using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{

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

    int moveToUse;


    GameObject AIHealthBar;
    public BattleManager battleManager;
    public EncounterUI encounterUI;

    AttackAnimationController attackAnimController;

    private void Start()
    {
        attackAnimController = GameObject.Find("AttackAnimationController").GetComponent<AttackAnimationController>();

    }

    private IEnumerator animateTextCoroutineRef = null;

    bool isAttacking = false;

    public override void TakeTurn(EncounterInstance encounter)
    {
        //myEncounter = encounter;
        //opponent = myEncounter.Player;
        //StartCoroutine(DelayDecision(myEncounter));

    }

    public void loadScendo()
    {
        this.GetComponent<SpriteRenderer>().sprite = ScendoMonster.FrontSprite;
        scendoAttacks = ScendoMonster.MonsterAbilities;
        ScendoMonster.CurrentHp = ScendoMonster.TotalHp;
        AIHealthBar = GameObject.Find("EnemyHealthBar");
    }

    public void UseAbility(int slot)
    {

        scendoAttacks[slot].Cast(this, opponent);
        //scendoAttacks[slot].Cast(this, opponent);
        //myEncounter.AdvanceTurns();

    }

    public void TakeDamage(Ability move, int damageRecieved)
    {
        TypeEffectiveness result = battleManager.checkTypeEffectiveness(move, ScendoMonster);

        Debug.Log("player used: " + move + "type: " + move.type + "damage: " + move.damage);

        if(result == TypeEffectiveness.NOTVERYEFFECTIVE)
        {
            ScendoMonster.CurrentHp -= damageRecieved /2;
            Debug.Log("not very effective damage taken: " + damageRecieved / 2);
        }

        else if (result == TypeEffectiveness.SUPEREFFECTIVE)
        {
            ScendoMonster.CurrentHp -= damageRecieved * 2;
            Debug.Log("super effective damage taken: " + damageRecieved * 2);
        }
        else if (result == TypeEffectiveness.NEUTRAL)
        {
            ScendoMonster.CurrentHp -= damageRecieved;
            Debug.Log("damage taken: " + damageRecieved);
        }

        
        
        Debug.Log("HP remaining: " + ScendoMonster.CurrentHp);
        AIHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
    
    }


    public IEnumerator DelayDecisionBetter()
    {

        Debug.Log("Enemy taking turn");

        //moveToUse = Random.Range(0, 1);
        moveToUse = 1;
       
        animateTextCoroutineRef = AnimateTextCoroutine("Enemy " + ScendoMonster.name + " used " + scendoAttacks[moveToUse].name + "!");
        encounterUI.TakeDamage(ScendoMonster.MonsterAbilities[moveToUse], ScendoMonster.MonsterAbilities[moveToUse].damage);
        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);

        StartCoroutine(animateTextCoroutineRef);
        attackAnimController.OnAttackAnim(ScendoMonster.MonsterAbilities[moveToUse]);



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
