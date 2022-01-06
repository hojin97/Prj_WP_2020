using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse_gravity : MonoBehaviour
{
    private GameObject player; // 플레이어
    private Rigidbody2D myRigid; //플레이어 물리
    private GameObject mainCam; // 메인카메라
    public static bool reversed; // 중력 반전 여부
    private bool enable; //발판 사용가능 여부

    private void Awake()
    {
        enable = true;
        reversed = false;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        myRigid = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enable)
        {
            enable = false;
            PlayerController.isJump = true;
			FindObjectOfType<Game_SoundManager>().EffectPlay(3);

			if (collision.gameObject == player)
                player.transform.localEulerAngles = (!reversed) ? new Vector2(180f, 0f) : new Vector2(0f, 0f);

            reversed = !reversed;
            PlayerController.posChanged = true;
            myRigid.gravityScale *= -1;
            PlayerController.jHeight *= -1;
            CameraWork.gimmick = true;
        }
    }
}
