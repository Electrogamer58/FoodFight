using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStand : ICommand
{
    private Animator _animator;
    private Health _health;
    private Health _enemyHealth;

    public LastStand(Animator animator, Health health, Health enemyHealth)
    {
        _animator = animator;
        _health = health;
        _enemyHealth = enemyHealth;

    }

    public void Execute()
    {
        Debug.Log("Used Last Stand");
        _animator.SetTrigger("heal");

        _health._currentSugar -= 4;

        if (_health._currentHealth < _health._maxHealth / 10)
        { //if below 10% health
            Debug.Log("STRONG!");
            _enemyHealth.TakeDamage(50);
        } else
        {
            Debug.Log("Weak...");
            _enemyHealth.TakeDamage(5);
        }

        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? LastStand");

        //_health._currentHealth -= 30;
    }
}
