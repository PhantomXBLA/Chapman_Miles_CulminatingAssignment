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



}
