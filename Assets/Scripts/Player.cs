using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int price;

    private void Start()
    {
        gameObject.layer = 9;
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
