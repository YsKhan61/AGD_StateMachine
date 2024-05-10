using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClassroomIGI.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        private LevelSelectionUIController owner;
        private int levelId;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnLevelButtonClicked);

        public void SetOwner(LevelSelectionUIController owner) => this.owner = owner;

        private void OnLevelButtonClicked() => owner.OnLevelSelected(levelId);

        public void SetLevelID(int levelId)
        {
            this.levelId = levelId;
            buttonText.SetText("Level " + levelId);
        }
    }
}