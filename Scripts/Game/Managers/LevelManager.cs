using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Text scoreValueText;
    [SerializeField] private GameObject tutorialPanel,pausePanel;
    [SerializeField] private GameObject winPanel,losePanel, questionPanel;
    [SerializeField] private Text[] scoreTexts;
    [SerializeField] private Animator transitionPanel;
    [SerializeField] private PlayerController Player;
    private TimeController timeController;
    private bool paused;
    public bool canPause = false;
    private AudioManager audioManager;
    private UserManager userManager;
    private int scoreValue = 0;

	private void Awake()
	{
        userManager = gameObject.GetComponent<UserManager>();
        timeController = gameObject.GetComponent<TimeController>();
        audioManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
        Player.enabled = false;
        Player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        tutorialPanel.SetActive(true);
	}

    //eger unity editordeyken escape tusuna basarsa durdur
#if UNITY_EDITOR
    public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            Pause();
		}
	}
#endif

    //Oyunu durdur
    public void Pause()
	{
		if (canPause)
		{
            if (paused)
            {
                Resume();
            }
            else
            {
                Player.enabled = false;
                paused = true;
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
        }
    }

    //Tutorial Paneli Tamam butonu
	public void TutorialOK()
	{
        timeController.GameActive = true;
        Player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Player.enabled = true;
        tutorialPanel.SetActive(false);
        canPause = true;
	}

    //Devam et
    public void Resume()
    {
        Player.enabled = true;
        paused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    //Ana menuye don
    public void Home()
	{
        StartCoroutine(LoadScene(1));
    }

    //Bir sonraki sahneyi yukle
	public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadScene(index));
    }
    
    //Mevcut sahneyi yeniden baslat
    public void Restart()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(index));
    }

    //Sahne yukle(index)
    private IEnumerator LoadScene(int index)
    {
        Time.timeScale = 1f;
        transitionPanel.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }

    //Skor arttir
    public void AddScore(int score)
    {
        scoreValue += score;
        scoreValueText.text = scoreValue.ToString();
		foreach (var text in scoreTexts)
		{
            text.text = scoreValue.ToString();
		}
    }


    /// <summary>
    /// Value = win , lose, question
    /// </summary>
    public void Status(string value) //Status oyuncunun durumunu degistirir. Summaryde belirtilen degerleri alir.
	{
        if (value == "win")//kazanma
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            /*
            int savedLevel = userManager.GetLevel();
			if (savedLevel< sceneIndex)
			{
                userManager.UpdateLevel(sceneIndex);
            }
            userManager.UpdateScore(scoreValue);
            */
            audioManager.PlayClip(7);
            winPanel.SetActive(true);
        }
        if (value == "lose")//kaybetme
        {
            audioManager.PlayClip(6);
            losePanel.SetActive(true);
        }
        if (value == "question")//soru
        {
            audioManager.PlayClip(9);
            questionPanel.SetActive(true);
        }
    }

}