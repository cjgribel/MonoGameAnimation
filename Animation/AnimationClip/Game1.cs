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

        private AnimationClip marioWalkClip;
        private AnimationClip currentClip;

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

            Rectangle[] marioWalkRects = new Rectangle[]
            {
                new Rectangle(0*16 + 1, 1, 16, 16),
                new Rectangle(1*16 + 2, 1, 16, 16),
                new Rectangle(2*16 + 3, 1, 16, 16)
            };
            marioWalkClip = new AnimationClip(marioWalkRects);

            currentClip = marioWalkClip;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            currentClip.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Rectangle destRect = new Rectangle(100, 100, 100, 100);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, Microsoft.Xna.Framework.Graphics.SamplerState.PointWrap, null, null);

            _spriteBatch.Draw(
                marioTex,
                destRect,
                currentClip.GetCurrentSourceRectangle(),
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