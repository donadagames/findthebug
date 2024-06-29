using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] TMP_InputField cardsQuantityField;
    CanvasGroup startCanvasGroup;
    CanvasGroup gameoverCanvasGroup;

    private bool canStart = true;
    private bool canPlayAgain = true;

    private void Start()
    {
        canStart = true;
        canPlayAgain = true;

        References.instance.timerController.OnTimerCompleted += OnTimeEnds;
        gameoverCanvasGroup = gameoverPanel.GetComponent<CanvasGroup>();
        startCanvasGroup = startPanel.GetComponent<CanvasGroup>();
        startCanvasGroup.alpha = 1.0f;
    }

    public void UpdateTimerText(string newText)
    {
        timeText.text = $"{newText}";
    }

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
        finalScoreText.text = newScore.ToString() + " PONTOS";
    }


    #region BUTTONS
    public void OnStartGameButton()
    {
        if (!canStart) return;

        if (cardsQuantityField.text == string.Empty || int.Parse(cardsQuantityField.text) <= 10)
        {
            return;
        }

        References.instance.gridController.cardsQuantity = int.Parse(cardsQuantityField.text);

        canStart = false;
        References.instance.soundController.PlayMusic();
        References.instance.turnManager.StartNewTurn(true);
        startCanvasGroup.LeanAlpha(0, 1).
            setOnComplete(OnStartComplete);
    }

    private void OnStartComplete()
    {
        References.instance.gridController.SetCardGridPosition();
        References.instance.turnManager.canPlay = true;
        startPanel.SetActive(false);
    }

    public void OnPlayAgainButton()
    {
        if (!canPlayAgain) return;

        canPlayAgain = false;
        UpdateScoreText(0);
        UpdateTimerText($"Tempo: {References.instance.turnManager.turnDuration}");
        gameoverCanvasGroup.LeanAlpha(0, 2).setOnComplete(OnConpletePlayAgain);
    }

    private void OnConpletePlayAgain()
    {

        References.instance.turnManager.StartNewTurn(false);
        References.instance.turnManager.canPlay = true;
        gameoverPanel.SetActive(false);
    }

    public void OnQuidGameButton()
    {
        Application.Quit();
    }

    private void OnTimeEnds(object sender, TimerController.OnTimerCompletedEventHandler e)
    {
        gameoverCanvasGroup.alpha = 0;
        gameoverPanel.SetActive(true);
        gameoverCanvasGroup.LeanAlpha(1, 2);
        canPlayAgain = true;
    }

    #endregion




}
