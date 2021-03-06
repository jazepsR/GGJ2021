using System;
using UniRx;
using UnityEngine;

namespace GameSystem
{
    public static class GameStateManager
    {
        private static ISubject<Unit> PlayerDiedAnimationCompleteSubject { get; } = new Subject<Unit>();
        
        internal static MonoBehaviour currentPlayerSet;
        internal static ISubject<Unit> PlayerCompleteLevelSubject { get; } = new Subject<Unit>();
        public static IObservable<Unit> PlayerDiedAnimationCompleted => PlayerDiedAnimationCompleteSubject;

        public static MonoBehaviour CurrentPlayer => currentPlayerSet;
        
        public static IReactiveProperty<GameState> CurrentGameState { get; } =
            new ReactiveProperty<GameState>(GameState.Initial);
        public static IReactiveProperty<bool> TreasureSpawned { get; } =
            new ReactiveProperty<bool>(false);
        public static IReactiveProperty<bool> TreasureChasing { get; } =
            new ReactiveProperty<bool>(false);

        public static IObservable<Unit> PlayerCompleteLevel => PlayerCompleteLevelSubject;

        public static void PlayerDiedAnimationComplete() => PlayerDiedAnimationCompleteSubject.OnNext(Unit.Default);
        public static IObservable<T> IsPlaying<T>(this IObservable<T> observable)
        {
            return observable.Where(_ => CurrentGameState.Value == GameState.Playing);
        }
    }
}