using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IPlayerStates mCurrentState;
    public IPlayerStates mCurrentColor;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentState = new StandingPlayerState();
        mCurrentColor = new NeutralColorState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mCurrentState.Execute(this);
        mCurrentColor.Execute(this);
    }
}
