using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void TakeTurn(EncounterInstance encounter)
    {
        StartCoroutine(DelayDecision(encounter));
    }

    IEnumerator DelayDecision(EncounterInstance encounter)
    {
        //Choose what action to do
        //Cast some ability

        Debug.Log("Enemy taking turn");
        yield return new WaitForSeconds(5.0f);
        encounter.AdvanceTurns();
    }
}
