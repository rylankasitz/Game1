using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.ECSCore
{
    public abstract class System
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();
        public abstract List<Type> Components { get; }

        public void LoadEntities(Matcher matcher)
        {
            Entities = matcher.Match(Components);
        }

        public abstract void Initialize();
    }
}
