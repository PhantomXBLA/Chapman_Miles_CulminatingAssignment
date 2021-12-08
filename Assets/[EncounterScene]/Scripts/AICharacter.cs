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
    public GameObject player;

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
        player = GameObject.Find("Mourntooth");
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

    public void TakeDamage(int damageRecieved)
    {
        ScendoMonster.CurrentHp -= damageRecieved;
        AIHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
    }


    public IEnumerator DelayDecisionBetter()
    {

        Debug.Log("Enemy taking turn");

        //moveToUse = Random.Range(0, 1);
        moveToUse = 0;
       
        animateTextCoroutineRef = AnimateTextCoroutine("Enemy " + ScendoMonster.name + " used " + scendoAttacks[moveToUse].name + "!");
        //encounterUI.TakeDamage(ScendoMonster.MonsterAbilities[moveToUse], ScendoMonster.MonsterAbilities[moveToUse].damage);
        int damageToDeal = battleManager.TakeDamage(scendoAttacks[moveToUse], ScendoMonster, player.GetComponent<EncounterPlayerCharacter>().Mourntooth);
        encounterUI.TakeDamage(damageToDeal);
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
