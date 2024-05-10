/**  This script demonstrates implementation of the Service Locator Pattern.
*  If you're interested in learning about Service Locator Pattern, 
*  you can find a dedicated course on Outscal's website.
*  Link: https://outscal.com/courses
**/

using UnityEngine;
using ClassroomIGI.Utilities;
using ClassroomIGI.Enemy;
using ClassroomIGI.Sound;
using ClassroomIGI.Level;
using ClassroomIGI.Player;
using System.Collections.Generic;
using ClassroomIGI.UI;
using ClassroomIGI.Events;
using ClassroomIGI.Coin;

namespace ClassroomIGI.Main
{
    [DefaultExecutionOrder(-1000)]
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public SoundService SoundService { get; private set; }
        public EventService EventService { get; private set; }
        public LevelService LevelService { get; private set; }
        public PlayerService PlayerService { get; private set; }
        public EnemyService EnemyService { get; private set; }

        public CoinService CoinService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        // Scriptable Objects:
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private List<LevelScriptableObject> levelScriptableObjects;

        // Scene References:
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgMusicSource;

        // prefabs
        [SerializeField] private CoinView coinPrefab;

        /// layers
        [SerializeField] private LayerMask obstacleLayer;
        public LayerMask ObstacleLayer => obstacleLayer;

        [SerializeField] private LayerMask playerLayer;
        public LayerMask PlayerLayer => playerLayer;

        [SerializeField] private LayerMask coinLayer;
        public LayerMask CoinLayer => coinLayer;

        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            SoundService = new SoundService(soundScriptableObject, sfxSource, bgMusicSource);
            LevelService = new LevelService(levelScriptableObjects);
            PlayerService = new PlayerService(playerScriptableObject);
            EnemyService = new EnemyService();
            CoinService = new CoinService(coinPrefab);
        }

        private void Start() => UIService.ShowLevelSelectionUI(levelScriptableObjects.Count);
    }
}