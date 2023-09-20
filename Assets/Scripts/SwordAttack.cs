using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    [SerializeField] float damage = 3;
    [SerializeField] Collider2D swordCollider;
    Vector2 attackOffset;


    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = attackOffset;
    }

    public void AttackLeft() 
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(attackOffset.x * -1, attackOffset.y);
    }

    public void StopAttack() 
    {
        swordCollider.enabled = false;

    }
    void Start()
    {
        attackOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null) 
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
