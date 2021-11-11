using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] Health _playerHealthComponent = null;
    [SerializeField] Animator _playerAnimator = null;
    [SerializeField] PlayerTurnBattleState _playerTurnAccess = null;

    private ICommand _spellCommand;

    [Header("Spell Animations")]
    //public Animation _healingAnimation = null;
    AnimationClip animClip;

    public void UsedHealingHaiku()
    {
        //hook into commands
        _spellCommand = new HealingHaiku(_playerAnimator, _playerHealthComponent);
        _spellCommand.Execute();
        _playerTurnAccess.GoToEnemyState();
    }

    public void UsedCreatePancake()
    {
        //hook into commands
        //_spellCommand = new HealingHaiku(_healingAnimation, _playerHealthComponent);
        _spellCommand.Execute();
    }

    public void UsedInAPickle()
    {
        //hook into commands
        //_spellCommand = new HealingHaiku(_healingAnimation, _playerHealthComponent);
        _spellCommand.Execute();
    }

    public void UsedExcalibread()
    {
        //hook into commands
        //_spellCommand = new HealingHaiku(_healingAnimation, _playerHealthComponent);
        _spellCommand.Execute();
    }

    public void UsedLastStand()
    {
        //hook into commands
        //_spellCommand = new HealingHaiku(_healingAnimation, _playerHealthComponent);
        _spellCommand.Execute();
    }

    
}
