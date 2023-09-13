using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleDefence
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance { get; private set; }	

        public GameObject healthBarPrefab;
		public GameObject gameOverUI;
        [SerializeField] GameObject winGameUI;

        private List<HealthBar> healthBars;
        private Transform healthBarContainer;

		private void Awake()
		{
			healthBars = new List<HealthBar>();
            healthBarContainer = new GameObject("HealthBarContainer").transform;

            if (Instance != null)
            {
                Debug.LogError("There's more than one UIManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

		public void AddHealthUI(ThinkingPlaceable p)
        {
            GameObject newUIObject = Instantiate<GameObject>(healthBarPrefab, p.transform.position+ new Vector3(0,3f,0), Quaternion.identity, healthBarContainer);
            p.healthBar = newUIObject.GetComponent<HealthBar>(); //store the reference in the ThinkingPlaceable itself
            p.healthBar.Initialise(p);
			
			healthBars.Add(p.healthBar);
        }

		public void RemoveHealthUI(ThinkingPlaceable p)
		{
			healthBars.Remove(p.healthBar);
			
			Destroy(p.healthBar.gameObject);
		}

		public void ShowGameOverUI()
		{
			gameOverUI.SetActive(true);
		}

        public void ShowWinGameUI()
        {
            winGameUI.SetActive(true);
        }


        public void OnRetryButton()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}

		private void LateUpdate()
		{
			for(int i=0; i<healthBars.Count; i++)
			{
				healthBars[i].Move();
			}
		}
	}
}