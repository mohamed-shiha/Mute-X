using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupsType
{
    Health,
    Ammo,
    Shotgun,
    Pistol,
    Machinegun,
    Nothing
}
public class Pickups : MonoBehaviour {

    public int Amount;
    public PickupsType Type;
    public Sprite Sprite;
    public GameController gameController;
    public SpriteRenderer spriteRenderer;
    protected PlayerData data;
    TextMesh text;

    protected virtual void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMesh>();
        spriteRenderer.sprite = Sprite;
    }

    private void Update()
    {
        if(text != null)
            text.text = ""+Amount;
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    public void Dropped()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        Invoke("UnIgnorPlayer",3);
    }

    public void UnIgnorPlayer()
    {
        gameController.IgnorePlayer(gameObject.GetComponent<Collider2D>(), false);
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Player"))
        {
            gameController.PickedUp(gameObject);
            Destroy(gameObject);
        }
    }
}
