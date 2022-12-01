using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
	private LevelManager levelManager;
	private PlayerController controller;
	private AudioManager audioManager;

	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
		controller = GetComponent<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//Oyuncu coine degerse
		if (col.CompareTag("Coin"))
		{
			Destroy(col.gameObject);
			audioManager.PlayClip(4);
			levelManager.AddScore(100);
		}
		//Oyuncu bitis rozetine degerse
		else if (col.CompareTag("Finish"))
		{
			Destroy(col.gameObject);
			StartCoroutine(controller.Wait(false));
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		//Oyuncu dusman veya dikene degerse
		if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Spike"))
		{
			controller.Die();
		}
	}
}
