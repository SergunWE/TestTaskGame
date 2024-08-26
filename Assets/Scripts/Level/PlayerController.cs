using UnityEngine;

public class PlayerController : GameController
{
    [SerializeField] private GameContext gameContext;
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private SpriteRenderer playerRenderer;

    private void Awake()
    {
        DisablePlayer();
    }

    public override void Init(MapComponents mapComponents)
    {
        _playerRb.gameObject.SetActive(false);
        _playerRb.velocity = Vector2.zero;
        _playerRb.transform.position = mapComponents.StartPlayerPosition.position;
        playerRenderer.sprite = gameContext.CurrentSkin.PlayerSprite;
        _playerRb.gameObject.SetActive(true);
    }

    public void DisablePlayer()
    {
        _playerRb.gameObject.SetActive(false);
    }
}