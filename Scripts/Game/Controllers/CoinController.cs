using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float coinRotateSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f,coinRotateSpeed,0f));
    }
}



