using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

public class EnemyController : MonoBehaviour
{
    private float speed;
    private int health;

    private float range;
    private int damage;
    public EnemyType type;
    public float eatCoolDown;
    public LayerMask playerMask;
    public Player targetPlayer;
    private bool canEat = true;

    private void Start()
    {
        health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCoolDown = type.eatCoolDown;

        GetComponent<SpriteRenderer>().sprite = type.sprite;

    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, range, playerMask);

        if (hit.collider)
        {
            targetPlayer = hit.collider.GetComponent<Player>();
            Eat();
        }

        if (health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = type.deathSprite;
        }
    }

    void Eat()
    {
        if (!canEat || !targetPlayer)
            return;
        canEat = false;
        Invoke("ResetEatCoolDown", eatCoolDown);

        targetPlayer.Hit(damage);
    }

    void ResetEatCoolDown()
    {
        canEat = true;
    }

    private void FixedUpdate()
    {
        if (!targetPlayer)
            transform.position -= new Vector3(0, speed, 0);
    }

    public void Hit(int damage, bool freeze)
    {
        health -= damage;
        if (freeze)
        {
            Freeze();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Freeze()
    {
        CancelInvoke("UnFreeze");
        GetComponent<SpriteRenderer>().color = Color.blue;
        speed = type.speed / 2f;
        Invoke("UnFreeze", 5);
    }

    void UnFreeze()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        speed = type.speed;
    }
}
