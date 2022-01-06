using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool posChanged; // 위치 변경 여부
    public static float speed; //속도
    public static float jHeight; // 플레이어 점프 높이
    public static bool isJump; // 현재 점프 중인가?
    private Rigidbody2D playerRigid; // 플레이어 물리엔진

    Animator playerAni; //플레이어 애니메이션
    
    private void Awake()
    {
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        playerAni = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        speed = 2.7f;
        jHeight = 7.8f;
        isJump = false;
    }

	// Update is called once per frame
	void Update()
	{

		// 공중에 떠 있을 시, 점프 불가
		if (playerRigid.velocity.y != 0)
		{
			isJump = true;
		}

		if (!DataManager.Instance.gameClear && !Die.playerDie)
		{
			// 게임 안 끝난 상태
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}
		else
		{
			// 게임 끝
			speed = 0.0f;
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}

		// 중력 반전 시, 이동
		if (posChanged)
		{
			if (isJump)
			{
				float updown = (Reverse_gravity.reversed) ? 2.7f : -2.7f;
				transform.Translate(new Vector2(0f, updown) * Time.deltaTime);
			}
		}

		//Spacebar 혹은 휴대폰 스크린 터치로 점프
		if (((Input.touchCount > 0) || Input.GetMouseButtonDown(0)) && !isJump)
		{
			playerRigid.AddForce(Vector2.up * jHeight, ForceMode2D.Impulse);
			playerAni.SetTrigger("jump");
			isJump = true;
		}
	}

	//바닥 착지 시
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJump = false;
            posChanged = false;
            CameraWork.gimmick = false;
            Portal_teleport.moved = false;
        }
    }
}
