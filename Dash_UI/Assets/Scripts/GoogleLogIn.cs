using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleLogIn : MonoBehaviour
{
	PlayGamesUserProfile user;
	static bool isLogIn = false;
	// Start is called before the first frame update
    void Start()
    {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
		.RequestIdToken()
		.Build();

		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		if (!isLogIn)
		{
			SignIn();
			isLogIn = true;
		}
    }

    public void SignIn(){
		Social.localUser.Authenticate(sucess => {
			if (sucess)		// 로그인 성공
			{
				MainUI.uID =  Social.localUser.userName;
				FindObjectOfType<MainUI>().SetUserName();
			}
			else
			{
				MainUI.uID = "User" + Random.Range(0, 99999).ToString();          // 랜덤 UID 넣어주는 부분
				FindObjectOfType<MainUI>().SetUserName();
			}

		});
	}

	public void SignOut(){
		if (Social.localUser.authenticated)
			((PlayGamesPlatform)Social.localUser).SignOut();
	}
}
