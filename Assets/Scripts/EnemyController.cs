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
    public Animator anim;
    private Coroutine attackSoundCoroutine;

    private void Start()
    {
        health = type.health;
        speed = type.speed;
        damage = type.damage;
        range = type.range;
        eatCoolDown = type.eatCoolDown;

        GetComponent<SpriteRenderer>().sprite = type.sprite;
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("isDie");
            StartCoroutine(DestroyCoroutine());
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetPlayer = other.GetComponent<Player>();
            // Bắt đầu phát âm thanh tấn công liên tục
            attackSoundCoroutine = StartCoroutine(PlayAttackSound());
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UnAttack();
            CancelInvoke("ResetEatCoolDown");
            canEat = true;

            if (attackSoundCoroutine != null)
            {
                StopCoroutine(attackSoundCoroutine);
                attackSoundCoroutine = null;
            }
        }
    }

    private IEnumerator PlayAttackSound()
    {
        while (true) // Lặp vô hạn cho đến khi dừng
        {
            SoundManager.PlaySound(SoundType.ENEMYATTACK);
            yield return new WaitForSeconds(0.7f); // Thay đổi thời gian chờ tùy theo nhu cầu
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }

    private void Attack()
    {
        anim.SetBool("isAttacking", true);
    }

    private void UnAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
