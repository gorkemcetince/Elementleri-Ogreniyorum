using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	//Paneller
	[SerializeField] private GameObject MenuPanel, SettingsPanel, LevelsPanel;
	[SerializeField] private Animator transitionPanel;

	//Level Button olusturmak icin
	[SerializeField] private int Size;
	[SerializeField] private GameObject[] Levels;
	[SerializeField] private Sprite[] spr_LevelButton;
	[SerializeField] private Text scoreValue;
	private int SavedLevel;

	private UserManager userManager;

	private void Start()
	{
		userManager = gameObject.GetComponent<UserManager>();
		//print(userManager.GetUserId());
		//StartCoroutine(GetValues());
	}

	/*
	//Degerleri al
	private IEnumerator GetValues()
	{
		yield return new WaitUntil(() => userManager.loaded);
		SavedLevel = userManager.GetLevel();
		scoreValue.text = userManager.GetScore().ToString();
		EditLevels();
	}
	*/

	#region Panellerin acilip kapanmasi
	public void PlayGame()
	{
		MenuPanel.SetActive(false);
		SettingsPanel.SetActive(false);
		LevelsPanel.SetActive(true);
	}
	public void Settings()
	{
		MenuPanel.SetActive(false);
		SettingsPanel.SetActive(true);
		LevelsPanel.SetActive(false);
	}
	public void Back()
	{
		MenuPanel.SetActive(true);
		SettingsPanel.SetActive(false);
		LevelsPanel.SetActive(false);
	}
	#endregion

	//Bolumleri ayarla
	public void EditLevels()
	{
		for (int i = 0; i < Levels.Length; i++)
		{
			int x = new int();
			x = i;
			Levels[i].GetComponent<Button>().onClick.AddListener(delegate {LoadScene(x + 2); });
		}
		if (SavedLevel == 0)
		{
			UncompletedLevel(0);
			for (int i = 1; i < Levels.Length; i++)
			{
				LockLevel(i);
			}
		}
		else
		{
			for (int i = 0; i < SavedLevel; i++)
			{
				CompletedLevel(i);
			}
			if (SavedLevel != 7)
			{
				UncompletedLevel(SavedLevel);
				for (int i = SavedLevel + 1; i < Levels.Length; i++)
				{
					LockLevel(i);
				}
			}
		}
	}

	//tamamlanmis bolum
	public void CompletedLevel(int index)
	{
		Levels[index].GetComponent<Image>().sprite = spr_LevelButton[2];
	}

	//tamamlanmamis bolum
	private void UncompletedLevel(int index)
	{
		Levels[index].GetComponent<Image>().sprite = spr_LevelButton[1];
	}

	//kilitli bolum
	private void LockLevel(int index)
	{
		Levels[index].GetComponent<Button>().interactable = false;
		Levels[index].GetComponent<Image>().sprite = spr_LevelButton[0];
		Levels[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
	}

	//Sahne yukle
	public void LoadScene(int No)
	{
		StartCoroutine(LoadSceneEnumerator(No));
	}

	private IEnumerator LoadSceneEnumerator(int index)
	{
		Time.timeScale = 1f;
		transitionPanel.SetTrigger("end");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(index);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
