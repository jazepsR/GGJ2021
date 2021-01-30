using UniRx;

namespace GameSystem
{
    public static class GameStateManager
    {
        public static IReactiveProperty<GameState> CurrentGameState { get; } =
            new ReactiveProperty<GameState>(GameState.Initial);
    }
}