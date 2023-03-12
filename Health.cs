using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public static event UnityAction TakeOnePointOfLife;

     [Header("Health")]
    [SerializeField] private float _startingHealth;

    [Header("Hitframes")]
    [SerializeField] private float _framesFuration;
    [SerializeField] private float _numberOfFlashes;

    private int _hit = Animator.StringToHash("Hit");
    private int _isDead = Animator.StringToHash("IsDead");
    public float currentHealth { get; private set; }
    private bool _dead;
    private int _minimumHealth = 0 ;

    private Animator _animator;

    private void Awake()
    {
        currentHealth = _startingHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDemage(int demage)
    {
        currentHealth = Mathf.Clamp(currentHealth - demage, 0, _startingHealth);

        TakeOnePointOfLife?.Invoke();

        if (currentHealth > _minimumHealth)
        {
            _animator.SetBool(_hit,true);
        }
        else
        {
            if (!_dead)
            {
                _animator.SetBool(_isDead, true);
                GetComponent<CharacterMovement>().enabled = false;
                _dead = true;
            }
        }
    }
}
