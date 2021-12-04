using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour
{
    MonsterDatabase monster;

    TMPro.TextMeshPro monsterName;
    TMPro.TextMeshPro monsterLevel;

    // Start is called before the first frame update
    void Start()
    {
        monsterName = this.gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshPro>();
        monsterLevel = this.gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshPro>();

        if (this.gameObject.name == "PlayerHealthBar")
        {
            monster = GameObject.Find("Mourntooth").GetComponent<EncounterPlayerCharacter>().Mourntooth;
            
        }

        else if (this.gameObject.name == "EnemyHealthBar")
        {
            monster = GameObject.Find("Enemy").GetComponent<AICharacter>().ScendoMonster;

        }

        monsterName.text = monster.MonsterName;
        monsterLevel.text = ("Lvl: " + monster.Level.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
