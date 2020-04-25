using GavinCardGame.GameObjects;
using GavinCardGame.GameObjects.SceneObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static GavinCardGame.GSystems;

namespace GavinCardGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        public static MainGame Game { get; private set; }

        public GraphicsDeviceManager Graphics;
        SpriteBatch _SpriteBatch;

        // TODO: Create systems instead of having object manager in here. Make net system with both tcp and udp. use tcp for object creation.
        // Have objects create on client from server, assign Id from the server

        public MainGame()
        {
            Game = this;

            Graphics = new GraphicsDeviceManager(this);

            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;

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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            GLoad();

            //var _p1Cards = GScene.Create<ObjBase>();
            //var _card1 = GScene.Create<Card>(_p1Cards);

            GScreens.OpenScreen<Screens.Objects.MainScreen>();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GUpdate(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _SpriteBatch.Begin(SpriteSortMode.BackToFront);

            GDraw(gameTime, _SpriteBatch);

            _SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
