using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public SoundManager soundManager;
	public BTNType currentType;
	public Text MusicTitle;
	public static int MusicIndex = 0;

	public Transform buttonScale;
	Vector3 defaultScale;

	public void Start()
	{
		defaultScale = buttonScale.localScale;
		MusicTitle.text = soundManager.MusicList[MusicIndex].name;
	}


	public void BtnArrowClick()
	{
		switch (currentType)
		{
			case BTNType.Arrow_Up:
				if (MusicIndex == 0) MusicIndex += soundManager.MusicList.Count;
				MusicIndex += -1;
				break;
			case BTNType.Arrow_Down:
				MusicIndex += 1;
				break;
		}
		SoundManager.CallUpdateMusic1 = true;
		MusicIndex %= soundManager.MusicList.Count;
		SoundManager.CurrentMusic = soundManager.MusicList[MusicIndex];
		SoundManager.CallUpdateMusic2 = true;
		MusicTitle.text = SoundManager.CurrentMusic.name;
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
