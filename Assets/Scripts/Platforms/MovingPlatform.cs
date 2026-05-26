using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Platform
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _timeToDestination;

        private Vector3 _originalPosition;
        private Vector3 _destination;
        private float _timer;
        private void Awake()
        {
            _originalPosition = transform.position;
            _destination = _originalPosition + _offset;
        }

        private void Start()
        {
            StartCoroutine(GoToDestination());
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private IEnumerator GoToDestination()
        {
            _timer = 0f;
            while(_timer <= _timeToDestination)
            {
                transform.position = Vector3.Lerp(_originalPosition, _destination, _timer / _timeToDestination );
                yield return null;
            }
            StartCoroutine(GoToOriginalPosition());
        }

        private IEnumerator GoToOriginalPosition()
        {
             _timer = 0f;
            while(_timer <= _timeToDestination)
            {
                transform.position = Vector3.Lerp(_destination, _originalPosition, _timer / _timeToDestination );
                yield return null;
            }

            StartCoroutine(GoToDestination());
        }


    }
}