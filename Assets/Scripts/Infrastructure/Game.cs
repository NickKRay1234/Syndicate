using Scripts.Services.Inputs;

namespace Scripts.Infrastructure
{
  public class Game
  {
    public static IInputService InputService;
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
    }
  }
}