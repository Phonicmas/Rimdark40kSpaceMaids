using HarmonyLib;
using RimWorld;
using Verse;

namespace NyanDark40k;

[HarmonyPatch(typeof(MemoryThoughtHandler), "RemoveMemory")]
public class NekonaFeedResumeMentalBreak
{
    public static void Postfix(Pawn ___pawn, Thought_Memory th)
    {
        if (th is not ThoughtNekonaFeed thoughtNekonaFeed)
        {
            return;
        }

        if (ThoughtNekonaFeed.ActiveOn(___pawn) != null)
        {
            return;
        }

        thoughtNekonaFeed.TryDoMentalBreak();
    }
}
