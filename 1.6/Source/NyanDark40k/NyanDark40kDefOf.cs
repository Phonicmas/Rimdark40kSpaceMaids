using RimWorld;
using Verse;

namespace NyanDark40k;

[DefOf]
public static class NyanDark40kDefOf
{
    public static BodyPartDef Jaw;
    public static StatDef BEWH_Cuteness;
    
    public static ThoughtDef BEWH_NekonaHandFed;
    
    public static ThingDef BEWH_SpaceMaidsDropPodBuilding;
    public static ThingDef BEWH_SpaceMaidsDropPodSkyfaller;
    
    public static ThingDef BEWH_PicnicTableDropPod;
    public static ThingDef BEWH_PicnicStoolDropPod;
    
    public static ThingDef BEWH_PicnicProjectile;
    
    public static ThingDef BEWH_BoltgunCookie;
    public static ThingDef BEWH_Cattuccino;
    public static ThingDef BEWH_Omurice;
    
    static NyanDark40kDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(NyanDark40kDefOf));
    }
}