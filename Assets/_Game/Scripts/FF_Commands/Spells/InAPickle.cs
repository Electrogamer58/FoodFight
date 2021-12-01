using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAPickle : ICommand
{
    private Animator _animator;
    private Health _health;

    public InAPickle(Animator animator, Health health)
    {
        _animator = animator;
        _health = health;

    }

    public void Execute()
    {
        Debug.Log("Used In A Pickle");
        _animator.SetTrigger("spell");

        _health._currentSugar -= 2;
        _health._shieldAmt = 2;


        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? In A Pickle");

        _health._shieldAmt = 1;

        //_health._currentHealth -= 30;
    }
}
