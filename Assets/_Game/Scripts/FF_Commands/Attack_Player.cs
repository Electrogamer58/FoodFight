using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : ICommand
{
    Animation _animation;
    int _rolledNumber;
    int _rollToHit;
    int _damageToDeal;
    public static event Action HitPlayer;
    public static event Action MissedPlayer;

    public Attack_Player(Animation animation, int rolledNumber, int rollToHit, int damageToDeal)
    {
        _animation = animation;
        _rolledNumber = rolledNumber;
        _rollToHit = rollToHit;
        _damageToDeal = damageToDeal;
    }

    public void Execute()
    {
        Debug.Log("Tried Attack Player!");
        if (_animation != null)
            _animation.Play();

        if (_rolledNumber >= _rollToHit)
        {
            HitPlayer?.Invoke();
        }
        if (_rolledNumber < _rollToHit)
        {
            MissedPlayer?.Invoke();
        }

    }

    public void Undo()
    {
        Debug.Log("Stopped Attacking Player");
        if (_animation != null)
        {
            if (_animation.isPlaying)
                _animation.Stop();
        }
    }
}
