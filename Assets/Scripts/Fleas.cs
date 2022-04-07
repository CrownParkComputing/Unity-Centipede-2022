using UnityEngine;

public class Fleas : MonoBehaviour
{
    public Flea fleaPrefab;
    public MushroomField mushroomField;

    public void Respawn()
    {
        BoxCollider2D field = mushroomField.GetComponent<BoxCollider2D>();
        Bounds area = field.bounds;
        Vector2 position = Vector2.zero;

        position.x = Mathf.Round(Random.Range(area.min.x, area.max.x));
        position.y = area.max.y;
        Flea thisFlea = Instantiate(fleaPrefab, position, Quaternion.identity, transform);
        thisFlea.SetTarget(position);
    }
}
