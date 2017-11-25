using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace MonoGame2D
{

    static class Constants
    {
        // Constante de diretorio ativo
        public const string directory = "Content";

        // Contantes de movimentação dos inimigos
        public const float acelerationFactor = (float)0.25; 
        public const float decAceleration = (float)0.2;
        public const float rigthAceleration = 1; // Direção da aceleração 
        public const float angleObstacleToRigth = 0; // Ângulo da aceleração
        // Constantes de controle de loop do jogo junto a atualização de obstaculos
        

        // Constantes de valores default de vida,nivel, pontos e etc do jogo
        public const int initialLives = 5;
        public const int initialLevel = 0;
        public const int initialScore = 0;
        public const int maxLevel = 8;

        // Constantes de nome de arquivos a serem caregados
        public const string towerSprite = "Content/tower.png";
        public const string orangeCuteSprite = "Content/orange_cute_enemy.png";
        public const string orangeOnionSprite = "Content/orange_onion_enemy.png";
        public const string blackEnemySprites = "Content/black_enemy.png";
        public const string backgroundSprite = "background";
        public const string startSprite = "start-splash";

    }

    public class Game1 : Game
    {
        // Declaração de variaveis globias dentre a classe
        // Variaveis de ambiente grafico
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont stateFont;
        SpriteFont scoreFont;
        Texture2D startGameSplash;
        Texture2D background;
        float scale;

        // Variaveis de posicionamento
        float screenWidth;
        float screenHeight;

        // Variaveis posicionamento angular e aceleração todas já iniializadas aqui
        float angleToRight = Constants.angleObstacleToRigth;
        float aceleretionToRigth = Constants.rigthAceleration;

        // Variaveis de controle de estado de jogo
        bool gameStarted;
        bool gameOver;
        bool win;
        int score;
        int lives;
        int level;

        // Variavel para geração ramdomica
        Random random;

        // Declaração da lista de inimigos e torres
        List<Enemies> enemies = new List<Enemies>();
        List<Towers> towers = new List<Towers>();

        // Fim da declaração de globais da classe

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
         
            IsMouseVisible = true;
            Content.RootDirectory = Constants.directory;
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
        }

        /* Método de carga de elementos externos */
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            /* Carrega texturas de do jogo */
            background = Content.Load<Texture2D>("background");
            startGameSplash = Content.Load<Texture2D>("start-splash");
 
            spawnNewObstacle();
            float scale = ScaleToHighDPI(1.3f);

          
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
            enemies[0].Update(elapsedTime);
            base.Update(gameTime);
        }

        /* Metodo de desenho dos elementos gráficos */
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            /* Inicializa o ambiente de operações de desenho na tela */
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White); 
            enemies[0].Draw(spriteBatch);
  
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
                //spriteBatch.Draw(background, new Rectangle(0, 0,
                // (int)screenWidth, (int)screenHeight), Color.Transparent);
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
            enemies[0].x = 10;
            enemies[0].y = (screenHeight / 3) - 50; //hardcoded, definir constante

        }
        public void spawnNewObstacle()
        {
            //Instanciar aqui os tipos de inimigos, e calcular sua movimentação com base no mapa

            Enemies crow;
            crow = new Enemies(GraphicsDevice, "Content/black_enemy.png", ScaleToHighDPI(0.3f));

            crow.x = -screenWidth / 17; //definir constante
            crow.dX = (float)(aceleretionToRigth * (Constants.acelerationFactor)); // 1 é a aceleração pra frente, 0,25 é fator de aceleração, street + 2

            crow.angle = 0; //angulo pra direita

            enemies.Add(crow);
        }


    }
}
