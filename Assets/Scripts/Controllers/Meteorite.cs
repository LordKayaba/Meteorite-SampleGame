using System.Collections;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 14;
    [SerializeField] float health = 100;
    [SerializeField] float damage;

    [Header("State")]
    [SerializeField] bool isCollision = false;
    public bool isMove = true, isDamage = true;

    bool isDead = false;
    float time = 0;
    Rigidbody2D rigi;
    SpriteRenderer spriteRenderer;
    TrailRenderer trailRenderer;

    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        Setup();
    }

    void Setup()
    {
        Vector3 point = GameManager.Instance.SavedPoint;
        point.y += 2;
        SetStartPoint(point);
    }

    void Update()
    {
        if (isDamage)
        {
            if (time < 3)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                Damage(damage);
            }
        }
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        if (!isMove) return;

        float move = Input.GetAxis("Horizontal");
        if (Mathf.Abs(move) > 0.01f)
        {
            rigi.AddForce(new Vector2(move * speed * Time.deltaTime * 100, 0f), ForceMode2D.Force);
        }
    }

    void HandleJump()
    {
        if (!isMove || !isCollision) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isCollision = false;
        }
    }

    GameObject collision;

    void OnCollisionEnter2D(Collision2D collision)
    {
        isCollision = true;
        this.collision = collision.gameObject;
    }

    public void Bomb()
    {
        if (collision)
        {
            var Action = collision.GetComponent<IAction>();
            if (Action != null)
            {
                /*GameData.data.level.AddBomb(-1);
                UIManager.ui.SetUI();
                GameObject NewGameObject = Instantiate(BombFX);
                NewGameObject.transform.position = transform.position;
                collision0.GetComponent<IAction>().Explode();
                AudioManager.Audio.FX(AudioManager.Audio.data.FXs.Wood);*/
            }
        }
    }

    public void Damage(float amount)
    {
        if (isDead) return;

        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        //UIManager.ui.SetUIPlayer(false);
        if (health <= 0)
        {
            isDead = true;
            isMove = false;
            Trail(false);
            /*spriteRenderer.sprite = player.sprites.cold;
            spriteRenderer.color = player.sprites.color;*/
            StartCoroutine(SetSimulatedFalseAfterDelay(3));
            GameManager.Instance.OnPlayerDied();
        }
    }

    IEnumerator SetSimulatedFalseAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        rigi.simulated = false;
    }

    public void AddHealth(float amount)
    {
        time = 0;
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        //UIManager.ui.SetUIPlayer(true);
    }

    public void AddMass(float amount) => rigi.mass += amount;

    public void Trail(bool Status) => trailRenderer.enabled = Status;

    public void SetStartPoint(Vector3 Point)
    {
        Trail(false);
        transform.position = Point;
        Camera.main.GetComponent<CameraController>().SetTargetToFollow(transform);
        Trail(true);
    }
}
