namespace GameSystem.Components.Sanity
{
    public class LoseSanityCollider : SanityCollider
    {
        protected override void OnPlayerCollide()
        {
            GameEvents.LowerSanity(SanityAmount);
        }
    }
}