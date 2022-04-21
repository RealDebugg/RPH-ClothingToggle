using System;
using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using RAGENativeUI;
using RAGENativeUI.Elements;
using RAGENativeUI.PauseMenu;

[assembly: Plugin("Outfit Toggler", Description = "Allows you to toggle between EUP clothing | Made by Debugg#8770.", Author = "Debugg")]
namespace outfitToggler
{
    public static class EntryPoint
    {
        private static MenuPool pool;
        private static UIMenu mainMenu;
        private static Keys KeyBinding;
        private static Props pedProps = new Props();

        public static InitializationFile initialiseFile()
        {
            InitializationFile ini = new InitializationFile("Plugins/outfitSettings.ini");
            ini.Create();
            return ini;
        }

        public static String getMyKeyBinding()
        {
            InitializationFile ini = initialiseFile();
            string keyBinding = ini.ReadString("Keybindings", "openMenuBinding", "F6");
            return keyBinding;
        }

        public static void Main()
        {
            #region Keybinding
            KeysConverter kc = new KeysConverter();

            try
            {
                KeyBinding = (Keys)kc.ConvertFromString(getMyKeyBinding());
            }
            catch
            {
                KeyBinding = Keys.F6;
                Game.DisplayNotification("There was an error reading the .ini file. Setting defaults...");
            }
            #endregion

            #region Menu
            pool = new MenuPool();
            mainMenu = new UIMenu("Outfit Menu", "Select an option to toggle it.");
            UIMenuItem hair = new UIMenuItem("Hair", "Toggle hair bun or down");
            UIMenuItem bag = new UIMenuItem("Bag", "Open/Close bag");
            UIMenuItem Glasses = new UIMenuItem("Glasses", "Toggle glasses");
            UIMenuItem Ear = new UIMenuItem("Ear", "Toggle ear accessory");
            UIMenuItem Necklace = new UIMenuItem("Necklace", "Toggle necklace");
            UIMenuItem Bracelet = new UIMenuItem("Bracelet", "Toggle bracelet");
            UIMenuItem Watch = new UIMenuItem("Watch", "Toggle watch");
            UIMenuItem Vest = new UIMenuItem("Vest", "Toggle vest");
            UIMenuItem Mask = new UIMenuItem("Mask", "Toggle mask");
            UIMenuItem Shoes = new UIMenuItem("Shoes", "Toggle shoes");
            UIMenuItem Hat = new UIMenuItem("Hat", "Toggle hat");
            UIMenuItem Gloves = new UIMenuItem("Gloves", "Toggle gloves");
            UIMenuItem Pants = new UIMenuItem("Pants", "Toggle pants");
            UIMenuItem Shirt = new UIMenuItem("Shirt", "Toggle shirt");
            mainMenu.AddItems(Shirt, Pants, Gloves, Hat, Shoes, Mask, Vest, Watch, Bracelet, Necklace, Ear, Glasses, bag, hair);
            mainMenu.OnItemSelect += ItemSelectHandler;
            pool.Add(mainMenu);
            GameFiber.StartNew(ProcessMenus);
            #endregion
        }

        private static void ItemSelectHandler(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            string text = selectedItem.Text;
            switch (text)
            {
                case "Hair":
                    SwitchClothing(1);
                    break;
                case "Bag":
                    SwitchClothing(2);
                    break;
                case "Glasses":
                    SwitchClothing(3);
                    break;
                case "Ear":
                    SwitchClothing(4);
                    break;
                case "Necklace":
                    SwitchClothing(5);
                    break;
                case "Bracelet":
                    SwitchClothing(6);
                    break;
                case "Watch":
                    SwitchClothing(7);
                    break;
                case "Vest":
                    SwitchClothing(8);
                    break;
                case "Mask":
                    SwitchClothing(9);
                    break;
                case "Shoes":
                    SwitchClothing(10);
                    break;
                case "Hat":
                    SwitchClothing(11);
                    break;
                case "Gloves":
                    SwitchClothing(12);
                    break;
                case "Pants":
                    SwitchClothing(13);
                    break;
                case "Shirt":
                    SwitchClothing(14);
                    break;

                default:
                    Game.DisplayNotification("An error occurred???");
                    break;
            }
        }

        private static void PlayAnim(string dict, string anim, int move, int dur)
        {
            Ped myChar = Game.LocalPlayer.Character;
            myChar.Tasks.PlayAnimation(dict, anim, 3.0f, (AnimationFlags)move);
            GameFiber.Wait(dur);
            myChar.Tasks.ClearSecondary();
        }
        
        private static void ToggleVariation(int comp, int draw, int tex) 
        {
            Ped myChar = Game.LocalPlayer.Character;
            //myChar.SetVariation(comp, draw, tex); // int componentIndex, int drawableIndex, int drawableTextureIndex
            //public void GetVariation(
            //  int componentIndex,
            //  out int drawableIndex,
            //  out int drawableTextureIndex
            //)
        }

        private static void ToggleProp(int prop, int draw, int tex) 
        {
            Ped myChar = Game.LocalPlayer.Character;
            //SetPedPropIndex(Ped, Prop.Prop, Last.Prop, Last.Texture, true) -- FiveM
            //ClearPedProp(Ped, v.Id) -- FiveM
        }
        private static void SwitchClothing(int id)
        {
            switch (id)
            {
                case 1: //Hair Variation: 2 (Draw)
                    PlayAnim("clothingtie", "check_out_a", 51, 2000);
                    break;
                case 2: //Bag Variation: 5 (Draw)
                    PlayAnim("anim@heists@ornate_bank@grab_cash", "intro", 51, 1600);
                    break;
                case 3: //Glasses Prop: 1 (Draw)
                    PlayAnim("clothingspecs", "take_off", 51, 1400);
                    break;
                case 4: //Ear Prop: 2 (Draw)
                    PlayAnim("mp_cp_stolen_tut", "b_think", 51, 900);
                    break;
                case 5: //Necklace Variation: 7 (Draw)
                    PlayAnim("clothingtie", "try_tie_positive_a", 51, 2100);
                    break;
                case 6: //Bracelet Prop: 7 (Draw)
                    PlayAnim("nmt_3_rcm-10", "cs_nigel_dual-10", 51, 1200);
                    break;
                case 7: //Watch Prop: 6 (Draw)
                    PlayAnim("nmt_3_rcm-10", "cs_nigel_dual-10", 51, 1200);
                    break;
                case 8: //Vest Variation: 9 (Draw)
                    PlayAnim("clothingtie", "try_tie_negative_a", 51, 1200);
                    break;
                case 9: //Mask Variation: 1 (Draw)
                    PlayAnim("anim@heists@ornate_bank@grab_cash", "intro", 51, 800);
                    break;
                case 10: //Shoes Variation: 6 (Draw)
                    PlayAnim("random@domestic", "pickup_low", 0, 1200);
                    break;
                case 11: //Hat Prop: 0 (Draw)
                    PlayAnim("mp_masks@standard_car@ds@", "put_on_mask", 51, 600);
                    break; 
                case 12: //Gloves Variation: 3 (Draw)
                    PlayAnim("nmt_3_rcm-10", "cs_nigel_dual-10", 51, 1200);
                    break;
                case 13: //Pants Variation: 4 (Draw)
                    PlayAnim("re@construction", "out_of_breath", 51, 1300);
                    break;
                case 14: //Shirt Variation: 11 (Draw)
                    PlayAnim("clothingtie", "try_tie_negative_a", 51, 1200);
                    break;
                default:
                    Game.DisplayNotification("The fuck have you done?");
                    break;
            }
        }

        private static void ProcessMenus()
        {
            while (true)
            {
                GameFiber.Yield();
                pool.ProcessMenus();
                if (Game.IsKeyDown(KeyBinding) && !UIMenu.IsAnyMenuVisible && !TabView.IsAnyPauseMenuVisible)
                {
                    mainMenu.Visible = true;
                }
            }
        }
    }
}
