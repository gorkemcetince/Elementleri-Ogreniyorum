using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrawGUI : MonoBehaviour
{
	//Eger unity editordeysek noktalar arasinda cizgi olustur
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
		}
		for (int i = 0; i < transform.childCount - 1; i++)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
		}
	}
#endif
}
