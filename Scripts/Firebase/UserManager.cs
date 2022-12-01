using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Database;
*/
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour
{
	/*
	FirebaseAuth auth;
	DatabaseReference reference;

	public bool loaded = false;
	public User userData;

	private void Awake()
	{
		loaded = false;
		auth = FirebaseAuth.DefaultInstance;
		//eger aktif kullanici yoksa giris ekranina git
		if (auth.CurrentUser == null)
		{
			SceneManager.LoadScene(0);
		}
		//aktif kullanici varsa referans olusturup verileri cek
		else
		{
			reference = FirebaseDatabase.DefaultInstance.RootReference;
			GetData();
		}
	}

	//verileri cek
	private void GetData()
	{
		reference.Child("Users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWithOnMainThread(task => {
			if (task.IsFaulted || task.IsCanceled)
			{
				Debug.LogError("Basarisiz");
			}
			else if (task.IsCompleted)
			{
				DataSnapshot snapshot = task.Result;
				if (snapshot.GetRawJsonValue() == null)
				{
					CreateEmptyUser();
					GetData();
				}
				else
				{
					userData = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
					loaded = true;
				}
			}
		});
	}

	//Skor degerini al
	public int GetScore()
	{
		return userData.Score;
	}

	//Level degerini al
	public int GetLevel()
	{
		return userData.Level;
	}

	//Kullanici idsini al
	public string GetUserId()
	{
		return auth.CurrentUser.UserId;
	}

	//Bos kullanici olustur
	private void CreateEmptyUser()
	{
		User user = new User
		{
			Score = 0,
			Level = 0
		};

		string emptyJson = JsonUtility.ToJson(user);
		reference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(emptyJson);
	}


	//Skoru guncelle
	public void UpdateScore(int value)
	{
		GetData();
		value += userData.Score;
		reference.Child("Users").Child(auth.CurrentUser.UserId).Child("Score").SetValueAsync(value);
		GetData();
	}

	//Leveli guncelle
	public void UpdateLevel(int value)
	{
		reference.Child("Users").Child(auth.CurrentUser.UserId).Child("Level").SetValueAsync(value);
		GetData();
	}

	//Oturumu kapat
	public void LogOut()
	{
		auth.SignOut();
		SceneManager.LoadScene(0);
	}
	*/
}

//Kullanici
public class User
{
	public int Score;
	public int Level;
}