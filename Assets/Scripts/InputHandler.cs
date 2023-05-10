using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private CommandClass _fly;
    private CommandClass _left;
    private CommandClass _right;
    private bool flying = false;
    
    public void LeftRotate(KeyCode key, LeftRotateCommand left)
    {
        _left = left;
        if (Input.GetKey(key))
        {
            _left.Execute();
        }
    }

    public void RightRotate(KeyCode key, RightRotateCommand right)
    {
        _right = right;
        if (Input.GetKey(key))
        {
            _right.Execute();
        }
    }

    public void Fly(KeyCode key, FlightCommand fly)
    {
        _fly = fly;
        if (Input.GetKey(key))
        {
            _fly.Execute();
            flying = true;
        } else
        {
            flying = false;
        }
    }

    public bool IsFlying() { return flying; }
}
