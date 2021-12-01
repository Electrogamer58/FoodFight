using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static int EnemiesKilled = 0;
    public static int Tastecoins = 50;

    [SerializeField] Text _enemyScore;
    [SerializeField] Text _tastecoins;

    // Update is called once per frame
    void FixedUpdate()
    {
        _enemyScore.text = "Enemies Killed: " + EnemiesKilled;
        _tastecoins.text = "Tastecoins: " + Tastecoins;
    }
}
