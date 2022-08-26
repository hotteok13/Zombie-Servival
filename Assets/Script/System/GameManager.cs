using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject resultScreen;
    [SerializeField] Text playTime;
    [SerializeField] Text kill;
    public int count;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playTime.text = "Play Time : " + Time.time.ToString("N2");
        kill.text = "Zombie Kill : " + count.ToString();
    }
}
