using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;

namespace MonoGameWindowsStarter.Systems
{
    public class PhysicsHandler : ECSCore.System
    {
        public override List<Type> Components
        {
            get => new List<Type>()
            {
                typeof(Physics),
                typeof(Transform)
            };
        }

        public override void Initialize() { }

        public override void InitializeEntity(Entity entity) { }

        public void HandlePhysics()
        {
            foreach (Entity entity in Entities)
            {
                Transform transform = entity.GetComponent<Transform>();
                Physics physics = entity.GetComponent<Physics>();

                transform.Position += physics.Velocity;
            }
        }

    }
}
