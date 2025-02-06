using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine) // Constructor, takes the reference to the state machine
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime) //use this when the object only affected by the outside force, like when Idle but got hit by the player
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime) // use this when the object is moving by itself
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected bool IsInChaseRange() // Checks if the player is in the chase range
    {
        if(stateMachine.Player.IsDead) { return false; } // If the player is dead, return false

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange; // Returns true if the player is in the chase range
    }

    protected void FaceToPlayer() // Makes the enemy face the player
    {
        if (stateMachine.Player == null) { return; }
        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;

        lookPos.y = 0; //dont care about y axis
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

}
