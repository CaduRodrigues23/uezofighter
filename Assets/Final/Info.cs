using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Info : MonoBehaviour {
    public Player p1;
    public Player p2;
    private float barWidth;

    public Text p1Name;
    public Text p1Score;
    public RectTransform p1Bar;

    public Text p2Name;
    public Text p2Score;
    public RectTransform p2Bar;
    // Use this for initialization
    void Start () {
        p1Name.text = p1.name;
        p2Name.text = p2.name;

        barWidth = p1Bar.localScale.x;
	}

    // Update is called once per frame
    void Update()
    {
        p1Score.text = FindObjectOfType<Judge>().getScore(0).ToString();
        p2Score.text = FindObjectOfType<Judge>().getScore(1).ToString();

        p1Bar.localScale = new Vector3(p1.GetHPRelative() * barWidth, p1Bar.localScale.y, 0);
        p2Bar.localScale = new Vector3(p2.GetHPRelative() * barWidth, p2Bar.localScale.y, 0);
    }
}
