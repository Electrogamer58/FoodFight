using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHaiku : ICommand
{
    private Animator _animator;
    private Health _health;
   
    public HealingHaiku(Animator animator, Health health)
    {
        _animator = animator;
        _health = health;
        
    }

    public void Execute()
    {
        Debug.Log("Used Healing Haiku");
        _animator.SetTrigger("heal");
        _health.TakeDamage(-30);
        
        //_health._currentHealth += 30;
    }

    public void Undo()
    {
        Debug.Log("UnUsed? Healing Haiku");
        
        //_health._currentHealth -= 30;
    }
}
