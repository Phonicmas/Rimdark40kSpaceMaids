using HarmonyLib;
using Verse;
using Verse.AI;

namespace NyanDark40k;

[HarmonyPatch(typeof(MentalStateHandler), "TryStartMentalState")]
public class NekonaFeedStopMentalState
{
    public static bool Prefix(ref bool __result, Pawn ___pawn, MentalStateDef stateDef, string reason = null, bool causedByMood = false, Pawn otherPawn = null, bool transitionSilently = false, bool causedByDamage = false, bool causedByPsycast = false)
    {
        var nekonaThought = ThoughtNekonaFeed.ActiveOn(___pawn);
        if (nekonaThought == null)
        {
            return true;
        }

        nekonaThought.SetMentalState(stateDef, reason, causedByMood, otherPawn, transitionSilently, causedByDamage, causedByPsycast);
        __result = false;
        return false;
    }
}
