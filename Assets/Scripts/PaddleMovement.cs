using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// References for paddle and screen limit collision: https://forum.unity.com/threads/im-not-able-to-keep-a-sprite-on-screen-properly.382540/

public class PaddleMovement : MonoBehaviour
{
  [Range(1, 10)]
  public float velocity;

  private Vector2 leftBottom;
  private Vector2 rightTop;
  private SpriteRenderer paddleRenderer;
  private Vector2 paddleHalfSize;

  GameManager gm;

  // Start is called before the first frame update
  void Start()
  {
    // get the world location of the bottom left corner and top right corner of camera
    // if your camera moves, this will have to be in Update or LateUpdate
    leftBottom = Camera.main.ViewportToWorldPoint(Vector3.zero);
    rightTop = Camera.main.ViewportToWorldPoint(Vector3.one);

    paddleRenderer = GetComponent<SpriteRenderer>();
    paddleRenderer.color = new Color(1, 1, 0);

    paddleHalfSize = paddleRenderer.bounds.extents;

    gm = GameManager.GetInstance();
  }

  // Update is called once per frame
  void Update()
  {
    if (gm.gameState != GameManager.GameState.GAME) return;

      float inputX = Input.GetAxis("Horizontal");

      transform.position += new Vector3(inputX, 0, 0) * Time.deltaTime * velocity;

    if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME)
    {
      gm.ChangeState(GameManager.GameState.PAUSE);
    }
  }

  //LateUpdate will correct the position before rendering
  private void LateUpdate()
  {
    // get the sprite's edge positions
    float paddleLeft = transform.position.x - paddleHalfSize.x;
    float paddleRight = transform.position.x + paddleHalfSize.x;

    // initialize the new position to the current position
    Vector3 clampedPosition = transform.position;

    // if any of the edges surpass the camera's bounds,
    // set the position TO the camera bounds (accounting for sprite's size)
    if (paddleLeft < leftBottom.x)
    {
      clampedPosition.x = leftBottom.x + paddleHalfSize.x;
    }
    else if (paddleRight > rightTop.x)
    {
      clampedPosition.x = rightTop.x - paddleHalfSize.x;
    }

    transform.position = clampedPosition;
  }
}
