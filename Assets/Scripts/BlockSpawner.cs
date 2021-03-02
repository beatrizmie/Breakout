using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject Block;

    GameObject InstantiatedBlock;

    GameManager gm;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += BuildBlockWall;
        BuildBlockWall();
    }

    void BuildBlockWall()
    {
        if (gm.gameState == GameManager.GameState.GAME)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    position = new Vector3(-9 + 1.55f * i, 4 - 0.55f * j);
                    InstantiatedBlock = Instantiate(Block, position, Quaternion.identity, transform);
                    InstantiatedBlock.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                    InstantiatedBlock.GetComponent<Block>().setDurability(1);
                }
                position = new Vector3(-9 + 1.55f * i, 4 - 0.55f * 5);
                InstantiatedBlock = Instantiate(Block, position, Quaternion.identity, transform);
                InstantiatedBlock.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
                InstantiatedBlock.GetComponent<Block>().setDurability(2);
            }
        }
    }

    void Update()
    {
        if (transform.childCount <= 0 && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }
}
