using System;
using UniRx;
using UnityEngine;

namespace Interactables
{
    public class KeyPressComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyPress.InterActionButton))
            {
                KeyPress.InterActionButtonPressed.OnNext(Unit.Default);
            }
        }
    }
}