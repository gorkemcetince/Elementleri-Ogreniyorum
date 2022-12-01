using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround;
    public bool canMove = true;
    private bool canDoubleJump;

    private Animator myAnimator;
    [SerializeField] private GameObject arrow;
    private bool attacked;
    [SerializeField] private float currentAttackTimer;
    [SerializeField] private float defaultAttackTimer;
    [SerializeField] private int arrowNumber;
    [SerializeField] private Text arrowNumberText;

    private AudioManager audioManager;
    private LevelManager levelManager;
    private TimeController timeContoller;

	private void Awake()
	{
        audioManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
        timeContoller = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TimeController>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}

	private void Start()
    {
        attacked = false;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        arrowNumberText.text = "Kalan Ok: " + arrowNumber.ToString();
    }

    private void Update()
    {
        //eger unity editordeysek hareket w,a,s,d space olur
        #if UNITY_EDITOR
        mySpeedX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
            if (onGround == true)
            {
                audioManager.PlayClipWithPitch(1, 1.2f);
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true;
                myAnimator.SetTrigger("Jump");
            }
            else
            {
                if (canDoubleJump == true)
                {
                    audioManager.PlayClipWithPitch(1, 1.3f);
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false;
                }
            }
        }
		if (Input.GetKeyDown(KeyCode.Space))
		{
            if (attacked == false)
            {
                attacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke("Fire", 0.5f);
            }
        }
        #endif
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);

        #region hareket yonune gore yuz donmesi
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion
        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }
        if (currentAttackTimer <= 0)
        {
            attacked = false;
        }
    }

    //ok atilma butonuna basildiginda
    public void BowFire()
	{
        if (attacked == false && arrowNumber > 0)
        {
            attacked = true;
            myAnimator.SetTrigger("Attack");
            Invoke("Fire", 0.5f);
        }
    }
    //ok atma
    private void Fire()
    {
        audioManager.PlayClip(2);
        GameObject _arrow = Instantiate(arrow, transform.position, Quaternion.identity);

        if (transform.localScale.x > 0)
        {
            _arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);
        }
        else
        {
            Vector3 _arrowScale = _arrow.transform.localScale;
            _arrow.transform.localScale = new Vector3(-_arrowScale.y, _arrowScale.z);
            _arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0f);
        }
        arrowNumber--;
        arrowNumberText.text = "Kalan Ok: " + arrowNumber.ToString();
    }

    /// <summary>
    /// tusa basildiginda 1; basilmadiginda 0;
    /// </summary>
    public void Right(int value)
	{
        mySpeedX = value;
	}

    /// <summary>
    /// tusa basildiginda -1; basilmadiginda 0;
    /// </summary>
    public void Left(int value)
	{
        mySpeedX = value;
	}

    //ziplama
    public void Jump()
    {
        if (onGround == true)
        {
            audioManager.PlayClipWithPitch(1, 1.2f);
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            canDoubleJump = true;
            myAnimator.SetTrigger("Jump");
        }
        else
        {
            if (canDoubleJump == true)
            {
                audioManager.PlayClipWithPitch(1, 1.3f);
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = false;
            }
        }
    }

    //yanma durumu
    public void Die()
    {
        audioManager.PlayClipWithPitch(5,0.8f);
        myAnimator.SetTrigger("Die");

        StartCoroutine(Wait(true));
    }

    //beklet ve oyuncuyu ayarla
    public IEnumerator Wait(bool lose) //lose true ise kaybeder false ise soru sorar (levelmanager status)
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        levelManager.canPause = false;
        timeContoller.enabled = false;
        yield return new WaitUntil(() => onGround);
        yield return new WaitForSeconds(0.2f);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;

        audioManager.MusicOnOff("off");
        canMove = false;
        myAnimator.SetFloat("Speed", 0);

        yield return new WaitForSecondsRealtime(0.5f);
		if (lose)
		{
            levelManager.Status("lose");
		}
		else
		{
            levelManager.Status("question");
        }
    }




}
