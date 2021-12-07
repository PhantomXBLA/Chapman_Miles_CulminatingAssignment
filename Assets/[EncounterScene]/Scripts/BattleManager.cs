using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    MonsterDatabase Mourntooth;
    MonsterDatabase ScendoOpponent;

    public EncounterUI encounterUI;
    public AICharacter aiCharacter;

    public bool playerFaster; // 0 = player turn -> 1 = enemy turn
    // Start is called before the first frame update
    void Start()
    {
        Mourntooth = GameObject.Find("Mourntooth").GetComponent<EncounterPlayerCharacter>().Mourntooth;
        ScendoOpponent = GameObject.Find("Enemy").GetComponent<AICharacter>().ScendoMonster;

        if(Mourntooth.Speed > ScendoOpponent.Speed)
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
