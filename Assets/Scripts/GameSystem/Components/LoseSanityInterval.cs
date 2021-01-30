namespace GameSystem.Components
{
    public class LoseSanityInterval : SanityInterval
    {
        protected override void TriggerSanity() => GameEvents.LowerSanity(SanityAmount);
    }
}