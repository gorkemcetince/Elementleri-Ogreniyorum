using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] private GameObject pointsParent;
	private GameObject[] points;
	private int pointCounter = 0;
	private bool way, takeOne = true;
	private Vector3 distance;
	[SerializeField] private float speed;

	private void Start()
	{
		//Belirtilen parent objesinin alt-objeleri(point) al
		points = new GameObject[pointsParent.transform.childCount];

		for (int i = 0; i < pointsParent.transform.childCount; i++)
		{
			points[i] = pointsParent.transform.GetChild(i).gameObject;
		}
	}


	private void FixedUpdate()
	{
		//pointler arasinda hareket et
		if (takeOne)
		{
			distance = (points[pointCounter].transform.position - transform.position).normalized;
			takeOne = false;
		}
		float distance2 = Vector3.Distance(transform.position, points[pointCounter].transform.position);
		transform.position += distance * Time.deltaTime * speed;
		if (distance2 < 0.5f)
		{
			takeOne = true;
			if (pointCounter == points.Length - 1)
			{
				way = false;
			}
			else if (pointCounter == 0)
			{
				way = true;
			}

			if (way)
			{
				pointCounter++;
			}
			else
			{
				pointCounter = 0;
			}
		}
	}
}
