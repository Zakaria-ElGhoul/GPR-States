using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private UnityEvent ChaseState;
    [SerializeField] private UnityEvent AttackState;
    [SerializeField] private UnityEvent IdleState;
    [SerializeField] private UnityEvent fleeState;

    [SerializeField] private EnemyStates currentState;

    [SerializeField] private GameObject currentTarget;

    [SerializeField] private float _attackRange = 3f;
    [SerializeField] private float _maxRange = 12f;
    
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _fleeHealth = 10f;

    private RangeChecker _rangeChecker;
    
    // Start is called before the first frame update
    void Start()
    {
        _rangeChecker = GetComponent<RangeChecker>();
        currentTarget = GameObject.FindWithTag("Player");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState = state();
        Debug.Log("State: " + currentState);

        switch (currentState)
        {
            case EnemyStates.Flee:
                fleeState?.Invoke();
                break;
            case EnemyStates.Attack:
                AttackState?.Invoke();
                break;
            case EnemyStates.Chase:
                ChaseState?.Invoke();
                break;
            case EnemyStates.Idle:
                IdleState?.Invoke();
                break;
        }
    }

    private bool isTargetInAttackRange()
    {
        return _rangeChecker.distanceWithTarget(currentTarget) < _attackRange;
    }

    private EnemyStates state()
    {
        if (_rangeChecker.isInRange(currentTarget, _maxRange))
        {
            return fightStates();
        }

        return EnemyStates.Idle;
    }

    private EnemyStates fightStates()
    {
        
        if (needToFlee()) return EnemyStates.Flee;

        if (isTargetInAttackRange()) return EnemyStates.Attack;

        return EnemyStates.Chase;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _maxRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private bool needToFlee()
    {
        return _health <= _fleeHealth;
    }
}