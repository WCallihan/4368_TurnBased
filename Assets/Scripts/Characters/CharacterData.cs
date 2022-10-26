using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject {

    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterSprite;
    [SerializeField, Range(0, 100)] private float healthStat;
    [SerializeField, Range(0, 100)] private float damageStat;
    [SerializeField, Range(0, 100)] private float healStat;

    public string Name => characterName;
    public Sprite Sprite => characterSprite;
    public float HealthStat => healthStat;
    public float DamageStat => damageStat;
    public float HealStat => healStat;
}