using System.Collections.Generic;
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


    public void GenerateField()
    {

        GenerateMushroomType(prefab, amount);
        GenerateMushroomType(prefab2, amount);
    }
    private void GenerateMushroomType(Mushroom thisMushroom, int thisAmount)
    {
        Bounds bounds = area.bounds;

        for (int i = 0; i < thisAmount; i++)
        {
            Vector2 position = Vector2.zero;

            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            Instantiate(thisMushroom, position, Quaternion.identity, transform);
        }
    }

    public void ClearField()
    {
        Mushroom[] mushrooms = FindObjectsOfType<Mushroom>();
        foreach (Mushroom mushroom in mushrooms)
        {
            Destroy(mushroom.gameObject);
        }

    }

    public void HealField()
    {
        Mushroom[] mushrooms = FindObjectsOfType<Mushroom>();
        foreach (Mushroom mushroom in mushrooms)
        {
            mushroom.Heal();
        }       
    }
}
