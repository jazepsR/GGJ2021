using System.Globalization;
using GameSystem;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
    public class SanityCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            PlayerStats.CurrentSanity
                .Subscribe(sanity => text.text = sanity.ToString(CultureInfo.InvariantCulture))
                .AddTo(this);
        }
    }
}
