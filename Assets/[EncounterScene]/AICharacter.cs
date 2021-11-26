using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{
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

    
    public Ability ability0, ability1;

    private IEnumerator animateTextCoroutineRef = null;

    bool isAttacking = false;

    public override void TakeTurn(EncounterInstance encounter)
    {
        StartCoroutine(DelayDecision(encounter));
        myEncounter = encounter;
        opponent = myEncounter.Player;
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

        yield return new WaitForSeconds(5.0f);
        Debug.Log("Enemy taking turn");
        animateTextCoroutineRef = AnimateTextCoroutine( "Opponent used " + ability1.name + "!");
        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);
        StartCoroutine(animateTextCoroutineRef);
        yield return new WaitForSeconds(5.0f);
        mainPanel.SetActive(true);

        

        


        //Debug.Log("Enemy taking turn");
        //yield return new WaitForSeconds(5.0f);
        //myEncounter.AdvanceTurns();


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
