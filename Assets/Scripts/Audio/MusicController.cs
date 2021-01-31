using System;
using GameSystem;
using UniRx;
using UnityEngine;

namespace Audio
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource normalMusic;
        [SerializeField] private AudioSource chaseMusic;

        private void Start()
        {
            normalMusic.loop = true;
            chaseMusic.loop = true;
            normalMusic.Play();

            GameStateManager.TreasureChasing
                .IfTrue()
                .Subscribe(_ =>
                {
                    normalMusic.Stop();
                    chaseMusic.Play();
                }).AddTo(this);
        }

        private void OnDestroy()
        {
            normalMusic.Stop();
            chaseMusic.Stop();
        }
    }
}