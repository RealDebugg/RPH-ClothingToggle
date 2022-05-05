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
        private static Props pedProps = new Props();
        private static Variations pedCloths = new Variations();
        private static Config cfg = new Config();
        private static Gloves mG = new Gloves();
        private static Hair pH = new Hair();
        #endregion

        #region Set Data
        private static void SetMaleGloves()
        {
            mG.male.Add(16, 4);
            mG.male.Add(17, 4);
            mG.male.Add(18, 4);
            mG.male.Add(19, 0);
            mG.male.Add(20, 1);
            mG.male.Add(21, 2);
            mG.male.Add(22, 4);
            mG.male.Add(23, 5);
            mG.male.Add(24, 6);
            mG.male.Add(25, 8);
            mG.male.Add(26, 11);
            mG.male.Add(27, 12);
            mG.male.Add(28, 14);
            mG.male.Add(29, 15);
            mG.male.Add(30, 0);
            mG.male.Add(31, 1);
            mG.male.Add(32, 2);
            mG.male.Add(33, 4);
            mG.male.Add(34, 5);
            mG.male.Add(35, 6);
            mG.male.Add(36, 8);
            mG.male.Add(37, 11);
            mG.male.Add(38, 12);
            mG.male.Add(39, 14);
            mG.male.Add(40, 15);
            mG.male.Add(41, 0);
            mG.male.Add(42, 1);
            mG.male.Add(43, 2);
            mG.male.Add(44, 4);
            mG.male.Add(45, 5);
            mG.male.Add(46, 6);
            mG.male.Add(47, 8);
            mG.male.Add(48, 11);
            mG.male.Add(49, 12);
            mG.male.Add(50, 14);
            mG.male.Add(51, 15);
            mG.male.Add(52, 0);
            mG.male.Add(53, 1);
            mG.male.Add(54, 2);
            mG.male.Add(55, 4);
            mG.male.Add(56, 5);
            mG.male.Add(57, 6);
            mG.male.Add(58, 8);
            mG.male.Add(59, 11);
            mG.male.Add(60, 12);
            mG.male.Add(61, 14);
            mG.male.Add(62, 15);
            mG.male.Add(63, 0);
            mG.male.Add(64, 1);
            mG.male.Add(65, 2);
            mG.male.Add(66, 4);
            mG.male.Add(67, 5);
            mG.male.Add(68, 6);
            mG.male.Add(69, 8);
            mG.male.Add(70, 11);
            mG.male.Add(71, 12);
            mG.male.Add(72, 14);
            mG.male.Add(73, 15);
            mG.male.Add(74, 0);
            mG.male.Add(75, 1);
            mG.male.Add(76, 2);
            mG.male.Add(77, 4);
            mG.male.Add(78, 5);
            mG.male.Add(79, 6);
            mG.male.Add(80, 8);
            mG.male.Add(81, 11);
            mG.male.Add(82, 12);
            mG.male.Add(83, 14);
            mG.male.Add(84, 15);
            mG.male.Add(85, 0);
            mG.male.Add(86, 1);
            mG.male.Add(87, 2);
            mG.male.Add(88, 4);
            mG.male.Add(89, 5);
            mG.male.Add(90, 6);
            mG.male.Add(91, 8);
            mG.male.Add(92, 11);
            mG.male.Add(93, 12);
            mG.male.Add(94, 14);
            mG.male.Add(95, 15);
            mG.male.Add(96, 4);
            mG.male.Add(97, 4);
            mG.male.Add(98, 4);
            mG.male.Add(99, 0);
            mG.male.Add(100, 1);
            mG.male.Add(101, 2);
            mG.male.Add(102, 4);
            mG.male.Add(103, 5);
            mG.male.Add(104, 6);
            mG.male.Add(105, 8);
            mG.male.Add(106, 11);
            mG.male.Add(107, 12);
            mG.male.Add(108, 14);
            mG.male.Add(109, 15);
            mG.male.Add(110, 4);
            mG.male.Add(111, 4);
            mG.male.Add(115, 112);
            mG.male.Add(116, 112);
            mG.male.Add(117, 112);
            mG.male.Add(118, 112);
            mG.male.Add(119, 112);
            mG.male.Add(120, 112);
            mG.male.Add(121, 112);
            mG.male.Add(122, 113);
            mG.male.Add(123, 113);
            mG.male.Add(124, 113);
            mG.male.Add(125, 113);
            mG.male.Add(126, 113);
            mG.male.Add(127, 113);
            mG.male.Add(128, 113);
            mG.male.Add(129, 114);
            mG.male.Add(130, 114);
            mG.male.Add(131, 114);
            mG.male.Add(132, 114);
            mG.male.Add(133, 114);
            mG.male.Add(134, 114);
            mG.male.Add(135, 114);
            mG.male.Add(136, 15);
            mG.male.Add(137, 15);
            mG.male.Add(138, 0);
            mG.male.Add(139, 1);
            mG.male.Add(140, 2);
            mG.male.Add(141, 4);
            mG.male.Add(142, 5);
            mG.male.Add(143, 6);
            mG.male.Add(144, 8);
            mG.male.Add(145, 11);
            mG.male.Add(146, 12);
            mG.male.Add(147, 14);
            mG.male.Add(148, 112);
            mG.male.Add(149, 113);
            mG.male.Add(150, 114);
            mG.male.Add(151, 0);
            mG.male.Add(152, 1);
            mG.male.Add(153, 2);
            mG.male.Add(154, 4);
            mG.male.Add(155, 5);
            mG.male.Add(156, 6);
            mG.male.Add(157, 8);
            mG.male.Add(158, 11);
            mG.male.Add(159, 12);
            mG.male.Add(160, 14);
            mG.male.Add(161, 112);
            mG.male.Add(162, 113);
            mG.male.Add(163, 114);
            mG.male.Add(165, 4);
            mG.male.Add(166, 4);
            mG.male.Add(167, 4);
        }

        private static void SetFemaleGloves()
        {
            mG.female.Add(16, 11);
            mG.female.Add(17, 3);
            mG.female.Add(18, 3);
            mG.female.Add(19, 3);
            mG.female.Add(20, 0);
            mG.female.Add(21, 1);
            mG.female.Add(22, 2);
            mG.female.Add(23, 3);
            mG.female.Add(24, 4);
            mG.female.Add(25, 5);
            mG.female.Add(26, 6);
            mG.female.Add(27, 7);
            mG.female.Add(28, 9);
            mG.female.Add(29, 11);
            mG.female.Add(30, 12);
            mG.female.Add(31, 14);
            mG.female.Add(32, 15);
            mG.female.Add(33, 0);
            mG.female.Add(34, 1);
            mG.female.Add(35, 2);
            mG.female.Add(36, 3);
            mG.female.Add(37, 4);
            mG.female.Add(38, 5);
            mG.female.Add(39, 6);
            mG.female.Add(40, 7);
            mG.female.Add(41, 9);
            mG.female.Add(42, 11);
            mG.female.Add(43, 12);
            mG.female.Add(44, 14);
            mG.female.Add(45, 15);
            mG.female.Add(46, 0);
            mG.female.Add(47, 1);
            mG.female.Add(48, 2);
            mG.female.Add(49, 3);
            mG.female.Add(50, 4);
            mG.female.Add(51, 5);
            mG.female.Add(52, 6);
            mG.female.Add(53, 7);
            mG.female.Add(54, 9);
            mG.female.Add(55, 11);
            mG.female.Add(56, 12);
            mG.female.Add(57, 14);
            mG.female.Add(58, 15);
            mG.female.Add(59, 0);
            mG.female.Add(60, 1);
            mG.female.Add(61, 2);
            mG.female.Add(62, 3);
            mG.female.Add(63, 4);
            mG.female.Add(64, 5);
            mG.female.Add(65, 6);
            mG.female.Add(66, 7);
            mG.female.Add(67, 9);
            mG.female.Add(68, 11);
            mG.female.Add(69, 12);
            mG.female.Add(70, 14);
            mG.female.Add(71, 15);
            mG.female.Add(72, 0);
            mG.female.Add(73, 1);
            mG.female.Add(74, 2);
            mG.female.Add(75, 3);
            mG.female.Add(76, 4);
            mG.female.Add(77, 5);
            mG.female.Add(78, 6);
            mG.female.Add(79, 7);
            mG.female.Add(80, 9);
            mG.female.Add(81, 11);
            mG.female.Add(82, 12);
            mG.female.Add(83, 14);
            mG.female.Add(84, 15);
            mG.female.Add(85, 0);
            mG.female.Add(86, 1);
            mG.female.Add(87, 2);
            mG.female.Add(88, 3);
            mG.female.Add(89, 4);
            mG.female.Add(90, 5);
            mG.female.Add(91, 6);
            mG.female.Add(92, 7);
            mG.female.Add(93, 9);
            mG.female.Add(94, 11);
            mG.female.Add(95, 12);
            mG.female.Add(96, 14);
            mG.female.Add(97, 15);
            mG.female.Add(98, 0);
            mG.female.Add(99, 1);
            mG.female.Add(100, 2);
            mG.female.Add(101, 3);
            mG.female.Add(102, 4);
            mG.female.Add(103, 5);
            mG.female.Add(104, 6);
            mG.female.Add(105, 7);
            mG.female.Add(106, 9);
            mG.female.Add(107, 11);
            mG.female.Add(108, 12);
            mG.female.Add(109, 14);
            mG.female.Add(110, 15);
            mG.female.Add(111, 3);
            mG.female.Add(112, 3);
            mG.female.Add(113, 3);
            mG.female.Add(114, 0);
            mG.female.Add(115, 1);
            mG.female.Add(116, 2);
            mG.female.Add(117, 3);
            mG.female.Add(118, 4);
            mG.female.Add(119, 5);
            mG.female.Add(120, 6);
            mG.female.Add(121, 7);
            mG.female.Add(122, 9);
            mG.female.Add(123, 11);
            mG.female.Add(124, 12);
            mG.female.Add(125, 14);
            mG.female.Add(126, 15);
            mG.female.Add(127, 3);
            mG.female.Add(128, 3);
            mG.female.Add(132, 129);
            mG.female.Add(133, 129);
            mG.female.Add(134, 129);
            mG.female.Add(135, 129);
            mG.female.Add(136, 129);
            mG.female.Add(137, 129);
            mG.female.Add(138, 129);
            mG.female.Add(139, 130);
            mG.female.Add(140, 130);
            mG.female.Add(141, 130);
            mG.female.Add(142, 130);
            mG.female.Add(143, 130);
            mG.female.Add(144, 130);
            mG.female.Add(145, 130);
            mG.female.Add(146, 131);
            mG.female.Add(147, 131);
            mG.female.Add(148, 131);
            mG.female.Add(149, 131);
            mG.female.Add(150, 131);
            mG.female.Add(151, 131);
            mG.female.Add(152, 131);
            mG.female.Add(154, 153);
            mG.female.Add(155, 153);
            mG.female.Add(156, 153);
            mG.female.Add(157, 153);
            mG.female.Add(158, 153);
            mG.female.Add(159, 153);
            mG.female.Add(160, 153);
            mG.female.Add(162, 161);
            mG.female.Add(163, 161);
            mG.female.Add(164, 161);
            mG.female.Add(165, 161);
            mG.female.Add(166, 161);
            mG.female.Add(167, 161);
            mG.female.Add(168, 161);
            mG.female.Add(169, 15);
            mG.female.Add(170, 15);
            mG.female.Add(171, 0);
            mG.female.Add(172, 1);
            mG.female.Add(173, 2);
            mG.female.Add(174, 3);
            mG.female.Add(175, 4);
            mG.female.Add(176, 5);
            mG.female.Add(177, 6);
            mG.female.Add(178, 7);
            mG.female.Add(179, 9);
            mG.female.Add(180, 11);
            mG.female.Add(181, 12);
            mG.female.Add(182, 14);
            mG.female.Add(183, 129);
            mG.female.Add(184, 130);
            mG.female.Add(185, 131);
            mG.female.Add(186, 153);
            mG.female.Add(187, 0);
            mG.female.Add(188, 1);
            mG.female.Add(189, 2);
            mG.female.Add(190, 3);
            mG.female.Add(191, 4);
            mG.female.Add(192, 5);
            mG.female.Add(193, 6);
            mG.female.Add(194, 7);
            mG.female.Add(195, 9);
            mG.female.Add(196, 11);
            mG.female.Add(197, 12);
            mG.female.Add(198, 14);
            mG.female.Add(199, 129);
            mG.female.Add(200, 130);
            mG.female.Add(201, 131);
            mG.female.Add(202, 153);
            mG.female.Add(203, 161);
            mG.female.Add(204, 161);
            mG.female.Add(206, 3);
            mG.female.Add(207, 3);
            mG.female.Add(208, 3);
        }

        private static void SetMaleHair()
        {
            pH.male.Add(7, 15);
            pH.male.Add(9, 43);
            pH.male.Add(11, 43);
            pH.male.Add(16, 43);
            pH.male.Add(17, 43);
            pH.male.Add(20, 43);
            pH.male.Add(22, 43);
            pH.male.Add(45, 43);
            pH.male.Add(47, 43);
            pH.male.Add(49, 43);
            pH.male.Add(51, 43);
            pH.male.Add(52, 43);
            pH.male.Add(53, 43);
            pH.male.Add(56, 43);
            pH.male.Add(58, 43);
        }

        private static void SetFemaleHair()
        {
            pH.female.Add(1, 49);
            pH.female.Add(2, 49);
            pH.female.Add(7, 49);
            pH.female.Add(9, 49);
            pH.female.Add(10, 49);
            pH.female.Add(11, 48);
            pH.female.Add(14, 53);
            pH.female.Add(15, 42);
            pH.female.Add(21, 42);
            pH.female.Add(23, 42);
            pH.female.Add(39, 49);
            pH.female.Add(40, 49);
            pH.female.Add(45, 49);
            pH.female.Add(54, 55);
            pH.female.Add(59, 42);
            pH.female.Add(68, 53);
            pH.female.Add(76, 48);
        }
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
            string keyBinding = ini.ReadString("Keybindings", "openMenuBinding", "F6");
            return keyBinding;
        }

        public static void Main()
        {
            #region Set Data
            SetMaleGloves();
            SetFemaleGloves();
            SetMaleHair();
            SetFemaleHair();
            #endregion

            #region Keybinding & Config
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
            LoadConfig();
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
            mainMenu.AddItems(Pants, Gloves, Hat, Shoes, Mask, Vest, Watch, Bracelet, Necklace, Ear, Glasses, bag, hair, Shirt);
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
        private static void LoadBackupConfig()
        {
            cfg.mShoes = 34;
            cfg.fShoes = 35;
            cfg.mPants = 61;
            cfg.fPants = 14;
            cfg.Undershirt = 15;
            cfg.fShirt = 74;
            cfg.mShirt = 252;
        }

        private static void LoadConfig()
        {
            InitializationFile ini = initialiseFile();
            try
            {
                cfg.mShoes = ini.ReadInt32("Clothing", "mShoes", 34);
                cfg.fShoes = ini.ReadInt32("Clothing", "fShoes", 35);
                cfg.mPants = ini.ReadInt32("Clothing", "mPants", 61);
                cfg.fPants = ini.ReadInt32("Clothing", "fPants", 14);
                cfg.Undershirt = ini.ReadInt32("Clothing", "Undershirt", 15);
                cfg.fShirt = ini.ReadInt32("Clothing", "fShirt", 74);
                cfg.mShirt = ini.ReadInt32("Clothing", "mShirt", 252);
            }
            catch (Exception)
            {
                LoadBackupConfig();
            }
        }

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
