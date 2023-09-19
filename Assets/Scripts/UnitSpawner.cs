using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CastleDefence
{
    public class UnitSpawner : MonoBehaviour
    {
        //Singleton
        public static UnitSpawner Instance { get; private set; }

        public GameObject[] playerUnits;
        public GameObject[] enemyUnits;

        [SerializeField] Vector3 playerSpawnPoint = new Vector3(0, 2, 28);

        //public static event EventHandler OnAnyUnitSpawned;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitSpawner! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            // PlayerCash.Instance.OnBuySpearman += PlayerCash_OnSpawnSpearman;
            //PlayerCash.Instance.OnBuyTank += PlayerCash_OnSpawnTank;
            //PlayerCash.Instance.OnBuyRanger += PlayerCash_OnSpawnRanger;
        }

        //public void PlayerCash_OnSpawnSpearman(object sender, EventArgs e)
        //{
        //    GameObject clone = Instantiate(playerUnits[0], playerSpawnPoint, Quaternion.identity);

        //    //allUnits.Add(spearman);

        //    //AddUnitInList();

        //    UnitManager.Instance.SetupPlaceable(playerUnits[0]);
        //}


        private void PlayerCash_OnSpawnTank(object sender, EventArgs e)
        {
            Instantiate(playerUnits[1], playerSpawnPoint, Quaternion.identity);
        }
        private void PlayerCash_OnSpawnRanger(object sender, EventArgs e)
        {
            Instantiate(playerUnits[2], playerSpawnPoint, Quaternion.identity);
        }
    }
}
