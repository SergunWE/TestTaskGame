using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRb;

    public void Init(MapComponents mapComponents)
    {
        _playerRb.velocity = Vector2.zero;
        _playerRb.transform.position = mapComponents.StartPlayerPosition.position;
    }
}