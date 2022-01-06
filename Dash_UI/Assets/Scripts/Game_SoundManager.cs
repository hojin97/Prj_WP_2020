using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_SoundManager : MonoBehaviour
{
	public List<AudioClip> EffectList = new List<AudioClip>();
	private static AudioSource[] audioSources = new AudioSource[7];
	//	0-> 버튼, 1-> 코인, 2-> 죽음, 3-> 발판, 4->승리, 5->텔레포트, 6->반전포탈

	public static AudioClip CurrentMusic;

	// BGM
	public AudioSource PlayMusic;

	//Effects
	public AudioSource PlayEffect;

	public void Awake()
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			audioSources[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
			audioSources[i].clip = EffectList[i];
			audioSources[i].loop = false;
			audioSources[i].playOnAwake = false;
		}
	}

	public void BtnEffectSound()
	{
		PlayEffect.GetComponent<AudioSource>().clip = EffectList[0];
		PlayEffect.GetComponent<AudioSource>().Play();
	}

	public void EffectPlay(int index)
	{
		audioSources[index].Play();
	}
}
