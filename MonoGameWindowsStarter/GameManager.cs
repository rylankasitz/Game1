using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Entities;
using MonoGameWindowsStarter.Systems;
using MonoGameWindowsStarter.Systems.Global;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameWindowsStarter
{
    public class GameManager : Game
    {
        public int WindowWidth { get; set; } = 1280;
        public int WindowHeight { get; set; } = 720;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<ECSCore.System> systems = new List<ECSCore.System>();

        private Renderer renderer;
        private CollisionHandler collisionHandler;
        private PhysicsHandler physicsHandler;
        private AnimationHandler animationHandler;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Systems
            systems.Add(renderer = new Renderer());
            systems.Add(collisionHandler = new CollisionHandler());
            systems.Add(physicsHandler = new PhysicsHandler());
            systems.Add(animationHandler = new AnimationHandler(Content));
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            SceneManager.systems = systems;

            SceneManager.Initialize(this);
            SceneManager.LoadScene("testScene");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AudioManager.LoadContent(Content);
            renderer.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.NewKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneManager.UpdateScene(gameTime);

            physicsHandler.HandlePhysics();
            collisionHandler.CheckCollisions();
            animationHandler.UpdateAnimations(gameTime);

            InputManager.OldKeyboardState = InputManager.NewKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin();

            renderer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
