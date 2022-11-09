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
            StartCoroutine(InitiateStateChange(targetState));
        }
    }

    //start the state change if it is a new state and we are not currently transitioning
    private IEnumerator InitiateStateChange(State targetState) {
        if(currentState != targetState) {
            //wait until not transitioning, then transition to target state
            yield return new WaitUntil(() => !InTransition);
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