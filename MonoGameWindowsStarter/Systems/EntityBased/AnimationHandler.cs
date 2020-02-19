using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using TiledSharp;

namespace MonoGameWindowsStarter.Systems
{
    public class AnimationHandler : ECSCore.System
    {
        private Dictionary<string, AnimationTracker> animationData = new Dictionary<string, AnimationTracker>();
        private ContentManager content;
        private List<TmxTileset> tilesets;

        public override bool SetSystemRequirments(Entity entity)
        {
            return entity.HasComponent<Sprite>() &&
                   entity.HasComponent<Transform>() &&
                   entity.HasComponent<Animation>();
        }

        public AnimationHandler(ContentManager contentManager)
        {
            content = contentManager;
        }

        public override void Initialize()
        {
            tilesets = new List<TmxTileset>();

            string[] files = Directory.GetFiles(content.RootDirectory + "\\Maps", "*.tmx", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                TmxMap map = new TmxMap(file);
                foreach (TmxTileset tileset in map.Tilesets)
                {
                    tilesets.Add(tileset);
                }
            }

            loadFromTileSet();
        }

        public override void InitializeEntity(Entity entity) { }

        public void UpdateAnimations(GameTime gameTime)
        {
            foreach (Entity entity in Entities)
            {
                Animation animation = entity.GetComponent<Animation>();

                AnimationTracker current = getAnimation(animation.CurrentAnimation);

                if (current != null)
                {
                    Sprite sprite = entity.GetComponent<Sprite>();

                    current.TimeIntoAnimation += gameTime.ElapsedGameTime.TotalSeconds;

                    if (current.TimeIntoAnimation > current.TotalDuration)
                        current.TimeIntoAnimation = 0;

                    current.FrameNumber = (int)Math.Floor(current.TimeIntoAnimation / current.FrameDuration);

                    sprite.SpriteLocation = current.CurrentFrame;
                }
            }
        }

        private AnimationTracker getAnimation(string name)
        {
            if (animationData.ContainsKey(name)) return animationData[name];

            Debug.WriteLine("Animation '" + name + "' was not found");

            return null;
        }

        private void loadFromTileSet()
        {    
            foreach (TmxTileset tileset in tilesets)
            {
                SpriteSheetAnimations spriteSheetAnimation = new SpriteSheetAnimations();
                spriteSheetAnimation.Width = tileset.TileWidth;
                spriteSheetAnimation.Height = tileset.TileHeight;
                spriteSheetAnimation.Margin = tileset.Margin;
                spriteSheetAnimation.Spacing = tileset.Spacing;
                foreach (KeyValuePair<int, TmxTilesetTile> tile in tileset.Tiles)
                {
                    if (tile.Value.AnimationFrames.Count > 0)
                    {
                        AnimationTracker animationTracker = new AnimationTracker(spriteSheetAnimation);
                        string name = "Player";
                        foreach (TmxAnimationFrame animationFrame in tile.Value.AnimationFrames)
                        {
                            int x = (int)(animationFrame.Id % tileset.Columns);
                            int y = (int)Math.Floor(animationFrame.Id / (float) tileset.Columns);
                            animationTracker.AddFrame(animationFrame.Duration / (float) 1000, x, y);
                        }
                        animationData[name] = animationTracker;
                    }
                }
            }     
        }
    }

    public class SpriteSheetAnimations
    {
        public List<AnimationTracker> Animations { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Margin { get; set; }
        public int Spacing { get; set; }
    }

    public class AnimationTracker
    {
        public int FrameNumber { get; set; } = 0;
        public double TimeIntoAnimation { get; set; } = 0;

        public float FrameDuration
        {
            get
            {
                return frames[FrameNumber].Duration;
            }
        }

        public float TotalDuration
        {
            get
            {
                float duration = 0;

                foreach(Frame frame in frames) 
                {
                    duration += frame.Duration;
                }

                return duration;
            }
        }

        public Rectangle CurrentFrame
        {
            get
            {
                return frames[FrameNumber].FrameLocation;
            }
        }

        private List<Frame> frames;
        private SpriteSheetAnimations parent;

        public AnimationTracker(SpriteSheetAnimations parent)
        {
            this.parent = parent;
            frames = new List<Frame>();
        }

        public void AddFrame(float duration, int spriteX, int spriteY)
        {
            frames.Add(new Frame(duration, spriteX, spriteY, parent));
        }
    }

    public class Frame
    {
        public float Duration { get; set; }
        public Rectangle FrameLocation { get; set; }
        public Frame(float duration, int spriteX, int spriteY, SpriteSheetAnimations parent)
        {
            Duration = duration;
            FrameLocation = new Rectangle(spriteX * (parent.Width + parent.Spacing) + parent.Margin,
                                          spriteY * (parent.Height + parent.Spacing) + parent.Margin,
                                          parent.Width, parent.Height);
        }
    }
}
