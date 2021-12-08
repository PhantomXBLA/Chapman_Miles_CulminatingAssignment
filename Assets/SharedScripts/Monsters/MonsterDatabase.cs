using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idea for ScriptableObjects from Game Engines III lab with Joss

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Create Monster")]
public class MonsterDatabase : ScriptableObject
{


    [SerializeField]
    string monsterName;

    [SerializeField]
    int level;

    [TextArea]
    [SerializeField]
    string description;

    [SerializeField]
    MonsterType type;

    [SerializeField]
    int totalHp;

    [SerializeField]
    int currentHp;

    [SerializeField]
    int attack;

    [SerializeField]
    int defense;

    [SerializeField]
    int speed; //decides who attacks first

    [SerializeField]
    Ability[] monsterAbilities;

    //Still need to create sprites
    //May only need from sprite for enemy monsters
    [SerializeField]
    Sprite frontSprite;

    [SerializeField]
    Sprite backSprite;


    //C# properties. Similar to using a Getter function
    //Need monster name available outside of the class
    public string MonsterName
    {
        get { return monsterName; } 
    }

    public string Description
    {
        get { return description; }
    }

    public int TotalHp
    {
        get { return totalHp; }
    }

    public int CurrentHp { get; set; }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public int Level
    {
        get { return level; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public Ability[] MonsterAbilities
    {
        get { return monsterAbilities; }
    }

    public MonsterType ScendoType
    {
        get { return type; }
    }

}





//Idea for enum from Mobile Game Development lab with Tom Tsiliopolus
public enum MonsterType
{
    NONE,
    WATER,
    FIRE,
    GRASS,
    NORMAL
    // can add other types as needed
}
