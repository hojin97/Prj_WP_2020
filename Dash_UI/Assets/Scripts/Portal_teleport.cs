using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_teleport : MonoBehaviour
{
    private GameObject player; //플레이어
    private GameObject myCam; //카메라
    public Transform dest; //순간이동 목표 지점
    public static bool moved; // 순간이동 여부

    private void Awake()
    {
        moved = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
			FindObjectOfType<Game_SoundManager>().EffectPlay(4);
			player.transform.position = dest.localPosition; //목표 지점으로 이동.
            PlayerController.isJump = true;
            PlayerController.posChanged = true;
            CameraWork.gimmick = true;
            moved = true;
        }
    }
}
