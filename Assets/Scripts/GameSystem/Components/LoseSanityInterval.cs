namespace GameSystem.Components
{
    public class LoseSanityInterval : SanityInterval
    {
        public bool shouldLowerSanity = true;
        
        protected override void TriggerSanity()
        {
            if (shouldLowerSanity)
            {
                GameEvents.LowerSanity(SanityAmount);
            }
        } 
    }
}