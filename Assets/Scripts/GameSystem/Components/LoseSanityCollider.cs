namespace GameSystem.Components
{
    public class LoseSanityCollider : SanityCollider
    {
        protected override void OnPlayerCollide()
        {
            GameEvents.LowerSanity(SanityAmount);
        }
    }
}