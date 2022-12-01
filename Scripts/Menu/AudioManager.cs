using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioSource source;
	[SerializeField] private AudioClip[] clips;

	[SerializeField] private AudioSource sound_source, music_source;
	private int sound, music; //0 = acik; 1=kapali;

	private void Awake()
	{
		sound = PlayerPrefs.GetInt("sound");
		music = PlayerPrefs.GetInt("music");

		#region baslangic kontrol
		if (sound == 0)
		{
			SoundOnOff("on");
		}
		else
		{
			SoundOnOff("off");
		}

		if (music == 0)
		{
			MusicOnOff("on");
		}
		else
		{
			MusicOnOff("off");
		}
		#endregion
	}

	//value = on, off degerlerini alir ve aldigi degere gore sesi ayarlar
	public void SoundOnOff(string value)
	{
		if (value == "on")
		{
			sound_source.enabled = true;
		}
		else if (value == "off")
		{
			sound_source.enabled = false;
		}
	}

	//value = on, off degerlerini alir ve aldigi degere gore muzigi ayarlar
	public void MusicOnOff(string value)
	{
		if (value == "on")
		{
			music_source.volume = 0.2f;
		}
		else if (value == "off")
		{
			music_source.volume= 0;
		}
	}

	//kayitli olan kliplerden belirtileni cal
	public void PlayClip(int index)
	{
		source.pitch = 1f;
		source.PlayOneShot(clips[index]);
	}

	//kayitli olan kliplerden belirtileni(index) belirtilen oranda(pitch) oktav degistirerek cal
	public void PlayClipWithPitch(int index, float pitch = 1f)
	{
		source.pitch = pitch;
		source.PlayOneShot(clips[index]);
	}
}
