using System.Collections.Generic;
using UnityEngine;

public class CentipedeSegment : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; private set;}
    public Centipede centipede {get; set;}
    public CentipedeSegment ahead {get; set;}
    public CentipedeSegment behind {get;set;}
    public bool isHead => ahead == null;
    
    private Vector2 direction = Vector2.right + Vector2.down;
    private Vector2 targetPostion;

    [Header("Scoring")]
    public int pointsHead = 100;
    public int pointsBody = 10;
    private void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        targetPostion = transform.position;      
    }

    private void Update()
    {
        if (isHead && Vector2.Distance(transform.position, targetPostion) < 0.1f)
        {
            UpdateHeadSegment();
        }
        Vector2 currentPosition = transform.position;
        float speed = centipede.centipedeSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPostion, speed); 

        Vector2 movementDirection = (targetPostion - currentPosition).normalized;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    
    }

    


    public void UpdateHeadSegment()
    {
        Vector2 gridPosition = GridPosition(transform.position);
        targetPostion = gridPosition;
        targetPostion.x += direction.x;

        if (Physics2D.OverlapBox(targetPostion, Vector2.zero, 0f, centipede.collisionMask))
        {
            direction.x = -direction.x;

            targetPostion.x = gridPosition.x;
            targetPostion.y = gridPosition.y + direction.y;

            Bounds homeBounds = centipede.homeArea.bounds;

            if ((direction.y == 1f && targetPostion.y > homeBounds.max.y) || 
                (direction.y == -1F && targetPostion.y < homeBounds.min.y))
                {   
                    direction.y = -direction.y;
                    targetPostion.y = gridPosition.y + direction.y;
                }
        }
        if (behind != null){
            behind.UpdateBodySegment();
        }
    }

    private void UpdateBodySegment()
    {
        targetPostion =GridPosition(ahead.transform.position);
        direction = ahead.direction;
        if (behind != null){
            behind.UpdateBodySegment();
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
            GameManager.Instance.ResetRound();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Dart") && collision.collider.enabled)
        {
            collision.collider.enabled = false;
            centipede.Remove(this);
            FindObjectOfType<GameManager>().IncreaseScore(50);
        }
    }


}   