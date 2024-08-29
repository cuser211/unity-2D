using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow=0.4f;
    private float attackDirection;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .1f;
        if (comboCounter > 2|| Time.time>=lastTimeAttacked+comboWindow)
            comboCounter = 0;
        player.anim.SetInteger("comboCounter", comboCounter);
        attackDirection = player.facingdirection;
        if(xInput!=0)
            attackDirection= xInput;

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y);
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
        player.StartCoroutine("BusyFor", .15f);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        player.SetVelocity(0, 0);
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
