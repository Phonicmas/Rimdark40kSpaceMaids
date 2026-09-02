using RimWorld;
using Verse;

namespace NyanDark40k;

public class CompAbilityEffect_HandFeed : CompAbilityEffect
{
    public new CompProperties_AbilityHandFeed Props => (CompProperties_AbilityHandFeed)props;

    public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
    {
        return CanFeed(target.Pawn);
    }
        
    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        var pawn = target.Pawn;

        if (!CanFeed(pawn))
        {
            return;
        }

        pawn.needs.food.CurLevelPercentage += Props.foodRestored;

        pawn.needs.mood?.thoughts.memories.TryGainMemory(Props.thoughtGained ?? NyanDark40kDefOf.BEWH_NekonaHandFed);
    }
        
    public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
    {
        return base.Valid(target, throwMessages) && CanFeed(target.Pawn);
    }

    private static bool CanFeed(Pawn pawn)
    {
        return pawn?.needs?.food != null;
    }
}
