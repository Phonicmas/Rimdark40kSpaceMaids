using RimWorld;
using Verse;

namespace NyanDark40k;

public class ThoughtWorker_Servitile : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    { 
        var needServitile = p.needs.TryGetNeed<Need_Servitile>();

        if (needServitile == null)
        {
            return false;
        }

        return needServitile.CurLevelPercentage switch
        {
            <= 0.1f => ThoughtState.ActiveAtStage(0),
            <= 0.3f => ThoughtState.ActiveAtStage(1),
            <= 0.5f => ThoughtState.ActiveAtStage(2),
            < 0.9f => ThoughtState.ActiveAtStage(3),
            >= 0.9f => ThoughtState.ActiveAtStage(4),
            _ => false
        };
    }
}