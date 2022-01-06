using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BTNType
{
	Game,
	Option,
	Rank,
	Back,
	Arrow_Up,
	Arrow_Down
}

public enum RankBTNType{
	TopMusic,
	Ranking,
	Arrow_Left,
	Arrow_Right
}

public enum SceneType{
	Main,
	Option,
	Game,
	Rank,
	Game_
}

public class MainUI : MonoBehaviour
{
	public static SceneType CurrentScene;

	public List<CanvasGroup> GroupList = new List<CanvasGroup>();   // 0 : main, 1 : option, 2: rank
	public List<CanvasGroup> RankGroup = new List<CanvasGroup>();	// 0 : Ranking

	public static CanvasGroup curruntGroup;
	public static CanvasGroup prevGroup;
	public static string uID;

	public Text UserName;

	// 랭킹 구현용
	public List<Text> RankList = new List<Text>();      // Top 3 뮤직 구현용

	public void Start()
	{
		SetUserName();
		if(GroupList.Count != 0){
			curruntGroup = GroupList[0];
			prevGroup = GroupList[0];
		}
		
	}

	public static void changeGroup(CanvasGroup cg){
		prevGroup = curruntGroup;
		curruntGroup = cg;
	}

	public void SetUserName(){
		UserName.text = uID + "님 반갑습니다!";
	}
}
