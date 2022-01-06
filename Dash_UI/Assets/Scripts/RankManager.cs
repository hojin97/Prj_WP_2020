
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
	private static DatabaseReference reference;

	void Start()
	{
		// Set this before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://jumpjump-53563673.firebaseio.com/");
		reference = FirebaseDatabase.DefaultInstance.RootReference;		// ref의 시작을 별 다른 조건 안넣고 루트부터 시작
	}
	public async Task TopMusic3()		// TopMusic3 불러오는 함수
	{
		string mTitle;
		List<Score> mCount = new List<Score>();

		for (int i=0; i < FindObjectOfType<SoundManager>().MusicList.Count; i++){
			mTitle = FindObjectOfType<SoundManager>().MusicList[i].name;
			DataSnapshot ds = await reference.Child(mTitle).Child(mTitle).GetValueAsync().ContinueWith(task =>
			{
				return task.Result;
			});
			if (ds.Value == null) { }
			else
			{
				string key = ds.Key;
				double value = double.Parse(ds.Value.ToString());
				mCount.Add(new Score(key, value));
			}
        }
		SetTopMusic3(mCount);		 	
	}

	public void SetTopMusic3(List<Score> ds){
		ds.Sort(delegate(Score A, Score B)
		{
			if (A.score > B.score)
				return 1;
			else if (A.score < B.score)
				return -1;
			else
				return 0;
		});
		ds.Reverse();
		string mTitle;
		for (int i = 0; i < 3; i++)
		{
			try
			{
				mTitle = ds[i].uid;
				FindObjectOfType<MainUI>().RankList[i].text = i + 1 + "등 : " + mTitle + " : " + ds[i].score + "회";
			}
			catch (Exception e)
			{
				FindObjectOfType<MainUI>().RankList[i].text = "-";
			}
		}
	}


	public async Task CountingMusic(string mTitle)		// 카운팅 함수
	{		
		DataSnapshot ds = await reference.Child(mTitle).Child(mTitle).GetValueAsync().ContinueWith(task =>
		{
			return task.Result;
		});

		if(ds.Value == null){		// 차일드가 없는 경우 key, value push
			await reference.Child(mTitle).Child(mTitle).SetValueAsync(1);
		}
		else{
			await reference.Child(mTitle).Child(mTitle).SetValueAsync(int.Parse(ds.Value.ToString()) + 1);
		}
	}

	public async Task PushScore(string mTitle, double score)		// 점수 푸쉬 함수
	{
		bool find = false;
		DataSnapshot ds = await reference.Child(mTitle).Child("Score").OrderByValue().GetValueAsync().ContinueWith(task =>
		{
			return task.Result;
		});

		if (ds.HasChildren)
		{
			foreach (var item in ds.Children)
			{
				if(MainUI.uID.Equals(item.Key)){
					find = true;
					if(double.Parse(item.Value.ToString()) < score)		// 현재 플레이한 스코어가 더 큰 경우면 갱신하도록 설정.
						await reference.Child(SoundManager.CurrentMusic.name).Child("Score").Child(MainUI.uID).SetValueAsync(score);
				}
				if (find)
					break;
			}
		}
		if(!find)			// 첫 데이터 삽입 시
		{       
			await reference.Child(SoundManager.CurrentMusic.name).Child("Score").Child(MainUI.uID).SetValueAsync(score);
		}
	}
	
	public async Task MusicRankingAsync(string mTitle){		// 뮤직 별 Top3 받아오는 함수
		List <Score> userList = new List<Score>();

		DataSnapshot ds = await reference.Child(mTitle).Child("Score").OrderByValue().GetValueAsync().ContinueWith(task =>
		{
			return task.Result;
		});

		if (ds.HasChildren)
		{
			foreach (var item in ds.Children)
			{
				userList.Add(new Score(item.Key, double.Parse(item.Value.ToString())));
			}
		}
		SetMusicRanking(userList);
	}

	public void SetMusicRanking(List<Score> userList) {
		userList.Reverse();
	for(int i=0; i< 3; i++){
			try
			{
				FindObjectOfType<MainUI>().RankList[i].text = i+1+"등" + userList[i].uid + ", " + userList[i].score + "점";
			}catch(Exception e){
				FindObjectOfType<MainUI>().RankList[i].text = "-";
			}
		}
	}
}