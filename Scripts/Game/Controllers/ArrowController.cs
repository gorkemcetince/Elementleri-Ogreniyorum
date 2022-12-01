using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    private AudioManager audioManager;

	private void Awake()
	{
        audioManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
			if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Platform")) //eger ok baska yerlere carparsa
			{
                audioManager.PlayClip(3);
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Enemy")) //eger ok dusmana carparsa
            {
                audioManager.PlayClip(3);
                Destroy(gameObject);
                Destroy(collision.gameObject);
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().AddScore(100);
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
            }
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
