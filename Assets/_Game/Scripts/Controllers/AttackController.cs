using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Health _enemyHealthComponent = null;
    [SerializeField] Animator _playerAnimator = null;
    [SerializeField] PlayerTurnBattleState _playerTurnAccess = null;
    [SerializeField] GameObject missText = null;

    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] AudioClip _attackSound = null;
    [SerializeField] AudioClip _missSound = null;
    [SerializeField] AudioClip _upgradeSound = null;

    public int damage = 5;
    public float accuracyMultiplier = 1.3f;
    float targetTime = 2f;

    private ICommand _attackCommand;

    public void Attack()
    {
        if (_playerTurnAccess._inputController.inputEnabled)
        {
            _enemyHealthComponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
            float accuracyRoll = Random.Range(1, 101) * accuracyMultiplier; //roll accuracy roll and multiply by multiplier.
            _attackCommand = new PlayerAttack(_playerAnimator, damage, accuracyRoll, missText, _enemyHealthComponent);
            _attackCommand.Execute();
            _playerTurnAccess.GoToEnemyState();

            if (accuracyRoll > 40)
            {
                _audioSource.clip = _attackSound;
                _audioSource.Play();
            } else
            {
                _audioSource.clip = _missSound;
                _audioSource.Play();
            }
        }
    }

    private void Update()
    {
        if (missText.activeSelf)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
        }

    }

    void timerEnded()
    {
        missText.SetActive(false);
        targetTime = 2f;
    }

    public void UpgradeDamage()
    {
        damage += Mathf.CeilToInt(5 + damage / 4);

        _playerTurnAccess.GoToEnemyState();
        _audioSource.clip = _upgradeSound;
        _audioSource.Play();
    }

    public void UpgradeAccuracy()
    {
        accuracyMultiplier += 0.2f;
        _playerTurnAccess.GoToEnemyState();
        _audioSource.clip = _upgradeSound;
        _audioSource.Play();
    }

}
