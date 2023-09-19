using CastleDefence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

namespace CastleDefence
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } //singleton

        //public event EventHandler OnSittingAnimation;
        public event EventHandler OnAngryAnimation;
        public event EventHandler OnClappingAnimation;
        public event EventHandler OnRallyingAnimation;
        public event EventHandler OnCheeringAnimation;

        [SerializeField] AudioEvent winAudioEvent;
        [SerializeField] AudioEvent loseAudioEvent;
        [SerializeField] AudioEvent backGroundMusicAudioEvent;

        SpectatorAnimations specAnimations;

        [SerializeField] AudioSource source;
        [Space]

        [SerializeField] GameObject gameplayCanvas;
        public GameObject shopCanvas;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one GameManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            Time.timeScale = 0f;
            backGroundMusicAudioEvent.Play(source);

            var randomAnimation2 = UnityEngine.Random.Range(0, 2); 
            if (randomAnimation2 == 1) OnClappingAnimation?.Invoke(this, EventArgs.Empty); //change animation if this is 1
        }

        public void Replay() //Replay- and Back Button
        {
            SceneManager.LoadScene(0); //load menu
        }

        public void GameOver()
        {
            source.Stop(); //stop backgroundMusic
            loseAudioEvent.Play(source);

            OnAngryAnimation?.Invoke(this, EventArgs.Empty);

            //Time.timeScale = 0f;
            UIManager.Instance.ShowGameOverUI();
            gameplayCanvas.SetActive(false); //close shopPanel (UI)
        }

        public void WinGame()
        {
            source.Stop(); //stop backgroundMusic
            winAudioEvent.Play(source);

            var randomAnimation = UnityEngine.Random.Range(0, 2);
            if (randomAnimation == 1) OnRallyingAnimation?.Invoke(this, EventArgs.Empty);
            else OnCheeringAnimation?.Invoke(this, EventArgs.Empty);
            //Debug.Log(randomAnimation);

            //Time.timeScale = 0f;
            UIManager.Instance.ShowWinGameUI();
            gameplayCanvas.SetActive(false); //close shopPanel (UI)
        }

        //ShopPanel
        public void PlayGame() //Battle Button
        {
            gameplayCanvas.SetActive(true);
            shopCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Shopping()
        {
            Time.timeScale = 0f;
            shopCanvas.SetActive(true);
            gameplayCanvas.SetActive(false);
        }
    }
}
