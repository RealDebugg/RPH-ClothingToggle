using System;
using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using Rage.Native;
using RAGENativeUI;
using RAGENativeUI.Elements;
using RAGENativeUI.PauseMenu;

[assembly: Plugin("Outfit Toggler", Description = "Allows you to toggle between EUP clothing | Made by Debugg#8770.", Author = "Debugg")]
namespace outfitToggler
{
    public static class EntryPoint //Left: Clothing, hair, sound
    {
        private static MenuPool pool;
        private static UIMenu mainMenu;
        private static Keys KeyBinding;
        private static Props pedProps = new Props();
        private static Variations pedCloths = new Variations();

        #region Menu handling
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
        #endregion

        #region Functions
        private static void PlayAnim(string dict, string anim, int move, int dur)
        {
            Ped myChar = Game.LocalPlayer.Character;
            myChar.Tasks.PlayAnimation(dict, anim, 3.0f, (AnimationFlags)move);
            GameFiber.Wait(dur);
            myChar.Tasks.ClearSecondary();
        }
        
        private static bool ToggleVariation(int comp) 
        {
            Ped myChar = Game.LocalPlayer.Character;
            int clothDraw = -1;
            int clothTex = -1;
            myChar.GetVariation(comp, out clothDraw, out clothTex);
            Console.WriteLine(clothDraw);
            Console.WriteLine(clothTex);
            switch (comp) //gloves, pants, shoes, shirt
            {
                case 1: //mask
                    if (clothDraw == -1 || clothDraw == 0 && pedCloths._lastMaskDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1)
                        {
                            myChar.SetVariation(comp, pedCloths._lastMaskDraw, pedCloths._lastMaskText);
                            pedCloths._lastMaskDraw = 0;
                            pedCloths._lastMaskText = 0;
                            return true;
                        }
                        else
                        {
                            pedCloths._lastMaskDraw = clothDraw;
                            pedCloths._lastMaskText = clothTex;
                            myChar.SetVariation(comp, 0, 0);
                            return true;
                        }
                    }
                case 3: //gloves --look into dpclothing variations.lua
                    return false;
                case 4: //pants M:61, F:14
                    return false;
                case 5: //bag
                    if (clothDraw == -1 || clothDraw == 0 && pedCloths._lastBagDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1)
                        {
                            myChar.SetVariation(comp, pedCloths._lastBagDraw, pedCloths._lastBagText);
                            pedCloths._lastBagDraw = 0;
                            pedCloths._lastBagText = 0;
                            return true;
                        }
                        else
                        {
                            pedCloths._lastBagDraw = clothDraw;
                            pedCloths._lastBagText = clothTex;
                            myChar.SetVariation(comp, 0, 0);
                            return true;
                        }
                    }
                case 6: //shoes M:34, F:35
                    return false;
                case 7: //necklace
                    if (clothDraw == -1 || clothDraw == 0 && pedCloths._lastNeckDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1)
                        {
                            myChar.SetVariation(comp, pedCloths._lastNeckDraw, pedCloths._lastNeckText);
                            pedCloths._lastNeckDraw = 0;
                            pedCloths._lastNeckText = 0;
                            return true;
                        }
                        else
                        {
                            pedCloths._lastNeckDraw = clothDraw;
                            pedCloths._lastNeckText = clothTex;
                            myChar.SetVariation(comp, 0, 0);
                            return true;
                        }
                    }
                case 9: //vest
                    if (clothDraw == -1 || clothDraw == 0 && pedCloths._lastVestDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1)
                        {
                            myChar.SetVariation(comp, pedCloths._lastVestDraw, pedCloths._lastVestText);
                            pedCloths._lastVestDraw = 0;
                            pedCloths._lastVestText = 0;
                            return true;
                        }
                        else
                        {
                            pedCloths._lastVestDraw = clothDraw;
                            pedCloths._lastVestText = clothTex;
                            myChar.SetVariation(comp, 0, 0);
                            return true;
                        }
                    }
                case 11: //shirt M:252, F:74 //undershirt(8,15,0), gloves(3,15,0), decals(10,0,0)
                    return false;
                default:
                    return false;
            }
        }

        private static bool ToggleProp(int prop) 
        {
            Ped myChar = Game.LocalPlayer.Character;
            int currentProp = NativeFunction.Natives.GetPedPropIndex<int>(myChar, prop);
            int currentPropTex = NativeFunction.Natives.GetPedPropTextureIndex<int>(myChar, prop);
            switch (prop)
            {
                case 0: //hat
                    if (currentProp == -1 && pedProps._lastHatDraw == 0) 
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    } 
                    else 
                    {
                        if (currentProp == -1) 
                        {
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastHatDraw, pedProps._lastHatText, true);
                            pedProps._lastHatDraw = 0;
                            pedProps._lastHatText = 0;
                            return true;
                        } else {
                            pedProps._lastHatDraw = currentProp;
                            pedProps._lastHatText = currentPropTex;
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, -1, 0, true); //0?
                            return true;
                        }
                    }
                case 1: //glasses
                    if (currentProp == -1 && pedProps._lastGlassesDraw == 0) 
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    } 
                    else 
                    {
                        if (currentProp == -1) {
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastGlassesDraw, pedProps._lastGlassesText, true);
                            pedProps._lastGlassesDraw = 0;
                            pedProps._lastGlassesText = 0;
                            return true;
                        } else {
                            pedProps._lastGlassesDraw = currentProp;
                            pedProps._lastGlassesText = currentPropTex;
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, -1, 0, true); //0?
                            return true;
                        }
                    }
                case 2: //ear
                    if (currentProp == -1 && pedProps._lastEarDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (currentProp == -1)
                        {
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastEarDraw, pedProps._lastEarText, true);
                            pedProps._lastEarDraw = 0;
                            pedProps._lastEarText = 0;
                            return true;
                        }
                        else
                        {
                            pedProps._lastEarDraw = currentProp;
                            pedProps._lastEarText = currentPropTex;
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, -1, 0, true); //0?
                            return true;
                        }
                    }
                case 6: //watch
                    if (currentProp == -1 && pedProps._lastWatchDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (currentProp == -1)
                        {
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastWatchDraw, pedProps._lastWatchText, true);
                            pedProps._lastWatchDraw = 0;
                            pedProps._lastWatchText = 0;
                            return true;
                        }
                        else
                        {
                            pedProps._lastWatchDraw = currentProp;
                            pedProps._lastWatchText = currentPropTex;
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, -1, 0, true); //0?
                            return true;
                        }
                    }
                case 7: //bracelet
                    if (currentProp == -1 && pedProps._lastBraceletDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (currentProp == -1)
                        {
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastBraceletDraw, pedProps._lastBraceletText, true);
                            pedProps._lastBraceletDraw = 0;
                            pedProps._lastBraceletText = 0;
                            return true;
                        }
                        else
                        {
                            pedProps._lastBraceletDraw = currentProp;
                            pedProps._lastBraceletText = currentPropTex;
                            NativeFunction.Natives.SetPedPropIndex(myChar, prop, -1, 0, true); //0?
                            return true;
                        }
                    }
                default:
                    return false;
            }
            
        }
        #endregion

        #region Main function handler
        private static void SwitchClothing(int id)
        {
            switch (id)
            {
                case 1: //Hair Variation: 2 (Draw)
                    PlayAnim("clothingtie", "check_out_a", 51, 2000);
                    break;
                case 2: //Bag
                    if (ToggleVariation(5))
                        PlayAnim("anim@heists@ornate_bank@grab_cash", "intro", 51, 1600);
                    break;
                case 3: //Glasses
                    if (ToggleProp(1)) 
                        PlayAnim("clothingspecs", "take_off", 51, 1400);
                    break;
                case 4: //Ear
                    if (ToggleProp(2))
                        PlayAnim("mp_cp_stolen_tut", "b_think", 51, 900);
                    break;
                case 5: //Necklace
                    if (ToggleVariation(7))
                        PlayAnim("clothingtie", "try_tie_positive_a", 51, 2100);
                    break;
                case 6: //Bracelet
                    if (ToggleProp(7))
                        PlayAnim("nmt_3_rcm-10", "cs_nigel_dual-10", 51, 1200);
                    break;
                case 7: //Watch
                    if (ToggleProp(6))
                        PlayAnim("nmt_3_rcm-10", "cs_nigel_dual-10", 51, 1200);
                    break;
                case 8: //Vest
                    if (ToggleVariation(9))
                        PlayAnim("clothingtie", "try_tie_negative_a", 51, 1200);
                    break;
                case 9: //Mask
                    if (ToggleVariation(1))
                        PlayAnim("anim@heists@ornate_bank@grab_cash", "intro", 51, 800);
                    break;
                case 10: //Shoes
                    if (ToggleVariation(6))
                        PlayAnim("random@domestic", "pickup_low", 0, 1200);
                    break;
                case 11: //Hat
                    if (ToggleProp(0))
                        PlayAnim("mp_masks@standard_car@ds@", "put_on_mask", 51, 600);
                    break; 
                case 12: //Gloves
                    if (ToggleVariation(3))
                        PlayAnim("nmt_3_rcm-10", "cs_nigel_dual-10", 51, 1200);
                    break;
                case 13: //Pants
                    if (ToggleVariation(4))
                        PlayAnim("re@construction", "out_of_breath", 51, 1300);
                    break;
                case 14: //Shirt
                    if (ToggleVariation(11))
                        PlayAnim("clothingtie", "try_tie_negative_a", 51, 1200);
                    break;
                default:
                    Game.DisplayNotification("The fuck have you done?");
                    break;
            }
        }
        #endregion

        #region Menu processor
        private static void ProcessMenus()
        {
            while (true)
            {
                GameFiber.Yield();
                pool.ProcessMenus();
                if (Game.IsKeyDown(KeyBinding) && !UIMenu.IsAnyMenuVisible && !TabView.IsAnyPauseMenuVisible)
                {
                    mainMenu.Visible = true;
                    mainMenu.MouseControlsEnabled = false;
                }
            }
        }
        #endregion
    }
}
