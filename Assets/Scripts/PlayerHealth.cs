using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Debug.Log("Player Is Dead");
        }
    }
}
