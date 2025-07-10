using System.Runtime.InteropServices;

namespace TextRpg
{
    internal class Program
    {
        static Player player;
        static SceneLoad sceneLoader;
        static void Main(string[] args)
        {
            GameData();
            sceneLoader.SetupPlayerScene();
        }

        static void GameData()
        {
            player = Player.Instance;
            //test
            //player.inv = new List<Items>()
            //{
            //    new("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.Armor, 0, 5, 1000),
            //    new("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, 0, 9, 2000),
            //    new("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Armor, 0, 15, 3500),
            //    new("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, 2, 0, 600),
            //    new("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.Weapon, 5, 0, 1500),
            //    new("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon, 7, 0, 2500),
            //};
            sceneLoader = new SceneLoad();

            sceneLoader.dungeons = new List<Dungeon>()
            {
                new Dungeon("쉬운", 5, 1000f),
                new Dungeon("일반", 11, 1700f),
                new Dungeon("어려운", 17, 2500f),
            };
            sceneLoader.shopItems = new List<Items>()
            {
                new("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.Armor, 0, 5, 1000),
                new("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, 0, 9, 2000),
                new("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Armor, 0, 15, 3500),
                new("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, 2, 0, 600),
                new("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.Weapon, 5, 0, 1500),
                new("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon, 7, 0, 2500),
            };
        }
    }
}