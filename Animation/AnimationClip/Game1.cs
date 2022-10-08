using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Animation
{
    public static class DebugRectangle
    {
        private static Texture2D texture;

        public static void Init(GraphicsDevice graphicsDevice)
        {
            texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] color = new Color[1] { Color.White };
            texture.SetData(color);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D marioTex;

        private AnimationClip walkClip, deathClip;
        private AnimationClip currentClip; // currently active clip

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            Window.Title = "AnimationClip";
            Window.Title = "DebugRectangle";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            DebugRectangle.Init(GraphicsDevice);

            marioTex = this.Content.Load<Texture2D>(@"mario-pauline");

            // Source rectangles for all Mario clips 
            Rectangle[] walkRects = new Rectangle[]
            {
                new Rectangle(0*16 + 1, 1, 16, 16),
                new Rectangle(1*16 + 2, 1, 16, 16),
                new Rectangle(2*16 + 3, 1, 16, 16)
            };
            Rectangle[] deathRects = new Rectangle[]
            {
                new Rectangle(15*16 + 16, 1, 16, 16),
                new Rectangle(16*16 + 17, 1, 16, 16),
                new Rectangle(17*16 + 18, 1, 16, 16),
                new Rectangle(18*16 + 19, 1, 16, 16),
                new Rectangle(19*16 + 20, 1, 16, 16)
            };

            // Create Mario clips from source rectangles
            walkClip = new AnimationClip(walkRects, 7.5f);
            deathClip = new AnimationClip(deathRects, 5.0f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Based on game events, set current clip, play/pause etc
            currentClip = walkClip;
            currentClip.SetPlay();

            // Update current clip
            currentClip.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Rectangle destRect = new Rectangle(100, 100, 300, 300);

            Rectangle rect1 = new Rectangle(100, 100, 100, 100);
            Rectangle rect2 = new Rectangle(200, 100, 100, 100);
            Rectangle rect3 = new Rectangle(300, 100, 100, 100);

            //_spriteBatch.Begin();
            //_spriteBatch.Draw(
            //    marioTex,
            //    destRect,
            //    marioCurrentClip.GetCurrentSourceRectangle(),
            //    Color.White);
            //_spriteBatch.End();

            _spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                null,
                Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp,
                null,
                null);

            _spriteBatch.Draw(
                marioTex,
                destRect,
                currentClip.GetCurrentSourceRectangle(),
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                0.5f);

            ////            DebugRectangle.DrawRectangle(_spriteBatch, destRect, Color.Green);
            //_spriteBatch.DrawRectangle(rect1, Color.Red);
            //_spriteBatch.DrawRectangle(rect2, Color.Green);
            //_spriteBatch.DrawRectangle(rect3, Color.Blue);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}