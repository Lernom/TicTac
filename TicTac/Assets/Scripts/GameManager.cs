using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public AGameModule GameModule;

        public bool GameWorking => _gameRoutine != null;

        public UnityEvent<string> OnGameEnd;

        public UnityEvent<string> OnGameTurn;

        private APlayer[] _players;

        private Coroutine _gameRoutine;

        private void Awake()
        {
            Instance = this;
            _players = new APlayer[2];
        }

        private void Start()
        {
            SetupPlayer(0, PlayerType.Human);
            SetupPlayer(1, PlayerType.Human);
        }

        public void SetupPlayer(int id, PlayerType type)
        {
            _players[id] = PlayerContainer.Instance.GetPlayer(type);
        }

        public void StartGame()
        {
            if (_gameRoutine != null)
            {
                StopCoroutine(_gameRoutine);
                _gameRoutine = null;
            }
            if (_gameRoutine == null)
            {
              
                _gameRoutine = StartCoroutine(GameRoutine());
            }
        }

        private void GameEnd(string message)
        {
            Debug.Log(message);
            OnGameEnd?.Invoke(message);
            _gameRoutine = null;
            StartGame();
        }

        private IEnumerator GameRoutine()
        {
            yield return new WaitForSeconds(1f);

            GameModule.SetupGame();
            for (int i = 0; i < _players.Length; i++)
            {
                _players[i].Setup(GameModule);
            }

            yield return new WaitForSeconds(2f);

            while (GameModule.AnyEligibleMoves())
            {
                for (int i = 0; i < _players.Length; i++)
                {
                    OnGameTurn?.Invoke($"Player {i + 1} Turn");
                    if (!GameModule.AnyEligibleMoves())
                    {
                        yield return new WaitForSeconds(2f);
                        GameEnd("DRAW");
                        yield break;
                    }
                    var turnDone = false;
                    while (!turnDone)
                    {
                        yield return _players[i].MakeTurn();
                        if (GameModule.PlayerSelectedCell(_players[i].SelectedCell, i))
                        {
                            if (GameModule.CheckWinningCondition(_players[i].SelectedCell))
                            {
                                GameEnd("PLAYER " + (i + 1) + " WON");
                                yield break;
                            }
                            turnDone = true;
                        }
                    }
                    yield return new WaitForSeconds(0.03f);
                }
            }
            yield return new WaitForSeconds(2f);
            GameEnd("DRAW");
        }
    }
}