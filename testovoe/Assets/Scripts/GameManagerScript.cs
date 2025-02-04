using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    [SerializeField] private List<RectTransform> ropes;
    [SerializeField] private List<RectTransform> knots;

    [SerializeField] private GameObject windowWin;

    [SerializeField] private TextMeshProUGUI textScore;

    [SerializeField] private int scoreWin;

    private void Awake() => Instance = this;

    private int score = 0;

    private void Start()
    {
        AllRopeAdaptation();
    }

    public void AllRopeAdaptation()
    {
        for (int i = 0; i < ropes.Count; i++)
        {
            var ropeScript = ropes[i].GetComponent<RopeScript>();
            int numberFirstKnot = ropeScript.FirstKnot - 1;
            int numberSecondKnot = ropeScript.SecondKnot - 1;

            RopeAdaptation(ropes[i], knots[numberFirstKnot], knots[numberSecondKnot]);
        }
    }

    private void RopeAdaptation(RectTransform rope, RectTransform knot1, RectTransform knot2)
    {
        var centerPos = new Vector2((knot1.position.x + knot2.position.x) / 2, (knot1.position.y + knot2.position.y) / 2);
        rope.position = centerPos;

        float angle = AngleKnots(knot1.position, knot2.position);
        rope.eulerAngles = new Vector3(0, 0, angle);

        float distance = Vector2.Distance(knot1.position, knot2.position);
        rope.sizeDelta = new Vector2(distance, rope.sizeDelta.y);

        var collider = rope.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(distance - 10, 0.01f);
    }

    private float AngleKnots(Vector2 pos1, Vector2 pos2)
    {
        Vector2 dir = pos2 - pos1;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    public void ButtonSKIP()
    {
        knots[0].position = new Vector2(530, 741);
        knots[1].position = new Vector2(329, 1003);
        knots[2].position = new Vector2(871, 922);
        knots[3].position = new Vector2(906, 474);
        knots[4].position = new Vector2(1300, 613);
        knots[5].position = new Vector2(1213, 725);
        knots[6].position = new Vector2(1475, 594);
        knots[7].position = new Vector2(1627, 1006);
        knots[8].position = new Vector2(1615, 93);
        knots[9].position = new Vector2(909, 580);
        knots[10].position = new Vector2(897, 375);
        knots[11].position = new Vector2(348, 223);
        knots[12].position = new Vector2(743, 687);
        knots[13].position = new Vector2(1038, 848);

        AllRopeAdaptation();

        Win();
    }

    public void CheckOnWin()
    {
        int countGreenRopes = 0;

        for (int i = 0; i < ropes.Count; i++)
        {
            var ropeScript = ropes[i].GetComponent<RopeScript>();
            if (ropeScript.Green)
                countGreenRopes++;
        }

        if (countGreenRopes == ropes.Count)
            Win();
    }

    private void Win()
    {
        windowWin.SetActive(true);
        score += scoreWin;
        textScore.text = "Score: " + score;
    }
}
