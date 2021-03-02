public class GameManager
{
  private static GameManager _instance;

  public enum GameState { MENU, GAME, PAUSE, ENDGAME };
  public GameState gameState { get; private set; }

  public delegate void ChangeStateDelegate();
  public static ChangeStateDelegate changeStateDelegate;

  public float lives;
  public int points;

  public static GameManager GetInstance()
  {
    if (_instance == null)
    {
      _instance = new GameManager();
    }

    return _instance;
  }

  private GameManager()
  {
    lives = 3;
    points = 0;
    gameState = GameState.MENU;
  }

  public void ChangeState(GameState nextState)
  {
    if (nextState == GameState.GAME) Reset();
    gameState = nextState;
    changeStateDelegate();
  }

  private void Reset()
  {
    lives = 3;
    points = 0;
  }
}
