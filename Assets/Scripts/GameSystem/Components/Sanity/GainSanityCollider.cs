namespace GameSystem.Components.Sanity
{
    public class GainSanityCollider : SanityCollider
    {
        protected override void OnPlayerCollide()
        {
            GameEvents.GainSanity(SanityAmount);
        }
    }
}