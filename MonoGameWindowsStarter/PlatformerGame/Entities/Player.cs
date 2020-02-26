using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.Systems;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Systems.Global;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.PlatformerGame.Scenes;

namespace MonoGameWindowsStarter.PlatformerGame.Entities
{
    [Transform(X: 260, Y: 340, Width: 20, Height: 20)]
    [Sprite(ContentName: "spritesheet", Layer: 0f)]
    [Physics(VelocityX: 0, VelocityY: 0)]
    [BoxCollision(X: 0, Y: 0, Width: 1, Height: 1)]
    [Animation(CurrentAnimation: "PlayerWalk")]
    [StateMachine()]
    public class Player : Entity
    {
        public float Speed = 2.5f;
        public float Gravity = .15f;
        public float JumpForce = 4f;

        private Physics physics;
        private Transform transform;
        private BoxCollision boxCollision;
        private Animation animation;
        private Sprite sprite;
        private StateMachine stateMachine;

        private bool isGrounded;
        private double waitTime;
        private int levelnum;

        public override void Initialize()
        {
            physics = GetComponent<Physics>();
            transform = GetComponent<Transform>();
            boxCollision = GetComponent<BoxCollision>();
            animation = GetComponent<Animation>();
            sprite = GetComponent<Sprite>();
            stateMachine = GetComponent<StateMachine>();

            stateMachine.States.Add("idle", idle);
            stateMachine.States.Add("walk left", walkLeft);
            stateMachine.States.Add("walk right", walkRight);
            stateMachine.States.Add("jump", jump);
            stateMachine.States.Add("falling", falling);
            stateMachine.States.Add("next level", nextLevel);
            stateMachine.CurrentState = "idle";

            boxCollision.HandleCollision = handleCollision;
            isGrounded = false;

            waitTime = 0;
            levelnum = 1;

            Camera.Zoom = 3;
        }

        public override void Update(GameTime gameTime) 
        {
            handleStates();

            Camera.Position.X = (int) transform.Position.X;
            Camera.Position.Y = 300;

            isGrounded = false;
        }

        private void handleCollision(Entity collider, string side)
        {
            if (side == "Top")
                isGrounded = true;

            if (collider.Name == "Lava" || collider.Name == "Water" || collider.Name == "Enemy")
            {
                transform.Position = new Vector(260, 340);
            }

            if (collider.Name == "Flag")
            {
                stateMachine.CurrentState = "next level";
            }
        }

        private void handleStates()
        {
            switch (stateMachine.CurrentState) {
                case "idle":
                    if (InputManager.KeyPressed(Keys.A))
                    {
                        stateMachine.CurrentState = "walk left";
                    }
                    else if (InputManager.KeyPressed(Keys.D))
                    {
                        stateMachine.CurrentState = "walk right";
                    }
                    else if (InputManager.KeyDown(Keys.Space) && isGrounded)
                    {
                        stateMachine.CurrentState = "jump";
                    }
                    else if (!isGrounded)
                    {
                        stateMachine.CurrentState = "falling";
                    }
                    break;

                case "walk right":
                    if (InputManager.KeyUp(Keys.D))
                    {
                        stateMachine.CurrentState = "idle";
                    }
                    else if (InputManager.KeyDown(Keys.Space) && isGrounded)
                    {
                        stateMachine.CurrentState = "jump";
                    }
                    break;

                case "walk left": 
                    if (InputManager.KeyUp(Keys.A))
                    {
                        stateMachine.CurrentState = "idle";
                    }
                    else if (InputManager.KeyDown(Keys.Space) && isGrounded)
                    {
                        stateMachine.CurrentState = "jump";
                    }
                    break;

                case "jump":   
                    stateMachine.CurrentState = "falling";
                    break;

                case "falling":
                    if (isGrounded)
                    {
                        stateMachine.CurrentState = "idle";
                    }
                    break;

                case "next level":
                    if (waitTime > 1)
                    {
                        stateMachine.CurrentState = "idle";
                        waitTime = 0;
                        if (levelnum != 2)
                        {
                            MapManager.LoadMap(SceneManager.GetCurrentScene().GameManager.Content, "Level2", SceneManager.GetCurrentScene());
                            levelnum++;
                            transform.Position = new Vector(260, 340);
                            ((Level)SceneManager.GetCurrentScene()).WinScreen.Show("");
                        }
                    }
                    break;

                default:
                    stateMachine.CurrentState = "idle";
                    break;         
            }
        }

        #region States

        private void idle(GameTime gameTime)
        {
            animation.CurrentAnimation = "PlayerIdle";
            physics.Velocity.X = 0;
        }

        private void walkLeft(GameTime gameTime)
        {
            physics.Velocity.X = -Speed;
            sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
            animation.CurrentAnimation = "PlayerWalk";
        }

        private void walkRight(GameTime gameTime)
        {
            physics.Velocity.X = Speed;
            sprite.SpriteEffects = SpriteEffects.None;
            animation.CurrentAnimation = "PlayerWalk";
        }

        private void jump(GameTime gameTime)
        {
            physics.Velocity.Y = -JumpForce;
            animation.CurrentAnimation = "Jump";
        }

        private void falling(GameTime gameTime)
        {
            physics.Velocity.Y += Gravity;
            
            if (InputManager.KeyPressed(Keys.A))
            {
                physics.Velocity.X = -Speed;
            }
            else if (InputManager.KeyPressed(Keys.D))
            {
                physics.Velocity.X = Speed;
            }
            else
            {
                physics.Velocity.X = 0;
            }
        }

        private void nextLevel(GameTime gameTime)
        {
            waitTime += gameTime.ElapsedGameTime.TotalSeconds;
            ((Level)SceneManager.GetCurrentScene()).WinScreen.Show("Level Complete");
        }

        #endregion

    }
}