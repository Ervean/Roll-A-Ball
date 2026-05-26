using System.Collections;
using System.Collections.Generic;
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

        private Vector3 _previousPosition;

        private List<GameObject> _objectsOnPlatform = new List<GameObject>();
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

        private void FixedUpdate()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!_objectsOnPlatform.Contains(collision.gameObject))
            {
                _objectsOnPlatform.Add(collision.gameObject);
            }   
        }

        private void OnCollisionExit(Collision collision)
        {
            if(_objectsOnPlatform.Contains(collision.gameObject))
            {
                _objectsOnPlatform.Remove(collision.gameObject);
            }
        }

        private IEnumerator GoToDestination()
        {
            _timer = 0f;
            while(_timer <= _timeToDestination)
            {
                _previousPosition = transform.position;
                transform.position = Vector3.Lerp(_originalPosition, _destination, _timer / _timeToDestination );
                UpdateObjectsOnPlatform(_previousPosition, transform.position);
                yield return null;
            }
            StartCoroutine(GoToOriginalPosition());
        }

        private IEnumerator GoToOriginalPosition()
        {
             _timer = 0f;
            while(_timer <= _timeToDestination)
            {
                _previousPosition = transform.position;
                transform.position = Vector3.Lerp(_destination, _originalPosition, _timer / _timeToDestination );
                UpdateObjectsOnPlatform(_previousPosition, transform.position);
                yield return null;
            }

            StartCoroutine(GoToDestination());
        }

        private void UpdateObjectsOnPlatform(Vector3 previous, Vector3 current)
        {
            foreach(var gameObject in _objectsOnPlatform)
            {
                Vector3 diff = current - previous;
                gameObject.transform.position = gameObject.transform.position + diff;
            }
        }
    }
}