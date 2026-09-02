using System.Collections.Generic;
using Genes40k;
using RimWorld;
using Verse;

namespace NyanDark40k;

public class Ability_PicnicDrop: Ability_SteelRain
{
    protected override void SpawnSkyfaller(List<IntVec3> cellsToSpawn)
    {
        if (defMod?.fromFaction == null || defMod.innerThing == null || defMod.skyFaller == null)
        {
            return;
        }

        var map = pawn?.Map;
        if (map == null)
        {
            return;
        }
        
        var faction = Find.FactionManager.FirstFactionOfDef(defMod.fromFaction);
        foreach (var cell in cellsToSpawn)
        {
            if (!cell.InBounds(map))
            {
                continue;
            }

            var innerThing = ThingMaker.MakeThing(defMod.innerThing);
            innerThing.SetFactionDirect(faction);

            SkyfallerMaker.SpawnSkyfaller(defMod.skyFaller, innerThing, cell, map);
        }
    } 
}
