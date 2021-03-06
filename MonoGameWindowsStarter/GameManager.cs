﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Systems;
using MonoGameWindowsStarter.Systems.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MonoGameWindowsStarter
{
    public class GameManager : Game
    {
        public int WindowWidth { get; set; } = 1280;
        public int WindowHeight { get; set; } = 720;

        public Template Template { get; set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<ECSCore.System> systems = new List<ECSCore.System>();

        private Renderer renderer;
        private CollisionHandler collisionHandler;
        private PhysicsHandler physicsHandler;
        private AnimationHandler animationHandler;
        private StateHandler stateHandler;
        private ParticleSystemHandler particleSystemHandler;
        private ParallaxHandler parallaxHandler;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Template = new Template();

            // Systems
            systems.Add(renderer = new Renderer());
            systems.Add(collisionHandler = new CollisionHandler());
            systems.Add(physicsHandler = new PhysicsHandler());
            systems.Add(animationHandler = new AnimationHandler(Content));
            systems.Add(stateHandler = new StateHandler());
            systems.Add(particleSystemHandler = new ParticleSystemHandler());
            systems.Add(parallaxHandler = new ParallaxHandler());
        }

        #region Monogame Methods

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            Camera.Intitialize(GraphicsDevice.Viewport, WindowWidth, WindowHeight);

            SceneManager.systems = systems;
            SceneManager.Initialize(this);
            SceneManager.LoadScene("Level");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            string[] files = Directory.GetFiles(Content.RootDirectory + "\\Sprites", "*.xnb", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                textures[filename] = Content.Load<Texture2D>("Sprites\\" + filename);
            }

            AudioManager.LoadContent(Content);

            particleSystemHandler.LoadContent(Content);
            renderer.LoadContent(Content, textures);
            parallaxHandler.LoadContent(textures);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            InputManager.NewKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            physicsHandler.HandlePhysics();
            collisionHandler.CheckCollisions();
            animationHandler.UpdateAnimations(gameTime);
            stateHandler.UpdateStateMachine(gameTime);
            particleSystemHandler.UpdateParticleSystems(gameTime);
            parallaxHandler.UpdateParallax(gameTime);

            SceneManager.UpdateScene(gameTime);

            InputManager.OldKeyboardState = InputManager.NewKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            parallaxHandler.Draw(spriteBatch, this);

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.GetTransformation());

            renderer.Draw(spriteBatch);

            particleSystemHandler.DrawParticleSystems(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion      
    }
}
