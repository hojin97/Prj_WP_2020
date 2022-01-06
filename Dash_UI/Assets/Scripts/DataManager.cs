using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }

    public bool gameClear; //클리어 여부
    public float score; //현재 점수
    public float highScore; //최고 점수

    private void Awake() // 점수 및 최고점수 관리 스크립트
    {
        // 값 초기화
        gameClear = false;
        score = 0f;
        highScore = PlayerPrefs.GetFloat("hScore");

        if (!instance)
        {
            // Scene이 바뀌어도 유지되어야 하여 아래 메서드 사용.
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            // DataManager의 중복 생성 방지. 아래 코드를 절대 지우지 마시오.
            DestroyObject(gameObject);
        }
    }
}
