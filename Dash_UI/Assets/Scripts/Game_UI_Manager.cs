using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameBTNType
{
	Pause,
	Continue,
	Gomain,
	Replay
}

public enum GameResult{
	Clear,
	Over
}

public class Game_UI_Manager : MonoBehaviour
{
	public CanvasGroup PauseGroup;
	public CanvasGroup DefaultGroup;
	public static double time = 0;
	public Text Timer;

	public static bool pause = false;

	public CanvasGroup GameOverPanel;
	public CanvasGroup GameOverPanel_Text;
	public CanvasGroup GameOverPanel_Btn;

	public void Start()
	{
		time = 0;
		Timer.text = "Time : " + time.ToString();
	}

	public void Update()
	{
		if (pause) {}
		else{
			time += Time.deltaTime;
		}

		Timer.text = "Time : "+Math.Round(time, 2).ToString();
	}
}
