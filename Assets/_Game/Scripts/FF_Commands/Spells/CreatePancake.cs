using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePancake : ICommand
{
    private Animator _animator;
    private Health _health;
    private Health _enemyHealth;


    public CreatePancake(Animator animator, Health health, Health enemyHealth)
    {
        _animator = animator;
        _health = health;
        _enemyHealth = enemyHealth;

    }

    public void Execute()
    {
        Debug.Log("Used Create Pancake");
        _health._currentSugar -= 4;
        _animator.SetTrigger("pancake");

        DelayHelper.DelayAction(_enemyHealth, Damage, 2f);
        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? Create Pancake");

        //_health._currentHealth -= 30;
    }

    void Damage()
    {
        _enemyHealth.TakeDamage(30);
    }
}
