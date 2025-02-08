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

    protected bool IsInChaseRange()
    {
        if (stateMachine.Player == null)
        {
            Debug.LogError("EnemyBaseState: Player reference is NULL!");
            return false;
        }

        if (stateMachine.Player.IsDead)
        {
            Debug.Log("EnemyBaseState: Player is dead. Not chasing.");
            return false;
        }

        if (stateMachine == null)
        {
            Debug.LogError("EnemyBaseState: StateMachine reference is NULL!");
            return false;
        }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
       // Debug.Log($"EnemyBaseState: Distance to player = {playerDistanceSqr}, Chasing Range = {stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange}");

        return playerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }


    protected void FaceToPlayer() // Makes the enemy face the player
    {
        if (stateMachine.Player == null) { return; }
        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;

        lookPos.y = 0; //dont care about y axis
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

}
