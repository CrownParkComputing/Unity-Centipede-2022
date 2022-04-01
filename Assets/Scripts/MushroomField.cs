using UnityEngine;

public class MushroomField : MonoBehaviour
{
    private BoxCollider2D area;
    public Mushroom prefab;
    public Mushroom prefab2;
    public int amount = 25;


    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        GenerateField(prefab, amount);
        GenerateField(prefab2, amount);
    }

    private void GenerateField(Mushroom thisMushroom, int thisAmount)
    {
        Bounds bounds = area.bounds;

        for (int i = 0; i < thisAmount; i++)
        {
            Vector2 position = Vector2.zero;
            Vector2 position2 = Vector2.zero;

            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            Instantiate(thisMushroom, position, Quaternion.identity, transform);

        }
    }
}
