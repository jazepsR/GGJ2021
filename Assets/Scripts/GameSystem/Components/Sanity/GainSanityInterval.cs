namespace GameSystem.Components.Sanity
{
    public class GainSanityInterval : SanityInterval
    {
        protected override void TriggerSanity() => GameEvents.GainSanity(SanityAmount);
    }
}