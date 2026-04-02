using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NyanDark40k;

public class PicnicProjectile : Projectile
{
    private List<ThingDef> SpaceMaidFood =
        [
            NyanDark40kDefOf.BEWH_BoltgunCookie, 
            NyanDark40kDefOf.BEWH_Cattuccino, 
            NyanDark40kDefOf.BEWH_Omurice
        ];
    
    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        var table = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicTableDropPod, Position, Map);
        
        table.SetFactionDirect(Faction.OfPlayer);
        
        GenSpawn.Spawn(SpaceMaidFood.RandomElement(), table.Position, Map);
        
        var stoolLoc = table.Position;

        var northLoc = new IntVec3(stoolLoc.x, stoolLoc.y, stoolLoc.z + 1);
        var southLoc = new IntVec3(stoolLoc.x, stoolLoc.y, stoolLoc.z - 1);
        var eastLoc = new IntVec3(stoolLoc.x + 1, stoolLoc.y, stoolLoc.z);
        var westLoc = new IntVec3(stoolLoc.x - 1, stoolLoc.y, stoolLoc.z);
        
        var stool1 = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicStoolDropPod, northLoc, Map);
        var stool2 = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicStoolDropPod, southLoc, Map);
        var stool3 = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicStoolDropPod, eastLoc, Map);
        var stool4 = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicStoolDropPod, westLoc, Map);
        
        stool1.SetFactionDirect(Faction.OfPlayer);
        stool2.SetFactionDirect(Faction.OfPlayer);
        stool3.SetFactionDirect(Faction.OfPlayer);
        stool4.SetFactionDirect(Faction.OfPlayer);
        
        base.Impact(hitThing, blockedByShield);
    }
}