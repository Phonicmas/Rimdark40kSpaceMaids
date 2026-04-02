using HarmonyLib;
using NyanDark40k;
using RimWorld;
using Verse;

namespace Genes40k;

[HarmonyPatch(typeof(MemoryThoughtHandler), "RemoveMemory")]
public class NekonaFeedStartMentalUndraft
{
    public static void Postfix(Pawn ___pawn, Thought_Memory th)
    {
        if (th is not ThoughtNekonaFeed thoughtNekonaFeed)
        {
            return;
        }

        thoughtNekonaFeed.TryDoMentalBreak();
    }
}