using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using PokeEngine.Input;
using PokeEngine.Tools;

namespace PokeEngine.Screens
{
    class TitleScreen : Screen
    {
        
        private Texture2D pokemonLogo;
    	private Texture2D version;
        private Rectangle pokemonLogoRect;
	    private Rectangle versionRect;
	    private SoundEffect titleTheme;
	    private SoundEffectInstance titleThemeInstance;
        private string pressToStart;

        public TitleScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
        	:base(g, c, f)
        {

	        pokemonLogo = SaveLoad.LoadTexture2D(@"Content\Textures\Title\pokemon_logo.png", graphics.GraphicsDevice);
            version = SaveLoad.LoadTexture2D(@"Content\Textures\Title\pokemon_version.png", graphics.GraphicsDevice);

            pokemonLogoRect = new Rectangle(0, 0, pokemonLogo.Width, pokemonLogo.Height);

            //SoundEffects.TitleTheme = content.Load<SoundEffect>(@"SoundEffects\Menu\title_theme");
            //SoundEffects.titleTheme = SoundEffects.TitleTheme.CreateInstance();

            pressToStart = "Press z to start!";

            Name = "TitleScreen";
        }

	    public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
        	if(InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[4], 10))
        	{
        		ScreenHandler.PushScreen(new TitleMenuScreen(graphics, content, font));
                IsVisible = false;
            }
            if (InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[5], 10))
                Close();
        	
        }

        public override void Update(GameTime gameTime)
        {

            IsVisible = IsActive;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pokemonLogo, pokemonLogoRect, Color.White);
            spriteBatch.Draw(version, versionRect, Color.White);
            spriteBatch.DrawString(
                font, 
                pressToStart, 
                new Vector2(
                    (ScreenHandler.SCREEN_WIDTH / 2) - (font.MeasureString(pressToStart).X / 2),
                    (ScreenHandler.SCREEN_HEIGHT / 2) - (font.MeasureString(pressToStart).Y / 2)),
                Color.White);
        }
        
    }
}
