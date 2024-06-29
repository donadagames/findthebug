using UnityEngine;

public class References : MonoBehaviour
{
    [HideInInspector] public SoundController soundController;
    [HideInInspector] public UIController uiController;
    [HideInInspector] public GridController gridController;
    [HideInInspector] public TurnManager turnManager;
    [HideInInspector] public TimerController timerController;
    [HideInInspector] public DataController dataController;

    #region SINGLETON PARA REFERÊNCIAS EXTERNAS
    public static References instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        soundController = GetComponent<SoundController>();
        uiController = GetComponent<UIController>();
        gridController = GetComponent<GridController>();
        turnManager = GetComponent<TurnManager>();
        timerController = GetComponent<TimerController>();
        dataController = GetComponent<DataController>();
    }
    #endregion

    [Space(10)]
    [Header("PREFAB DA CARTA")]
    public GameObject cardPrefab;

    [Space(10)]
    [Header("SPRITES NA ORDEM: [0]BUG, [1]UP, [2]DOWN, [3]LEFT, [4]RIGHT, [5]UP-LEFT, [6]UP-RIGHT, [7]DOWN-LEFT, [8]DOWN-RIGHT, [9]BACK-CARD")]
    public Sprite[] cardSprites;

}
