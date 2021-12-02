using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(speed, speed, speed) * Time.deltaTime);
    }
}
