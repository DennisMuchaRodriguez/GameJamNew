using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    private bool isControlsInverted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Invertir controles si el debuff está activo
        if (isControlsInverted)
        {
            horizontalInput *= -1;
            verticalInput *= -1;
        }

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("debuff1")) // Debuff: invertir controles
        {
            StartCoroutine(InvertControlsForSeconds(5f));
        }
    }

    public void StartLento(float value, int timebuff)
    {
        StartCoroutine(Lento(value, timebuff));
    }

    public IEnumerator Lento(float value, int time)
    {
        float originalMoveSpeed = moveSpeed;
        moveSpeed = value;
        yield return new WaitForSeconds(time);
        moveSpeed = originalMoveSpeed;
    }

    public IEnumerator InvertControlsForSeconds(float seconds)
    {
        isControlsInverted = true;
        yield return new WaitForSeconds(seconds);
        isControlsInverted = false;
    }
}