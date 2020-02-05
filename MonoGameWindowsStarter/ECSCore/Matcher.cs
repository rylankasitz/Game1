﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.ECSCore
{
    public class Matcher
    {
        private List<Entity> allEntities;

        public Matcher(List<Entity> entityPool)
        {
            allEntities = entityPool;
        }

        public List<Entity> Match(List<Type> types)
        {
            List<Entity> matchedEntities = new List<Entity>();
            foreach (Entity entity in allEntities)
            {
                if (MatchEntity(types, entity)) matchedEntities.Add(entity);
            }

            return matchedEntities;
        }

        public bool MatchEntity(List<Type> types, Entity entity)
        {
            bool addable = true;
            foreach (Type type in types)
            {
                if (!entity.Components.Keys.Contains(type.ToString())) addable = false;
            }

            return addable;
        }
    }
}
