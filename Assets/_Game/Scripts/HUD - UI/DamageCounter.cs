using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] public Text _text;

    Vector3 target;
    Vector3 spawn;
    private void Awake()
    {
        spawn = new Vector3(transform.position.x, transform.position.y);
        target = new Vector3(600, 600);
        _text = GetComponent<Text>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = spawn + target;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);
        transform.position = smoothedPosition;
        DelayHelper.DelayAction(this, Destroy, 1f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
