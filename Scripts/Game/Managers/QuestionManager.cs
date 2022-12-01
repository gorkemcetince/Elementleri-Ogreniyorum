using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
	public enum Options { A = 0, B = 1, C = 2, D = 3 }

    [SerializeField] private Image[] img_Options; //0 = A; 1 = B; 2 = C; 3 = D;
    [SerializeField] private Sprite[] spr_Options; //0 = isaretlenmemis; 1 = isaretlenmis;
	[SerializeField] private Button btn_Answer;
	[SerializeField] private Options true_answer;

	private LevelManager levelManager;
	private int choosen = -1; //secilmemis

	private void Awake()
	{
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}

	private void Update()
	{
		if (choosen == -1)
		{
			btn_Answer.interactable = false;
		}
		else
		{
			btn_Answer.interactable = true;
		}
	}

	//Isaretle
	public void Mark(int index) //0 = A; 1 = B; 2 = C; 3 = D;
    {
		if (choosen != index)
		{
			UnmarkAll();
			img_Options[index].sprite = spr_Options[1];
			choosen = index;
		}
		else
		{
			UnmarkAll();
		}
	}

	//Hepsinin isaretini kaldir
    public void UnmarkAll()
	{
		foreach (var img in img_Options)
		{
			img.sprite = spr_Options[0];
			choosen = -1;
		}
	}

	//Cevapla butonu
	public void Answer()
	{
		if (choosen == (int)true_answer)
		{
			levelManager.Status("win");
		}
		else
		{
			levelManager.Status("lose");
		}
		gameObject.SetActive(false);
	}
}
