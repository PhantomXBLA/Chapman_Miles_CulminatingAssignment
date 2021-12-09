using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{

    MonsterDatabase Mourntooth;
    MonsterDatabase ScendoOpponent;

    public EncounterUI encounterUI;
    public AICharacter aiCharacter;
    public MasterScendoDatabase scendoDatabase;

    public HealthBarScript playerHealthbar;
    public HealthBarScript enemyHealthbar;

    AudioController audioController;

    string assignEncounter;


    public bool playerFaster; // true = player Scendo is faster -> false = enemy Scendo is faster
    // Start is called before the first frame update
    void Start()
    {

        audioController = GameObject.Find("BattleAudioController").GetComponent<AudioController>();
        Mourntooth = GameObject.Find("Mourntooth").GetComponent<EncounterPlayerCharacter>().Mourntooth;

        playerHealthbar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBarScript>();
        enemyHealthbar = GameObject.Find("EnemyHealthBar").GetComponent<HealthBarScript>();


        if (PlayerPrefs.GetInt("EncounterCheck") == 0)
        {
            assignEncounter = PlayerPrefs.GetString("RandomEncounter");
            //aiCharacter.ScendoMonster
            
            if(assignEncounter == "Parchpaw")
            {
                aiCharacter.ScendoMonster = scendoDatabase.Parchpaw;
            }

            else if (assignEncounter == "Dampurr")
            {
                aiCharacter.ScendoMonster = scendoDatabase.Dampurr;
            }

            aiCharacter.loadScendo();
            playerHealthbar.LoadHealthBar();
            enemyHealthbar.LoadHealthBar();
            audioController.PlayWildTheme();
        }

        ScendoOpponent = GameObject.Find("Enemy").GetComponent<AICharacter>().ScendoMonster;

        if (Mourntooth.Speed > ScendoOpponent.Speed)
        {
            playerFaster = true;
        }
        else
        {
            playerFaster = false;
        }


    }

    public void TurnStart(int move, string moveName)
    {
        if(playerFaster == true)
        {
            StartCoroutine(encounterUI.DoAttack(move, moveName));
            
        }
        else if(playerFaster == false)
        {
            StartCoroutine(aiCharacter.DelayDecisionBetter());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int TakeDamage(Ability move, MonsterDatabase scendoAttacking, MonsterDatabase scendoReceiving)
    {

        //Using the attack formula from the actual Pokemon games
        //https://bulbapedia.bulbagarden.net/wiki/Damage

        float damage;

        float superEffectiveMultiplier = 1;


        int criticalHitMultiplier;
        int criticalHitOdds = Random.Range(1, 11);

        int chanceToMiss;
        int accuracyCheck = Random.Range(1, 101);

        float typeMultiplier;

        if (criticalHitOdds <= 1)
        {
            criticalHitMultiplier = 2;
            Debug.Log("critical hit");
        }
        else
        {
            criticalHitMultiplier = 1;
        }

        if(move.type == scendoAttacking.ScendoType)
        {
            typeMultiplier = 1.5f;
        }
        else
        {
            typeMultiplier = 1;
        }

        TypeEffectiveness result = checkTypeEffectiveness(move, scendoReceiving);

        if (result == TypeEffectiveness.NOTVERYEFFECTIVE)
        {
            superEffectiveMultiplier = 0.5f;
            Debug.Log("not very effective");
        }

        else if (result == TypeEffectiveness.SUPEREFFECTIVE)
        {
            superEffectiveMultiplier = 2f;
            Debug.Log("super effective");
        }
        else if (result == TypeEffectiveness.NEUTRAL)
        {
            superEffectiveMultiplier = 1f;
            Debug.Log("neutral");
        }

        damage = scendoAttacking.Level * 2 / 5 + 2 * move.damage * scendoAttacking.Attack / scendoReceiving.Defense / 50 + 2 * Random.Range(0.85f, 1.01f) * criticalHitMultiplier * superEffectiveMultiplier * typeMultiplier;

        if (damage < 1)
        {
            damage = 1;
        }

        chanceToMiss = 100 - move.accuracy;

        if(accuracyCheck <= chanceToMiss)
        {
            damage = 0;
            Debug.Log("the attack missed " + "rolled number: " + accuracyCheck);
        }


        Debug.Log("move base power: " + move.damage +" damage to deal: " + damage);
        int damageAsInt = Mathf.FloorToInt(damage);
        //Debug.Log("damage received: " + damageAsInt);
        return damageAsInt;

        
    }

    public int HealHealth(Ability move, MonsterDatabase scendoUsing)
    {
        float healAmount = scendoUsing.TotalHp * 0.5f;
        

        int healAmountAsInt = Mathf.FloorToInt(healAmount);
        return healAmountAsInt;
    }

        public TypeEffectiveness checkTypeEffectiveness(Ability move, MonsterDatabase scendo)
    {
        
        TypeEffectiveness typeEffectiveness = TypeEffectiveness.NEUTRAL;

        if (move.type == MonsterType.NORMAL)
        {
            typeEffectiveness = TypeEffectiveness.NEUTRAL;
            return typeEffectiveness;
        }



        //FIRE MOVE CHECK
        else if (move.type == MonsterType.FIRE && scendo.ScendoType == MonsterType.GRASS)
        {
            typeEffectiveness = TypeEffectiveness.SUPEREFFECTIVE;
            return typeEffectiveness;
        }

        else if (move.type == MonsterType.FIRE && scendo.ScendoType == MonsterType.WATER)
        {
            typeEffectiveness = TypeEffectiveness.NOTVERYEFFECTIVE;
            return typeEffectiveness;
        }
        else if (move.type == MonsterType.FIRE && scendo.ScendoType == MonsterType.GRASS)
        {
            typeEffectiveness = TypeEffectiveness.NOTVERYEFFECTIVE;
            return typeEffectiveness;
        }




        //GRASS MOVE CHECK
        else if (move.type == MonsterType.GRASS && scendo.ScendoType == MonsterType.FIRE)
        {
            typeEffectiveness = TypeEffectiveness.NOTVERYEFFECTIVE;
            return typeEffectiveness;
        }

        else if (move.type == MonsterType.GRASS && scendo.ScendoType == MonsterType.WATER)
        {
            typeEffectiveness = TypeEffectiveness.SUPEREFFECTIVE;
            return typeEffectiveness;
        }
        else if (move.type == MonsterType.GRASS && scendo.ScendoType == MonsterType.GRASS)
        {
            typeEffectiveness = TypeEffectiveness.NOTVERYEFFECTIVE;
            return typeEffectiveness;
        }





        //WATER MOVE CHECK
        else if (move.type == MonsterType.WATER && scendo.ScendoType == MonsterType.FIRE)
        {
            typeEffectiveness = TypeEffectiveness.SUPEREFFECTIVE;
            return typeEffectiveness;
        }

        else if (move.type == MonsterType.WATER && scendo.ScendoType == MonsterType.WATER)
        {
            typeEffectiveness = TypeEffectiveness.NOTVERYEFFECTIVE;
            return typeEffectiveness;
        }
        else if (move.type == MonsterType.WATER && scendo.ScendoType == MonsterType.GRASS)
        {
            typeEffectiveness = TypeEffectiveness.NOTVERYEFFECTIVE;
            return typeEffectiveness;
        }



        return typeEffectiveness;
    }

    

}

public enum TypeEffectiveness : int
{
    NOEFFECT = 1,
    NOTVERYEFFECTIVE = 2,
    NEUTRAL = 3,
    SUPEREFFECTIVE = 4
}
