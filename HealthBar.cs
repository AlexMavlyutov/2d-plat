using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Image _totalHealth;
    [SerializeField] private Image _currentHealth;

    private int _maxHealth = 10;

    private void OnEnable()
    {
        Health.TakeOnePointOfLife += OnTakeOnePointOfLife;
    }

    private void OnDisable()
    {
        Health.TakeOnePointOfLife -= OnTakeOnePointOfLife;
    }

    private void Start()
    {
        _currentHealth.fillAmount = _playerHealth.currentHealth / _maxHealth;
    }

    private void OnTakeOnePointOfLife()
    {
        _currentHealth.fillAmount = _playerHealth.currentHealth / _maxHealth;
    }
}
