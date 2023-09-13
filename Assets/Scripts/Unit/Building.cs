using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleDefence
{
    public class Building : ThinkingPlaceable
    {
        public void Activate(Faction pFaction, PlaceableData pData)
        {
            pType = pData.pType;
            faction = pFaction;
            hitPoints = pData.hitPoints;
            targetType = pData.targetType;
        }

        protected override void Die()
        {
            if (OnDie != null)
                base.Die();
        }
    }
}
