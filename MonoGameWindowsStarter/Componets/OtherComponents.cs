using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Componets
{
    public delegate void State(GameTime gameTime);

    public class StateMachine : Component
    {
        public string CurrentState { get; set; }
        public Dictionary<string, State> States { get; set; } = new Dictionary<string, State>();
        public StateMachine() { }
    }
}
