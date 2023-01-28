using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerRotator _playerRotator;
        [SerializeField]
        private UserMoveTimeLimiter _userMoveTimeLimiter;
        [SerializeField]
        private SpriteRenderer _aimSprite;

        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _movementVelocity;

        private Vector3 _startPosition;
        private bool _isMoving;


        private void Awake()
        {
            _startPosition = transform.position;
            _isMoving = false;
        }

        //вызывается через коллбэк нажатия кнопки передвижения
        [UsedImplicitly]
        public void Move()
        {
            if (!_isMoving)
            {
                _isMoving = !_isMoving;
                _aimSprite.enabled = false;
                _playerRotator.StopRotation();
                _userMoveTimeLimiter.StopTimeLimiter();

                _rigidbody.velocity = transform.up * _movementVelocity;
            }
        }
        
        //вызывается по ивенту при коллизии с врагом
        [UsedImplicitly]
        public void ChangeDirection()
        {
            _rigidbody.velocity *= -1;
        }

        //вызывается через ивент, при возвращении игрока в стар поинт триггер
        [UsedImplicitly]
        public void ResetPosition() 
        {
            if (_isMoving)
            {
                _isMoving = !_isMoving;
                _aimSprite.enabled = true;
                _playerRotator.StartRotation();
                _userMoveTimeLimiter.RestartTimeLimiter();

                _rigidbody.velocity = Vector2.zero;
                transform.position = _startPosition;
            }
        }
    }
}