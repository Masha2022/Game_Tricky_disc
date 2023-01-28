using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _minMovingDuration;
    [SerializeField]
    private float _maxMovingDuration;
    
    private float _minPointX;
    private float _maxPointX;
    private SpriteRenderer _enemySprite;
    private float _delayBetweenMovements;
    private Sequence _moveSequence;
    
    
    private float GetRandomMovementDuration()
    {
        return Random.Range(_minMovingDuration, _maxMovingDuration);
    }
    private void Move()
    {
        var randomMovementDuration = GetRandomMovementDuration();
        var nextPosition = GetNextRandomPositionX();
        
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(nextPosition, randomMovementDuration));
        _moveSequence.AppendInterval(_delayBetweenMovements);
        _moveSequence.OnComplete(Move);
    }
    private float GetNextRandomPositionX()
    {
        return Random.Range(_minPointX, _maxPointX);
    }

    public void Initialize(float minPointX, float maxPointX, float delayBetweenMovements)
    {
        _enemySprite = GetComponent<SpriteRenderer>();
        var offsetX = _enemySprite.bounds.size.x / 2;

        _minPointX = minPointX + offsetX;
        _maxPointX = maxPointX - offsetX;
        _delayBetweenMovements = delayBetweenMovements;

        Move();
    }
    [UsedImplicitly]
    public void Destroy()//вызывается по событие уничтожения врага.
    {
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        _moveSequence.Kill();
    }
}
