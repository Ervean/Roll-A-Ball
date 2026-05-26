using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {

#region Inspector

        public TextMeshProUGUI countText;
        public TextMeshProUGUI winText;

#endregion

#region Settings

        public float Speed = 10f;

        public float JumpForce = 100f;
#endregion

        private Collider c;
        private Rigidbody rb;
        private float movementX;
        private float movementY; 
        private int count;
        private float yExtend;

        private float jumpCoolDown = .2f;
        private float jumpTimer = 0f;
        private const int MaxPickUp = 7;
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            count = 0;
            c = GetComponent<Collider>();
            yExtend = c.bounds.extents.y;
            rb = GetComponent<Rigidbody>();    
            SetCountText();
            winText.gameObject.SetActive(false);
        }

        private void Update()
        {
            jumpTimer += Time.deltaTime;
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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                winText.gameObject.SetActive(true);
                winText.text = "You Lose!";
            }
        }

        private void OnJump(InputValue jumpValue)
        {
            if(!IsGrounded())
            {
                return;
            }

            // wait for timer to past cooldown
            if(jumpTimer <= jumpCoolDown)
            {
                return;
            }

            Vector3 force = new Vector3(0, JumpForce, 0);
            rb.AddForce(force);
            jumpTimer = 0f;
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
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, yExtend + .1f);
        }
    
    }
}