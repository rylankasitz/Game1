﻿using Microsoft.Xna.Framework;
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
    [Sprite(ContentName: "spritesheet", Layer: 0.1f)]
    [Physics(VelocityX: 0, VelocityY: 0)]
    [BoxCollision(X: 0, Y: 0, Width: 1, Height: 1)]
    [Animation(CurrentAnimation: "PlayerWalk")]
    [ParticleSystem(Texture: "Sprites/Pixel", ParticleCount: 20, SpawnPerFrame: 2, Time: -1f, 
                    EmmiterX: 0f, EmmiterY: 1, DirectionX: -1, DirectionY: 0, Range: 30, Life: .5f)]
    [StateMachine()]
    public class Player : Entity
    {
        public float Speed = 4;
        public float Gravity = .4f;
        public float JumpForce = 7;

        private Physics physics;
        private Transform transform;
        private BoxCollision boxCollision;
        private Animation animation;
        private Sprite sprite;
        private StateMachine stateMachine;
        private ParticleSystem particleSystem;
        private Gun gun;

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
            particleSystem = GetComponent<ParticleSystem>();
            gun = SceneManager.GetCurrentScene().CreateEntity<Gun>();

            stateMachine.States.Add("idle", idle);
            stateMachine.States.Add("walk left", walkLeft);
            stateMachine.States.Add("walk right", walkRight);
            stateMachine.States.Add("jump", jump);
            stateMachine.States.Add("falling", falling);
            stateMachine.States.Add("next level", nextLevel);
            stateMachine.CurrentState = "idle";

            particleSystem.Color = Color.WhiteSmoke;
            boxCollision.HandleCollision = handleCollision;
            isGrounded = false;

            waitTime = 0;
            levelnum = 1;

            Camera.Zoom = 3;
        }

        public override void Update(GameTime gameTime) 
        {
            handleStates();

            gun.SetPos(transform.Position + transform.Scale/2);

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
                foreach(Enemy enemy in SceneManager.GetCurrentScene().GetEntities<Enemy>())
                {
                    enemy.GetComponent<Sprite>().Enabled = true;
                    enemy.GetComponent<BoxCollision>().Enabled = true;
                    enemy.GetComponent<Physics>().Velocity.X = 1;
                }
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
                            ((Level1)SceneManager.GetCurrentScene()).WinScreen.Show("");
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
            particleSystem.Stop();
        }

        private void walkLeft(GameTime gameTime)
        {
            physics.Velocity.X = -Speed;
            sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
            animation.CurrentAnimation = "PlayerWalk";
            particleSystem.Direction = new Vector(1, -0.33f);
            particleSystem.Emitter = new Vector(1, 1);
            particleSystem.Play();
        }

        private void walkRight(GameTime gameTime)
        {
            physics.Velocity.X = Speed;
            sprite.SpriteEffects = SpriteEffects.None;
            animation.CurrentAnimation = "PlayerWalk";
            particleSystem.Direction = new Vector(-1, -0.33f);
            particleSystem.Emitter = new Vector(0, 1);
            particleSystem.Play();
        }

        private void jump(GameTime gameTime)
        {
            physics.Velocity.Y = -JumpForce;
            animation.CurrentAnimation = "Jump";
            particleSystem.Stop();
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
            ((Level1)SceneManager.GetCurrentScene()).WinScreen.Show("Level Complete");
        }

        #endregion

    }
}