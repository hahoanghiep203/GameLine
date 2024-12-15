using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;

    private void Start()
    {
        gameObject.layer = 9;
    }

    public void Hit(int damage)
    {
        SoundManager.PlaySound(SoundType.PLAYERHURT);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
