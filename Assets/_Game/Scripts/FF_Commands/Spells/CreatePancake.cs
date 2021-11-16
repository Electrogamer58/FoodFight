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
        _animator.SetTrigger("heal");

        _enemyHealth.TakeDamage(30);


        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? Create Pancake");

        //_health._currentHealth -= 30;
    }
}
