using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChecker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxRange = 12f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange())
        {
            Debug.Log("Is in range!");
        }
    }

    private float distanceWithTarget()
    {
        return Vector3.Distance(transform.position, _target.position);
    }

    private bool isInRange()
    {
        return distanceWithTarget() < _maxRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _maxRange);
    }

}
