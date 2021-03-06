using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public event Action Damaged = delegate { };// event action in case player is damaged. For UI Purposes
    public event Action UsedMagic = delegate { };
    public event Action Halved = delegate { };
    public event Action Died = delegate { };

    public int _maxHealth;
    public int _currentHealth;

    public int _maxSugar;
    public int _currentSugar;

    public float _killDelay;
    public int _heal;


    [Header("Basic Info and Feedback")]
    [SerializeField] Health _health;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound = null;
    [SerializeField] GameObject _damageCounterPrefab;
    [SerializeField] GameObject _UIController;
    public bool _isAlive = true;
    public int _shieldAmt = 1;
    GameObject myDamageCounter;


    [Header("Drops")]
    public bool _dropSomething = false;
    public bool _dropsMultipleThings = false;
    [SerializeField] GameObject _objectToDrop;
    [SerializeField] GameObject _objectToDrop2;
    [SerializeField] GameObject _objectToDrop3;
    [SerializeField] GameObject _objectToDrop4;
    public int _dropAmount;

    [Header("Destroy on Death?")]
    public bool _allowDestroy = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
        //_currentHealth = _maxHealth;
        _currentSugar = _maxSugar;

        if (_UIController == null)
        {
            _UIController = FindObjectOfType<BattleGameUIController>().gameObject;
            
        }
    }

    public virtual void TakeDamage(int damage)
    {
        _health._currentHealth -= damage/_shieldAmt;
        _health._currentHealth = Mathf.Clamp(_health._currentHealth, 0, _health._maxHealth);

        Damage();

        myDamageCounter = Instantiate(_damageCounterPrefab, transform.position, Quaternion.identity);
        if (damage > 0)
        {
            myDamageCounter.GetComponent<DamageCounter>()._text.text = " " + damage / _shieldAmt;
        } else if (damage < 0)
        {
            myDamageCounter.GetComponent<DamageCounter>()._text.text = "Healed!";
        } else if (damage == 0)
        {
            myDamageCounter.GetComponent<DamageCounter>()._text.text = "Missed!";
        }
        
        myDamageCounter.transform.SetParent(_UIController.transform);

        if (_currentHealth <= _maxHealth / 2) //if at half health, do something
        {
            Halved?.Invoke();
        }
    }

    public void Damage()
    {
        //Invoke the event appropriately
        Damaged?.Invoke(); // null check 

        if (_currentHealth <= 0)
        {
            Die();
            Kill(_killDelay);
        }
    }

    public void Die()
    {
        Died?.Invoke();
        AudioHelper.PlayClip2D(_impactSound, 1f);
    }

    public void UseMagic()
    {
        UsedMagic?.Invoke();
    }

    //public virtual void Heal(int health)
    //{
    //_health._currentHealth += health;

    //}

    protected void DeathFeedback(AudioClip _feedback)
    {
        //particles
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);

        }
        // audio. TODO: Consider object pooling - helps performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_feedback, 1f);
        }

        transform.position = new Vector3(transform.position.x, -100, transform.position.z);
    }

    public void Kill(float delay)
    {
        //DeathFeedback(_impactSound);
        
        _isAlive = false;

        if (_dropSomething && !_dropsMultipleThings)
        {
            while (_dropAmount > 0)
            {

                Vector3 _v3 = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-2, 2), 101, UnityEngine.Random.Range(-2, 2));
                _objectToDrop = Instantiate(_objectToDrop, _v3, Quaternion.identity);
                _dropAmount -= 1;

            }
        }

        if (_dropSomething && _dropsMultipleThings)
        {
            while (_dropAmount > 0)
            {
                int diceRoll = UnityEngine.Random.Range(1, 5);
                Debug.Log(diceRoll);
                Vector3 _v3 = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-2, 2), 101, UnityEngine.Random.Range(-2, 2));
                if (diceRoll == 1)
                {
                    _objectToDrop = Instantiate(_objectToDrop, _v3, Quaternion.identity);
                    _dropAmount -= 1;
                }

                else if (_objectToDrop2 != null && diceRoll == 2)
                {
                    _objectToDrop2 = Instantiate(_objectToDrop2, _v3, Quaternion.identity);
                    _dropAmount -= 1;

                }
                else if (_objectToDrop3 != null && diceRoll == 3)
                {
                    _objectToDrop3 = Instantiate(_objectToDrop3, _v3, Quaternion.identity);
                    _dropAmount -= 1;
                }
                else if (_objectToDrop4 != null && diceRoll == 4)
                {
                    _objectToDrop4 = Instantiate(_objectToDrop4, _v3, Quaternion.identity);
                    _dropAmount -= 1;
                }
                else return;


            }
        }

        if (_allowDestroy)
        {
            Destroy(gameObject, delay);
        }
        else if (!_allowDestroy)
        {
            gameObject.SetActive(false);
        }

    }

}
