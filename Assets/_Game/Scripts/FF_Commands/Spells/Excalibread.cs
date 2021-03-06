using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excalibread : ICommand
{
    private Animator _animator;
    private Health _health;
    private Health _enemyHealth;


    public Excalibread(Animator animator, Health health, Health enemyHealth)
    {
        _animator = animator;
        _health = health;
        _enemyHealth = enemyHealth;

    }

    public void Execute()
    {
        Debug.Log("Used Excalibread");
        _animator.SetTrigger("bread");

        DelayHelper.DelayAction(_enemyHealth, Damage, 2f);
        _health._currentSugar -= 6;

        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? Excalibread");

        //_health._currentHealth -= 30;
    }

    void Damage()
    {
        _enemyHealth.TakeDamage(140);
    }
}
