using System.Collections;
using System.Collections.Generic;


using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserAuth : MonoBehaviour
{

	[SerializeField] private Animator transitionPanel;

	[Header("Login")]
	[SerializeField] private InputField loginMail;
	[SerializeField] private InputField loginPassword;

	[Header("Register")]
	[SerializeField] private InputField registerMail;
	[SerializeField] private InputField registerPassword;

	
	//dogrulama yapilacak degisken
	FirebaseAuth auth;

	private void Awake()
	{
		//referans olusturuyoruz
		auth = FirebaseAuth.DefaultInstance;
	}

	private void Start()
	{
		auth.StateChanged += AuthStateChange;

		//eger referansta mevcut bir kullanici varsa direk oyun sahnesine gitsin
		if (auth.CurrentUser != null)
		{
			StartCoroutine(GoGame());
		}
	}

	//dogrulama durumu degistiginde (giris veya kayit islemleri yapildiginda) otomatik olarak oyun sahnesine gitsin
	private void AuthStateChange(object sender, System.EventArgs eventArgs)
	{
		if (auth.CurrentUser != null)
		{
			StartCoroutine(GoGame());
		}
	}

	//kullanici kayit
	#region Register
	public void Register()
	{
		if (CheckRegister())
		{
			auth.CreateUserWithEmailAndPasswordAsync(registerMail.text, registerPassword.text).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
					Debug.LogError("Iptal Edildi");
					return;
				}
				if (task.IsFaulted)
				{
					Debug.LogError("CreateUserWithEmailAndPasswordAsync hata verdi: " + task.Exception);
					return;
				}

				Firebase.Auth.FirebaseUser newUser = task.Result;
				Debug.LogFormat("Kullanici basariyla olusturuldu: {0} ({1})", newUser.DisplayName, newUser.UserId);
			});
		}
		else
		{
			Debug.LogError("Lutfen alanlari bos birakmayin");
		}
	}

	//kayit ui elementleri icin kontrol
	private bool CheckRegister()
	{
		if (registerMail.text == null || registerMail.text == "" || registerPassword.text == null || registerPassword.text == "")
		{
			return false;
		}
		return true;
	}
	#endregion


	//kullanici giris
	#region Login
	public void Login()
	{
		if (CheckLogin())
		{
			auth.SignInWithEmailAndPasswordAsync(loginMail.text, loginPassword.text).ContinueWith(task => {
				if (task.IsCanceled)
				{
					Debug.LogError("Iptal Edildi");
				}
				if (task.IsFaulted)
				{
					Debug.LogError("SignInWithEmailAndPasswordAsync hata verdi: " + task.Exception);
				}

				Firebase.Auth.FirebaseUser newUser = task.Result;
				Debug.LogFormat("Kullanici basariyla giris yapti: {0} ({1})", newUser.DisplayName, newUser.UserId);
				StartCoroutine(GoGame());

			});
		}
	}

	//giris ui elementleri icin kontrol
	private bool CheckLogin()
	{
		if (loginMail.text == null || loginMail.text == "" || loginPassword.text == null || loginPassword.text == "")
		{
			return false;
		}
		return true;
	}
	#endregion

	//oyundan cik
	public void ExitGame()
	{
		Application.Quit();
	}

	//oyun sahnesine git
	private IEnumerator GoGame()
	{
		Time.timeScale = 1f;
		transitionPanel.SetTrigger("end");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(1);
	}
	

}
