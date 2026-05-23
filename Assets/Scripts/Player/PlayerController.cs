using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float Speed = 10f;
        private Rigidbody rb;
        private float movementX;
        private float movementY;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            rb = GetComponent<Rigidbody>();    
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(movementX, 0, movementY);
            rb.AddForce(movement * Speed);



        }


        private void OnMove (InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        
    
    }
}