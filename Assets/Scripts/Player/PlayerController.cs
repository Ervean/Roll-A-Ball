using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public TextMeshProUGUI countText;
        public TextMeshProUGUI winText;
        public float Speed = 10f;
        private Rigidbody rb;
        private float movementX;
        private float movementY;
        private int count;

        private const int MaxPickUp = 7;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            count = 0;
            rb = GetComponent<Rigidbody>();    
            SetCountText();
            winText.gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(movementX, 0, movementY);
            rb.AddForce(movement * Speed);



        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);        
                count++;  
                SetCountText();
            }
        }

        private void OnMove (InputValue movementValue)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        
        private void SetCountText()
        {
            countText.text = "Count: " + count.ToString();

            if(count >= MaxPickUp)
            {
                winText.gameObject.SetActive(true);
            }
        }
    
    }
}