using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerEffectsAnimations _player;
    public GameObject tap;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEffectsAnimations>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (!_playerController.canMove && !OpponentController.canOpponentMove && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        tap.SetActive(false);
        OpponentController.canOpponentMove = true;
        _playerController.canMove = true;
        _player.Run();
        this.gameObject.SetActive(false);
    }
}
