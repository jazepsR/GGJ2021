using UniRx;
using UnityEngine;

namespace GameSystem
{
    public static class PlayerStats
    {
        internal static readonly IReactiveProperty<float> SanityUpdater = new ReactiveProperty<float>(0f);

        public static IReadOnlyReactiveProperty<float> CurrentSanity => SanityUpdater;
        public static float MaxSanity = 0f;

        public static IReactiveProperty<bool> IsJumping { get; } = new ReactiveProperty<bool>(false);
    }
}