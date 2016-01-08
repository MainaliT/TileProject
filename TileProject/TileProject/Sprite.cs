using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using TileProject;
namespace Jerrys_Sprites { 





    public class Sprite {

        #region Properties and instancevariables

        // declair the objects Properties
        public Texture2D Skin { set; get; }
        public Vector2 Position { set; get; }
        public float Theta { set; get; }
        public float HealthBarRotation { set; get; }
        public Boolean Alive { set; get; }
        public String SkinPath { set; get; }
        public Game1 game { set; get; }
        public float DeltaX { set; get; }
        public float DeltaY { set; get; }
        public Point FrameSize { set; get; }
        public Rectangle CollisionRecrangle { set; get; }
        public Rectangle offScreen;
        public int halfHeightOfSkin;
        public int halfWidthtOfSkin;
        protected Texture2D collisionSkin;
        public SpriteEffects Effects { set; get;}
        public Vector2 CenterOfSprite;
        public HealthBar health;
        public float healthBarScale { set; get; }
        public bool DEBUG { set; get; }
        public bool hasHealth;


        #endregion


       public Sprite() { }

        


        public virtual void Initialize() {

            Skin = game.Content.Load<Texture2D>(SkinPath);
            collisionSkin = game.Content.Load<Texture2D>("Images/collisionSkin");
            FrameSize = new Point(Skin.Width, Skin.Height);


            halfWidthtOfSkin = FrameSize.X / 2;
            halfHeightOfSkin = FrameSize.Y / 2;

            CenterOfSprite = new Vector2(0, 0);

            CollisionRecrangle = new Rectangle((int)Position.X - FrameSize.X / 2,
                                        (int)Position.Y - FrameSize.Y / 2,
                                        FrameSize.X,
                                        FrameSize.Y);
            


            if (hasHealth) {

                
                health = new HealthBar()
                {

                    Theta = MathHelper.ToRadians(HealthBarRotation),
                    deltaAmmount = 0,
                    gagePosition = new Vector2(Position.X, Position.Y - halfHeightOfSkin - 10),
                    game = this.game,
                    Scale = healthBarScale
                };

                

                health.Initilize();

                //DEBUG = true;
            }

            if (!Alive) {
                CollisionRecrangle = offScreen;
            
            }
        }

        public void UpdateRectangle()
        {
            // recalculate the new collision rectangle
            CollisionRecrangle = new Rectangle((int)Position.X,
                                    (int)Position.Y,
                                    FrameSize.X,
                                    FrameSize.Y);
        }

        public virtual void Update(GameTime gameTime) {

            // allows the image to rotate
            //theta += MathHelper.ToRadians(1.0f);

            if (Alive) {

                Position = new Vector2(Position.X + DeltaX, Position.Y + DeltaY);

                // recalculate the new collision rectangle
                UpdateRectangle();

                if (hasHealth)
                {
                    health.gagePosition.X = Position.X;
                    health.gagePosition.Y = Position.Y - halfHeightOfSkin - 10;
                    health.Update(gameTime);
                }

                
            }

            if (!Alive)
            {
                CollisionRecrangle = offScreen;

            }


        }


        public virtual void Draw(GameTime gameTime) {

            if (Alive) {
                game.spriteBatch.Draw(Skin,                                             // texture to draw
                                     Position + game.DrawOffset,                                          // where to draw it on the screen
                                     new Rectangle(0, 0, Skin.Width, Skin.Height),      // how much of the texture we should use
                                     Color.White,                                       // tinting color
                                     Theta,                                             // texture rotation angle
                                     CenterOfSprite,                                    // center of the sprite
                                     1,                                             // magnification multiplier
                                     Effects,                                            // fliping flag
                                     1);                                            // layering position 


                if (hasHealth)
                {
                    health.Draw(gameTime);
                }

                if (DEBUG)
                {
                    Rectangle drawnPosition = new Rectangle(CollisionRecrangle.X + (int)game.DrawOffset.X, CollisionRecrangle.Y + (int)game.DrawOffset.Y,
                        CollisionRecrangle.Width, CollisionRecrangle.Height);

                    game.spriteBatch.Draw(collisionSkin, drawnPosition, Color.White);
                }
            }


        }

    }
}
