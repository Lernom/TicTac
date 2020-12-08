using System;
using System.Collections;
using UnityEngine;

namespace TicTacGame
{
    public class UIManager: MonoBehaviour
    {
        public GameObject MainMenu;

        public TMPro.TMP_Text GameResultText;

        private bool _state = true;

        public void SetState(bool state)
        {
            if (!GameManager.Instance.GameWorking)
                state = true;
            _state = state; 
            MainMenu.SetActive(state);
        }

        public void SwitchPlayerToAI(int playerId)
        {
            GameManager.Instance.SetupPlayer(playerId, PlayerType.AI);
        }

        public void SwitchPlayerToHuman(int playerId)
        {
            GameManager.Instance.SetupPlayer(playerId, PlayerType.Human);
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
            SetState(false);
        }

        public void GameResult(string result)
        {
            GameResultText.text = result;
        }

        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else 
            Application.Quit();
#endif
        }

        private void Start()
        {
            SetState(true);
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                SetState(!_state);
            }
        }
    }
}


