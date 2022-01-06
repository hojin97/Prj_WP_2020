using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public BTNType currentType;
	public MainUI mainUI;

	public Transform buttonScale;
	Vector3 defaultScale;


	private void Start()
	{
		defaultScale = buttonScale.localScale;
	}

	public void OnBtnClick(){
		switch(currentType){
			case BTNType.Game:
				Debug.Log("게임시작");
				SceneLoader.LoadSceneHandle(SoundManager.CurrentMusic.name, 1);

				break;

			case BTNType.Option:
				Debug.Log("옵션");
				MainUI.CurrentScene = SceneType.Option;
				MainUI.changeGroup(mainUI.GroupList[1]);
				CanvasGroupOff(MainUI.prevGroup);
				CanvasGroupOn(MainUI.curruntGroup);
				break;

			case BTNType.Back:
				Debug.Log("뒤로가기");
				MainUI.changeGroup(mainUI.GroupList[0]);
				CanvasGroupOff(MainUI.prevGroup);
				CanvasGroupOn(MainUI.curruntGroup);
				break;

			case BTNType.Rank:
				Debug.Log("랭크 화면");
				MainUI.CurrentScene = SceneType.Rank;
				MainUI.changeGroup(mainUI.GroupList[2]);
				CanvasGroupOff(MainUI.prevGroup);
				CanvasGroupOn(MainUI.curruntGroup);
				FindObjectOfType<RankManager>().TopMusic3();			// 랭크 화면 전환 시 TopMusic3이 먼저 보이게 함.
				break;

		}
	}

	public void CanvasGroupOn(CanvasGroup cg){
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		buttonScale.localScale = defaultScale * 1.2f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonScale.localScale = defaultScale;
	}
}
