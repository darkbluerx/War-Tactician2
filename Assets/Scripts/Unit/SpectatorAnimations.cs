using System;
using System.Collections;
using UnityEngine;

namespace CastleDefence
{
    /// <summary>
    /// Katsomon sotilas. Istuu ottelun ajan. Kun ottelu päättyy, oman joukkueen
    /// voittaessa sotilas tanssii ja hävitessä suuttuu.
    /// Kuuntelee GameManagerin olemassa olevia tapahtumia:
    ///   WinGame()  -> OnRallying/OnCheering = sininen (Player) voitti
    ///   GameOver() -> OnAngry               = punainen (Opponent) voitti
    /// Voiton voi ilmoittaa myös suoraan omasta koodista: TeamWon(Placeable.Faction).
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class SpectatorAnimations : MonoBehaviour
    {
        public enum Team { Blue, Red } // Blue = Placeable.Faction.Player, Red = Placeable.Faction.Opponent

        [SerializeField] Animator animator;
        [Tooltip("Jos tyhjä, haetaan kohtauksesta automaattisesti.")]
        [SerializeField] GameManager gameManager;

        [Header("Joukkue")]
        [SerializeField] Team team = Team.Blue;

        [Header("Ottelun loppu")]
        [Tooltip("Animator-tilat, joista voittajat arpovat tanssinsa.")]
        [SerializeField] string[] danceStates = { "Dancing", "Dancing2" };
        [SerializeField] bool losersGetAngry = true;
        [SerializeField] float crossFadeTime = 0.25f;
        [Tooltip("Satunnainen viive sekunteina, ettei koko yleisö reagoi samalla hetkella.")]
        [SerializeField] float maxReactionDelay = 0.5f;

        static readonly string[] Triggers = { "IsSitting", "IsAngry", "IsClapping", "IsRallying", "IsCheering", "IsDancing" };

        bool matchOver;

        public Team SpectatorTeam => team;

        private void Awake()
        {
            if (animator == null) animator = GetComponent<Animator>();
            if (gameManager == null) gameManager = FindFirstObjectByType<GameManager>();
        }

        private void OnEnable()
        {
            if (gameManager == null) return;
            gameManager.OnClappingAnimation += UnitAnimations_OnClapping;
            gameManager.OnRallyingAnimation += GameManager_OnBlueWon;
            gameManager.OnCheeringAnimation += GameManager_OnBlueWon;
            gameManager.OnAngryAnimation += GameManager_OnRedWon;
        }

        private void OnDisable()
        {
            if (gameManager == null) return;
            gameManager.OnClappingAnimation -= UnitAnimations_OnClapping;
            gameManager.OnRallyingAnimation -= GameManager_OnBlueWon;
            gameManager.OnCheeringAnimation -= GameManager_OnBlueWon;
            gameManager.OnAngryAnimation -= GameManager_OnRedWon;
        }

        private void GameManager_OnBlueWon(object sender, EventArgs e) => TeamWon(Placeable.Faction.Player);
        private void GameManager_OnRedWon(object sender, EventArgs e) => TeamWon(Placeable.Faction.Opponent);

        /// <summary>Ilmoita voittaja. Voittajajoukkueen katsojat tanssivat, häviäjät suuttuvat.</summary>
        public void TeamWon(Placeable.Faction winner)
        {
            if (matchOver) return;
            matchOver = true;

            bool myTeamWon = (team == Team.Blue && winner == Placeable.Faction.Player)
                          || (team == Team.Red && winner == Placeable.Faction.Opponent);

            if (myTeamWon && danceStates != null && danceStates.Length > 0)
                StartCoroutine(PlayEndState(danceStates[UnityEngine.Random.Range(0, danceStates.Length)]));
            else if (!myTeamWon && losersGetAngry)
                StartCoroutine(PlayEndState("Angry"));
        }

        private IEnumerator PlayEndState(string stateName)
        {
            if (maxReactionDelay > 0f)
                yield return new WaitForSeconds(UnityEngine.Random.Range(0f, maxReactionDelay));

            foreach (string trigger in Triggers) animator.ResetTrigger(trigger);

            int stateHash = Animator.StringToHash(stateName);
            if (!animator.HasState(0, stateHash))
            {
                Debug.LogWarning($"SpectatorAnimations: Animatorissa ei ole tilaa '{stateName}'.", this);
                yield break;
            }

            // Satunnainen aloituskohta, jotta tanssijat eivät liiku samassa tahdissa
            animator.CrossFadeInFixedTime(stateHash, crossFadeTime, 0, UnityEngine.Random.Range(0f, 1.5f));
        }

        public void UnitAnimations_OnSitting(object sender, EventArgs e)
        {
            animator.SetTrigger("IsSitting");
        }

        public void UnitAnimations_OnAngry(object sender, EventArgs e)
        {
            animator.SetTrigger("IsAngry");
        }

        public void UnitAnimations_OnClapping(object sender, EventArgs e)
        {
            if (matchOver) return;
            animator.SetTrigger("IsClapping");
        }

        public void UnitAnimations_OnRallying(object sender, EventArgs e)
        {
            animator.SetTrigger("IsRallying");
        }

        public void UnitAnimations_OnCheering(object sender, EventArgs e)
        {
            animator.SetTrigger("IsCheering");
        }

        public void UnitAnimations_OnDancing(object sender, EventArgs e)
        {
            animator.SetTrigger("IsDancing");
        }
    }
}
