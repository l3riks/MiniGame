using UnityEngine;
using UnityEngine.UI;

public class RopeScript : MonoBehaviour
{
    [SerializeField] private Sprite greenRope;
    [SerializeField] private Sprite redRope;

    [SerializeField] private int firstKnot;
    [SerializeField] private int secondKnot;

    private bool green = true;
    private Image icon;
    private int countCollision = 0;

    public int FirstKnot => firstKnot;
    public int SecondKnot => secondKnot; 
    public bool Green => green;

    private void Start()
    {
        icon = GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        countCollision++;

        if (countCollision > 0)
        {
            green = false;
            icon.sprite = redRope;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(countCollision == 1)
        {
            green = true;
            icon.sprite = greenRope;
        }

        countCollision--;
    }
}
