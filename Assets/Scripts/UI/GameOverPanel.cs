using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] Text _header;
        [SerializeField] GameObject _panel;
        [SerializeField] PlayerController _playerController;

        private void Awake()
        {
            _playerController.OnFinishGame += FinishGame;
        }
        private void OnDestroy()
        {
            _playerController.OnFinishGame -= FinishGame;
        }
        public void Restart()
        {
            SceneManager.LoadScene("MainScene");
        }
        private void FinishGame(bool isWin)
        {
            _panel.SetActive(true);
            if(isWin)
            {
                _header.text = "YOU WIN";
            }
            else
            {
                _header.text = "GAME OVER";
            }
        }
    }
}
