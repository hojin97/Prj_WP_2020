using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
	public List<AudioClip> MusicList = new List<AudioClip>();
	public List<AudioClip> EffectList = new List<AudioClip>();

	public static AudioClip CurrentMusic;

	// BGM
	public AudioSource PlayMusic;
	public Slider BGMSlider;
	public static float BGMVolume = 0.5f;

	//Effects
	public AudioSource PlayEffect;
	public Slider EffectSlider;
	public static float EffectVolume = 0.5f;

	public static bool volumeUpdate;
	public static bool CallUpdateMusic1, CallUpdateMusic2;      // music flag

	public void Start()
	{
		if (CurrentMusic == null)
		{
			CurrentMusic = MusicList[0];
			PlayMusic.gameObject.GetComponent<AudioSource>().clip = CurrentMusic;
			PlayMusic.gameObject.GetComponent<AudioSource>().Play();
		}
		else
		{
			PlayMusic.gameObject.GetComponent<AudioSource>().clip = CurrentMusic;
			PlayMusic.gameObject.GetComponent<AudioSource>().Play();
		}
	}

	public void BtnEffectSound()
	{
		PlayEffect.GetComponent<AudioSource>().clip = EffectList[0];
		PlayEffect.GetComponent<AudioSource>().Play();
	}

	public void SetVolumeBgm(float volume)
	{
		PlayMusic.volume = volume;
		BGMVolume = volume;
	}

	public void SetVolumeEffect(float volume)
	{
		PlayEffect.volume = volume;
		EffectVolume = volume;
	}

	public void Update()
	{
		if(MainUI.CurrentScene == SceneType.Game)
        {
			volumeUpdate = false;
        }
        if (!volumeUpdate)
        {
			PlayMusic.volume = BGMVolume;
			PlayEffect.volume = EffectVolume;
			volumeUpdate = true;
			MainUI.CurrentScene = SceneType.Game_;
		}
		if(BGMSlider != null && EffectSlider != null){
			BGMSlider.value = BGMVolume;
			EffectSlider.value = EffectVolume;
		}

		if (CallUpdateMusic1)
		{
			PlayMusic.gameObject.GetComponent<AudioSource>().Stop();
			CallUpdateMusic1 = false;
		}
		if (CallUpdateMusic2)
		{
			PlayMusic.gameObject.GetComponent<AudioSource>().clip = CurrentMusic;
			PlayMusic.gameObject.GetComponent<AudioSource>().Play();
			PlayMusic.volume = SoundManager.BGMVolume;
			PlayEffect.volume = SoundManager.EffectVolume;
			CallUpdateMusic2 = false;
		}
	}
}
