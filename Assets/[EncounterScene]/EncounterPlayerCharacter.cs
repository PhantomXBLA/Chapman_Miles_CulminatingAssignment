using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPlayerCharacter : ICharacter
{
    [SerializeField]
    private AICharacter opponent;
    [SerializeField]
    private EncounterInstance myEncounter;

    public override void TakeTurn(EncounterInstance encounter)
    {
        myEncounter = encounter;
        opponent = encounter.Enemy;
        Debug.Log("Player taking turn");
    }

    public void UseAbility(int slot)
    {

        abilities[slot].Cast(this, opponent);
        myEncounter.AdvanceTurns();
    }
}
