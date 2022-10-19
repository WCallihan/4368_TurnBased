using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour {

    private State currentState;
    public State CurrentState => currentState;

    protected bool InTransition { get; private set; }
    protected State previousState;

    private void Update() {
        //make Tick simulate Update
        if(currentState != null && !InTransition) {
            currentState.Tick();
        }
    }

    //make sure that there is a state to change to, and then initiate the change
    public void ChangeState<T>() where T : State {
        T targetState = GetComponent<T>();
        if(targetState == null) {
            Debug.LogWarning($"Cannot change to state because it does not exist on {gameObject.name}");
        } else {
            InitiateStateChange(targetState);
        }
    }

    //start the state change if it is a new state and we are no current transitioning
    private void InitiateStateChange(State targetState) {
        if(currentState != targetState && !InTransition) {
            Transition(targetState);
        }
    }

    //reverts back to the previous state if possible
    public void RevertState() {
        if (previousState != null) InitiateStateChange(previousState);
    }

    //transitions to the specified game state
    private void Transition(State newState) {
        //set transitioning flag
        InTransition = true;
        //transition
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
        //un-set transition flag
        InTransition = false;
    }
}