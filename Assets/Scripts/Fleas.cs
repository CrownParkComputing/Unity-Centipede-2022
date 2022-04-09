using System.Collections;
using UnityEngine;

public class Fleas : MonoBehaviour
{
    public Flea fleaPrefab;
    public MushroomField mushroomField;
    private AudioSource mySound;
    private int fleaInterval;

    private void Awake()
    {
        mySound = GetComponent<AudioSource>();

    }

    public void StartFleas(int interval)
    {
        fleaInterval = interval;
        StartCoroutine(nameof(RepeatSpawn));
    }
    public void KillFleas()
    {
        StopAllCoroutines();
    }

    private void Respawn()
    {
        BoxCollider2D field = mushroomField.GetComponent<BoxCollider2D>();
        Bounds area = field.bounds;
        Vector2 position = Vector2.zero;

        position.x = Mathf.Round(Random.Range(area.min.x, area.max.x));
        position.y = area.max.y;
        _ = Instantiate(fleaPrefab, position, Quaternion.identity, transform);
        if (!mySound.isPlaying)
        {
            mySound.Play();
        }

    }

    private IEnumerator RepeatSpawn()
    {
        int i= 100;
        while (i>0)
        {
            Respawn();
            yield return new WaitForSeconds(fleaInterval);
        } 


    }


}
