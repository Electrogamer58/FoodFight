using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _enemyTransform;

    Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    private void OnEnable()
    {
        PlayerTurnBattleState.PlayerTurnStarted += OnPlayerTurn;
        EnemyTurnBattleState.EnemyTurnBegan += OnEnemyTurn;
    }

    private void OnDisable()
    {
        PlayerTurnBattleState.PlayerTurnStarted -= OnPlayerTurn;
        EnemyTurnBattleState.EnemyTurnBegan -= OnEnemyTurn;
    }

    void Start()
    {
        target = _playerTransform;
    }

    void OnPlayerTurn()
    {
        DelayHelper.DelayAction(this, TargetPlayer, .3f);
    }

    void OnEnemyTurn()
    {
        DelayHelper.DelayAction(this, TargetEnemy, .8f);
    }

    void TargetPlayer()
    {
        target = _playerTransform;
        offset.x *= -1;
    }

    void TargetEnemy()
    {
        target = _enemyTransform;
        offset.x *= -1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
