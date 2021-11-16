using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : ICommand
{
    private Animator _animator;
    private int _damage;
    private float _accuracyRoll;
    private GameObject _missText;
    private Health _enemyHealth;


    public PlayerAttack(Animator animator, int damage, float accuracyRoll, GameObject missText, Health enemyHealth)
    {
        _animator = animator;
        _damage = damage;
        _accuracyRoll = accuracyRoll;
        _missText = missText;
        _enemyHealth = enemyHealth;

    }

    public void Execute()
    {
        Debug.Log("Attacked Enemy!");
        _animator.SetTrigger("heal");

        if (_accuracyRoll > 40)
        {
            _enemyHealth.TakeDamage(_damage);
        }
        else
        {
            Debug.Log("MISSED!");
            _missText.SetActive(true);
        }

        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("Unattacked...?");

        //_health._currentHealth -= 30;
    }

}
