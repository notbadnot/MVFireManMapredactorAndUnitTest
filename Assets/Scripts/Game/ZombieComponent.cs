using UnityEngine;

namespace Game
{
    public class ZombieComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _aliveView;

        [SerializeField] private GameObject _diedView;

        [SerializeField] private float _speed = 25f; //[SerializeField] private float _speed = 5f

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Vector3[] _deltaPath;
        [SerializeField] private PlayerInput PlayerInput;

        private int _currentPoint = 0;
        private Vector3 _initPosition;

        private void Awake()
        {
            _initPosition = transform.position;
        }

        private void OnEnable()
        {
            SetState(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsAlive)
            {
                Debug.Log(collision.gameObject.GetComponent<PlayerController>());
                var player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.Hitpoints = player.Hitpoints - 200;
                }
            }
        }


        private void Update()
        {
            if (PlayerInput != null && IsAlive)
            {


                var (moveDirection, viewDirection, shoot) = PlayerInput.CurrentInput();



                _rigidbody.velocity = moveDirection.normalized * _speed;
                transform.rotation = viewDirection;
            }
            else
            {

                if (_deltaPath == null || _deltaPath.Length < 2)
                    return;

                var direction = _initPosition + _deltaPath[_currentPoint] - transform.position;
                _rigidbody.velocity = IsAlive ? direction.normalized * _speed : Vector3.zero;

                if (direction.magnitude <= 0.1f)
                {
                    _currentPoint = (_currentPoint + 1) % _deltaPath.Length;
                }
            }
        }

        public void SetState(bool alive)
        {
            _aliveView.SetActive(alive);
            _diedView.SetActive(!alive);
        }

        public bool IsAlive => _aliveView.activeInHierarchy;
    }
}