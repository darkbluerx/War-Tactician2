using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace CastleDefence
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } //Singleton

        public event EventHandler OnAngryAnimation;
        public event EventHandler OnClappingAnimation;
        public event EventHandler OnRallyingAnimation;
        public event EventHandler OnCheeringAnimation;

        [SerializeField] AudioEvent winAudioEvent;
        [SerializeField] AudioEvent loseAudioEvent;
        [SerializeField] AudioEvent backgroundMusicAudioEvent;
        [SerializeField] AudioEvent shopMusicAudioEvent;

        [SerializeField] AudioSource source;
        [Space]

        [SerializeField] GameObject gameplayCanvas;
        public GameObject shopCanvas;
        [Space]

        [Tooltip("Monenko sekunnin päästä häviöstä palataan päävalikkoon.")]
        [SerializeField] float returnToMenuDelay = 4f;

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
            shopMusicAudioEvent.Play(source);

            Time.timeScale = 0f;

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
            // LoseImage on GameplayCanvasin lapsi, joten koko canvasin piilottaminen piilotti myös häviöruudun.
            // Piilotetaan vain pelin aikaiset napit ja palataan hetken päästä päävalikkoon.
            HideGameplayButtons(UIManager.Instance.gameOverUI);
            StartCoroutine(ReturnToMainMenu());
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
            HideGameplayButtons(UIManager.Instance.WinGameUI);
        }

        //Piilottaa GameplayCanvasin pelin aikaiset napit (BlueShop, RedShop), mutta jättää lopputulosruudun näkyviin
        private void HideGameplayButtons(GameObject endScreen)
        {
            foreach (Transform child in gameplayCanvas.transform)
            {
                if (endScreen != null && endScreen.transform.IsChildOf(child)) continue;
                child.gameObject.SetActive(false);
            }
        }

        private IEnumerator ReturnToMainMenu()
        {
            yield return new WaitForSecondsRealtime(returnToMenuDelay);
            Replay(); //load menu
        }

        //ShopPanel
        public void PlayGame() //Battle Button
        {
            source.Stop();
            backgroundMusicAudioEvent.Play(source);
            gameplayCanvas.SetActive(true);
            shopCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Shopping()
        {
            Time.timeScale = 0f;
            shopCanvas.SetActive(true);
            //gameplayCanvas.SetActive(false);
        }
    }
}
