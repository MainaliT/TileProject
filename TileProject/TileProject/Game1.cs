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
using System.IO;
using Jerrys_Sprites;

namespace TileProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        enum gameState {
            Load,
            Play,
            Pause,
            GameOver,
            Direction,
            Credits
        };

        gameState value;
        Vector2 screenResolution;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        Texture2D[] tiles;
        Texture2D collskin;
        int numberOfTiles;
        int tileWidth;
        int Row;
        int Col;
        float deltay;
        int tileHeight;
        int currentMapTile;
        int JumpCount;
        int nextMapTile;
        bool falling;
      
        bool Jumping;
        int[,] map;             // symbolic representation of the map
        int mapWidth;           //
        int mapHeight;          //
        int tilesPerRow;        // calculated count of the tiles per row
        int tilesPerColumn;     // calculated count of the tiles per column
        int nextMapRow;
        List<Rectangle> ledges;
        List<Rectangle> walls;
        Rectangle rectangle;
        Sprite sprite;
        Sprite strawberry;
        Sprite sword;
        Sprite gift;
        Sprite danger;

        // The offset at which everything will draw at.
        public Vector2 DrawOffset;

        // determins the Upper corner of the map for the draw method. we start here and draw 'n'tiles over and 'n' tiles down
        int upperLeftRowPosition;
        int upperLeftColumnPosition;

        // variables used in the draw method
        int MapRowPos;
        int MapColPos;

        public static float MaximumJumpSeconds = 0.2f;
        public float JumpTime;


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Row = 0;
            Col = 0;
            nextMapRow = 0;
            JumpCount = 0;
            previousKeyboardState = currentKeyboardState = Keyboard.GetState();

            collskin = Content.Load<Texture2D>("Images/collisionSkin");
            Jumping = false;
            falling = true;
            

            // these valus indicate where we start on the map
            // maybe these coule have been read in from a file
            upperLeftRowPosition = 0;
            upperLeftColumnPosition = 0;

            numberOfTiles = 10;                                                  // consider reading this value from the text file
            tiles = new Texture2D[numberOfTiles];
            ledges = new List<Rectangle>();
            walls = new List<Rectangle>();



            screenResolution = new Vector2(600, 800);
            graphics.PreferredBackBufferWidth = (int)screenResolution.X;
            graphics.PreferredBackBufferHeight = (int)screenResolution.Y;
            graphics.ApplyChanges();

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            strawberry = new Sprite()
            {
                game = this,
                SkinPath = "Images/jerry",
                Position = new Vector2(300, 160),
                DEBUG = false,
                Alive = true,
                DeltaX = 0,
                DeltaY = 0
                
            };

            gift = new Sprite()
            {
                game = this,
                SkinPath = "Images/gifts",
                Position = new Vector2(300, 360),
                DEBUG = false,
                Alive = true,
                DeltaX = 0,
                DeltaY = 0

            };


            danger = new Sprite()
            {
                game = this,
                SkinPath = "Images/spike",
                Position = new Vector2(515, 450),
                DEBUG = false,
                Alive = true,
                DeltaX = 0,
                DeltaY = 0

            };

            sword = new Sprite()
            {
                game = this,
                SkinPath = "Images/touch",
                Position = new Vector2(600, 400),
                DEBUG = false,
                Alive = true,
                DeltaX = 0,
                DeltaY = 0

            };

            sprite = new Sprite()
            {
                game = this,
                SkinPath = "Images/smily",
                Position = new Vector2(60, 40),
                Theta = MathHelper.ToRadians(0.0f),
                DEBUG = false,


                DeltaX = 0,
                DeltaY = 0,
               // hasHealth = true,
               // healthBarScale = .5f,
               // HealthBarRotation = MathHelper.ToRadians(0),

                Alive = true
            };
            gift.Initialize();
            danger.Initialize();
            sprite.Initialize();
           // sword.Initialize();
            strawberry.Initialize();

          

            for (int i = 0; i < numberOfTiles; i++)
                tiles[i] = Content.Load<Texture2D>("Images/tile" + i);
            //tile0 = Content.Load<Texture2D>("Images/tile0");
            //tile1 = Content.Load<Texture2D>("Images/tile1");

            tileWidth = tiles[0].Width;
            tileHeight = tiles[0].Height;

            tilesPerColumn = (int)(screenResolution.X / tiles[0].Width);
            tilesPerRow = (int)(screenResolution.Y / tiles[0].Height);

            MapReader();

            value = gameState.Play;
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            DrawOffset = -sprite.Position + new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (value) {
                case gameState.Load:

                    break;

                case gameState.Play:
                    break;

                case gameState.Pause:
                    break;

                case gameState.GameOver:
                    
                    break;

                case gameState.Direction:
                    break;

                case gameState.Credits:
                    break;
            }
            // Set the camera's draw offset accordingly so that the player is centered on screen.

            currentKeyboardState = Keyboard.GetState();

            
            if (sprite.Alive == false && currentKeyboardState.IsKeyUp(Keys.X) && previousKeyboardState.IsKeyDown(Keys.X)){
                
                sprite.Alive = true;
                sprite.Position = new Vector2(90, 140);
            }

          

            if (strawberry.CollisionRecrangle.Intersects(sprite.CollisionRecrangle))
            {
                strawberry.Alive = false;
               
            }
            strawberry.CollisionRecrangle = new Rectangle(0, 0, 4000, 5000);



            if (gift.CollisionRecrangle.Intersects(sprite.CollisionRecrangle))
            {
                gift.Alive = false;
                
            }
           

            if (danger.CollisionRecrangle.Intersects(sprite.CollisionRecrangle))
            {
                sprite.Alive = false;
                
            }



            if (sword.CollisionRecrangle.Intersects(sprite.CollisionRecrangle))
            {
                sprite.Alive = false;
              

            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                sprite.DeltaX = -2;
            }
          
           else if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                sprite.DeltaX = 5;
               

            }
            else {
                sprite.DeltaX = 0;
            }



            if (JumpCount == 0)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
                {
                    Jumping = true;
                    deltay = -7;
                    JumpTime = 0;

                    sprite.DeltaY = deltay;
                }
            }




            danger.Update(gameTime);
            gift.Update(gameTime);
            // sword.Update(gameTime);
            //
            strawberry.Update(gameTime);
            Vector2 oldPosition = sprite.Position;

            sprite.Update(gameTime);

            Vector2 deltaPosition = sprite.Position - oldPosition;
            Vector2 playerDeltaX = new Vector2(deltaPosition.X, 0);
            Vector2 playerDeltaY = new Vector2(0, deltaPosition.Y);

            // Check Y collision now
            sprite.Position = oldPosition;
            sprite.Position += playerDeltaY;
            sprite.UpdateRectangle();

            foreach (Rectangle rectangle in walls)
            {
                if (sprite.CollisionRecrangle.Intersects(rectangle))
                {
                    deltaPosition = new Vector2(deltaPosition.X, 0);
                    sprite.DeltaY = 0;
                }
            }

            foreach (Rectangle rectangle in ledges)
            {
                if (sprite.CollisionRecrangle.Intersects(rectangle))
                {
                    deltaPosition = new Vector2(deltaPosition.X, 0);
                    sprite.DeltaY = 0;
                }
            }

            // Check X collision first
            sprite.Position = oldPosition;
            sprite.Position += playerDeltaX;

            sprite.UpdateRectangle();
            foreach (Rectangle rectangle in walls)
            {
                if (sprite.CollisionRecrangle.Intersects(rectangle))
                {
                    deltaPosition = new Vector2(0, deltaPosition.Y);
                    sprite.DeltaX = 0;
                }
            }

            foreach (Rectangle rectangle in ledges)
            {
                if (sprite.CollisionRecrangle.Intersects(rectangle))
                {
                    deltaPosition = new Vector2(0, deltaPosition.Y);
                    sprite.DeltaX = 0;
                }
            }

            sprite.Position = oldPosition;
            sprite.Position += deltaPosition;
            sprite.UpdateRectangle();
            
             if (sprite.DeltaY == 0)
                sprite.DeltaY = 1;

            if (Jumping)
            {
                JumpTime += deltaSeconds;

                if (JumpTime >= MaximumJumpSeconds)
                {
                    Jumping = false;
                    sprite.DeltaY = 1;
                }
            }

            previousKeyboardState = currentKeyboardState;
            falling = true;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            // variables used to draw the background
            MapRowPos = upperLeftRowPosition;
            MapColPos = upperLeftColumnPosition;

            // 0, 17
            for (int row = 0; row <= map.GetUpperBound(1); row++)
            {
                for (int col = 0; col <= map.GetUpperBound(0); col++)
                {
                    spriteBatch.Draw(tiles[map[col, row]], new Vector2(col * tileWidth, row * tileHeight) + DrawOffset, Color.White);
                }
            }

            //foreach (Rectangle ledge in ledges)
            //{
            //    Rectangle drawnPosition = new Rectangle(ledge.X + (int)DrawOffset.X, ledge.Y + (int)DrawOffset.Y,
            //    ledge.Width, ledge.Height);

            //    spriteBatch.Draw(collskin, drawnPosition, Color.White);
            //}

            //foreach (Rectangle wall in walls)
            //{
            //    Rectangle drawnPosition = new Rectangle(wall.X + (int)DrawOffset.X, wall.Y + (int)DrawOffset.Y,
            //    wall.Width, wall.Height);

            //    spriteBatch.Draw(collskin, drawnPosition, Color.White);
            //}



            //this will add the collision rectangle around the ledges
            //foreach (Rectangle rectangle in ledges)
            //    spriteBatch.Draw(collskin, rectangle, Color.Yellow);
           

            //foreach (Rectangle rectangle in walls)
            //    spriteBatch.Draw(collskin, rectangle, Color.Red);


            gift.Draw(gameTime);
           danger.Draw(gameTime);
           // sword.Draw(gameTime);
            sprite.Draw(gameTime);
           strawberry.Draw(gameTime);

            spriteBatch.End();

           

            base.Draw(gameTime);
        }

        public void MapReader()
        {

            //rectangle = new Rectangle(0, 0, 50, 600);
            // walls.Add(rectangle);
            //rectangle = new Rectangle(800, 0, 50, 600);
            //walls.Add(rectangle);

            // Load the file contents into a temporary buffer, keeping track of the width and height for later allocations
            mapWidth = 0;
            mapHeight = 0;
            List<String> buffer = new List<String>();

            try
            {
                // First grab the path where the executable is located the afix the folder & file spec and finally open the file
                String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                using (StreamReader sr = new StreamReader(path + "\\Content\\Maps\\map.txt"))

                {
                    while (!sr.EndOfStream)
                    {
                        ++mapHeight;

                        String line = sr.ReadLine();
                        buffer.Add(line);

                        mapWidth = line.Length;
                    }
                }
            }

            catch (Exception e)
            {

                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

            }

            map = new int[mapWidth, mapHeight];

            // Load the map data from the buffer into our map 2D array
            for (int row = 0; row <= map.GetUpperBound(1); row++)
            {
                String line = buffer[row];
                Console.WriteLine(line);

                for (int column = 0; column < line.Length; column++)
                {
                    map[column, row] = int.Parse(line[column].ToString());
                }
            }

            // Find a viable spawn at the bottom of the map for the player
            for (int column = 0; column <= map.GetUpperBound(0) - 1; column++)
                if (map[column, map.GetUpperBound(1) - 1] == 8)
                {
                    sprite.Position = new Vector2(column * 50, (map.GetUpperBound(1) - 1) * 50);
                }

                    //sets the rows 
                    for (int row = 0; row <= map.GetUpperBound(1); row++)
            for (int column = 0; column <= map.GetUpperBound(0); column++)
            {          
                {
                    currentMapTile = map[column, row];


                    if (currentMapTile == 4 || currentMapTile == 7)
                    {
                        nextMapRow = row;
                        nextMapTile = 4;
                        rectangle = new Rectangle(column * 50, row * 50, 50, 50);

                            /*
                            while (nextMapTile == 4)
                            {
                                rectangle.Height += 50;
                                nextMapTile = map[row, column];
                               if (nextMapTile != 4)
                                  walls.Add(rectangle);

                            }
                            */

                            walls.Add(rectangle);
                    }

                }

            }



            //sets the ledges
            for (int row = 0; row <= map.GetUpperBound(1); row++)
            {
                for (int column = 0; column <= map.GetUpperBound(0); column++)
                {
                    currentMapTile = map[column, row];

                    if (currentMapTile == 7 || currentMapTile == 4)
                    {
                        rectangle = new Rectangle(column * 50, row * 50, 50, 50);

                        /*
                        while (currentMapTile == 7)
                        {
                            
                            rectangle.Width += 50;
                            column += 1;
                            currentMapTile = map[row, column];
                            if (currentMapTile != 7)
                                ledges.Add(rectangle);
                          
                        }
                        */

                        ledges.Add(rectangle);
                    }

                }
                
            }




        }
    }
  }