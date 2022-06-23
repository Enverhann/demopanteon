using System.Collections;
using UnityEngine;

public class PlayerEffectsAnimations : MonoBehaviour
{
    public float knockback;
    private Rigidbody _rb;
    private Animator _anim;
    private PlayerController _playerController;
    public int platformSpeed;
    private Position _position;
    public GameObject finishPlace;
    public GameObject wall;
    public GameObject gameCamera;
    public GameObject instructions;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _position = GameObject.FindGameObjectWithTag("PositionController").GetComponent<Position>();
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -2)
        {
            transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("RotatingPlatform"))
        {
            _rb.AddForce(Vector3.right * platformSpeed, ForceMode.Force);
        }
        else if (collision.collider.CompareTag("RotatingPlatform2"))
        {
            _rb.AddForce(Vector3.right * -platformSpeed * 1.85f, ForceMode.Force);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Rotator"))
        {
            StartCoroutine(Fall());
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            transform.position = new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);
        }
    }
    public void Run()
    {
        _anim.SetBool("isRunning", true);
    }
    
    public IEnumerator Fall()
    {
        _playerController.canMove = false;
        _anim.SetBool("isFalling", true);
        _rb.AddForce(0, 0, knockback);
        yield return new WaitForSeconds(1);
        _playerController.canMove = true;
        _anim.SetBool("isFalling", false);
        _anim.SetBool("isRunning", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _anim.SetBool("isFinished", true);
            _playerController.canMove = false;
            _position.text.enabled = false;
            gameCamera.SetActive(false);
            finishPlace.SetActive(true);
            OpponentController.canOpponentMove = false;
            wall.SetActive(true);
            instructions.SetActive(true);
        }
    }
}
