using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject {

    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterSprite;
	[SerializeField] private Sprite classSymbol;
    [SerializeField] private float maxHealth;
    [SerializeField, Range(0, 100)] private float attackStat;
    [SerializeField, Range(0, 100)] private float defenseStat;
    [SerializeField, Range(0, 100)] private float healingStat;
    [SerializeField] private AbilityBase ability1;
    [SerializeField] private AbilityBase ability2;
    [SerializeField] private DamagingAbility basicAttack;
    [SerializeField] private StatusAbility basicDodge;

    public string Name => characterName;
    public Sprite Sprite => characterSprite;
	public Sprite ClassSymbol => classSymbol;
    public float MaxHealth => maxHealth;
    public float AttackStat => attackStat;
    public float DefenseStat => defenseStat;
    public float HealingStat => healingStat;
    public AbilityBase Ability1 => ability1;
    public AbilityBase Ability2 => ability2;
    public DamagingAbility BasicAttack => basicAttack;
    public StatusAbility BasicDodge => basicDodge;
}