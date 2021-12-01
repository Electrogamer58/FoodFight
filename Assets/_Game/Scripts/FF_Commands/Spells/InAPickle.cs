using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAPickle : ICommand
{
    private Animator _animator;
    private Health _health;
    private GameObject _pickle;

    public InAPickle(Animator animator, Health health, GameObject pickle)
    {
        _animator = animator;
        _health = health;
        _pickle = pickle;

    }

    public void Execute()
    {
        Debug.Log("Used In A Pickle");
        _animator.SetTrigger("spell");

        _health._currentSugar -= 2;
        _health._shieldAmt = 2;
        _pickle.SetActive(true);


        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? In A Pickle");

        _health._shieldAmt = 1;
        _pickle.SetActive(false);

        //_health._currentHealth -= 30;
    }
}
