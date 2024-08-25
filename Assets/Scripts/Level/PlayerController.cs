using UnityEngine;

public class PlayerController : GameController
{
    [SerializeField] private Rigidbody2D _playerRb;

    public override void Init(MapComponents mapComponents)
    {
        _playerRb.isKinematic = true;
        _playerRb.velocity = Vector2.zero;
        _playerRb.transform.position = mapComponents.StartPlayerPosition.position;
        _playerRb.isKinematic = false;
    }

    public void DisablePlayer()
    {
        _playerRb.isKinematic = true;
    }
}