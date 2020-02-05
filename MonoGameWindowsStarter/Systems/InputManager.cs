﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter.Systems
{
    public static class InputManager
    {
        public static KeyboardState OldKeyboardState { get; set; }
        public static KeyboardState NewKeyboardState { get; set; }

        public static bool KeyDown(Keys key)
        {
            return NewKeyboardState.IsKeyDown(key)
                && !OldKeyboardState.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return NewKeyboardState.IsKeyUp(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return NewKeyboardState.IsKeyDown(key);
        }

        public static bool LeftMousePressed()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed; 
        }

        public static Vector2 GetMousePosition()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
    }
}
