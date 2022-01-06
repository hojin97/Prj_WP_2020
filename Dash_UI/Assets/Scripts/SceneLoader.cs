using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	public SceneType curruntType;
	public Text loadtext;
	public static string loadScene;
	public static int loadType;
	public Slider progressbar;
   
    private void Start()
    {
		StartCoroutine(LoadingScene());
	}

	public static void LoadSceneHandle(string _name, int _loadType){		// 0 : Main, 1 : Game
		loadScene = _name;
		loadType = _loadType;
		SceneManager.LoadScene("Loading");
		switch(_loadType){
			case 0:
				MainUI.CurrentScene = SceneType.Main;
				break;
			case 1:
				MainUI.CurrentScene = SceneType.Game;
				break;
		}
	}

	IEnumerator LoadingScene(){
		yield return null;

		AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
		try
		{
			operation.allowSceneActivation = false;
		}
		catch (Exception e)
		{
			Debug.LogError("해당 스테이지 정보가 없습니다.");
			operation = SceneManager.LoadSceneAsync("Main");
			operation.allowSceneActivation = false;
		}

		while(!operation.isDone){
			yield return null;

			if(progressbar.value < 0.9f){
				progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
			}
			else if(operation.progress >= 0.9f){
				progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
				if(MainUI.CurrentScene == SceneType.Game){
					loadtext.text = "Touch the Screen...";
				}
			}

			if(MainUI.CurrentScene != SceneType.Game){		// Game 화면으로 넘어가는 중이 아닐 때는 씬 바로 넘기기
				operation.allowSceneActivation = true;
			}
			else{
				if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0 ) && progressbar.value >= 1f && operation.progress >= 0.9f)
				{
					operation.allowSceneActivation = true;
					SoundManager.volumeUpdate = false;
				}
			}
			
		}
	}
}
