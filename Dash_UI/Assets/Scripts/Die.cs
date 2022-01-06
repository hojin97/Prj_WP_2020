using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private GameObject Player; //플레이어
    private Vector2 playerPos; //플레이어 초기위치
    public static bool playerDie; //사망상태

    private void Awake()
    {
        playerDie = false;
        Player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 검색
        playerPos = Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌할 시,
        if (collision.gameObject == Player)
        {
			FindObjectOfType<Game_SoundManager>().EffectPlay(2);
			Debug.Log("Collide");
            Player.SetActive(false); //플레이어가 보이지 않음
            playerDie = true; //사망판정 내림
			FindObjectOfType<Game_GameOverPanel>().Initial(GameResult.Over);
			//Invoke("restart", 1.5f); //사망 후 1.5초 뒤에 게임 재시작
		}
    }

    private void restart()
    {
        //재시작을 위한 초기화
        playerDie = false;
        DataManager.Instance.score = 0f;

		Game_UI_Manager.time = 0;
        Player.transform.position = playerPos;
        Player.SetActive(true);
    }
}
