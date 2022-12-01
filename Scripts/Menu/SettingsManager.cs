using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
	[SerializeField] private Image btn_Sound, btn_Music;
	[SerializeField] private Sprite[] spr_Sound, spr_Music;
	private AudioManager audioManager;

	private void Awake()
	{
		audioManager = GetComponent<AudioManager>();

		#region baslangic kontrol
		if (PlayerPrefs.GetInt("sound") == 0)
		{ 
			btn_Sound.sprite = spr_Sound[0]; 
		}
		else 
		{ 
			btn_Sound.sprite = spr_Sound[1]; 
		}

		if (PlayerPrefs.GetInt("music") == 0)
		{
			btn_Music.sprite = spr_Music[0];
		}
		else
		{
			btn_Music.sprite = spr_Music[1];
		}
		#endregion
	}

	#region Ses butonu ve durumunu degistir
	public void SoundChange()
	{
		int sound = PlayerPrefs.GetInt("sound");
		if (sound == 0)
		{
			PlayerPrefs.SetInt("sound", 1);
			audioManager.SoundOnOff("off");
			btn_Sound.sprite = spr_Sound[1];
		}
		else
		{
			PlayerPrefs.SetInt("sound", 0);
			audioManager.SoundOnOff("on");
			btn_Sound.sprite = spr_Sound[0];
		}
	}
	#endregion

	#region Muzik butonu ve durumunu degistir
	public void MusicChange()
	{
		int music = PlayerPrefs.GetInt("music");
		if (music == 0)
		{
			PlayerPrefs.SetInt("music", 1);
			audioManager.MusicOnOff("off");
			btn_Music.sprite = spr_Music[1];
		}
		else
		{
			PlayerPrefs.SetInt("music", 0);
			audioManager.MusicOnOff("on");
			btn_Music.sprite = spr_Music[0];
		}
	}
	#endregion
}
