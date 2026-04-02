using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace NyanDark40k;

[HarmonyPatch(typeof(MentalStateHandler), "TryStartMentalState")]
public class NekonaFeedStopMentalState
{
    public static bool Prefix(ref bool __result, Pawn ___pawn, MentalStateDef stateDef, string reason = null, bool causedByMood = false, Pawn otherPawn = null, bool transitionSilently = false, bool causedByDamage = false, bool causedByPsycast = false)
    {
        var thoughts = new List<Thought>();
        ___pawn?.needs?.mood?.thoughts?.GetAllMoodThoughts(thoughts);
        
        if (thoughts.FirstOrDefault(thought => thought is ThoughtNekonaFeed) is not ThoughtNekonaFeed nekonaThought)
        {
            return true;
        }
        
        nekonaThought.SetMentalState(stateDef, reason, causedByMood, otherPawn, transitionSilently, causedByDamage, causedByPsycast);
        __result = false;
        return false;
    }
}