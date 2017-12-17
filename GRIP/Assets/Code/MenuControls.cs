using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GRIP
{
    public class MenuControls : MonoBehaviour {

        [SerializeField]
        private GameObject _view1;
        [SerializeField]
        private GameObject _view2;        
        [SerializeField]
        private Slider _musicVol;
        [SerializeField]
        private Slider _sfxVol;

        private void Start()
        {
            _musicVol.value = GameManager.instance.musicVolume;
            _sfxVol.value = GameManager.instance.effectVolume;
            MusicPlayer.Instance.PlayTrack(1);
        }

        public void NewGame()
        {            
            SFXPlayer.Instance.Play(Sound.MenuClick);
            GameManager.instance.Reset();
            SceneManager.LoadScene("Level 1"); 
            MusicPlayer.Instance.Stop();
            MusicPlayer.Instance.PlayTrack(0);
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
            Application.Quit();
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
    }
}