using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float StartHealth = 3.0f;
    public float DeathDelay = 0.0f;

    public float currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            GameObject.Destroy(gameObject, DeathDelay);
        }
    }
}
