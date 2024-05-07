using StatePattern.Utilities;
using UnityEngine;

namespace StatePattern.UI
{
    public class ScoreUIView : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;
        [SerializeField] private IntDataSO scoreData;

        private void Awake()
        {
            scoreText.text = "Score: 0";
        }

        private void OnEnable()
        {
            scoreData.OnValueChanged += UpdateScore;
        }

        private void OnDisable()
        {
            scoreData.OnValueChanged -= UpdateScore;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void UpdateScore(int score)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}