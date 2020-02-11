using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using static MonoGameWindowsStarter.Componets.RenderComponents;
using Newtonsoft.Json;

namespace MonoGameWindowsStarter.Systems
{
    public class AnimationHandler : ECSCore.System
    {
        private Dictionary<string, AnimationData> animationData = new Dictionary<string, AnimationData>();
        private ContentManager content;

        public override List<Type> Components
        {
            get => new List<Type>()
            {
                typeof(Sprite),
                typeof(Transform),
                typeof(Animation)
            };
        }

        public AnimationHandler(ContentManager contentManager)
        {
            content = contentManager;
        }

        public override void Initialize() 
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            foreach (string file in Directory.GetFiles(Path.Combine(content.RootDirectory, "AnimationData")))
            {
                animationData[Path.GetFileName(file)] = JsonConvert.DeserializeObject<AnimationData>(File.ReadAllText(file), jsonSerializerSettings);
            }
        }

        public override void InitializeEntity(Entity entity) { }

        public void UpdateAnimations(GameTime gameTime)
        {
            foreach (Entity entity in Entities)
            {
                Animation animation = entity.GetComponent<Animation>();
                AnimationTracker current = getAnimation(animation.CurrentAnimation, animationData[animation.AnimationFile + ".json"]);
                Sprite sprite = entity.GetComponent<Sprite>();

                double secondsIntoAnimation = current.TotalTime.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;
                double remainder = secondsIntoAnimation % current.Duration.TotalSeconds;

                current.TotalTime = TimeSpan.FromSeconds(remainder);

                sprite.SpriteLocation = current.CurrentFrame;
            }
        }

        private AnimationTracker getAnimation(string name, AnimationData data)
        {
            foreach(AnimationTracker animationTracker in data.Animations)
            {
                if (animationTracker.Name == name) return animationTracker;
            }

            return null;
        }
    }

    public class AnimationTracker
    {
        public List<AnimationFrame> Frames { get; } = new List<AnimationFrame>();
        public string Name;

        public TimeSpan TotalTime;

        public TimeSpan Duration
        {
            get
            {
                double totalSeconds = 0;
                foreach (var frame in Frames)
                {
                    totalSeconds += TimeSpan.FromSeconds(frame.Duration).TotalSeconds;
                }

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public Rectangle CurrentFrame
        {
            get
            {
                AnimationFrame currentFrame = null;
                TimeSpan accumulatedTime = new TimeSpan();

                foreach (var frame in Frames)
                {
                    if (accumulatedTime + TimeSpan.FromSeconds(frame.Duration) >= TotalTime)
                    {
                        currentFrame = frame;
                        break;
                    }
                    else
                    {
                        accumulatedTime += TimeSpan.FromSeconds(frame.Duration);
                    }
                }

                if (currentFrame == null)
                {
                    currentFrame = Frames.LastOrDefault();
                }

                if (currentFrame != null)
                {
                    return currentFrame.SourceRectangle;
                }
                else
                {
                    return Rectangle.Empty;
                }
            }
        }

        /*public void AddFrame(Rectangle bounds, TimeSpan duration)
        {
            AnimationFrame frame = new AnimationFrame()
            {
                SourceRectangle = bounds,
                Duration = duration
            };

            Frames.Add(frame);
        }*/
    }

    public class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }
        public float Duration { get; set; }
    }

    public class AnimationData
    {
        public List<AnimationTracker> Animations { get; set; }
    }
}
