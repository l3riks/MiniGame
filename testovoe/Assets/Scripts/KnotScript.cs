using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO.Compression;
using NUnit.Framework.Constraints;

public class KnotScript : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip dragClip;

    [SerializeField] private Sprite defaultKnot;
    [SerializeField] private Sprite usedKnot;
    [SerializeField] private Texture2D cursorHand;

    private RectTransform rectTransform;
    private AudioSource audioSource;
    private Image iconKnot;
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        rectTransform = GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        iconKnot = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = dragClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        GameManagerScript.Instance.AllRopeAdaptation();

        rectTransform.anchoredPosition += eventData.delta;
    } 
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.position.x < 240 || transform.position.x > 1680 || transform.position.y < 0 || transform.position.y > 1080)
        {
            transform.position = startPos;
            GameManagerScript.Instance.AllRopeAdaptation();
        }
        else
            startPos = transform.position;

        audioSource.loop = false;

        GameManagerScript.Instance.CheckOnWin();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.Auto);
        iconKnot.sprite = usedKnot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        iconKnot.sprite = defaultKnot;
    }
}
