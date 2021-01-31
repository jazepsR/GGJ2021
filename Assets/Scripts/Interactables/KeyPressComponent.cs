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
            
            if (Input.GetKeyDown(KeyPress.Menu))
            {
                KeyPress.MenuButtonPressed.OnNext(Unit.Default);
            }

            if (Input.anyKeyDown)
            {
                KeyPress.AnyKey.OnNext(Unit.Default);
            }
        }
    }
}