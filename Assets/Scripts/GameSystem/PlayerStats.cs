using UniRx;

namespace GameSystem
{
    public static class PlayerStats
    {
        internal static readonly IReactiveProperty<float> SanityUpdater = new ReactiveProperty<float>(0f);

        public static IReadOnlyReactiveProperty<float> CurrentSanity => SanityUpdater;
    }
}