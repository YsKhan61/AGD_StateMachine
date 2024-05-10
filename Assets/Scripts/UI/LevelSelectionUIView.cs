using UnityEngine;

namespace ClassroomIGI.UI
{
    public class LevelSelectionUIView : MonoBehaviour, IUIView
    {
        private LevelSelectionUIController controller;
        [SerializeField] private Transform levelButtonContainer;

        public void SetController(IUIController controllerToSet) => controller = controllerToSet as LevelSelectionUIController;

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public LevelButtonView AddButton(LevelButtonView levelButtonPrefab) => Instantiate(levelButtonPrefab, levelButtonContainer);
    }
}