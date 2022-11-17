using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour {

    [Header("Player Characters")]
    [SerializeField] private PlayerCharacter topCharacter;
    [SerializeField] private PlayerCharacter middleCharacter;
    [SerializeField] private PlayerCharacter bottomCharacter;

    [Header("Enemy Character Objects")]
    [SerializeField] private List<EnemyCharacter> enemyCharacters;
    [SerializeField] private List<CharacterData> enemyDatas;

    private PlayerCharacter[] orderedPlayerControllers;

    public static event Action ConfirmedSelection;

    private void Awake() {
        orderedPlayerControllers = new PlayerCharacter[] { topCharacter, middleCharacter, bottomCharacter };
    }

    //toggle whether the character is selected depending on the color of their blackout panel
    public void ToggleCharacterSelection(CharacterData data, GameObject panel) {
        if(!panel.activeInHierarchy) {
            AssignCharacter(data, panel);
        } else {
            UnassignCharacter(data, panel);
        }
    }

    //assign the character to the first available character controller
    private void AssignCharacter(CharacterData data, GameObject panel) {
        foreach (var c in orderedPlayerControllers) {
            if (c.CharData == null) {
                c.AssignCharacter(data);
                panel.SetActive(true);
                return;
            }
        }
    }

    //unassign the character from its currently assigned character controller
    private void UnassignCharacter(CharacterData data, GameObject panel) {
        foreach (var c in orderedPlayerControllers) {
            if(c.CharData == data) {
                c.UnassignCharacter();
                panel.SetActive(false);
                return;
            }
        }
    }

    //called by the Confirm button and triggers the end of the setup state
    public void ConfirmSelection() {
        //check that three characters are selected
        foreach (var c in orderedPlayerControllers) {
            if(c.CharData == null) {
                return;
            }
        }

        topCharacter.SetBaseHitChance(100);
        middleCharacter.SetBaseHitChance(150); //middle character's hit chance is larger by 50%
        bottomCharacter.SetBaseHitChance(100);

        //randomly assign enemy characters
        List<CharacterData> enemyDataCopy = enemyDatas;
        foreach (var e in enemyCharacters) {
            int randInd = UnityEngine.Random.Range(0, enemyDataCopy.Count);
            e.AssignCharacter(enemyDataCopy[randInd]);
            enemyDataCopy.RemoveAt(randInd);
        }

        ConfirmedSelection?.Invoke();
    }
}