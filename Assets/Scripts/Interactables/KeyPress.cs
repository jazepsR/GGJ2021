using System;
using UniRx;
using UnityEngine;

namespace Interactables
{
    public static class KeyPress
    {
        internal static readonly ISubject<Unit> InterActionButtonPressed = new Subject<Unit>();

        public static KeyCode InterActionButton = KeyCode.E;
        public static KeyCode Menu = KeyCode.Escape;
        
        public static IObservable<Unit> InteractionPressed { get; } = InterActionButtonPressed.ThrottleFrame(1);
    }
}