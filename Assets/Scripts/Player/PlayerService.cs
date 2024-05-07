using StatePattern.Main;

namespace StatePattern.Player
{
    public class PlayerService
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerController playerController;

        public PlayerService(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            ResetPlayerDatas();
            SubscribeToEvents();
        }

        ~PlayerService()
        {
            UnsubscribeToEvents();
        }

        public void SpawnPlayer(int levelId)
        {
            playerController = new PlayerController(playerScriptableObject);
            GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnPlayer);
        }

        public PlayerController GetPlayer() => playerController;

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnPlayer);
            GameService.Instance.EventService.OnCoinCollected.AddListener(UpdateScore);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnPlayer);
            GameService.Instance.EventService.OnCoinCollected.RemoveListener(UpdateScore);
        }

        private void UpdateScore(int score)
        {
            int currentScore = playerScriptableObject.CurrentScore.Value;
            playerScriptableObject.CurrentScore.SetValue(currentScore + score);
        }

        private void ResetPlayerDatas()
        {
            playerScriptableObject.CurrentScore.SetValue(0);
        }
    }
}