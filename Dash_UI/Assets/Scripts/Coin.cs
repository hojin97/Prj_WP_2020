using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject coin;
    private GameObject player;
    public static int coinCount = 0;

    private void Awake()
    {
        coin = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            coin.SetActive(false);
			FindObjectOfType<Game_SoundManager>().EffectPlay(1);
			coinCount++;
        }
    }
}
