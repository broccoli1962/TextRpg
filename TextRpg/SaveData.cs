using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace TextRpg
{
    internal class SaveData
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job {  get; set; }
        public float Hp { get; set; }
        public float Gold
        {
            get; set;
        }
        public float BaseStrengh {  get; set; }
        public float BaseDefence { get; set; }

        public List<Items> Inventory { get; set; }
        public List<bool> ShopItemSold { get; set; }
    }

    class DataManage
    {
        private static readonly string SAVE_FILE_PATH = "savegame.json";

        public static void SaveData(Player player, GameData gameData)
        {
            SaveData saveData = new SaveData()
            {
                Level = player.Level,
                Name = player.Name,
                Job = player.Job,
                Hp = player.Hp,
                Gold = player.Gold,
                BaseStrengh = player.BaseStrengh,
                BaseDefence = player.BaseDefence,
                Inventory = player.Inv,
                ShopItemSold = gameData.ShopItems.Select(item => item.IsSell).ToList(),
            };

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            string jsonString = JsonSerializer.Serialize(saveData, options);

            File.WriteAllText(SAVE_FILE_PATH, jsonString);
            Console.WriteLine("저장 완료");
        }

        public static bool LoadGame(Player player, GameData gameData)
        {
            if (!File.Exists(SAVE_FILE_PATH))
            {
                Console.WriteLine("저장된 게임이 없습니다");
                return false;
            }

            string jsonString = File.ReadAllText(SAVE_FILE_PATH);
            SaveData saveData = JsonSerializer.Deserialize<SaveData>(jsonString);

            player.Level = saveData.Level;
            player.Name = saveData.Name;
            player.Job = saveData.Job;
            player.Hp = saveData.Hp;
            player.Gold = saveData.Gold;
            player.BaseStrengh = saveData.BaseStrengh;
            player.BaseDefence = saveData.BaseDefence;
            player.Inv = saveData.Inventory;

            for(int i = 0; i<gameData.ShopItems.Count; i++)
            {
                if (i < saveData.ShopItemSold.Count)
                {
                    gameData.ShopItems[i].IsSell = saveData.ShopItemSold[i];
                }
            }

            player.UpdateStatus();

            Console.WriteLine("게임 로드 완료");
            return true;
        }
    }
}
