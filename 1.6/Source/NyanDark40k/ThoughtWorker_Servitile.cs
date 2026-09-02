using RimWorld;
using Verse;

namespace NyanDark40k;

public class ThoughtWorker_Servitile : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    { 
        var needServitile = p.needs?.TryGetNeed<Need_Servitile>();

        if (needServitile == null)
        {
            return false;
        }

        return needServitile.CurLevelPercentage switch
        {
            <= Need_Servitile.UnservedThreshold => ThoughtState.ActiveAtStage(0),
            <= Need_Servitile.IdleThreshold => ThoughtState.ActiveAtStage(1),
            <= Need_Servitile.ServingThreshold => ThoughtState.ActiveAtStage(2),
            < Need_Servitile.WellServingThreshold => ThoughtState.ActiveAtStage(3),
            _ => ThoughtState.ActiveAtStage(4),
        };
    }
}
