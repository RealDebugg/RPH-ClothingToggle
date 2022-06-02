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
    public static class EntryPoint
    {
        #region Variables
        private static MenuPool pool;
        private static UIMenu mainMenu;
        private static Keys KeyBinding;
        private static Keys GloveKey;
        private static Keys GlassKey;
        private static Keys ModifierKey;
        private static Props pedProps = new Props();
        private static Variations pedCloths = new Variations();
        private static Config cfg = new Config();
        private static Gloves mG = new Gloves();
        private static Jackets mJ = new Jackets();
        private static Bags mB = new Bags();
        private static Visor mV = new Visor();
        private static Hair pH = new Hair();
        private static bool isToggle = true;
        private static bool EnableQuickKey = true;
        private static bool UseModifierKey = true;
        #endregion

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
            KeysConverter kc = new KeysConverter();
            string keyBinding = ini.ReadString("Keybindings", "openMenuBinding", "F6");
            isToggle = ini.ReadBoolean("Keybindings", "ToggleKey", true);
            EnableQuickKey = ini.ReadBoolean("Keybindings", "EnableQuickKey", true);
            UseModifierKey = ini.ReadBoolean("Keybindings", "UseModifierKey", true);
            ModifierKey = (Keys)kc.ConvertFromString(ini.ReadString("Keybindings", "ModifierKey", "LShiftKey"));
            GlassKey = (Keys)kc.ConvertFromString(ini.ReadString("Keybindings", "toggGlasses", "V"));
            GloveKey = (Keys)kc.ConvertFromString(ini.ReadString("Keybindings", "toggGloves", "G"));
            return keyBinding;
        }

        public static void Main()
        {
            #region Keybinding & Config
            KeysConverter kc = new KeysConverter();
            KeyBinding = (Keys)kc.ConvertFromString(getMyKeyBinding());
            LoadConfig();
            #endregion

            #region Menu
            pool = new MenuPool();
            mainMenu = new UIMenu("Outfit Menu", "Select an option to toggle it.");
            UIMenuItem hair = new UIMenuItem("Hair", "Toggle hair bun or down");
            UIMenuItem bag = new UIMenuItem("Bag", "Toggle bag");
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
            UIMenuItem Visor = new UIMenuItem("Visor", "Toggle hat variation");
            UIMenuItem Jacket = new UIMenuItem("Jacket", "Toggle Jacket");
            UIMenuItem Bags = new UIMenuItem("Bag O/C", "Opens or closes your bag");
            mainMenu.AddItems(Pants, Gloves, Hat, Shoes, Mask, Vest, Watch, Bracelet, Necklace, Ear, Glasses, bag, hair, Shirt, Visor, Jacket, Bags);
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
                case "Visor":
                    SwitchClothing(15);
                    break;
                case "Jacket":
                    SwitchClothing(16);
                    break;
                case "Bag O/C":
                    SwitchClothing(17);
                    break;

                default:
                    Game.DisplayNotification("An error occurred???");
                    break;
            }
        }
        #endregion

        #region Functions
        private static void LoadConfig()
        {
            InitializationFile ini = initialiseFile();
            cfg.mShoes = ini.ReadInt32("Clothing", "mShoes", 34);
            cfg.fShoes = ini.ReadInt32("Clothing", "fShoes", 35);
            cfg.mPants = ini.ReadInt32("Clothing", "mPants", 61);
            cfg.fPants = ini.ReadInt32("Clothing", "fPants", 14);
            cfg.Undershirt = ini.ReadInt32("Clothing", "Undershirt", 15);
            cfg.fShirt = ini.ReadInt32("Clothing", "fShirt", 74);
            cfg.mShirt = ini.ReadInt32("Clothing", "mShirt", 252);
        }

        private static void PlayAnim(string dict, string anim, int move, int dur)
        {
            Ped myChar = Game.LocalPlayer.Character;
            myChar.Tasks.PlayAnimation(dict, anim, 3.0f, (AnimationFlags)move);
            GameFiber.Wait(dur);
            myChar.Tasks.ClearSecondary();
        }
        
        private static bool ToggleVars(int comp, int prop)
        {
            Ped myChar = Game.LocalPlayer.Character;
            uint myModel = NativeFunction.Natives.GetEntityModel<uint>(myChar);
            int clothDraw;
            int clothTex;
            myChar.GetVariation(comp, out clothDraw, out clothTex);
            int currentProp = NativeFunction.Natives.GetPedPropIndex<int>(myChar, prop);
            int currentPropTex = NativeFunction.Natives.GetPedPropTextureIndex<int>(myChar, prop);
            switch (comp)
            {
                case 0: //Visor
                    if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                    {
                        if (!mV.female.ContainsKey(currentProp) && pedProps._lastVisorDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mV.female.ContainsKey(currentProp) && pedProps._lastVisorDraw != 0)
                            {
                                NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastVisorDraw, pedProps._lastVisorText, true);
                                pedProps._lastVisorDraw = 0;
                                pedProps._lastVisorText = 0;
                                return true;
                            }
                            else if (mV.female.ContainsKey(currentProp) && pedProps._lastVisorDraw == 0)
                            {
                                pedProps._lastVisorDraw = currentProp;
                                pedProps._lastVisorText = currentPropTex;
                                NativeFunction.Natives.SetPedPropIndex(myChar, prop, mV.female[currentProp], currentPropTex, true);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (!mV.male.ContainsKey(currentProp) && pedProps._lastVisorDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mV.male.ContainsKey(currentProp) && pedProps._lastVisorDraw != 0)
                            {
                                NativeFunction.Natives.SetPedPropIndex(myChar, prop, pedProps._lastVisorDraw, pedProps._lastVisorText, true);
                                pedProps._lastVisorDraw = 0;
                                pedProps._lastVisorText = 0;
                                return true;
                            }
                            else if (mV.male.ContainsKey(currentProp) && pedProps._lastVisorDraw == 0)
                            {
                                pedProps._lastVisorDraw = currentProp;
                                pedProps._lastVisorText = currentPropTex;
                                NativeFunction.Natives.SetPedPropIndex(myChar, prop, mV.male[currentProp], currentPropTex, true);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                case 11: //Jacket
                    if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                    {
                        if (!mJ.female.ContainsKey(clothDraw) && pedCloths._lastJacketsDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mJ.female.ContainsKey(clothDraw) && pedCloths._lastJacketsDraw != 0)
                            {
                                myChar.SetVariation(comp, pedCloths._lastJacketsDraw, pedCloths._lastJacketsText);
                                pedCloths._lastJacketsDraw = 0;
                                pedCloths._lastJacketsText = 0;
                                return true;
                            }
                            else if (mJ.female.ContainsKey(clothDraw) && pedCloths._lastJacketsDraw == 0)
                            {
                                pedCloths._lastJacketsDraw = clothDraw;
                                pedCloths._lastJacketsText = clothTex;
                                myChar.SetVariation(comp, mJ.female[clothDraw], 0);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (!mJ.male.ContainsKey(clothDraw) && pedCloths._lastJacketsDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mJ.male.ContainsKey(clothDraw) && pedCloths._lastJacketsDraw != 0)
                            {
                                myChar.SetVariation(comp, pedCloths._lastJacketsDraw, pedCloths._lastJacketsText);
                                pedCloths._lastJacketsDraw = 0;
                                pedCloths._lastJacketsText = 0;
                                return true;
                            }
                            else if (mJ.male.ContainsKey(clothDraw) && pedCloths._lastJacketsDraw == 0)
                            {
                                pedCloths._lastJacketsDraw = clothDraw;
                                pedCloths._lastJacketsText = clothTex;
                                myChar.SetVariation(comp, mJ.male[clothDraw], 0);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                case 5: //Bags
                    if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                    {
                        if (!mB.female.ContainsKey(clothDraw) && pedCloths._lastBagsDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mB.female.ContainsKey(clothDraw) && pedCloths._lastBagsDraw != 0)
                            {
                                myChar.SetVariation(comp, pedCloths._lastBagsDraw, pedCloths._lastBagsText);
                                pedCloths._lastBagsDraw = 0;
                                pedCloths._lastBagsText = 0;
                                return true;
                            }
                            else if (mB.female.ContainsKey(clothDraw) && pedCloths._lastBagsDraw == 0)
                            {
                                pedCloths._lastBagsDraw = clothDraw;
                                pedCloths._lastBagsText = clothTex;
                                myChar.SetVariation(comp, mB.female[clothDraw], 0);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (!mB.male.ContainsKey(clothDraw) && pedCloths._lastBagsDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mB.male.ContainsKey(clothDraw) && pedCloths._lastBagsDraw != 0)
                            {
                                myChar.SetVariation(comp, pedCloths._lastBagsDraw, pedCloths._lastBagsText);
                                pedCloths._lastBagsDraw = 0;
                                pedCloths._lastBagsText = 0;
                                return true;
                            }
                            else if (mB.male.ContainsKey(clothDraw) && pedCloths._lastBagsDraw == 0)
                            {
                                pedCloths._lastBagsDraw = clothDraw;
                                pedCloths._lastBagsText = clothTex;
                                myChar.SetVariation(comp, mB.male[clothDraw], 0);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                default:
                    return false;
            }
        }

        private static bool ToggleVariation(int comp)
        {
            Ped myChar = Game.LocalPlayer.Character;
            uint myModel = NativeFunction.Natives.GetEntityModel<uint>(myChar);
            int clothDraw;
            int clothTex;
            myChar.GetVariation(comp, out clothDraw, out clothTex);
            switch (comp)
            {
                case 1: //mask
                    if ((clothDraw == -1 || clothDraw == 0) && pedCloths._lastMaskDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1 || clothDraw == 0)
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
                case 3: //gloves
                    if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                    {
                        if (!mG.female.ContainsKey(clothDraw) && pedCloths._lastGlovesDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        }
                        else
                        {
                            if (!mG.female.ContainsKey(clothDraw) && pedCloths._lastGlovesDraw != 0)
                            {
                                myChar.SetVariation(comp, pedCloths._lastGlovesDraw, pedCloths._lastGlovesText);
                                pedCloths._lastGlovesDraw = 0;
                                pedCloths._lastGlovesText = 0;
                                return true;
                            }
                            else if (mG.female.ContainsKey(clothDraw) && pedCloths._lastGlovesDraw == 0)
                            {
                                pedCloths._lastGlovesDraw = clothDraw;
                                pedCloths._lastGlovesText = clothTex;
                                myChar.SetVariation(comp, mG.female[clothDraw], 0);
                                return true;
                            }
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    } 
                    else
                    {
                        if(!mG.male.ContainsKey(clothDraw) && pedCloths._lastGlovesDraw == 0)
                        {
                            Game.DisplayNotification("You dont appear to have anything to toggle.");
                            return false;
                        } 
                        else
                        {
                            if (!mG.male.ContainsKey(clothDraw) && pedCloths._lastGlovesDraw != 0)
                            {
                                myChar.SetVariation(comp, pedCloths._lastGlovesDraw, pedCloths._lastGlovesText);
                                pedCloths._lastGlovesDraw = 0;
                                pedCloths._lastGlovesText = 0;
                                return true;
                            } 
                            else if (mG.male.ContainsKey(clothDraw) && pedCloths._lastGlovesDraw == 0)
                            {
                                pedCloths._lastGlovesDraw = clothDraw;
                                pedCloths._lastGlovesText = clothTex;
                                myChar.SetVariation(comp, mG.male[clothDraw], 0);
                                return true;
                            } 
                            else
                            {
                                Game.DisplayNotification("You dont appear to have anything to toggle.");
                                return false;
                            }
                        }
                    }
                case 4: //pants
                    if ((clothDraw == cfg.fPants || clothDraw == cfg.mPants) && pedCloths._lastPantsDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == cfg.fPants || clothDraw == cfg.mPants)
                        {
                            myChar.SetVariation(comp, pedCloths._lastPantsDraw, pedCloths._lastPantsText);
                            pedCloths._lastPantsDraw = 0;
                            pedCloths._lastPantsText = 0;
                            return true;
                        }
                        else
                        {
                            pedCloths._lastPantsDraw = clothDraw;
                            pedCloths._lastPantsText = clothTex;
                            if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                                myChar.SetVariation(comp, cfg.fPants, 0);
                            else
                                myChar.SetVariation(comp, cfg.mPants, 0);
                            return true;
                        }
                    }
                case 5: //bag
                    if ((clothDraw == -1 || clothDraw == 0) && pedCloths._lastBagDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1 || clothDraw == 0)
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
                case 6: //shoes
                    if ((clothDraw == cfg.mShoes || clothDraw == cfg.fShoes) && pedCloths._lastShoesDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == cfg.fShoes || clothDraw == cfg.mShoes)
                        {
                            myChar.SetVariation(comp, pedCloths._lastShoesDraw, pedCloths._lastShoesText);
                            pedCloths._lastShoesDraw = 0;
                            pedCloths._lastShoesText = 0;
                            return true;
                        }
                        else
                        {
                            pedCloths._lastShoesDraw = clothDraw;
                            pedCloths._lastShoesText = clothTex;
                            if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                                myChar.SetVariation(comp, cfg.fShoes, 0);
                            else
                                myChar.SetVariation(comp, cfg.mShoes, 0);
                            return true;
                        }
                    }
                case 7: //necklace
                    if ((clothDraw == -1 || clothDraw == 0) && pedCloths._lastNeckDraw == 0)
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
                    if ((clothDraw == -1 || clothDraw == 0) && pedCloths._lastVestDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == -1 || clothDraw == 0)
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
                case 11: //shirt
                    if ((clothDraw == cfg.fShirt || clothDraw == cfg.mShirt) && pedCloths._lastShirtDraw == 0)
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                    else
                    {
                        if (clothDraw == cfg.fShirt || clothDraw == cfg.mShirt)
                        {
                            myChar.SetVariation(comp, pedCloths._lastShirtDraw, pedCloths._lastShirtText);
                            myChar.SetVariation(8, pedCloths._lastUndershirtDraw, pedCloths._lastUndershirtText);
                            myChar.SetVariation(10, pedCloths._lastDecalDraw, pedCloths._lastDecalText);
                            pedCloths._lastShirtDraw = 0;
                            pedCloths._lastShirtText = 0;
                            pedCloths._lastUndershirtDraw = 0;
                            pedCloths._lastUndershirtText = 0;
                            pedCloths._lastDecalDraw = 0;
                            pedCloths._lastDecalText = 0;
                            return true;
                        }
                        else
                        {
                            int USDraw;
                            int USText;
                            int DecalDraw;
                            int DecalText;
                            myChar.GetVariation(10, out DecalDraw, out DecalText);
                            myChar.GetVariation(8, out USDraw, out USText);
                            pedCloths._lastShirtDraw = clothDraw;
                            pedCloths._lastShirtText = clothTex;
                            pedCloths._lastUndershirtDraw = USDraw;
                            pedCloths._lastUndershirtText = USText;
                            pedCloths._lastDecalDraw = DecalDraw;
                            pedCloths._lastDecalText = DecalText;
                            if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
                            {
                                myChar.SetVariation(comp, cfg.fShirt, 0);
                            }
                            else
                            {
                                myChar.SetVariation(comp, cfg.mShirt, 0);
                            }
                            myChar.SetVariation(10, 0, 0);
                            myChar.SetVariation(8, cfg.Undershirt, 0);
                            return true;
                        }
                    }
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
                            NativeFunction.Natives.ClearPedProp(myChar, prop);
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
                            NativeFunction.Natives.ClearPedProp(myChar, prop);
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
                            NativeFunction.Natives.ClearPedProp(myChar, prop);
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
                            NativeFunction.Natives.ClearPedProp(myChar, prop);
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
                            NativeFunction.Natives.ClearPedProp(myChar, prop);
                            return true;
                        }
                    }
                default:
                    return false;
            }
            
        }

        private static bool IsMPPed()
        {
            Ped myChar = Game.LocalPlayer.Character;
            uint myModel = NativeFunction.Natives.GetEntityModel<uint>(myChar);
            if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01") || myModel == NativeFunction.Natives.GetHashKey<uint>("mp_m_freemode_01"))
                return true;
            else
                return false;
        }

        private static bool ToggleHair(int comp)
        {
            Ped myChar = Game.LocalPlayer.Character;
            uint myModel = NativeFunction.Natives.GetEntityModel<uint>(myChar);
            int clothDraw;
            int clothTex;
            myChar.GetVariation(comp, out clothDraw, out clothTex);
            if (myModel == NativeFunction.Natives.GetHashKey<uint>("mp_f_freemode_01"))
            {
                if (!pH.female.ContainsKey(clothDraw) && pH._lastHairDraw == 0)
                {
                    Game.DisplayNotification("You dont appear to have anything to toggle.");
                    return false;
                }
                else
                {
                    if (!pH.female.ContainsKey(clothDraw) && pH._lastHairDraw != 0)
                    {
                        myChar.SetVariation(comp, pH._lastHairDraw, pH._lastHairText);
                        pH._lastHairDraw = 0;
                        pH._lastHairText = 0;
                        return true;
                    }
                    else if (pH.female.ContainsKey(clothDraw) && pH._lastHairDraw == 0)
                    {
                        pH._lastHairDraw = clothDraw;
                        pH._lastHairText = clothTex;
                        myChar.SetVariation(comp, pH.female[clothDraw], clothTex);
                        return true;
                    }
                    else
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                }
            }
            else
            {
                if (!pH.male.ContainsKey(clothDraw) && pH._lastHairDraw == 0)
                {
                    Game.DisplayNotification("You dont appear to have anything to toggle.");
                    return false;
                }
                else
                {
                    if (!pH.male.ContainsKey(clothDraw) && pH._lastHairDraw != 0)
                    {
                        myChar.SetVariation(comp, pH._lastHairDraw, pH._lastHairText);
                        pH._lastHairDraw = 0;
                        pH._lastHairText = 0;
                        return true;
                    }
                    else if (pH.male.ContainsKey(clothDraw) && pH._lastHairDraw == 0)
                    {
                        pH._lastHairDraw = clothDraw;
                        pH._lastHairText = clothTex;
                        myChar.SetVariation(comp, pH.male[clothDraw], clothTex);
                        return true;
                    }
                    else
                    {
                        Game.DisplayNotification("You dont appear to have anything to toggle.");
                        return false;
                    }
                }
            }
        }
        #endregion

        #region Main function handler
        private static void SwitchClothing(int id)
        {
            switch (id)
            {
                case 1: //Hair Variation: 2 (Draw)
                    if (ToggleHair(2))
                        PlayAnim("clothingtie", "check_out_a", 51, 2000);
                    break;
                case 2: //Bag
                    if (ToggleVariation(5))
                        PlayAnim("clothingtie", "try_tie_negative_a", 51, 1600);
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
                case 15: //Visor (PROP 0)
                    if (ToggleVars(0, 0))
                        PlayAnim("mp_masks@standard_car@ds@", "put_on_mask", 51, 600);
                    break;
                case 16: //Jacket
                    if (ToggleVars(11, 0))
                        PlayAnim("missmic4", "michael_tux_fidget", 51, 1500);
                    break;
                case 17: //Bag O/C (DRAWABLE 5, VARIATION)
                    if (ToggleVars(5, 0))
                        PlayAnim("anim@heists@ornate_bank@grab_cash", "intro", 51, 1600);
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

                if(!UIMenu.IsAnyMenuVisible && EnableQuickKey)
                {
                    if (UseModifierKey)
                    {
                        if (Game.GetKeyboardState().IsDown(ModifierKey))
                        {
                            if (Game.IsKeyDown(GloveKey))
                            {
                                SwitchClothing(12);
                            }

                            if (Game.IsKeyDown(GlassKey))
                            {
                                SwitchClothing(3);
                            }
                        }
                    } 
                    else if (!UseModifierKey)
                    {
                        if (Game.IsKeyDown(GloveKey))
                        {
                            SwitchClothing(12);
                        }

                        if (Game.IsKeyDown(GlassKey))
                        {
                            SwitchClothing(3);
                        }
                    }
                }

                if (isToggle)
                {
                    if (Game.IsKeyDown(KeyBinding) && !UIMenu.IsAnyMenuVisible && !TabView.IsAnyPauseMenuVisible)
                    {
                        if (IsMPPed())
                        {
                            mainMenu.Visible = true;
                            mainMenu.MouseControlsEnabled = false;
                        }
                        else
                        {
                            Game.DisplayNotification("~r~~h~Error~h~~s~: Switch to a MP ped to open this menu!!");
                        }
                    }
                }
                else
                {
                    if (Game.IsKeyDownRightNow(KeyBinding))
                    {
                        if (IsMPPed())
                        {
                            mainMenu.Visible = true;
                            mainMenu.MouseControlsEnabled = false;
                        }
                        else
                        {
                            Game.DisplayNotification("~r~~h~Error~h~~s~: Switch to a MP ped to open this menu!!");
                        }
                    }
                    else
                    {
                        mainMenu.Visible = false;
                    }
                }
            }
        }
        #endregion
    }
}
