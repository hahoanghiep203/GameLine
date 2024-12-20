using UnityEngine;
using System.Collections;

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
        StartCoroutine(BlinkEffect());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator BlinkEffect()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color hitColor = Color.red; // Color to indicate hit
        float blinkDuration = 0.05f; // Duration for each blink
        int blinkCount = 1; // Number of blinks

        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = hitColor; // Change to hit color
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor; // Change back to original color
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
