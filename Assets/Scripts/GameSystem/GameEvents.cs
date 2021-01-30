using System;
using GameSystem.Dto;
using UniRx;

namespace GameSystem
{
    public static class GameEvents
    {
        private static readonly ISubject<PlayerDeathDto> PlayerDeathSubject = new Subject<PlayerDeathDto>();
        private static readonly ISubject<SanityDto> SanityLoweredSubject = new Subject<SanityDto>();
        private static readonly ISubject<SanityDto> SanityGainedSubject = new Subject<SanityDto>();

        public static IObservable<PlayerDeathDto> PlayerDeath => PlayerDeathSubject;
        public static IObservable<SanityDto> SanityLowered => SanityLoweredSubject;
        public static IObservable<SanityDto> SanityGained => SanityGainedSubject;

        internal static void KillPlayer(PlayerDeathDto dto)
        {
            PlayerDeathSubject.OnNext(dto);
        }

        internal static void LowerSanity(SanityDto dto)
        {
            SanityLoweredSubject.OnNext(dto);
        }

        internal static void GainSanity(SanityDto dto)
        {
            SanityGainedSubject.OnNext(dto);
        }
    }
}
