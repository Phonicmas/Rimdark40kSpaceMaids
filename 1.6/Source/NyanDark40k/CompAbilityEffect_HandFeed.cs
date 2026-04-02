using RimWorld;
using Verse;

namespace NyanDark40k;

public class CompAbilityEffect_HandFeed : CompAbilityEffect
{
    public new CompProperties_AbilityHandFeed Props => (CompProperties_AbilityHandFeed)props;

    public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
    {
        var pawn = target.Pawn;
        return pawn != null;
    }
        
    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        var pawn = target.Pawn;

        pawn.needs.food.CurLevelPercentage += 1;

        pawn.needs.mood?.thoughts.memories.TryGainMemory(NyanDark40kDefOf.BEWH_NekonaHandFed);
    }
        
    public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
    {
        base.Valid(target, throwMessages);
        return target.Pawn != null;
    }
}