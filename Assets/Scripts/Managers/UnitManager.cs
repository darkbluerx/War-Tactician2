using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static CastleDefence.Placeable;

namespace CastleDefence
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance { get; private set; } //Singleton

        UIManager UIManager;
        [Space]

        [Header("Castles")]
        public GameObject playersCastle;
        public GameObject opponentCastle;
        public PlaceableData castlePData;
        [Space]

        [Header("Player Units")]
        public GameObject blueSpearman;
        public GameObject blueRanger;
        public GameObject blueTank;
        public GameObject blueCannoneer;
        public GameObject blueScout;
        [Space]

        [Header("Opponent Units")]
        public GameObject redSpearman;
        public GameObject redRanger;
        public GameObject redTank;
        public GameObject redCannoneer;
        public GameObject redScout;
        [Space]

        [Header("Units data")]
        public PlaceableData spearmanData;
        public PlaceableData rangerData;
        public PlaceableData tankData;
        public PlaceableData cannoneerData;
        public PlaceableData scoutData;

        [Header("Units spawn point")]
        [SerializeField] Vector3 playerSpawn, enemySpawn;
        [Space]

        [Header("Unit Lists")]
        List<ThinkingPlaceable> playerUnits, opponentUnits;
        List<ThinkingPlaceable> playerBuildings, opponentBuildings;
        List<ThinkingPlaceable> allPlayers, allOpponents; //contains both Buildings and Units
        List<ThinkingPlaceable> allThinkingPlaceables;
        [Space]

        //List<Projectile> allProjectiles = new List<Projectile>();
        bool gameOver = false;
        bool updateAllPlaceables = false; //used to force an update of all AIBrains in the Update loop

        private void Awake()
        {
            UIManager = GetComponent<UIManager>();

            Singleton();
            CopyLists();
        }

        private void Singleton()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void CopyLists()
        {
            playerUnits = new List<ThinkingPlaceable>();
            playerBuildings = new List<ThinkingPlaceable>();
            opponentUnits = new List<ThinkingPlaceable>();
            opponentBuildings = new List<ThinkingPlaceable>();
            allPlayers = new List<ThinkingPlaceable>();
            allOpponents = new List<ThinkingPlaceable>();
            allThinkingPlaceables = new List<ThinkingPlaceable>();
        }

        private void Start() //add team castles
        {
            SetupPlaceable(playersCastle, castlePData, Placeable.Faction.Player);
            SetupPlaceable(opponentCastle, castlePData, Placeable.Faction.Opponent);
        }
        private void FixedUpdate()
        {
            UpdateUnitStates();
        }

        private void UpdateUnitStates()
        {
            if (gameOver) return;

            ThinkingPlaceable p;
            ThinkingPlaceable targetToPass;

            for (int pN = 0; pN < allThinkingPlaceables.Count; pN++)
            {
                p = allThinkingPlaceables[pN];

                if (updateAllPlaceables) p.state = ThinkingPlaceable.States.Idle; //forces the assignment of a target in the switch below

                switch (p.state)
                {
                    case ThinkingPlaceable.States.Idle:
                        //this if is for innocuous testing Units
                        if (p.targetType == Placeable.PlaceableTarget.None)
                            break;

                        //find closest target and assign it to the ThinkingPlaceable
                        bool targetFound = FindClosestInList(p.transform.position, GetAttackList(p.faction, p.targetType), out targetToPass);
                        //if (!targetFound) Debug.LogError("No more targets!"); //this should only happen on Game Over
                        p.SetTarget(targetToPass);
                        //Debug.Log("idle " + targetToPass.ToString());
                        p.Seek();
                        break;

                    case ThinkingPlaceable.States.Seeking:
                        if (p.IsTargetInRange())
                        {
                            p.StartAttack();
                        }
                        else
                        {
                            bool targetF = FindClosestInList(p.transform.position, GetAttackList(p.faction, p.targetType), out targetToPass);
                            //if (!targetF) Debug.LogError("No more targets!"); //this should only happen on Game Over
                            p.SetTarget(targetToPass);
                            p.Seek();
                        }
                        break;

                    case ThinkingPlaceable.States.Attacking:
                        if (p.IsTargetInRange())
                        {
                            if (Time.time >= p.lastBlowTime + p.attackRatio)
                            {
                                p.DealBlow();
                                //Animation will produce the damage, calling animation events OnDealDamage and OnProjectileFired. See ThinkingPlaceable

                                p.DealDamage();
                            }
                            //Debug.Log("State is attacking");
                        }
                        break;

                    case ThinkingPlaceable.States.Dead:
                        Debug.LogError("A dead ThinkingPlaceable shouldn't be in this loop");
                        break;
                }
            }
        }

        public void BuySpearButton() => BuyUnit(blueSpearman, spearmanData, Placeable.Faction.Player);
        public void BuyTankButton() => BuyUnit(blueTank, tankData, Placeable.Faction.Player);
        public void BuyRangerButton() => BuyUnit(blueRanger, rangerData, Placeable.Faction.Player);
        public void BuyCannoneerButton() => BuyUnit(blueCannoneer, cannoneerData, Placeable.Faction.Player);
        public void BuyScoutButton() => BuyUnit(blueScout, scoutData, Placeable.Faction.Player);

        private void BuyUnit(GameObject go, PlaceableData pDataRef, Placeable.Faction pFaction)
        {
            if (OpponentCash.Instance.Enemy())
                //go = (go == blueSpearman) ? redSpearman : (go == blueTank) ? redTank : redRanger;
                go = (go == blueSpearman) ? redSpearman : (go == blueTank) ? redTank : (go == blueRanger) ? redRanger
                : (go == blueCannoneer) ? redCannoneer : redScout;

            if (go == blueSpearman || go == blueTank || go == blueRanger || go == blueCannoneer || go== blueScout)
            {
                GameObject clone = Instantiate(go,playerSpawn, Quaternion.identity);
                SetupPlaceable(clone, pDataRef, pFaction);
            }
            else
            {
                GameObject clone = Instantiate(go, enemySpawn, Quaternion.identity);
                SetupPlaceable(clone, pDataRef, Placeable.Faction.Opponent);
            }
            //GameObject clone = Instantiate(go, (go == blueSpearman || go == redSpearman) ? enemySpawn : playerSpawn, Quaternion.identity);
            //SetupPlaceable(clone, pDataRef, pFaction);
        }

        private void SetupPlaceable(GameObject go, PlaceableData pDataRef, Placeable.Faction pFaction)
        {
            //Add the appropriate script
            switch (pDataRef.pType)
            {
                case Placeable.PlaceableType.Unit:
                    Unit uScript = go.GetComponent<Unit>();
                    uScript.Activate(pFaction, pDataRef); //enables NavMeshAgent
                    uScript.OnDealDamage += OnPlaceableDealtDamage;
                    //uScript.OnProjectileFired += OnProjectileFired;
                    AddPlaceableToList(uScript); //add the Unit to the appropriate list
                    UIManager.AddHealthUI(uScript); //UIManager.AddHealthUI(uScript);
                    break;

                case Placeable.PlaceableType.Building:
                case Placeable.PlaceableType.Castle:
                    Building bScript = go.GetComponent<Building>();
                    bScript.Activate(pFaction, pDataRef);
                    bScript.OnDealDamage += OnPlaceableDealtDamage;
                    //bScript.OnProjectileFired += OnProjectileFired;
                    AddPlaceableToList(bScript); //add the Building to the appropriate list
                    UIManager.AddHealthUI(bScript);

                    //special case for castles
                    if (pDataRef.pType == Placeable.PlaceableType.Castle)
                    {
                        ThinkingPlaceable target = null;
                            bScript.OnDie += OnCastleDead;                      
                    }
                    break;
            }
            go.GetComponent<Placeable>().OnDie += OnPlaceableDead;
        }

        private void OnPlaceableDealtDamage(ThinkingPlaceable p)
        {
            if (p.target.state != ThinkingPlaceable.States.Dead)
            {
                float newHealth = p.target.SufferDamage(p.damage);
                p.target.healthBar.SetHealth(newHealth);
            }
            //Debug.Log("OnPlaceableDealtDamage");
        }

        private void OnCastleDead(Placeable c)
        {
            c.OnDie -= OnCastleDead;
            gameOver = true;  //stops the thinking loop
            if (c.faction == Faction.Player)
            {
                GameManager.Instance.GameOver();
            }

            if (c.faction == Faction.Opponent)
            {
                GameManager.Instance.WinGame();
            }

            //stop all the ThinkingPlaceables		
            ThinkingPlaceable thkPl;
            for (int pN = 0; pN < allThinkingPlaceables.Count; pN++)
            {
                thkPl = allThinkingPlaceables[pN];
                if (thkPl.state != ThinkingPlaceable.States.Dead)
                {
                    thkPl.Stop();
                    thkPl.transform.LookAt(c.transform.position);
                    UIManager.RemoveHealthUI(thkPl);
                }
            }
        }

        private List<ThinkingPlaceable> GetAttackList(Placeable.Faction f, Placeable.PlaceableTarget t)
        {
            switch (t)
            {
                case Placeable.PlaceableTarget.Both:
                    return (f == Placeable.Faction.Player) ? allOpponents : allPlayers;

                case Placeable.PlaceableTarget.OnlyBuildings:
                    return (f == Placeable.Faction.Player) ? opponentBuildings : playerBuildings;
                default:
                    Debug.LogError("What faction is this?? Not Player nor Opponent.");
                    return null;
            }
        }

        private bool FindClosestInList(Vector3 p, List<ThinkingPlaceable> list, out ThinkingPlaceable t)
        {
            t = null;
            bool targetFound = false;
            float closestDistanceSqr = Mathf.Infinity;

            for (int i = 0; i < list.Count; i++)
            {
                float sqrDistance = (p - list[i].transform.position).sqrMagnitude;
                if (sqrDistance < closestDistanceSqr)
                {
                    t = list[i];
                    closestDistanceSqr = sqrDistance;
                    targetFound = true;
                }
            }
            return targetFound;
        }

        private void OnPlaceableDead(Placeable p)
        {
            p.OnDie -= OnPlaceableDead; //remove the listener

            switch (p.pType)
            {
                case Placeable.PlaceableType.Unit:
                    Unit u = (Unit)p;
                    RemovePlaceableFromList(u);
                    u.OnDealDamage -= OnPlaceableDealtDamage;
                    //u.OnProjectileFired -= OnProjectileFired;
                    UIManager.RemoveHealthUI(u);
                    StartCoroutine(Dispose(u));
                    break;

                case Placeable.PlaceableType.Building:
                case Placeable.PlaceableType.Castle:
                    Building b = (Building)p;
                    RemovePlaceableFromList(b);
                    UIManager.RemoveHealthUI(b);
                    b.OnDealDamage -= OnPlaceableDealtDamage;
                    //b.OnProjectileFired -= OnProjectileFired;

                    //we don't dispose of the Castle
                    if (p.pType != Placeable.PlaceableType.Castle)
                        StartCoroutine(Dispose(b));
                    break;
            }
        }

        private IEnumerator Dispose(ThinkingPlaceable p)
        {
            yield return new WaitForSeconds(3f);

            Destroy(p.gameObject);
        }

        public void AddPlaceableToList(ThinkingPlaceable p)
        {
            allThinkingPlaceables.Add(p);

            if (p.faction == Placeable.Faction.Player)
            {
                allPlayers.Add(p);

                if (p.pType == Placeable.PlaceableType.Unit)
                    playerUnits.Add(p);
                else
                    playerBuildings.Add(p);
            }
            else if (p.faction == Placeable.Faction.Opponent)
            {
                allOpponents.Add(p);

                if (p.pType == Placeable.PlaceableType.Unit)
                    opponentUnits.Add(p);
                else
                    opponentBuildings.Add(p);
            }
            else
            {
                Debug.LogError("Error in adding a Placeable in one of the player/opponent lists");
            }
        }

        public void RemovePlaceableFromList(ThinkingPlaceable p)
        {

            allThinkingPlaceables.Remove(p);

            if (p.faction == Placeable.Faction.Player)
            {
                allPlayers.Remove(p);

                if (p.pType == Placeable.PlaceableType.Unit)
                    playerUnits.Remove(p);
                else
                    playerBuildings.Remove(p);
            }
            else if (p.faction == Placeable.Faction.Opponent)
            {
                allOpponents.Remove(p);

                if (p.pType == Placeable.PlaceableType.Unit)
                    opponentUnits.Remove(p);
                else
                    opponentBuildings.Remove(p);
            }
            else
            {
                Debug.LogError("Error in removing a Placeable from one of the player/opponent lists");
            }
        }
    }
}

