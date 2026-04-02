using HarmonyLib;
using Verse;

namespace NyanDark40k;

public class NyanDark40kMod : Mod
{
    public static string CurrentVersion;
    
    public static Harmony harmony;
    
    public NyanDark40kMod(ModContentPack content) : base(content)
    {
        CurrentVersion = content.ModMetaData.ModVersion;
        harmony = new Harmony("NyanDark40k.Mod");
        harmony.PatchAll();
    }
}