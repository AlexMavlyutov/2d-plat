using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MeeleEnemy : MonoBehaviour
{
    [SerializeField] private float _atackCoolDown;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private int _demage;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private LayerMask _playerMask;

    private Health _playerHealth;
    private int _attack = Animator.StringToHash("Attack");
    private float _coolDownTimer = Mathf.Infinity;
    private Animator _animator;
    private EnemyPatrol _enemyPatrol;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        _coolDownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (_coolDownTimer > _atackCoolDown)
            {
                _coolDownTimer = 0;
                _animator.SetTrigger(_attack);
            }
        }
        
        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider2D.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3 (_boxCollider2D.bounds.size.x * _range, _boxCollider2D.bounds.size.y, _boxCollider2D.bounds.size.z)
            ,0, Vector2.right,0,_playerMask);

        if (hit.collider != null)
        {
            _playerHealth = hit.transform.GetComponent<Health>();
        }
       
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider2D.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider2D.bounds.size.x * _range, _boxCollider2D.bounds.size.y, _boxCollider2D.bounds.size.z));
    }

    private void DemagePlayer()
    {
        if (PlayerInSight())
        {
            _playerHealth.TakeDemage(_demage);
        }
    }

}
