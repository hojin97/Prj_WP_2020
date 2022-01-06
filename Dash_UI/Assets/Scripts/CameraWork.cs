using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{    
    private GameObject player; // 플레이어
    private float y, z; //카메라 y z 좌표
    public static bool gimmick; // 포탈 등 통과 시

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 탐색
    }

    void Start()
    {
        gimmick = false; // 초기값 거짓
        player.SetActive(true); // 게임 시작        
        y = player.transform.position.y + 1.65f;
        z = -10f;
    }

    void Update()
    {
        z = (Portal_Oppo.oppo) ? 10f : -10f; //좌우 반전에 따라 카메라 z값 설정

        if (!PlayerController.isJump)
        {
            //중력 반전 여부에 따라 점프 방향 다름
            y = player.transform.position.y + ((Reverse_gravity.reversed) ? -1.65f : 1.65f);
        }

        //플레이어가 중력반전 혹은 순간이동 할 때
        else if (PlayerController.isJump && gimmick)
        {
            //중력 전환 중 각 카메라 워크
            if (Reverse_gravity.reversed)
            {
                if (player.transform.position.y >= Camera.main.transform.position.y + 1.65f)
                {
                    y = player.transform.position.y - 1.65f;
                    Camera.main.transform.localPosition = new Vector3(player.transform.position.x + 3.25f, y, z);
                }
            }
            else if (!Reverse_gravity.reversed)
            {
                if (player.transform.position.y <= Camera.main.transform.position.y - 1.65f)
                {
                    y = player.transform.position.y + 1.65f;
                    Camera.main.transform.localPosition = new Vector3(player.transform.position.x + 3.25f, y, z);
                }
            }

            //순간이동 시 카메라 워크
            if (Portal_teleport.moved)
            {
                y = player.transform.position.y + ((Reverse_gravity.reversed) ? -1.65f : 1.65f);

                Camera.main.transform.localPosition = new Vector3(player.transform.position.x + 3.25f, y, z);
            }
        }

        //전체적인 좌표 변경 후 프레임마다 카메라 좌표 결정
        Camera.main.transform.localPosition = new Vector3(player.transform.position.x + 3.25f, y, z);
    }
}