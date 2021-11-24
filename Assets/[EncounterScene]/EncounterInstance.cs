using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncounterInstance : MonoBehaviour
{
    [SerializeField]
    private EncounterPlayerCharacter player;
    [SerializeField]
    private AICharacter enemy;
    
    public AICharacter Enemy
    {
        get {return enemy;}
        private set { enemy = value; } 
    }

    //Events
    public UnityEvent<ICharacter> onCharacterTurnBegin;
    public UnityEvent<ICharacter> onCharacterTurnEnd;
    public UnityEvent<EncounterPlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<EncounterPlayerCharacter> onPlayerTurnEnd;
    public UnityEvent<AICharacter> onEnemyTurnBegin;
    public UnityEvent<AICharacter> onEnemyTurnEnd;


    //Turn Counter
    private int turnNumber = 0;

    [SerializeField]
    private ICharacter currentCharacterTurn;


    // Start is called before the first frame update
    void Start()
    {
        currentCharacterTurn = player;
        onPlayerTurnBegin.Invoke(player);
    }


    public void AdvanceTurns()
    {
        onCharacterTurnEnd.Invoke(currentCharacterTurn);

        if (currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = enemy;

        }
        else
        {
            
            currentCharacterTurn = player;
            onPlayerTurnBegin.Invoke(player);
        }
        turnNumber++;

        onCharacterTurnBegin.Invoke(currentCharacterTurn);
        currentCharacterTurn.TakeTurn(this);


    }

}
