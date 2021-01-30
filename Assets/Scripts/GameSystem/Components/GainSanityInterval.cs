namespace GameSystem.Components
{
    public class GainSanityInterval : SanityInterval
    {
        protected override void TriggerSanity() => GameEvents.GainSanity(SanityAmount);
    }
}