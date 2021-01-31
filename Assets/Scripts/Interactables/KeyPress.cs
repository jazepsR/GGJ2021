using System;
using UniRx;
using UnityEngine;

namespace Interactables
{
    public static class KeyPress
    {
        internal static readonly ISubject<Unit> InterActionButtonPressed = new Subject<Unit>();
        internal static readonly ISubject<Unit> MenuButtonPressed = new Subject<Unit>();
        internal static readonly ISubject<Unit> AnyKey = new Subject<Unit>();

        public static KeyCode InterActionButton = KeyCode.E;
        public static KeyCode Menu = KeyCode.Escape;
        
        public static IObservable<Unit> InteractionPressed { get; } = InterActionButtonPressed.ThrottleFrame(1);
        public static IObservable<Unit> MenuPressed { get; } = MenuButtonPressed.ThrottleFrame(1);
        public static IObservable<Unit> AnyKeyPressed { get; } = AnyKey.ThrottleFrame(1);
    }
}