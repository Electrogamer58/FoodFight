using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action EnemyTurnEnded;
    CommandStack _commandStack = new CommandStack();

    [Header("My Animation and Stuff")]
    [SerializeField] Animator myAnimator = null;


    [Header("Attack Stats")]
    [SerializeField] Health playerHealth;
    [SerializeField] Health myHealth;
    public int hitDC = 30;
    public int minAttackDamage = 1;
    public int maxAttackDamage = 10;

    int actionRoll = 0;
    EnemyTurnBattleState _enemyState;


    void Awake()
    {
        _enemyState = FindObjectOfType<EnemyTurnBattleState>();
        if (myHealth == null)
        {
            myHealth = GetComponent<Health>();
        }

    }

    private void OnEnable()
    {
        myHealth.Died += OnDie;
        myHealth.Damaged += OnDamaged;
    }

    public IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        //CHOOSE RANDOM ACTION: ATTACK/HEAL

        actionRoll = UnityEngine.Random.Range(1, 3);
        

        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        //PERFORM ACTION (AND ALL LOGIC ATTRIBUTED TO IT)
        if (actionRoll == 1)
        {
            //ATTACK
            Attack();
        }
        if (actionRoll == 2)
        {
            //HEAL if at low health
            if (myHealth._currentHealth < myHealth._maxHealth / 6)
            {
                Heal();
            }
            //otherwise, attack
            else
            {
                Attack();
            }
            
        }
        //PLAY ACTION ANIMATION

        yield return new WaitForSeconds(pauseDuration);
        EnemyTurnEnded?.Invoke();
        //turn over, go back to Player.
        _enemyState.ChangeState();
    }

    private void Attack()
    {
        int AttackDamage = UnityEngine.Random.Range(minAttackDamage, maxAttackDamage);
        int myRoll = UnityEngine.Random.Range(1, 101);
        _commandStack.ExecuteCommand(new Attack_Player(myAnimator, myRoll, hitDC, AttackDamage, playerHealth));
    }

    private void Heal()
    {
        myHealth.TakeDamage(UnityEngine.Random.Range(minAttackDamage, maxAttackDamage));
        Debug.Log("Heal!");
    }

    private void OnDamaged()
    {
        //TODO Add visuals
    }

    private void OnDie()
    {
        Debug.Log("Enemy Died! Loading new enemy...");
    }

    private void OnDisable()
    {
        myHealth.Died -= OnDie;
        myHealth.Damaged -= OnDamaged;
    }
}
