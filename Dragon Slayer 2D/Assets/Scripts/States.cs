using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{

    public State currentState;

    public void SetState(State state)
    {
        this.currentState = state;
    }

    public State GetState()
    {
        return this.currentState;
    }

    public enum State { IDLE, WALKING, JUMPING, ATTACKING, CLIMBING, JUMP_ATTACK }

}
