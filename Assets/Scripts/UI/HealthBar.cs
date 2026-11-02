using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CastleDefence
{
    public class HealthBar : MonoBehaviour
    {
        public RectTransform bar;
        public GameObject wholeWidget;

        [Header("Rakennukset (linnat)")]
        [Tooltip("Kuinka paljon rakennuksen korkeimman kohdan yläpuolelle palkki nostetaan.")]
        [SerializeField] float buildingBarMargin = 1f;
        [Tooltip("Rakennusten palkin koko verrattuna yksiköiden palkkiin.")]
        [SerializeField] float buildingBarScale = 2.5f;

        private bool isHidden = true;
        private float originalHP;
        private float currentHP;
        private Transform transformToFollow;
        private Vector3 followOffset = new Vector3(0f, 2f, 0f);

		private Color red = new Color32(252, 35, 13, 255);
		private Color blue = new Color32(31, 132, 255, 255);

        public void Initialise(ThinkingPlaceable p)
        {
            originalHP = currentHP = p.hitPoints;
            transformToFollow = p.transform;

            bar.GetComponent<Image>().color = (p.faction == Placeable.Faction.Player) ? blue : red;

            if (p.pType != Placeable.PlaceableType.Unit && TryGetModelBounds(p.transform, out Bounds bounds))
            {
                //Rakennukset: palkki mallin korkeimman kohdan yläpuolelle, ettei linna peitä sitä kameralta
                Vector3 topCenter = new Vector3(bounds.center.x, bounds.max.y + buildingBarMargin, bounds.center.z);
                followOffset = topCenter - p.transform.position;
                wholeWidget.transform.localPosition = Vector3.zero;
                wholeWidget.transform.localScale *= buildingBarScale;
            }
            else
            {
                wholeWidget.transform.localPosition = new Vector3(0f,
                                                                (p.pType == Placeable.PlaceableType.Unit) ? 3f : 6f,
                                                                (p.pType == Placeable.PlaceableType.Unit) ? 0f : -2f); //set the vertical position based on the type of Placeable
            }
            wholeWidget.SetActive(false);
        }

        public void SetHealth(float newHP)
        {
            if(isHidden)
            {
                wholeWidget.SetActive(true);
                isHidden = false;
            }

            float ratio = 0f;
            if(newHP > 0f)
            {
                ratio = newHP/originalHP;
            }

            bar.localScale = new Vector3(ratio, 1f, 1f);
        }

        public void Move()
        {
            if(transformToFollow != null)
                transform.position = transformToFollow.position + followOffset;
        }

        //Yhdistää mallin MeshRenderer- ja SkinnedMeshRenderer-rajat (partikkelit, kuten soihtujen tuli, jätetään pois)
        private static bool TryGetModelBounds(Transform root, out Bounds bounds)
        {
            bounds = default;
            bool found = false;
            foreach (Renderer r in root.GetComponentsInChildren<Renderer>())
            {
                if (!(r is MeshRenderer) && !(r is SkinnedMeshRenderer)) continue;
                if (!found) { bounds = r.bounds; found = true; }
                else bounds.Encapsulate(r.bounds);
            }
            return found;
        }
    }
}