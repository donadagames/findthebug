using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerDownHandler
{
    #region OLD CODE
    /*
    public enum element
    {
        Left, LeftDown,
        LeftUp,
        Right,
        RightDown, RightUp,
        Up,
        Down,
        Bug
    };

    public enum State
    {
        Closed,
        opening,
        Opened
    }

    public State _state { get; private set; }

    void Update()
    {
        if (_state == State.Closed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                        _state = State.opening;
                }
            }
        }
        else if (_state == State.opening)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 500);

            if (transform.rotation.eulerAngles.y == 180)
                _state = State.Opened;
        }
    }

    public element ElementToShow { get; set; }
    */
    #endregion

    public RectTransform rect;
    [HideInInspector] public bool isBug = false;
    [HideInInspector] public bool canFlip = true;
    [HideInInspector] public Vector2 gridPosition = new Vector2();
    Image graphics;
    Sprite showCardSprite;

    private void Start()
    {
        graphics = GetComponent<Image>();
        canFlip = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (canFlip == false || References.instance.turnManager.canPlay == false)
            return;

        else
        {
            canFlip = false;

            //CONDIÇÃO DE VITÓRIA
            if (isBug)
            {
                showCardSprite = References.instance.cardSprites[0];
                References.instance.turnManager.OnFindBug();
            }

            //PROCURAR PELO BUG E INDICAR DIREÇÃO
            else
            {
                showCardSprite = References.instance.gridController.GetBugDirection(this);
                References.instance.soundController.PlayRegularCardSound();
            }

            FlipCard();
        }
    }

    private void FlipCard()
    {
        transform.LeanRotateAroundLocal(Vector3.up, 180, .075f).
            setEase(LeanTweenType.easeInOutBounce).setOnComplete(OnConpleteFlipCard);
    }

    private void OnConpleteFlipCard()
    {
        graphics.sprite = showCardSprite;
    }

    public void HideCard()
    {
        transform.LeanRotateAroundLocal(Vector3.up, -180, .075f).
                setEase(LeanTweenType.easeInOutBounce).
                setOnComplete((OnConpleteHideCard));
    }
    private void OnConpleteHideCard()
    {
        graphics.sprite = References.instance.cardSprites[9];
        canFlip = true;
    }
}
