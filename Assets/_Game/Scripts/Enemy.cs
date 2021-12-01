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
    [SerializeField] Transform _enemySpawn = null;
    [SerializeField] ParticleSystem _impactParticles = null;
    [SerializeField] ParticleSystem _deathParticles = null;
    [SerializeField] AudioClip _impactSound = null;


    [Header("Attack Stats")]
    [SerializeField] Health playerHealth;
    [SerializeField] Health myHealth;
    public int hitDC = 30;
    public int minAttackDamage = 1;
    public int maxAttackDamage = 10;

    [Header("Enemies")]
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;

    int actionRoll = 0;
    EnemyTurnBattleState _enemyState;


    void Awake()
    {
        _enemyState = FindObjectOfType<EnemyTurnBattleState>();
        if (myHealth == null)
        {
            myHealth = GetComponent<Health>();
        }

        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        if (myAnimator == null)
        {
            myAnimator = GetComponent<Animator>();
            if (myAnimator == null)
            {
                gameObject.AddComponent<Animator>();
            }
        }

        if (_enemySpawn == null)
        {
            _enemySpawn = GameObject.FindGameObjectWithTag("EnemySpawn").transform;
        }
    }

    private void OnEnable()
    {
        myHealth.Died += OnDie;
        Debug.Log("Enabled");
        myHealth.Damaged += OnDamaged;
    }

    public IEnumerator EnemyThinkingRoutine(float pauseDuration, AudioSource _audioSource, AudioClip _hitSound, AudioClip _missSound)
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
            Attack(_audioSource, _hitSound, _missSound);
            myAnimator.SetTrigger("attack");
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
                Attack(_audioSource, _hitSound, _missSound);
                myAnimator.SetTrigger("attack");
            }
            
        }
        //PLAY ACTION ANIMATION

        yield return new WaitForSeconds(pauseDuration);
        
        //turn over, go back to Player.
        if (PlayerTurnBattleState.inGame)
            _enemyState.ChangeState();
    }

    private void Attack(AudioSource _audioSource, AudioClip _hitSound, AudioClip _missSound)
    {
        int AttackDamage = UnityEngine.Random.Range(minAttackDamage, maxAttackDamage);
        int myRoll = UnityEngine.Random.Range(1, 101);
        _commandStack.ExecuteCommand(new Attack_Player(myAnimator, myRoll, hitDC, AttackDamage, playerHealth));
        if (myRoll > hitDC)
        {
            _audioSource.clip = _hitSound;
            _audioSource.Play();
        }
        else
        {
            _audioSource.clip = _missSound;
            _audioSource.Play();
        }
    }

    private void Heal()
    {
        myHealth.TakeDamage(UnityEngine.Random.Range(minAttackDamage, maxAttackDamage));
        Debug.Log("Heal!");
    }

    private void OnDamaged()
    {
        //TODO Add visuals
        DelayHelper.DelayAction(this, VisualFeedback, 0.7f);
        
    }

    private void OnDie()
    {
        Debug.Log("Enemy Died! Loading new enemy...");

        if (_deathParticles != null)
        {
            _deathParticles = Instantiate(_deathParticles, transform.position, Quaternion.identity);
        }

        PlayerScore.EnemiesKilled += 1;
        PlayerScore.Tastecoins += UnityEngine.Random.Range(10, 30);

        int enemyType = UnityEngine.Random.Range(1, 3);
            
        if (enemyType == 1)
        {
            _enemyState.currentEnemyObject = Instantiate(enemy1, _enemySpawn.position, Quaternion.identity);
            Animator animator = _enemyState.currentEnemyObject.AddComponent<Animator>();
            Destroy(gameObject);
        }
        if (enemyType == 2)
        {
            _enemyState.currentEnemyObject = Instantiate(enemy2, _enemySpawn.position, Quaternion.identity);
            Animator animator = _enemyState.currentEnemyObject.AddComponent<Animator>();
            Destroy(gameObject);
        }

    }

    private void OnDisable()
    {
        myHealth.Died -= OnDie;
        myHealth.Damaged -= OnDamaged;
    }

    void VisualFeedback()
    {
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
        }

        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }
}
