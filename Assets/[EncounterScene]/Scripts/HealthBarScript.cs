using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour
{
    MonsterDatabase monster;

    TMPro.TextMeshPro monsterName;
    TMPro.TextMeshPro monsterLevel;
    TMPro.TextMeshPro monsterHP;
    GameObject HealthBarSprite;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadHealthBar()
    {
        monsterName = this.gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshPro>();
        monsterLevel = this.gameObject.transform.GetChild(2).GetComponent<TMPro.TextMeshPro>();
        HealthBarSprite = this.gameObject.transform.GetChild(3).gameObject;
        monsterHP = this.gameObject.transform.GetChild(4).GetComponent<TMPro.TextMeshPro>();

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
        monsterHP.text = "HP: " + monster.CurrentHp + " / " + monster.TotalHp;


    }

    public void UpdateHealthBar()
    {
        float hpBarXFinal = (float)monster.CurrentHp / (float)monster.TotalHp;

        HealthBarSprite.transform.localScale = new Vector2(hpBarXFinal, 1);

        if(HealthBarSprite.transform.localScale.x <= 0)
        {
            HealthBarSprite.transform.localScale = new Vector2(0, 1);
        }

        if (hpBarXFinal <= 0.5 && hpBarXFinal >= 0.26)
        {
            HealthBarSprite.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        
        else if(hpBarXFinal <= 0.25)
        {
            HealthBarSprite.GetComponent<SpriteRenderer>().color = Color.red;
        }

        monsterHP.text = "HP: " + monster.CurrentHp + " / " + monster.TotalHp;
    }
}
