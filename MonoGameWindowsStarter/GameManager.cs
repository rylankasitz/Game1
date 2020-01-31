using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Entities;
using MonoGameWindowsStarter.Systems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameWindowsStarter
{
    public class GameManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<ECSCore.System> systems = new List<ECSCore.System>();

        private Renderer renderer;
        private CollisionHandler collisionHandler;

        private SceneManager sceneManager;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Systems
            systems.Add(renderer = new Renderer());
            systems.Add(collisionHandler = new CollisionHandler());

            sceneManager = new SceneManager(systems);
            sceneManager.SetScene("testScene");
        }

        protected override void Initialize()
        {
            sceneManager.LoadScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            sceneManager.UpdateScene(gameTime);

            collisionHandler.CheckCollisions();

            InputManager.OldKeyboardState = InputManager.NewKeyboardState;

            // Fps
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Console.WriteLine(Math.Round(frameRate));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            renderer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
