using UnityEngine;
using UnityEngine.AI;

public class OpponentController : MonoBehaviour
{
    public static OpponentController Instance;

    public NavMeshAgent agent;
    private Rigidbody _rb;
    public int platformSpeed;
    public static bool canOpponentMove = false;
    private Animator _anim;
    [SerializeField] private Transform _target;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
    private void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
    }
    private void FixedUpdate()
    {
        if (canOpponentMove)
        {
            OpponentRun();
        }
        if (transform.position.y < -2)
        {
            agent.Warp(new Vector3(Random.Range(-1.5f, 1.5f), 0, 0));
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            agent.Warp(new Vector3(Random.Range(-1.5f, 1.5f), 0, 0));
            agent.destination = _target.position;
        }
        if (collision.collider.CompareTag("Rotator"))
        {
            _rb.AddForce(0, 0, -500);
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
    public void OpponentRun()
    {
        _anim.SetBool("isOpponentRunning", true);
        _rb.MovePosition(transform.position + agent.velocity * Time.fixedDeltaTime);
        agent.destination = _target.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _anim.SetBool("isOpponentFinished", true);
        }
    }
}
