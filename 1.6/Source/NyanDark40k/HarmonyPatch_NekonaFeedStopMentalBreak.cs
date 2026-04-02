using System.Collections.Generic;
using Genes40k;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace NyanDark40k;

[HarmonyPatch(typeof(MentalBreakWorker), "TryStart")]
public class NekonaFeedStopMentalBreak
{
    public static bool Prefix(ref bool __result, MentalBreakWorker __instance, Pawn pawn, string reason, bool causedByMood)
    {
        var thoughts = new List<Thought>();
        pawn?.needs?.mood?.thoughts?.GetAllMoodThoughts(thoughts);
        
        if (thoughts.FirstOrDefault(thought => thought is ThoughtNekonaFeed) is not ThoughtNekonaFeed nekonaThought)
        {
            return true;
        }

        nekonaThought.SetMentalBreak(__instance.def, reason, causedByMood);
        __result = false;
        return false;
    }
}