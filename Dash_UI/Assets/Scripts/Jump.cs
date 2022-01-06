using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private GameObject player; // 플레이어
    private string jDai; //점프대
    private float jHeight; // 점프 높이

    Animator playerAni;

    private void Awake()
    {
        jDai = gameObject.tag;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAni = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
			FindObjectOfType<Game_SoundManager>().EffectPlay(3);
			if (Reverse_gravity.reversed)
                jHeight = (jDai == "jLow") ? -7.8f : -13.5f;
            else
                jHeight = (jDai == "jLow") ? 7.8f : 13.5f; //낮은 점프대, 높은 점프대 점프 높이            

            if (jDai == "jLow")
                playerAni.SetTrigger("jump");
            else if (jDai == "jHigh")
                playerAni.SetTrigger("jumpH");

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, float.Parse(jHeight.ToString()));
            PlayerController.isJump = true;
        }
    }
}
