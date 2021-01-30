namespace GameSystem.Components
{
    public class GainSanityCollider : SanityCollider
    {
        protected override void OnPlayerCollide()
        {
            GameEvents.GainSanity(SanityAmount);
        }
    }
}