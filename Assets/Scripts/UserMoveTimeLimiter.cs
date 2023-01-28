using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UserMoveTimeLimiter : MonoBehaviour
{
    [SerializeField] private UnityEvent _timeIsOver;

    [SerializeField] private float _timeLimiterDuration;
    [SerializeField] private float _finishTimeLimiterScale;

    private Vector3 _startTimeLimiterScale;
    private Sequence _timeLimiterSequence;


    private void Start()
    {
        _startTimeLimiterScale = transform.localScale;

        StartTimeLimiterSequence();
    }

    public void RestartTimeLimiter()
    {
        _timeLimiterSequence.Restart();
    }

    public void StopTimeLimiter()
    {
        _timeLimiterSequence.Pause();
        transform.localScale = Vector3.zero;
    }

    private void StartTimeLimiterSequence()
    {
        _timeLimiterSequence = DOTween.Sequence();
        _timeLimiterSequence.SetAutoKill(false);
        _timeLimiterSequence.Append(transform.DOScale(_startTimeLimiterScale, 0f));
        _timeLimiterSequence.Append(transform.DOScale(_finishTimeLimiterScale, _timeLimiterDuration));
        _timeLimiterSequence.OnComplete(_timeIsOver.Invoke);
    }

    private void OnDestroy()
    {
        _timeLimiterSequence.Kill();
    }
}