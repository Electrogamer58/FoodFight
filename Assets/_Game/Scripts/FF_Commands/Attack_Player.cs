using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : ICommand
{
    Animator _animator;
    int _rolledNumber;
    int _rollToHit;
    int _damageToDeal;
    public static event Action HitPlayer;
    public static event Action MissedPlayer;

    public Attack_Player(Animator animator, int rolledNumber, int rollToHit, int damageToDeal)
    {
        _animator = animator;
        _rolledNumber = rolledNumber;
        _rollToHit = rollToHit;
        _damageToDeal = damageToDeal;
    }

    public void Execute()
    {
        Debug.Log("Tried Attack Player!");
 
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
       
    }
}
