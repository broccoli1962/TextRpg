using System.Runtime.InteropServices;

namespace TextRpg
{
    internal class Program
    {
        static Player player;
        static SceneLoad sceneLoader;
        static GameData gameData;
        static void Main(string[] args)
        {
            StartGame();
            sceneLoader.SetupPlayerScene();
        }

        static void StartGame()
        {
            player = Player.Instance;
            sceneLoader = new SceneLoad();
            gameData = GameData.Instance;

            if (DataManage.LoadGame(player, gameData)) {
                sceneLoader.QuickLoad(SceneList.StartScene);
            }
            else
            {
                sceneLoader.QuickLoad(SceneList.SetupScene);
            }
        }
    }
}