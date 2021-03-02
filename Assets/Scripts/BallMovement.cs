using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// References for ball and screen limit collision: https://forum.unity.com/threads/im-not-able-to-keep-a-sprite-on-screen-properly.382540/

public class BallMovement : MonoBehaviour
{
    [Range(1, 15)]
    public float velocity = 5.0f;

    private Vector3 direction;
    private Vector2 leftBottom;
    private Vector2 rightTop;
    private SpriteRenderer ballRenderer;
    private Vector2 ballHalfSize;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);

        direction = new Vector3(dirX, dirY).normalized;

        // get the world location of the bottom left corner and top right corner of camera
        // if your camera moves, this will have to be in Update or LateUpdate
        leftBottom = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightTop = Camera.main.ViewportToWorldPoint(Vector3.one);

        ballRenderer = GetComponent<SpriteRenderer>();
        ballHalfSize = ballRenderer.bounds.extents;

        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;

        transform.position += direction * Time.deltaTime * velocity;

        // get the sprite's edge positions
        float ballLeft = transform.position.x - ballHalfSize.x;
        float ballRight = transform.position.x + ballHalfSize.x;
        float ballBottom = transform.position.y - ballHalfSize.y;
        float ballTop = transform.position.y + ballHalfSize.y;

        if (ballLeft < leftBottom.x || ballRight > rightTop.x)
        {
            direction = new Vector3(-direction.x, direction.y);
        }
        if (ballTop < leftBottom.y || ballTop > rightTop.y)
        {
            direction = new Vector3(direction.x, -direction.y);
        }

        if (ballTop < leftBottom.y  )
        {
            Reset();
        }

        Debug.Log($"Vidas: {gm.lives} \t | \t Pontos: {gm.points}");
    }

    // Method called to reset ball position
    private void Reset()
    {
        if (gm.lives <= 1 && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + new Vector3(0, 0.5f, 0);

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(2.0f, 5.0f);

        direction = new Vector3(dirX, dirY).normalized;
        gm.lives--;
    }

  // Method called when a caollision is triggered
  private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            float dirX = Random.Range(-5.0f, 5.0f);
            float dirY = Random.Range(1.0f, 5.0f);

            direction = new Vector3(dirX, dirY).normalized;
        }
        else if (collider.gameObject.CompareTag("Block"))
        {
            direction = new Vector3(direction.x, -direction.y);
            gm.points++;
        }
    }
}
