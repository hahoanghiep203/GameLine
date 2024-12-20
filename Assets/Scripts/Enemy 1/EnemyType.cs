using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New EnemyType", menuName = "Enemy")]
public class EnemyType : ScriptableObject
{
    public int health;
    public float speed;

    public int damage;
    public float range = .5f;
    public float eatCoolDown = 1f;

    public Sprite sprite;

    public Sprite deathSprite;
}