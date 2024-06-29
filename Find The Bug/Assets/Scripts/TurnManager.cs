using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turnDuration = 61;

    [HideInInspector] public int points = 0;
    [HideInInspector] public bool canPlay = true;

    public void Start()
    {
        canPlay = false;
        References.instance.timerController.OnTimerCompleted += OnTimeEnds;
    }

    public void StartNewTurn(bool isFirstTurn)
    {
        canPlay = false;
        points = 0;
        References.instance.timerController.StartNewTimer(turnDuration);

        if (isFirstTurn == true)
            References.instance.gridController.InstantiateCards();
        else
            References.instance.gridController.RefreshCards();
    }

    public void OnFindBug()
    {
        points += 10;
        canPlay = false;
        References.instance.uiController.UpdateScoreText(points);
        References.instance.soundController.PlayBugCardSound();
        StartCoroutine(OnFindBugCoroutine());

    }

    private IEnumerator OnFindBugCoroutine()
    {
        yield return new WaitForSeconds(.75f);
        References.instance.gridController.RefreshCards();
        canPlay = true;
    }

    private void OnTimeEnds(object sender, TimerController.OnTimerCompletedEventHandler e)
    {
        canPlay = false;
        References.instance.dataController.SendJsonDataFile(References.instance.dataController.GetJsonDataFile(points));
    }
}
