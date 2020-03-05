﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformerContentExtension
{
    /// <summary>
    /// A class representing the details of a single 
    /// tile in a TiledSpriteSheet
    /// </summary>
    public class TileContent
    {
        public Rectangle Source { get; set; }
        public bool Solid { get; set; }
        public Rectangle BoxCollision { get; set; } = Rectangle.Empty;
        public Dictionary<string, string> Properties { get; set; }

        public TileContent()
        {
            Properties = new Dictionary<string, string>();
        }
    }
}
