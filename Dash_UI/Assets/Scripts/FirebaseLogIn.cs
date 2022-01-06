using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class FirebaseLogIn : MonoBehaviour
{
	static FirebaseAuth auth;


	// Start is called before the first frame update
	void Start()
    {
		auth = FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void LogInGoogle()
	{
		string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();   // 구글 아이디 토큰 받아와서

		Firebase.Auth.Credential credential =
			Firebase.Auth.GoogleAuthProvider.GetCredential(idToken, null);      // 크레덴셜 비교.

		auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
		{
			if (task.IsCanceled)
			{
				return;
			}
			if (task.IsFaulted)
			{
				return;
			}
		});
	}

	public void LogInAnonymous(){
		auth.SignInAnonymouslyAsync().ContinueWith(task => {
			if (task.IsCanceled)
			{
				Debug.LogError("SignInAnonymouslyAsync was canceled.");
				return;
			}
			if (task.IsFaulted)
			{
				Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
				return;
			}
		});
	}
}
