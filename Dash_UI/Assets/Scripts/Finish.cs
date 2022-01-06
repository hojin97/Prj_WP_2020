using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    GameObject player;
    GameObject myCam;
    public float finishTime;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.GetComponent<Animator>().SetTrigger("fin");
			FindObjectOfType<Game_SoundManager>().EffectPlay(6);
			player.SetActive(false);
			FindObjectOfType<Game_GameOverPanel>().Initial(GameResult.Clear);
		}
    }

}
