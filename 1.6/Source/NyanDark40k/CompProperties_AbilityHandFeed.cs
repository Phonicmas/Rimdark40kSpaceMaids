using RimWorld;

namespace NyanDark40k;

public class CompProperties_AbilityHandFeed : CompProperties_AbilityEffect
{
    public float foodRestored = 1f;

    public ThoughtDef thoughtGained;

    public CompProperties_AbilityHandFeed()
    {
        compClass = typeof(CompAbilityEffect_HandFeed);
    }
}
