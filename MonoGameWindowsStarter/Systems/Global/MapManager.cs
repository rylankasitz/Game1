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
using PlatformLibrary;

namespace MonoGameWindowsStarter.Systems.Global
{
    public static class MapManager
    {
        private static List<Entity> mapObjects = new List<Entity>();
        private static Tilemap tilemap;

        public static void LoadMap (ContentManager content, string name, Scene scene)
        {
            //map = new TmxMap(content.RootDirectory + "\\Maps\\" + name + ".tmx");
            tilemap = content.Load<Tilemap>("Maps\\" + name);

            removeMapObjects();
            createMapObjects(scene);
            Debug.WriteLine($"Loaded Map: {name}");
        }

        #region Private Methods

        private static void createMapObjects(Scene scene)
        {
            float layernum = .1f;
            foreach (var layer in tilemap.Layers)
            {
                for (uint y = 0; y < tilemap.MapHeight; y++)
                {
                    for (uint x = 0; x < tilemap.MapWidth; x++)
                    {
                        uint dataIndex = y * tilemap.MapWidth + x;
                        uint tileIndex = layer.Data[dataIndex];
                        if (tileIndex != 0 && tileIndex < tilemap.Tiles.Length)
                        {
                            Vector position = new Vector(x * tilemap.TileWidth, y * tilemap.TileHeight);
                            Vector scale = new Vector(tilemap.Tiles[tileIndex].Width, tilemap.Tiles[tileIndex].Height);

                            if (tilemap.Tiles[tileIndex].BoxCollider == Rectangle.Empty)
                            {
                                MapObject mapObject = scene.CreateEntity<MapObject>();
                                setObjectPosition(mapObject, position, scale, tilemap.Tiles[tileIndex].Source, layernum, "spritesheet");
                                mapObjects.Add(mapObject);
                            }
                            else
                            {
                                MapObjectCollision mapObject = scene.CreateEntity<MapObjectCollision>();
                                setObjectPosition(mapObject, position, scale, tilemap.Tiles[tileIndex].Source, layernum, "spritesheet");
                                setCollision(mapObject, tilemap.Tiles[tileIndex].BoxCollider);
                                mapObjects.Add(mapObject);
                            }
                        }
                    }
                }
                layernum += 1f / (float)(tilemap.Layers.Length * 2);
            }
        }

        /*private static void setObjects(Scene scene)
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

                        if (map.Tilesets[tileset].Tiles.ContainsKey(tile.Gid - 1))
                        {
                            MapObjectCollision obj = scene.CreateEntity<MapObjectCollision>();
                            setObjectPosition(obj, row, col, x, y, tile, tileset, layernum);
                            BoxCollision boxCollision = obj.GetComponent<BoxCollision>();
                            boxCollision.Layer = "Map";

                            TmxObject colObj = map.Tilesets[tileset].Tiles[tile.Gid - 1].ObjectGroups[0].Objects[0];

                            int w = (int)colObj.Width;
                            int h = (int)colObj.Height;

                            boxCollision.Position = new Vector((int)colObj.X, (int)colObj.Y);
                            boxCollision.Scale = new Vector(w / (float)map.TileWidth, h / (float)map.TileHeight);
                            boxCollision.TriggerOnly = getProperty("Trigger", tile, tileset) == "true";
                            mapObjects.Add(obj);
                        }
                        else
                        {
                            MapObject obj = scene.CreateEntity<MapObject>();
                            setObjectPosition(obj, row, col, x, y, tile, tileset, layernum);
                            mapObjects.Add(obj);
                        }
                    }
                    gridNum++;
                }
                layernum-=layerinc;
            }
        }*/

        private static void setObjectPosition(Entity obj, Vector position, Vector scale, Rectangle source, float layernum, string contentName)
        {
            Transform transform = obj.GetComponent<Transform>();
            Sprite sprite = obj.GetComponent<Sprite>();

            //obj.Name = getProperty("Name", tile, tileset);

            transform.Position = position;
            transform.Scale = scale;

            sprite.ContentName = contentName;
            sprite.SpriteLocation = source;
            sprite.Layer = layernum;
        }

        private static void setCollision(Entity obj, Rectangle collider)
        {
            BoxCollision col = obj.GetComponent<BoxCollision>();
            col.Position = new Vector(collider.X, collider.Y);
            col.Scale = new Vector(collider.Width / (float)tilemap.TileWidth, collider.Height / (float)tilemap.TileHeight);
            col.TriggerOnly = false;
        }

        /*private static string getProperty(string name, TmxLayerTile tile, int tileset, string unknownProp = "Unamed")
        {
            if (map.Tilesets[tileset].Tiles.ContainsKey(tile.Gid - 1))
            {
                if (map.Tilesets[tileset].Tiles[tile.Gid - 1].Properties.ContainsKey(name))
                    return map.Tilesets[tileset].Tiles[tile.Gid - 1].Properties[name];
            }
            return unknownProp;
        }*/

        private static void removeMapObjects()
        {
            foreach(Entity mapObject in mapObjects)
            {
                SceneManager.GetCurrentScene().RemoveEntity(mapObject);
            }
        }

        #endregion
    }

    #region Map Objects

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

    [Transform(X: 100, Y: 100, Width: 100, Height: 100)]
    [Sprite(ContentName: "spritesheet")]
    [BoxCollision()]
    public class MapObjectCollision : Entity
    {
        public override void Initialize()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }

    #endregion
}
