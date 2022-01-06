using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceMove : MonoBehaviour
{
	private GameObject player; //플레이어
	public static Vector2 macePos; //장애물 초기위치
	private float x, y;
	private float heightScale;
	public float xGap = 5f;
	public float reversGravityScale = -5.2f;
	public float gravityScale = 1.2f;

	private void Awake()
	{
		// 초기화
		player = GameObject.FindGameObjectWithTag("Player");
		macePos = this.gameObject.transform.position;
		x = macePos.x;
		y = macePos.y;
		heightScale = Mathf.Abs(((1000 + y) - (1000 + player.transform.position.y)) / 1.7f);
	}

	void Update()
	{
		//일정 이상 접근 시, 작동
		if (player.transform.position.x > gameObject.transform.position.x - xGap)
		{
			if ((1000 + y) - (1000 + gameObject.transform.position.y) > heightScale) // 장애물 일정 이상 낙하시
			{
				gameObject.GetComponent<Rigidbody2D>().gravityScale = reversGravityScale; //역중력
			}
			else
			{
				gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale; //중력 작용
			}
		}
		else // 접근 전 까지는 동작 안함
		{
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
		}

		if (Die.playerDie)
		{
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			this.gameObject.transform.position = new Vector2(x, y);
		}
	}
}