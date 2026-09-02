using RimWorld;
using Verse;

namespace NyanDark40k;

public class ThoughtWorker_Cuteness : ThoughtWorker
{
    private const int CutenessCacheTicks = 250;

    protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
    {
        if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
        {
            return false;
        }

        var cuteness = other.GetStatValue(NyanDark40kDefOf.BEWH_Cuteness, cacheStaleAfterTicks: CutenessCacheTicks);
        if (cuteness <= 5)
        {
            return false;
        }

        return cuteness switch
        {
            <= 10 => ThoughtState.ActiveAtStage(0),
            <= 25 => ThoughtState.ActiveAtStage(1),
            <= 50 => ThoughtState.ActiveAtStage(2),
            < 200 => ThoughtState.ActiveAtStage(3),
            _ => ThoughtState.ActiveAtStage(4),
        };
    }
}
