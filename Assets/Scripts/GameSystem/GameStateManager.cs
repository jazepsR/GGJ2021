using System;
using UniRx;

namespace GameSystem
{
    public static class GameStateManager
    {
        public static IReactiveProperty<GameState> CurrentGameState { get; } =
            new ReactiveProperty<GameState>(GameState.Initial);

        internal static IObservable<T> IsPlaying<T>(this IObservable<T> observable)
        {
            return observable.Where(_ => CurrentGameState.Value == GameState.Playing);
        }
    }
}