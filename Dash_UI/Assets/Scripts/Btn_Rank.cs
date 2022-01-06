using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Rank : MonoBehaviour
{
	public RankBTNType currentType;
	public Text MusicTitle;
	private static int MusicIndex = 0;

	// Start is called before the first frame update
	void Start()
    {
		MusicTitle.text = FindObjectOfType<SoundManager>().MusicList[0].name;
	}

	public void BtnClickOn()
	{
		switch (currentType)
		{
			case RankBTNType.TopMusic:
				CanvasGroupOff(FindObjectOfType<MainUI>().RankGroup[0]);

				FindObjectOfType<RankManager>().TopMusic3();
				break;
				
			case RankBTNType.Ranking:
				CanvasGroupOn(FindObjectOfType<MainUI>().RankGroup[0]);

				FindObjectOfType<RankManager>().MusicRankingAsync(MusicTitle.text);
				break;

			case RankBTNType.Arrow_Left:
				if (MusicIndex == 0) MusicIndex += FindObjectOfType<SoundManager>().MusicList.Count;
				MusicIndex += -1;
				MusicTitle.text = FindObjectOfType<SoundManager>().MusicList[MusicIndex].name;

				FindObjectOfType<RankManager>().MusicRankingAsync(MusicTitle.text);
				break;

			case RankBTNType.Arrow_Right:
				MusicIndex += 1;
				MusicIndex %= FindObjectOfType<SoundManager>().MusicList.Count;
				MusicTitle.text = FindObjectOfType<SoundManager>().MusicList[MusicIndex].name;

				FindObjectOfType<RankManager>().MusicRankingAsync(MusicTitle.text);
				break;
		}
		
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
