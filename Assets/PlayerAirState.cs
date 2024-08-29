using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        player.SetVelocity(0, 0);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
