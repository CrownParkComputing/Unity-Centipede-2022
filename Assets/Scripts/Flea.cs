using UnityEngine;

public class Flea : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;
    private Vector2 targetPostion;
    private float fleaSpeed = 5f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public void  Start()
    {
        targetPostion.x = transform.position.x;
        targetPostion.y = -21f;
    }
    private void Update()
    {
        _ = GridPosition(transform.position);

        Vector2 currentPosition = transform.position;
        _ = fleaSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPostion, fleaSpeed * Time.deltaTime); 

        if (currentPosition == targetPostion)
        {
            Destroy(this.gameObject);    
        }
   
    }
    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Destroy(this.gameObject);
            GameManager.Instance.ResetRound();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart"))
        {
            Destroy(this.gameObject);
            GameManager.Instance.IncreaseScore(200);
        }


    }
}