using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingPlayerState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered STANDING");
        player.mCurrentState = this;
    }

    public void Execute(Player player)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // transition to ducking
            DuckingPlayerState duckingState = new DuckingPlayerState();
            duckingState.Enter(player);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // transition to ducking
            JumpingPlayerState jumpingState = new JumpingPlayerState();
            jumpingState.Enter(player);
        }
    }
}

public class DuckingPlayerState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered DUCKING");
        player.mCurrentState = this;
        Rigidbody rbPlayer = player.GetComponent<Rigidbody>();
        rbPlayer.transform.localScale *= 0.5f;
    }

    public void Execute(Player player)
    {
        if (!Input.GetKey(KeyCode.S))
        {
            // transistion to standing
            StandingPlayerState standingState = new StandingPlayerState();
            standingState.Enter(player);
            Rigidbody rbPlayer = player.GetComponent<Rigidbody>();
            rbPlayer.transform.localScale *= 2;
        }
    }
}

public class JumpingPlayerState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered JUMPING");
        player.mCurrentState = this;
        Rigidbody rbPlayer = player.GetComponent<Rigidbody>();
        rbPlayer.AddForce(0, 500 * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    public void Execute(Player player)
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, 0.55f))
        {
            StandingPlayerState standingState = new StandingPlayerState();
            standingState.Enter(player);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // transition to ducking
            DivingPlayerState divingState = new DivingPlayerState();
            divingState.Enter(player);
        }
    }
}

public class DivingPlayerState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered DIVING");
        player.mCurrentState = this;
        Rigidbody rbPlayer = player.GetComponent<Rigidbody>();
        rbPlayer.AddForce(0, -800 * Time.deltaTime, 0, ForceMode.VelocityChange);
    }

    public void Execute(Player player)
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, 0.55f))
        {
            StandingPlayerState standingState = new StandingPlayerState();
            standingState.Enter(player);
            //DuckingPlayerState duckingState = new DuckingPlayerState();
            //duckingState.Enter(player);
        }
    }
}