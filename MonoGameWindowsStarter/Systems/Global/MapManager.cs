using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using TiledSharp;

namespace MonoGameWindowsStarter.Systems.Global
{
    public static class MapManager
    {
        private static List<MapObject> mapObjects;
        private static TmxMap map;

        public static void LoadMap (ContentManager content, string name, Scene scene)
        {
            map = new TmxMap(content.RootDirectory + "\\Maps\\" + name + ".tmx");
            mapObjects = new List<MapObject>();

            setObjects(scene);
            Debug.WriteLine($"Loaded Map: {name}");
        }

        #region Private Methods

        private static void setObjects(Scene scene)
        {
            int tileset = 0;
            float layernum = 1;
            float layerinc = 1f / (float)(map.Layers.Count*2);

            foreach (TmxLayer layer in map.Layers)
            {
                int gridNum = 0;
                foreach (TmxLayerTile tile in layer.Tiles)
                {
                    int tilenum = tile.Gid - 1;
                    
                    if (tilenum != -1)
                    {
                        int tileSetWidth = (int) map.Tilesets[tileset].Columns;

                        int col = tilenum % tileSetWidth;
                        int row = (int) Math.Floor(tilenum / (float) tileSetWidth);

                        int x = tile.X * (map.TileWidth - 1) + map.TileWidth/2;
                        int y = tile.Y * (map.TileHeight - 1) + map.TileHeight/2;

                        MapObject obj = scene.CreateEntity<MapObject>();
                        Transform transform = obj.GetComponent<Transform>();
                        Sprite sprite = obj.GetComponent<Sprite>();

                        obj.Name = getProperty("Name", tile, tileset);

                        transform.Position = new Vector(x, y);
                        transform.Scale = new Vector(map.TileWidth, map.TileHeight);

                        sprite.ContentName = "spritesheet";
                        sprite.SpriteLocation = 
                            new Rectangle(col * map.TileWidth + col * map.Tilesets[tileset].Spacing + map.Tilesets[tileset].Margin, 
                                          row * map.TileHeight + row * map.Tilesets[tileset].Spacing + map.Tilesets[tileset].Margin, 
                                          map.TileWidth, map.TileHeight);
                        sprite.Layer = layernum;

                        if (map.Tilesets[tileset].Tiles.ContainsKey(tile.Gid - 1))
                        {
                            BoxCollision boxCollision = obj.AddComponent<BoxCollision>();
                            boxCollision.Layer = "Map";

                            TmxObject colObj = map.Tilesets[tileset].Tiles[tile.Gid - 1].ObjectGroups[0].Objects[0];

                            int w = (int)colObj.Width;
                            int h = (int)colObj.Height;

                            boxCollision.Position = new Vector((int)colObj.X, (int)colObj.Y);
                            boxCollision.Scale = new Vector(w / (float)map.TileWidth, h / (float)map.TileHeight);
                            boxCollision.TriggerOnly = getProperty("Trigger", tile, tileset) == "true";
                        }

                        mapObjects.Add(obj);
                    }
                    gridNum++;
                }
                layernum-=layerinc;
            }
        }

        private static string getProperty(string name, TmxLayerTile tile, int tileset, string unknownProp = "Unamed")
        {
            if (map.Tilesets[tileset].Tiles.ContainsKey(tile.Gid - 1))
            {
                if (map.Tilesets[tileset].Tiles[tile.Gid - 1].Properties.ContainsKey(name))
                    return map.Tilesets[tileset].Tiles[tile.Gid - 1].Properties[name];
            }
            return unknownProp;
        }

        #endregion
    }

    [Transform(X: 100, Y: 100, Width: 100, Height: 100)]
    [Sprite(ContentName: "spritesheet")]
    public class MapObject : Entity
    {
        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
