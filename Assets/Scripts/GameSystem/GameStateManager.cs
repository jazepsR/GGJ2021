using System;
using UniRx;
using UnityEngine;

namespace GameSystem
{
    public static class GameStateManager
    {
        internal static MonoBehaviour CurrentPlayerSet;
        private static ISubject<Unit> PlayerDiedAnimationCompleteSubject { get; } = new Subject<Unit>();
        public static IReactiveProperty<GameState> CurrentGameState { get; } =
            new ReactiveProperty<GameState>(GameState.Initial);

        public static void PlayerDiedAnimationComplete() => PlayerDiedAnimationCompleteSubject.OnNext(Unit.Default);

        public static MonoBehaviour CurrentPlayer => CurrentPlayerSet;

        internal static IObservable<Unit> PlayerDiedAnimationCompleted => PlayerDiedAnimationCompleteSubject;

        internal static IObservable<T> IsPlaying<T>(this IObservable<T> observable)
        {
            return observable.Where(_ => CurrentGameState.Value == GameState.Playing);
        }
    }
}