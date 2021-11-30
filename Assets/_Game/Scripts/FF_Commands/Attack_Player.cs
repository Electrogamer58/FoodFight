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
    Health _playerHealth;
    public static event Action HitPlayer;
    public static event Action MissedPlayer;

    public Attack_Player(Animator animator, int rolledNumber, int rollToHit, int damageToDeal, Health playerHealth)
    {
        _animator = animator;
        _rolledNumber = rolledNumber;
        _rollToHit = rollToHit;
        _damageToDeal = damageToDeal;
        _playerHealth = playerHealth;
    }

    public void Execute()
    {
        Debug.Log("Tried Attack Player!");
 
        if (_rolledNumber >= _rollToHit)
        {
            HitPlayer?.Invoke();
            _playerHealth.TakeDamage(_damageToDeal);
            Debug.Log("Dealt " + _damageToDeal + " to player");

        }
        if (_rolledNumber < _rollToHit)
        {
            MissedPlayer?.Invoke();
            Debug.Log("Opponent Missed!");
        }

    }

    public void Undo()
    {
        Debug.Log("Stopped Attacking Player");
       
    }
}
