using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Animation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D marioTex;

        private AnimationClip marioWalkClip, marioDeathClip;
        private AnimationClip marioCurrentClip; // currently active clip

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            marioTex = this.Content.Load<Texture2D>(@"mario-pauline");

            // Source rectangles for all Mario clips 
            Rectangle[] marioWalkRects = new Rectangle[]
            {
                new Rectangle(0*16 + 1, 1, 16, 16),
                new Rectangle(1*16 + 2, 1, 16, 16),
                new Rectangle(2*16 + 3, 1, 16, 16)
            };
            Rectangle[] marioDeathRects = new Rectangle[]
            {
                new Rectangle(15*16 + 16, 1, 16, 16),
                new Rectangle(16*16 + 17, 1, 16, 16),
                new Rectangle(17*16 + 18, 1, 16, 16),
                new Rectangle(18*16 + 19, 1, 16, 16),
                new Rectangle(19*16 + 20, 1, 16, 16)
            };

            // Create Mario clips from source rectangles
            marioWalkClip = new AnimationClip(marioWalkRects, 7.5f);
            marioDeathClip = new AnimationClip(marioDeathRects, 5.0f);

            // Set the current Mario clip
            marioCurrentClip = marioWalkClip;
            // currentClip = marioDeathClip;

            marioCurrentClip.SetPlay();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            marioCurrentClip.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Rectangle destRect = new Rectangle(100, 100, 300, 300);

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
                marioCurrentClip.GetCurrentSourceRectangle(),
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                0.5f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}