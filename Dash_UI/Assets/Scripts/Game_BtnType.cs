using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Game_BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameBTNType currentType;

	public Transform buttonScale;
	Vector3 defaultScale;

	private void Start()
	{
		defaultScale = buttonScale.localScale;
	}

	public void BtnOnClick(){
		switch(currentType){
			case GameBTNType.Pause:
				Pause(true);
				break;
			case GameBTNType.Continue:
				Pause(false);
				break;
			case GameBTNType.Gomain:
				Time.timeScale = 1f;
				Game_UI_Manager.pause = false;
				SceneLoader.LoadSceneHandle("Main", 0);
				break;
			case GameBTNType.Replay:
				Game_UI_Manager.pause = false;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				break;
		}
	}

	public void Pause(bool type)
	{
		if(type){
			CanvasGroupOff(FindObjectOfType<Game_UI_Manager>().DefaultGroup);
			CanvasGroupOn(FindObjectOfType<Game_UI_Manager>().PauseGroup);
			Time.timeScale = 0f;
		}
		else{
			CanvasGroupOn(FindObjectOfType<Game_UI_Manager>().DefaultGroup);
			CanvasGroupOff(FindObjectOfType<Game_UI_Manager>().PauseGroup);
			Time.timeScale = 1f;
		}

		Game_UI_Manager.pause = type;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buttonScale.localScale = defaultScale * 1.2f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonScale.localScale = defaultScale;
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
