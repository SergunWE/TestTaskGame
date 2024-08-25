using UnityEngine;

public class PlayerController : GameController
{
    [SerializeField] private Rigidbody2D _playerRb;

    public override void Init(MapComponents mapComponents)
    {
        _playerRb.velocity = Vector2.zero;
        _playerRb.transform.position = mapComponents.StartPlayerPosition.position;
    }
}