using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Sprite[] states;
    private SpriteRenderer spriteRenderer;
    private int health;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = states.Length;    
    }

    private Sprite[] GetStates()
    {
        return states;
    }

    private void Damage(int amount, Sprite[] states)
    {
        health -= amount;
        if (health > 0)
        {
            spriteRenderer.sprite = states[^health];
        }
        else{
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        health = states.Length;
        spriteRenderer.sprite = states[0];
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart")){
            Damage(1, GetStates());
            FindObjectOfType<GameManager>().IncreaseScore(10);
            
        }
    }

}
