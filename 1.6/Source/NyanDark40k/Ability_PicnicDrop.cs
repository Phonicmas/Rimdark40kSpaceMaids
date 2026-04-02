using System;
using System.Collections.Generic;
using System.Linq;
using Genes40k;
using RimWorld;
using UnityEngine;
using Verse;

namespace NyanDark40k;

public class Ability_PicnicDrop: Ability_SteelRain
{
    protected override void SpawnSkyfaller(List<IntVec3> cellsToSpawn)
    {
        if (defMod.fromFaction == null)
        {
            return;
        }
        
        var faction = Find.FactionManager.FirstFactionOfDef(defMod.fromFaction);
        foreach (var cell in cellsToSpawn)
        {
            var innerThing = ThingMaker.MakeThing(NyanDark40kDefOf.BEWH_SpaceMaidsDropPodBuilding);
            innerThing.SetFactionDirect(faction);

            SkyfallerMaker.SpawnSkyfaller(NyanDark40kDefOf.BEWH_SpaceMaidsDropPodSkyfaller, innerThing, cell, pawn.Map);
        }
    } 
}