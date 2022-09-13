using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 20;

    public void Attack()
    {
        //Animaci�n de ataque
        //Detectar a los Enemigos

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Da�arlos
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}
