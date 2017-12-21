using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GRIP
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pauseMenu;
        [SerializeField]
        private GameObject _shade;
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private GameObject _view1;
        [SerializeField]
        private GameObject _view2;
        [SerializeField]
        private GameObject _menuConfirm;
        [SerializeField]
        private GameObject _exitConfirm;
        [SerializeField]
        private Slider _musicVol;
        [SerializeField]
        private Slider _sfxVol;

        private bool _confirmed = false;

        private void Awake()
        {
            _musicVol.value = MusicPlayer.Instance.Volume;
            _sfxVol.value = SFXPlayer.Instance.Volume;
        }

        public void Pause()
        {
            _pauseMenu.SetActive(true);
            _shade.SetActive(true);
            _pauseButton.interactable = false;
            SFXPlayer.Instance.Play(Sound.MenuClick);
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            _pauseMenu.SetActive(false);
            _shade.SetActive(false);
            _view1.SetActive(true);
            _view2.SetActive(false);
            _pauseButton.interactable = true;
            SFXPlayer.Instance.Play(Sound.MenuClick);
            Time.timeScale = 1f;
        }

        public void MainMenu()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            _menuConfirm.SetActive(true);                       
        }

        public void Options()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            _view1.SetActive(false);
            _view2.SetActive(true);
        }

        public void QuitGame()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            _exitConfirm.SetActive(true);                      
        }

        public void MusicVol()
        {
            MusicPlayer.Instance.Volume = _musicVol.value;
        }

        public void SFXVol()
        {
            SFXPlayer.Instance.Volume = _sfxVol.value;
        }

        public void Back()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            _view1.SetActive(true);
            _view2.SetActive(false);
        }

        public void MenuNo()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            _menuConfirm.SetActive(false);            
        }

        public void MenuYes()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void ExitNo()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            _exitConfirm.SetActive(false);
        }

        public void ExitYes()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            Application.Quit();
        }
    }
}