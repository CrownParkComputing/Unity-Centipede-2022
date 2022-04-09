using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    private List<CentipedeSegment> segments = new List<CentipedeSegment>();
    private CentipedeSegment[] centipedeSegments;
    private AudioSource mySound;
    public CentipedeSegment segmentPrefab;
    public Mushroom mushroomPrefab;
    public Sprite headSprite;
    public Sprite bodySprite;
    public float centipedeLength = 10f;
    public float centipedeSpeed = 4f;
    [Header("Scoring")]
    public int pointsHead = 100;
    public int pointsBody = 10;
    public LayerMask collisionMask;
    public BoxCollider2D homeArea;

    public void Respawn(Vector2 startPosition)
    {
        mySound = GetComponent<AudioSource>();
        foreach (CentipedeSegment segment in segments) {
            Destroy(segment.gameObject);
        }

        segments.Clear();

        for (int i = 0; i < centipedeLength; i++)
        {
            Vector2 position = GridPosition(startPosition) + (Vector2.left * i);
            CentipedeSegment segment = Instantiate(segmentPrefab, position, Quaternion.identity, transform);
            segment.spriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            segment.centipede = this;
            segments.Add(segment);
        }

        for (int i = 0; i < segments.Count; i++)
        {
            CentipedeSegment segment = segments[i];
            segment.ahead = GetSegmentAt(i-1);
            segment.behind = GetSegmentAt(i+1);
        }

        if (!mySound.isPlaying)
        {
            mySound.Play();
        }

    }

    private void Update()
    {
        centipedeSegments = FindObjectsOfType<CentipedeSegment>();

        if (centipedeSegments.Length <= 0)
            {
                mySound.Stop();
                GameManager.Instance.NextLevel();

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
        int points = segment.isHead ? pointsHead : pointsBody;
        GameManager.Instance.IncreaseScore(points);

        Instantiate(mushroomPrefab, GridPosition(segment.transform.position), Quaternion.identity);

        if (segment.ahead != null) {
            segment.ahead.behind = null;
        }

        if (segment.behind != null)
        {
            segment.behind.ahead = null;
            segment.behind.UpdateHeadSegment();
        }

        segments.Remove(segment);
        Destroy(segment.gameObject);

    }
    
}