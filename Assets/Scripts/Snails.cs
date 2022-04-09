using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snails : MonoBehaviour
{
    public Snail snailPrefab;
    private AudioSource mySound;
    private int snailInterval;

    private void Awake()
    {
        mySound = GetComponent<AudioSource>();

    }

    public void StartSnails(int interval)
    {
        snailInterval = interval;   
        StartCoroutine(nameof(RepeatSpawn));
    }
    public void KillSnails()
    {
        StopAllCoroutines();
    }

    private void Respawn()
    {
        Vector2 position = Vector2.zero;

        position.y = Mathf.Round(Random.Range(0f, 10f));
        position.x = 21f;
        _ = Instantiate(snailPrefab, position, Quaternion.identity, transform);


        if (!mySound.isPlaying)
        {
            mySound.Play();
        }

    }

    private IEnumerator RepeatSpawn()
    {
        int i = 100;
        while (i > 0)
        {
            Respawn();
            yield return new WaitForSeconds(snailInterval);
        }


    }
}
