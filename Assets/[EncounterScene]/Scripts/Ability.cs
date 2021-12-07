using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    public new string name;

    [SerializeField]
    public int damage;

    [SerializeField]
    public int accuracy;

    [SerializeField]
    private string description;

    [SerializeField]
    public IEffect[] effects;


    public void Cast(ICharacter self, ICharacter other)
    {
        Debug.Log("Used: " + name);
    }


}
