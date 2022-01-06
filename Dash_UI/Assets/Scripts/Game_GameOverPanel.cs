using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_GameOverPanel : MonoBehaviour
{
	public Text Title;
	public Text Text_Coin;
	public Text Text_Timer;
	public Text Text_Score;
	public Text Text_TouchScreen;

	private double final = 0;

	private bool isGameOver = false;
	private bool isUpdate = false;

	private double panelTimer;

	private void Update()
	{
		if(panelTimer == 0)
			Text_TouchScreen.text = "Touch the Screen... (2)";
		else if (0.05 < panelTimer && panelTimer <= 1)
			Text_TouchScreen.text = "Touch the Screen... (1)";
		else if (1 < panelTimer && panelTimer < 2)
			Text_TouchScreen.text = "Touch the Screen... (0)";
		else
			Text_TouchScreen.text = "Touch the Screen...";
		
		

		panelTimer += Time.deltaTime;

		if(panelTimer > 2 && isGameOver && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
		{
			CanvasGroupOff(FindObjectOfType<Game_UI_Manager>().GameOverPanel_Text);
			CanvasGroupOn(FindObjectOfType<Game_UI_Manager>().GameOverPanel_Btn);
			if(!isUpdate){
				Coin.coinCount = 0;
				isUpdate = true;
				UpdateScore();
			}
		}
	}

	public void UpdateScore(){
		RankManager rm = new RankManager();
		rm.CountingMusic(SoundManager.CurrentMusic.name);
		rm.PushScore(SoundManager.CurrentMusic.name, Math.Round(final, 2));
	}

	public void Initial(GameResult type){
		if(type == GameResult.Clear){
			Title.text = "게 임 성 공";
		}
		else if(type == GameResult.Over){
			Title.text = "게 임 실 패";
		}
		isGameOver = true;
		Game_UI_Manager.pause = true;
		panelTimer = 0;

		CanvasGroupOn(FindObjectOfType<Game_UI_Manager>().GameOverPanel);
		CanvasGroupOff(FindObjectOfType<Game_UI_Manager>().DefaultGroup);

		CountScore();
		Text_Coin.text = Coin.coinCount.ToString() + " 개";
		Text_Timer.text = Math.Round(Game_UI_Manager.time, 2).ToString() + " 초";
		Text_Score.text = final.ToString() + " 점";
	}

	public void CountScore(){
		final = 10 * Game_UI_Manager.time * (1 + (0.05 * Coin.coinCount));
	}

	public void CanvasGroupOn(CanvasGroup cg)
	{
		cg.alpha = 1;
		cg.interactable = true;
		cg.blocksRaycasts = true;
	}

	public void CanvasGroupOff(CanvasGroup cg)
	{
		cg.alpha = 0;
		cg.interactable = false;
		cg.blocksRaycasts = false;
	}
}
