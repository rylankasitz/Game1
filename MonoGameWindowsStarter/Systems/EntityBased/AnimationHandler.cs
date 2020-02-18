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
using Newtonsoft.Json;

namespace MonoGameWindowsStarter.Systems
{
    public class AnimationHandler : ECSCore.System
    {
        private Dictionary<string, AnimationData> animationData = new Dictionary<string, AnimationData>();
        private ContentManager content;

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
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            foreach (string file in Directory.GetFiles(Path.Combine(content.RootDirectory, "GameData\\Animations")))
            {
                animationData[Path.GetFileName(file)] = JsonConvert.DeserializeObject<AnimationData>(File.ReadAllText(file), jsonSerializerSettings);
            }

            foreach(Entity entity in Entities)
            {
                InitializeEntity(entity);
            }
        }

        public override void InitializeEntity(Entity entity) 
        { 
            if (!animationData.ContainsKey(entity.GetComponent<Animation>().AnimationFile + ".json"))
            {
                Debug.WriteLine("Failed to find animation data for file: " + entity.GetComponent<Animation>().AnimationFile);
                Entities.Remove(entity);
            }
        }

        public void UpdateAnimations(GameTime gameTime)
        {
            foreach (Entity entity in Entities)
            {
                Animation animation = entity.GetComponent<Animation>();
                AnimationTracker current = getAnimation(animation.CurrentAnimation, animationData[animation.AnimationFile + ".json"]);

                if (current != null)
                {
                    Sprite sprite = entity.GetComponent<Sprite>();

                    current.TimeIntoAnimation += gameTime.ElapsedGameTime.TotalSeconds;

                    if (current.TimeIntoAnimation > current.Duration)
                        current.TimeIntoAnimation = 0;

                    current.FrameNumber = (int)Math.Floor(current.TimeIntoAnimation / current.FrameDuration);

                    sprite.SpriteLocation = current.CurrentFrame;
                }
            }
        }

        private AnimationTracker getAnimation(string name, AnimationData data)
        {
            foreach(AnimationTracker animationTracker in data.Animations)
            {
                if (animationTracker.Name == name) return animationTracker;
            }

            Debug.WriteLine("Animation '" + name + "' was not found");

            return null;
        }
    }

    public class AnimationTracker
    {
        public int FrameCount, StartX, StartY, IncrementX, IncrementY, Width, Height;
        public float FrameDuration;
        public string Name;

        public int FrameNumber;
        public double TimeIntoAnimation;

        public float Duration
        {
            get
            {
                return FrameDuration * FrameCount;
            }
        }

        public Rectangle CurrentFrame
        {
            get
            {
                return new Rectangle(StartX + IncrementX * FrameNumber, StartY + IncrementY * FrameNumber, Width, Height);
            }
        }
    }

    public class AnimationData
    {
        public List<AnimationTracker> Animations { get; set; }
    }
}
