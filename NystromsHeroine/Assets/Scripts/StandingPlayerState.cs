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

public class NeutralColorState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered NEUTRAL");
        player.mCurrentState = this;
    }

    public void Execute(Player player)
    {
        if (Input.GetKey(KeyCode.R))
        {
            // transition to raging
            RagingPlayerState ragingState = new RagingPlayerState();
            ragingState.Enter(player);
        }
        if (Input.GetKey(KeyCode.B))
        {
            // transition to raging
            BlushingPlayerState blushingState = new BlushingPlayerState();
            blushingState.Enter(player);
        }
        if (Input.GetKey(KeyCode.E))
        {
            // transition to raging
            EmoPlayerState emoState = new EmoPlayerState();
            emoState.Enter(player);
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
        }
    }
}

public class RagingPlayerState : IPlayerStates
{
    Camera cam = GameObject.FindObjectOfType<Camera>();

    public void Enter(Player player)
    {
        Debug.Log("Entered RAGING");
        player.mCurrentState = this;
        player.gameObject.GetComponent<Renderer>().material.color = Color.red;
        Transform t = player.GetComponent<Transform>();
        t.position = new Vector3(Mathf.Sin(Time.time * 50f) * .1f,
            t.position.y, t.position.z);
        cam.transform.position = new Vector3(cam.transform.position.x,
            cam.transform.position.y - .002f, cam.transform.position.z + .007f);
    }

    public void Execute(Player player)
    {
        if (!Input.GetKey(KeyCode.R))
        {
            // transistion to standing
            NeutralColorState neutralState = new NeutralColorState();
            StandingPlayerState standingState = new StandingPlayerState();
            neutralState.Enter(player);
            standingState.Enter(player);
            player.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            player.GetComponent<Transform>().position = Vector3.zero;
            cam.transform.position = new Vector3(0, 1, -5);
        }
    }
}

public class BlushingPlayerState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered BLUSHING");
        player.mCurrentState = this;
        player.gameObject.GetComponent<Renderer>().material.color =
            (5 * Color.red) + Color.white;
        Transform t = player.GetComponent<Transform>();
        t.localScale -= new Vector3 (0.005f, 0.005f, 0.005f);
    }

    public void Execute(Player player)
    {
        if (!Input.GetKey(KeyCode.B))
        {
            // transistion to standing
            NeutralColorState neutralState = new NeutralColorState();
            StandingPlayerState standingState = new StandingPlayerState();
            neutralState.Enter(player);
            standingState.Enter(player);
            player.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            player.GetComponent<Transform>().localScale = new Vector3 (1,1,1);
        }
    }
}

public class EmoPlayerState : IPlayerStates
{
    public void Enter(Player player)
    {
        Debug.Log("Entered EMO PHASE");
        player.mCurrentState = this;
        player.gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    public void Execute(Player player)
    {
        if (!Input.GetKey(KeyCode.E))
        {
            // transistion to standing
            NeutralColorState neutralState = new NeutralColorState();
            StandingPlayerState standingState = new StandingPlayerState();
            neutralState.Enter(player);
            standingState.Enter(player);
            player.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
}