using UnityEngine;

namespace TicTacGame
{
    public class PlayerContainer : MonoBehaviour
    {
        public static PlayerContainer Instance;

        private APlayer[] _playerTypes;

        private void Awake()
        {
            Instance = this;
            _playerTypes = new APlayer[2];
            _playerTypes[0] = new HumanPlayer();
            _playerTypes[1] = new RandomAIPlayer();
        }

        public APlayer GetPlayer(PlayerType type)
        {
            return _playerTypes[(int)type];
        }
    }

    public enum PlayerType
    {
        Human,
        AI
    }
}