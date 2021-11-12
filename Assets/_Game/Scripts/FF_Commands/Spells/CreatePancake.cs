using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePancake : ICommand
{
    private Animator _animator;
    private Health _health;

    public CreatePancake(Animator animator, Health health)
    {
        _animator = animator;
        _health = health;

    }

    public void Execute()
    {
        Debug.Log("Used Create Pancake");
        _animator.SetTrigger("heal");
        

        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? Create Pancake");

        //_health._currentHealth -= 30;
    }
}
