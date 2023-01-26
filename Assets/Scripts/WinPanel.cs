using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private Animator _animator;

    private int _playerWinHash = Animator.StringToHash("PlayerWin");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        Finish.playerWin += OnPlayerWin;
        gameObject.SetActive(false);
    }

    private void OnPlayerWin()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(_playerWinHash);
    }
}
