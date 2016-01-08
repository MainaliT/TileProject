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

namespace Jerrys_Sprites
{

    public class HealthBar
    {
        
        public Vector2 gagePosition;
        Rectangle gageRectangle;
        Rectangle fillerRectangle;
        Texture2D Bezel;
        Texture2D[] Filler { set; get; }
        public Vector2 CenterOfSprite { set; get; }
        public float Layer { set; get; }
        int colorindex { set; get; }
        public float Scale { set; get; }
        private float halfHeightOfSkin { set; get; }
        private float halfWidthtOfSkin { set; get; }
        public float leavelOfGage { set; get; }
        public SpriteEffects Effects { set; get; }
        public Point FrameSize { set; get; }
        public float Theta { set; get; }
        public float deltaAmmount { set; get; }
        public Game1 game {set; get;}


        public HealthBar() { }

        public void Initilize() {

            Layer = 1;
            Filler = new Texture2D[3];
            
            colorindex = 0;
            leavelOfGage = 1.0f;
            Effects = SpriteEffects.None;
            


            Bezel = game.Content.Load<Texture2D>("Images/Bezel");
            gageRectangle = new Rectangle(0, 0,
                                           Bezel.Width, Bezel.Height);


            Filler[0] = game.Content.Load<Texture2D>("Images/greenFiller");
            Filler[1] = game.Content.Load<Texture2D>("Images/yellowFiller");
            Filler[2] = game.Content.Load<Texture2D>("Images/redFiller");

            fillerRectangle = new Rectangle(0, 0,
                                           Bezel.Width, Bezel.Height);

            
            

            halfWidthtOfSkin = Bezel.Width / 2;
            halfHeightOfSkin = Bezel.Height / 2;

            CenterOfSprite = new Vector2(halfWidthtOfSkin, halfHeightOfSkin);


        
        }


        public void Update(GameTime gameTime) {
            fillerRectangle.Width = (int)(Filler[colorindex].Width * leavelOfGage);

            if (leavelOfGage > .7f)
                colorindex = 0;
            else if (leavelOfGage > .35f)
                colorindex = 1;
            else
                colorindex = 2;

        }


        public void Draw(GameTime gameTime){

            game.spriteBatch.Draw(Filler[colorindex],                                             // texture to draw
                                    gagePosition,                                          // where to draw it on the screen
                                    fillerRectangle,      // how much of the texture we should use
                                    Color.White,                                       // tinting color
                                    Theta,                                             // texture rotation angle
                                    CenterOfSprite,                                    // center of the sprite
                                    Scale,                                             // magnification multiplier
                                    Effects,                                            // fliping flag
                                    Layer);


            game.spriteBatch.Draw(Bezel,                                             // texture to draw
                                    gagePosition,                                        // where to draw it on the screen
                                    gageRectangle,                                    // how much of the texture we should use
                                    Color.White,                                       // tinting color
                                    Theta,                                             // texture rotation angle
                                    CenterOfSprite,                                    // center of the sprite
                                    Scale,                                             // magnification multiplier
                                    Effects,                                            // fliping flag
                                    1);    

            //game.spriteBatch.Draw(Filler[colorindex], fillerRectangle, Color.White);
            //game.spriteBatch.Draw(Bezel, gageRectangle, Color.White);            
        }

    }
}
