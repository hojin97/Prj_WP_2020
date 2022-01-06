using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Oppo : MonoBehaviour
{
    public static bool oppo;
    private GameObject player;
    private GameObject myCam;
	private GameObject backGround;

    // Start is called before the first frame update
    private void Awake()
    {
        oppo = false;
        player = GameObject.FindGameObjectWithTag("Player");
        myCam = GameObject.FindGameObjectWithTag("MainCamera");
		backGround = GameObject.FindGameObjectWithTag("back");
    }

    //포탈 진입 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
			FindObjectOfType<Game_SoundManager>().EffectPlay(5);

			oppo = !oppo; //좌우 반전상태 전환

            //카메라 각도 반전
            Vector2 temp = myCam.transform.rotation.eulerAngles;

            temp.y += 180f;
            myCam.transform.rotation = Quaternion.Euler(temp);

			//배경의 z좌표 변경
			Vector3 bg = backGround.transform.position;
			bg.z *= -1;

			backGround.transform.position = new Vector3(bg.x, bg.y, bg.z);
		}
    }
}
