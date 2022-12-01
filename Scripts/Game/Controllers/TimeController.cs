using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] Text timeValue;
    [SerializeField] float time;
    public bool GameActive;

    [SerializeField] private GameObject Player;

	private void Awake()
	{
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
	}

	private void Start()
    {
        timeValue.text = time.ToString();
    }

    private void Update()
    {
        //sadece kontroller aktif ise sayaci baslat
        if(GameActive == true)
        {
            time -= Time.deltaTime;
            timeValue.text = ((int)time).ToString();
        }

        //sayac biterse kaybetme ekranı
        if (time < 0)
        {
            time = 60;
            GameActive = false;
            Player.GetComponent<PlayerController>().Die();
        }
    }
}
