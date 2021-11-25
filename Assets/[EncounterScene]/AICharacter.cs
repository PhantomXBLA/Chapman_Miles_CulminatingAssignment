using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{
    [SerializeField]
    private EncounterPlayerCharacter opponent;
    [SerializeField]
    private EncounterInstance myEncounter;

    public override void TakeTurn(EncounterInstance encounter)
    {
        StartCoroutine(DelayDecision(encounter));
    }

    public void UseAbility(int slot)
    {
        abilities[slot].Cast(this, opponent);
        myEncounter.AdvanceTurns();
    }


    IEnumerator DelayDecision(EncounterInstance encounter)
    {
        //Choose what action to do
        //Cast some ability

        myEncounter = encounter;
        opponent = encounter.Player;

        //if (Random.Range(1, 3) <= 1)
        //{
            
        //}

        Debug.Log("Enemy taking turn");
        yield return new WaitForSeconds(5.0f);
        encounter.AdvanceTurns();

    }
}
