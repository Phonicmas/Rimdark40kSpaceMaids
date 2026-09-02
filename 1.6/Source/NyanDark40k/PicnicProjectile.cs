using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NyanDark40k;

public class PicnicProjectile : Projectile
{
    private static readonly IntVec3[] StoolOffsets =
    [
        IntVec3.North,
        IntVec3.South,
        IntVec3.East,
        IntVec3.West,
    ];

    private static List<ThingDef> spaceMaidFood;

    private static List<ThingDef> SpaceMaidFood => spaceMaidFood ??= new List<ThingDef>
    {
        NyanDark40kDefOf.BEWH_BoltgunCookie,
        NyanDark40kDefOf.BEWH_Cattuccino,
        NyanDark40kDefOf.BEWH_Omurice,
    };

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        SpawnPicnic(Map, Position);

        base.Impact(hitThing, blockedByShield);
    }

    /// <summary>
    /// Spawns the table, a random dish and up to four stools, skipping any cell the map cannot take.
    /// </summary>
    private static void SpawnPicnic(Map map, IntVec3 position)
    {
        if (map == null || !position.InBounds(map))
        {
            return;
        }

        var table = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicTableDropPod, position, map);
        if (table == null)
        {
            return;
        }

        table.SetFactionDirect(Faction.OfPlayer);

        GenSpawn.Spawn(SpaceMaidFood.RandomElement(), table.Position, map);

        foreach (var offset in StoolOffsets)
        {
            var stoolLoc = table.Position + offset;
            if (!stoolLoc.InBounds(map))
            {
                continue;
            }

            var stool = GenSpawn.Spawn(NyanDark40kDefOf.BEWH_PicnicStoolDropPod, stoolLoc, map);
            stool?.SetFactionDirect(Faction.OfPlayer);
        }
    }
}
