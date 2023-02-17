using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    [Header("Enemy")]
    [SerializeField] private Transform _enemy;

    [Header("Movement parametrs")]
    [SerializeField] private float _speed;

    [Header("Idle behavior")]
    [SerializeField] private float _idleDuration;

    [Header("Enemy animator")]
    [SerializeField] private Animator _animator;

    private float _idleTimer;
    private Vector3 _initScale;
    private bool _movingRight;

    private void Awake()
    {
        _initScale = _enemy.localScale;
    }

    private void OnDisable()
    {
        _animator.SetBool("Move", false);        
    }

    private void Update()
    {
        if (_movingRight)
        {
            if (_enemy.position.x <= _rightPoint.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
        else
        {
            if (_enemy.position.x >= _leftPoint.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {      
        _animator.SetBool("Move", false);
        _idleTimer += Time.deltaTime;

        if (_idleTimer > _idleDuration)
        {
        _movingRight = !_movingRight;
        }
    }



    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        _animator.SetBool("Move", true);
        _enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.y);

        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * direction * _speed, _enemy.position.y, _enemy.position.z);
    }



}

