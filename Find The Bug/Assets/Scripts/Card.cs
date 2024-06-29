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

    [SerializeField] Transform graphics;
    [SerializeField] Image face;
    [SerializeField] GameObject[] cardSides;
    public bool isBug = false;
    public Vector2 gridPosition = new Vector2();
    public RectTransform rect;

    private void Start()
    {
        canFlip = true;
    }

    [HideInInspector] public bool canFlip = true;

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
                face.sprite = References.instance.cardSprites[0];
                References.instance.turnManager.OnFindBug();
            }

            //PROCURAR PELO BUG E INDICAR DIREÇÃO
            else
            {
                face.sprite = References.instance.gridController.GetBugDirection(this);
                References.instance.soundController.PlayRegularCardSound();
            }

            FlipShowCard();
        }
    }

    private void OnConpleteFlip()
    {
        cardSides[0].SetActive(!cardSides[0].activeSelf);
        cardSides[1].SetActive(!cardSides[1].activeSelf);
    }

    private void FlipShowCard()
    {
        graphics.LeanRotateAroundLocal(Vector3.up, 180, .075f).setOnComplete(OnConpleteFlip);
    }

    public void FlipHideCard()
    {
        graphics.LeanRotateAroundLocal(Vector3.up, -180, .075f).setOnComplete(OnConpleteFlip);
    }
}
