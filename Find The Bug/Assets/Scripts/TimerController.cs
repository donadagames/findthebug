using System;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private int timer;
    private References references;
    private int timerID;


    private void Start()
    {
        references = References.instance;
    }

    public void StartNewTimer(int turnDuration)
    {
        timer = turnDuration;
        references.uiController.UpdateTimerText($"Tempo: {timer}");

        timerID = LeanTween
            .value(gameObject, timer, 0, timer)
            .setOnUpdate(TimerHandlerOnUpdate)
            .setOnComplete(OnCompleteTimer)
            .id;

    }

    private void TimerHandlerOnUpdate(float value)
    {
        if ((int)value >= 10)
            references.uiController.UpdateTimerText($"Tempo: {(int)value}");

        else
            references.uiController.UpdateTimerText($"Tempo: 0{(int)value}");
    }

    private void OnCompleteTimer()
    {
        OnTimerCompleted?.Invoke(this, new OnTimerCompletedEventHandler { points = references.turnManager.points });
    }

    public void StopTimer()
    {
        LeanTween.cancel(timerID, false);
    }

    public event EventHandler<OnTimerCompletedEventHandler> OnTimerCompleted;

    public class OnTimerCompletedEventHandler : EventArgs
    {
        public int points;
    }
}
