using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = false;
    private Rigidbody _rb;
    public float sensitivityMultiplier;
    public float deltaThreshold;
    Vector2 firstTouchPosition;
    float finalTouchX;
    Vector2 currentTouchPosition;
    public float minXPos;
    public float maxXPos;
    public float runSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (canMove)
        {
            SwipeMovement();
        }
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            FixedRun();
        }
    }
    void ResetValues()
    {
        _rb.velocity = new Vector3(0f, _rb.velocity.y, _rb.velocity.z);
        firstTouchPosition = Vector2.zero;
        finalTouchX = 0f;
        currentTouchPosition = Vector2.zero;
    }
    public void MovementStop()
    {
        canMove = false;
        _rb.velocity = Vector3.zero;
    }
    void FixedRun()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, runSpeed * Time.fixedDeltaTime);
    }
    void SwipeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentTouchPosition = Input.mousePosition;
            Vector2 touchDelta = (currentTouchPosition - firstTouchPosition);

            if (firstTouchPosition == currentTouchPosition)
            {
                _rb.velocity = new Vector3(0f, _rb.velocity.y, _rb.velocity.z);
            }

            finalTouchX = transform.position.x;

            if (Mathf.Abs(touchDelta.x) >= deltaThreshold)
            {
                finalTouchX = (transform.position.x + (touchDelta.x * sensitivityMultiplier));
            }

            _rb.position = new Vector3(finalTouchX, transform.position.y, transform.position.z);
            _rb.position = new Vector3(Mathf.Clamp(_rb.position.x, minXPos, maxXPos), _rb.position.y, _rb.position.z);

            firstTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetValues();
        }
    }
}
