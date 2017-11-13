using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace MonoGame2D
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        float screenWidth;
        float screenHeight;
        Texture2D background;
        bool gameStarted;
        bool gameOver;
        bool win;
        int score;
        int lives;
        int level;
        SpriteFont stateFont;
        Random random;
        SpriteFont scoreFont;
        Texture2D startGameSplash;
        List<float> screen = new List<float>();
        List<int> lines = new List<int>();
        List<Enemies> enemies = new List<Enemies>();
        List<Towers> towers = new List<Towers>();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /* Método de inicialização */
        protected override void Initialize()
        {
            base.Initialize();

            /* Inicializa parametros de jogo */
            gameStarted = false;
            //gameOver = false;
            //win = false;

            random = new Random();

            /* Inicializa escala de frames da tela utilizada
               Inicializa em tela cheia
               Inicializa com ponteiro do mouse oculto */
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            screenHeight = ScaleToHighDPI((float)ApplicationView.GetForCurrentView().VisibleBounds.Height);
            screenWidth = ScaleToHighDPI((float)ApplicationView.GetForCurrentView().VisibleBounds.Width);
            this.IsMouseVisible = false;

        }

        /* Método de carga de elementos externos */
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            /* Carrega texturas de do jogo */
            background = Content.Load<Texture2D>("background");
            startGameSplash = Content.Load<Texture2D>("start-splash");
            //gameOverTexture = Content.Load<Texture2D>("game-over");
            //winTexture = Content.Load<Texture2D>("win");

            /* Carrega sprites do jogo */

            /* Carrega estilo de fontes */
            //stateFont = Content.Load<SpriteFont>("GameState");
            //scoreFont = Content.Load<SpriteFont>("Score");
        }

        /* Método de descarga de elementos externos */
        protected override void UnloadContent()
        {
        }

        /* Método de atualização do status dos elementos */
        protected override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardHandler();

            base.Update(gameTime);
        }

        /* Metodo de desenho dos elementos gráficos */
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            /* Inicializa o ambiente de operações de desenho na tela */
            spriteBatch.Begin();

            /* Desenha o background carregado */
            spriteBatch.Draw(background, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White);

            /* Se o jogo ainda não começou, fica em tela de início */
            if (!gameStarted)
            {
                /* Carrega tela inicial e espera resposta do jogador */
                spriteBatch.Draw(startGameSplash, new Rectangle(0, 0,
                (int)screenWidth, (int)screenHeight), Color.White);
       
            }
            else
            {
                /* Desenhar aqui restante dos elementos do início do jogo: vidas, timer, pontuação */
                spriteBatch.Draw(background, new Rectangle(0, 0,
                  (int)screenWidth, (int)screenHeight), Color.White);
            }

            spriteBatch.End();

            /* Encerra o ambiente de operações de desenho na tela */
            base.Draw(gameTime);
        }

        /* Metodo de identificação e escalonamento conforme dpis da tela utilizada */
        public float ScaleToHighDPI(float f)
        {
            DisplayInformation d = DisplayInformation.GetForCurrentView();
            f *= (float)d.RawPixelsPerViewPixel;
            return f;
        }

        /* Metodo de leitura do teclado */
        void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();

            /* Encerra o jogo se a tecla ESC for pressionada */
            if (state.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            /* Inicia o jogo se a tecla Enter for pressionada */
            if (!gameStarted)
            {
                if (state.IsKeyDown(Keys.Enter))
                {
                    StartGame();
                    gameStarted = true;
                    //gameOver = false;
                }
                return;
            }
            /* Reinicia se for pressionado enter após game over */
            /*
            if (gameOver && state.IsKeyDown(Keys.Enter))
                {
                    StartGame();
                    gameStarted = true;
                    gameOver = false;
                    win = false;
                }

            if (win && state.IsKeyDown(Keys.Enter))
            {
                StartGame();
                gameStarted = true;
                gameOver = false;
                win = false;
            }
            */
        }

        /* Método de início do jogo */
        public void StartGame()
        {

        }

    }
}
