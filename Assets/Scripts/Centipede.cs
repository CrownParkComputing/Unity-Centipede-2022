using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    private List<CentipedeSegment> segments = new List<CentipedeSegment>();

    public CentipedeSegment segmentPrefab;
    public Mushroom mushroomPrefab;
    public Sprite headSprite;
    public Sprite bodySprite;
    public float centipedeLength = 10f;
    public float centipedeSpeed = 4f;

    public LayerMask collisionMask;
    public BoxCollider2D homeArea;

    private void Start()
    {
        Respawn();
    }

    private void Respawn()
    {
        foreach (CentipedeSegment segment in segments) {
            Destroy(segment.gameObject); 
        }

        segments.Clear();

        for (int i = 0; i < centipedeLength; i ++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSegment segment = Instantiate(segmentPrefab, position, Quaternion.identity);
            segment.spriteRenderer.sprite = i == 0 ? headSprite : bodySprite;  
            segment.centipede = this;
            segments.Add(segment);
        }

        for (int i = 0; i < segments.Count; i ++)
        {
            CentipedeSegment segment = segments[i];  
            segment.ahead = GetSegmentAt(i-1);
            segment.behind = GetSegmentAt(i+1);          
        }
    }

    private CentipedeSegment GetSegmentAt(int index)
    {
        if (index >=0 && index < segments.Count){
            return segments[index];
        }
        else{
            return null;
        }
    
    }
    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    public void Remove(CentipedeSegment segment)
    {
        Vector3 position = GridPosition(segment.transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);

        if (segment.ahead != null) {
            segment.ahead.behind = null;
        }

        if (segment.behind != null) {
            segment.behind.ahead = null;
            segment.behind.spriteRenderer.sprite = headSprite;
            segment.behind.UpdateHeadSegment();
        }

        
        Destroy(segment.gameObject);
    }
}