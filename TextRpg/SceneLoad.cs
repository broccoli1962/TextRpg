using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    enum SceneList
    {
        StartScene,
        Info,
        Inventory,
        Shop,
        Dungeon,
        Rest
    }

    public struct Dungeon
    {
        public string name;
        public int defLimit;
        public float clearGold;

        public Dungeon(string name, int defLimit, float gold)
        {
            this.name = name;
            this.defLimit = defLimit;
            this.clearGold = gold;
        }
    }

    internal class SceneLoad
    {
        public SceneList scene;
        private Player p = Player.Instance;
        public List<Items> shopItems;
        public List<Dungeon> dungeons;
        //private StringBuilder sb = new StringBuilder(); //여기서 잘못된 선택을 한 듯하다...

        public void QuickLoad(SceneList scene)
        {
            switch (scene)
            {
                case SceneList.StartScene:
                    StartMainScene();
                    break;
                case SceneList.Info:
                    CharacterInfo();
                    break;
                case SceneList.Inventory:
                    CharacterInv();
                    break;
                case SceneList.Shop:
                    CharacterShop();
                    break;
                case SceneList.Dungeon:
                    CharacterDungeonEnter();
                    break;
                case SceneList.Rest:
                    CharacterRest();
                    break;
                default:
                    break;
            }
        }
        
        public void SetupPlayerScene()
        {
            SetupPlayerName();
            SetupPlayerJob();
            StartMainScene();
        }

        private void StartMainScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1.상태 보기");
                Console.WriteLine("2.인벤토리");
                Console.WriteLine("3.상점");
                Console.WriteLine("4.던전입장");
                Console.WriteLine("5.휴식하기");

                int num = ChoiceHelper(1, 5);

                switch (num)
                {
                    case 1:
                        QuickLoad(SceneList.Info);
                        break;
                    case 2:
                        QuickLoad(SceneList.Inventory);
                        break;
                    case 3:
                        QuickLoad(SceneList.Shop);
                        break;
                    case 4:
                        QuickLoad(SceneList.Dungeon);
                        break;
                    case 5:
                        QuickLoad(SceneList.Rest);
                        break;
                }
            }
        }

        private void CharacterInfo()
        {
            while(true){
                Console.Clear();
                Console.WriteLine($"상태보기");
                Console.WriteLine($"캐릭터의 정보가 표시됩니다.\n");
                if(p.Level <= 9)
                    //ConsoleColorHelper(p.level.ToString(), ConsoleColor.Red, true);
                    Console.WriteLine($"Lv. 0{p.Level}");
                else
                    Console.WriteLine($"Lv. {p.Level}");
                Console.WriteLine($"chad ( {p.Job} )");

                //공격력
                Console.Write($"공격력 : {p.TotalStrengh:F1}");
                if (p.AddStrengh > 0)
                    Console.WriteLine($" (+{p.AddStrengh:F1})");
                else Console.WriteLine();

                //방어력
                Console.Write($"방어력 : {p.TotalDefence:F1}");
                if (p.AddDefence > 0)
                    Console.WriteLine($" (+{p.AddDefence:F1})");
                else Console.WriteLine("");

                Console.WriteLine($"체 력 : {p.Hp:F0}");
                Console.WriteLine($"Gold : {p.Gold:F0} G");
                Console.WriteLine();
                Console.WriteLine($"0. 나가기");

                int num = ChoiceHelper(0, 0);

                if (num == 0) break;
            }
        }

        private void CharacterInv()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine($"보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine($"[아이템 목록]");

                for (int i = 0; i < p.Inv.Count; i++) {
                    Console.Write("- ");
                    p.Inv[i].ItemShow();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine($"1. 장착 관리");
                Console.WriteLine($"0. 나가기");

                int num = ChoiceHelper(0, 1);

                if (num == 0) break;
                else if (num == 1) {
                    CharacterInvEquipMode();
                }
            }
        }

        private void CharacterInvEquipMode()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"인벤토리 - 장착관리");
                Console.WriteLine($"보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine($"[아이템 목록]");

                for (int i = 0; i < p.Inv.Count; i++)
                {
                    Console.Write($"- {i+1} ");
                    p.Inv[i].ItemShow();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine($"0. 나가기");

                int num = ChoiceHelper(0, p.Inv.Count);

                if (num == 0) break;
                else{
                    Items selectItem = p.Inv[num - 1];
                    p.EquipItem(selectItem);
                }
            }
        }

        private void CharacterShop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{p.Gold:F0} G");
                Console.WriteLine();
                Console.WriteLine($"[아이템 목록]");

                for (int i = 0; i < shopItems.Count; i++)
                {
                    Console.Write($"- ");
                    shopItems[i].ItemShopShow();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");

                int num = ChoiceHelper(0, 2);

                if (num == 0) break;
                else if (num == 1)
                {
                    CharacterShopBuyMode();
                }
                else if (num == 2) {
                    CharacterShopSellMode();
                }

            }
        }

        private void CharacterShopBuyMode()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{p.Gold:F0} G");
                Console.WriteLine();
                Console.WriteLine($"[아이템 목록]");

                for (int i = 0; i < shopItems.Count; i++)
                {
                    Console.Write($"- {i + 1} ");
                    shopItems[i].ItemShopShow();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                int num = ChoiceHelper(0, shopItems.Count);

                if (num == 0) break;
                else
                {
                    if (shopItems[num - 1].isSell)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        Thread.Sleep(1000);
                    }
                    else if (p.Gold >= shopItems[num - 1].ItemGold)
                    {
                        p.Gold -= shopItems[num - 1].ItemGold;
                        p.Inv.Add(shopItems[num - 1]);
                        shopItems[num - 1].isSell = true;
                        Console.Write("구매를 완료했습니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        private void CharacterShopSellMode()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{p.Gold:F0} G");
                Console.WriteLine();
                Console.WriteLine($"[아이템 목록]");

                for (int i = 0; i < p.Inv.Count; i++)
                {
                    Console.Write($"- {i + 1} ");
                    p.Inv[i].ItemShopSellShow();
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                int num = ChoiceHelper(0, p.Inv.Count);

                if (num == 0) break;
                else
                {
                    p.Inv[num - 1].isEquip = true;
                    p.Inv[num - 1].isSell = false;
                    p.Gold += p.Inv[num - 1].ItemGold * 0.85f;
                    p.Inv.RemoveAt(num - 1);
                    p.UpdateStatus();
                }
            }
        }


        private void CharacterDungeonEnter()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 쉬운 던전\t| 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전\t| 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전\t| 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기");

                int num = ChoiceHelper(0, 3);

                if (num == 0) break;
                else
                {
                    bool check = ClearCheck(dungeons[num - 1].defLimit);
                    CharacterDungeonResult(check, dungeons[num-1].defLimit);
                }
            }
        }

        private bool ClearCheck(int defLimit)
        {
            if(p.TotalDefence < defLimit)
            {
                int rand = new Random().Next(0,10);
                if(rand < 4)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void ClearReward(bool clear, float defLimit)
        {
            if (clear)
            {
                System.Random rand = new System.Random();
                float randomNum = (defLimit - p.TotalDefence) + (float)rand.NextDouble() * ((35.0f + (defLimit - p.TotalDefence)) - (defLimit - p.TotalDefence));
                p.Hp -= randomNum;

                //float rand2 = new Random().Next(p.TotalStrengh, p.TotalStrengh * 2);
                float randomNum2 = (p.TotalStrengh) + (float)rand.NextDouble() * ((p.TotalStrengh * 2) - (p.TotalStrengh));


                float temp = randomNum2 / 100f;
                for (int i = 0; i < dungeons.Count; i++)
                {
                    if (defLimit == dungeons[i].defLimit)
                    {
                        p.Gold += dungeons[i].clearGold + (dungeons[i].clearGold*temp);
                    }
                }
            }
            else
            {
                p.Hp /= 2;
            }
        }

        private void CharacterLevelUp()
        {
            p.Level++;
            p.BaseStrengh += 0.5f;
            p.BaseDefence += 1;
        }

        private void CharacterDungeonResult(bool clear, int defLimit)
        {
            float beforeHealth = p.Hp;
            float beforeGold = p.Gold;
            
            ClearReward(clear, defLimit);
            if (clear)
            {
                while (true)
                {
                    Console.Clear();
                    CharacterLevelUp();
                    Console.WriteLine("던전 클리어");
                    Console.WriteLine("축하합니다!!");
                    for(int i = 0; i<dungeons.Count; i++)
                    {
                        if(defLimit == dungeons[i].defLimit)
                        {
                            Console.WriteLine(
                                $"{dungeons[i].name} 던전을 클리어 하였습니다.");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력 {beforeHealth} -> {p.Hp:F0}");
                    Console.WriteLine($"Gold {beforeGold} -> {p.Gold:F0}");
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");

                    int num = ChoiceHelper(0, 0);

                    if (num == 0) break; 
                }
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("던전 클리어 실패");
                    for (int i = 0; i < dungeons.Count; i++)
                    {
                        if (defLimit == dungeons[i].defLimit)
                        {
                            Console.WriteLine(
                                $"{dungeons[i].name} 던전을 클리어 하지 못했습니다.");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력 {beforeHealth} -> {p.Hp:F0}");
                    Console.WriteLine($"Gold {beforeGold} -> {p.Gold:F0}");
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");

                    int num = ChoiceHelper(0, 0);

                    if(num == 0) break;
                }
            }
        }

        private void CharacterRest()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {p.Gold:F0} G)");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");

                int num = ChoiceHelper(0, 1);

                if (num == 0)
                {
                    break;
                }
                else if (num == 1 && p.Gold >= 500) 
                {
                    p.Gold -= 500;
                    p.Hp = 100f;
                    Console.WriteLine("휴식을 완료했습니다.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Thread.Sleep(1000);
                }
            }
        }

        private void SetupPlayerName()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 이름을 설정해주세요.\n");
                p.Name = Console.ReadLine();

                Console.WriteLine($"\n입력하신 이름은 {p.Name}입니다.\n");
                Console.WriteLine($"1. 저장");
                Console.WriteLine($"2. 취소");

                int num = ChoiceHelper(1, 2);

                if (num == 1)
                {
                    break;
                }
            }
        }

        private void SetupPlayerJob()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 직업 설정해주세요.\n");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 도적");

                int num = ChoiceHelper(1, 2);

                if (num == 1)
                {
                    p.Job = "전사";
                    break;
                }
                else if (num == 2)
                {
                    p.Job = "도적";
                    break;
                }
            }
        }

        private int ChoiceHelper(int min, int max)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                int num;
                if(int.TryParse(input, out num) && (min <= num && max >= num))
                {
                    return num;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    return -1;
                }
            }
        }

        //콘솔 색 변경인데 쓰기 애매하네
        private void ConsoleColorHelper(string text, ConsoleColor color, bool line)
        {
            Console.ForegroundColor = color;
            if (line)
            {
                Console.Write(text);
            }
            else
            {
                Console.WriteLine(text);
            }
            Console.ResetColor();
        }
    }
}
