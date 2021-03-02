using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// References for lifeBar: https://dev.to/henriquederosa/unity-criar-uma-barra-de-vida-ge4

public class UI_lives : MonoBehaviour
{
    GameManager gm;
    public Image lifeBar;

    public float maxLives = 14;

    public float Life
    {
      get
      {
        return gm.lives;
      }
      set
      {
        gm.lives = Mathf.Clamp(value, 0, maxLives);
      }
    }

  // Start is called before the first frame update
  void Start()
    {
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
      lifeBar.fillAmount = Life / maxLives;
    }
}
