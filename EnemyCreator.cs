using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _spawner;
    [SerializeField] private float _timeBetween;

    private float _maxEmptyCount = 15;
    private float _enemyCount = 0;
    private bool _availability = true;

    private Transform[] _points;

    private void Start()
    {

        _points = new Transform[_spawner.childCount];

        for (int i = 0; i < _spawner.childCount; i++)
        {
            _points[i] = _spawner.GetChild(i);
        }

        StartCoroutine(CreateEmpty());
    }

    private IEnumerator CreateEmpty()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetween);

        while (_availability == true)
        {

            for (int i = 0; i < _points.Length; i++)
            {
                Instantiate(_enemy, _points[i].position, Quaternion.identity);

                _enemyCount ++;

                if (_enemyCount >= _maxEmptyCount)
                {
                    _availability = false;
                }

                yield return waitForSeconds;
            }
        }
    }
}
