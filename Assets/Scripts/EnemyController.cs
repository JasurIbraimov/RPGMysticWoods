using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    [SerializeField] float health = 1;
    public float Health
    {
        set
        {
            health = value;
            animator.SetTrigger("Damage");
            if(health <= 0)
            {
                Defeated();
            }
        }

        get
        {
            return health;  
        }
    }
    public void TakeDamage(float amount)
    {
        Health -= amount;
    }
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Defeated()
    {
        animator.SetTrigger("Death");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
