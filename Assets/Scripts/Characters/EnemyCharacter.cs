using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharacterControllerBase {

    private bool grappled = false;

    public bool Grappled => grappled;

    private void OnEnable() {
        PlayerTurnState.PlayerTurnsStarted += ResetGrappled;
    }

    private void OnDisable() {
        PlayerTurnState.PlayerTurnsStarted -= ResetGrappled;
    }

    public void SetGrappled(bool grapple) {
        grappled = grapple;
        //TODO: update UI
    }

    //reset the grappled status at the beginning of the player's turns
    private void ResetGrappled() {
        SetGrappled(false);
    }
}