using System;
using System.Collections.Generic;
using PokeEngine.Menu;
using PokeEngine.Screens;
using PokeEngine.Trainers;
using LuaInterface;
using PokeEngine.Map;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PokeEngine.Moves;
using PokeEngine.Pokemon;

namespace PokeEngine.TFSH
{
    /// <summary>
    /// A collection of useful methods for use in scripts
    /// </summary>
    public static class PokeEngineScriptHelper
    {
        public static object lockObject;
        private static World world;
        private static GraphicsDeviceManager graphics;
        private static ContentManager content;
        private static SpriteFont font;

        public static void Initialize(GraphicsDeviceManager g, ContentManager c, SpriteFont f, World inWorld)
        {
            graphics = g;
            content = c;
            font = f;

            world = inWorld;
            lockObject = new object();
        }

        #region menu
        /// <summary>
        /// Shows the dialog box
        /// </summary>
        /// <param name="text">Text to show</param>
        [LuaGlobalAttribute]
        public static void ShowMessage(String text)
        {
            DialogBox.newMessage(graphics, content, font, new Message(text));
        }

        
        [LuaGlobalAttribute] //Let me know when you can see it F-B
        public static int ShowOption(string message, LuaTable options)
        {

            List<string> strings = new List<string>();
            int choice = 0;

            foreach (var v in options.Values)
            {
                if(v.GetType() == typeof(string))
                {
                    strings.Add((string)v);
                }
            }
            DialogBox.newMessage(graphics, content, font, new Message(message, strings.ToArray()));

            lock (lockObject)
            {
                //wait for result to be chosen
                Monitor.Wait(lockObject);

                //get the result from the dialogbox class
                choice = DialogBox.choice;
            }

            //return what the user has chosen
            return choice;
        }

        [LuaGlobalAttribute]
        public static bool PercentChance(int percent)
        {
            Random random = new Random();
            if (random.Next(100) < percent)
                return true;
            else
                return false;
        }

         
 
        #endregion

        //modifies pokemon stuff
        #region pokemon

        #endregion

        //modifies player stuff
        #region player

        /// <summary>
        /// Changes map and map coordinates
        /// </summary>
        /// <param name="mapName">name of map to go to</param>
        /// <param name="xCoord">local x coordinates</param>
        /// <param name="yCoord">local y coordinates</param>
        [LuaGlobalAttribute]
        public static void SetMap(String mapName, int xCoord, int yCoord)
        {
            //the while loop makes sure the dialog box is closed before it changes the map
            
            //disable drawing while transitioning, cause this is in a separate thread
            GameScreen.StopDrawing = true;
            lock (lockObject)
            {
                GameScreen.world.currentArea.scenery = new List<Map.Scenery>();
                GameScreen.world.changeZone(mapName);
                GameScreen.player.tileCoords = new Microsoft.Xna.Framework.Point(xCoord, yCoord);
                GameScreen.player.nextTile = new Point(xCoord, yCoord);
                try
                {
                    GameDraw.MakeAdjBuffers(GameScreen.world);
                    GameDraw.UpdateNPCSpritesheets(GameScreen.world);
                }
                catch (InvalidOperationException) { }
            }
            //reenable drawing
            GameScreen.StopDrawing = false;
            
        }

        /// <summary>
        /// changes player's speed
        /// </summary>
        /// <param name="speed">speed value, must be greater than 0, lower is faster</param>
        [LuaGlobalAttribute]
        public static void SetPlayerSpeed(int speed)
        {
            if (speed >= 0)
            {
                GameScreen.player.speed = speed;
            }
        }

        #endregion

        [LuaGlobalAttribute]
        public static void EndCutScene()
        {
            if (ScreenHandler.TopScreen.GetType() == typeof(CutSceneScreen))
            {
                ((CutSceneScreen)ScreenHandler.TopScreen).EndScene();
            }
        }

        [LuaGlobalAttribute]
        public static void WaitTill(String npcName, int time)
        {
            if (ScreenHandler.TopScreen.GetType() == typeof(CutSceneScreen))
            {
                if (time >= 0)
                {
                    foreach (NPC n in world.currentArea.trainerList)
                    {
                        if (n.name == npcName)
                        {
                            n.actionCoolDown = time - ((CutSceneScreen)ScreenHandler.TopScreen).sceneTime;
                            //can't be less than zero
                            if (n.actionCoolDown < 0)
                                n.actionCoolDown = 0;
                        }
                    }
                }
            }
        }

        [LuaGlobalAttribute]
        public static void AddNewPokemon(Player player, string pokemonname, int level)
        {
            BaseMove tackle = new BaseMove("Tackle", "hits the opponent hard", 50, 100, "Normal", "Physical", 35);
            tackle.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove scratch = new BaseMove("Scratch", "hits the opponent hard", 40, 100, "Normal", "Physical", 35);
            scratch.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove pound = new BaseMove("Pound", "hits the opponent hard", 40, 100, "Normal", "Physical", 35);
            pound.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove growl = new BaseMove("Growl", "Lowers target's attack", 0, 100, "Normal", "Special", 40);
            growl.moveScript = @" if user:hits(move, target) then
                                    target:changeStat('Attack', -1)
                                    ShowMessage(target.pokemon.Nickname .. 'has had their attack lowered')
                                    end
                                ";
            BaseMove foresight = new BaseMove("Foresight", "Negates accuracy reduction moves", 0, "Normal", "Status", 40);
            foresight.moveScript = @"target:changeStat('Accuracy', 12)
                                    target:changeStat('Accuracy', -6)
                                    ShowMessage(target.pokemon.Nickname .. 'has had their accuracy reset')
                                ";
            BaseMove quickattack = new BaseMove("Quick Attack", "Always strikes first", 40, 100, "Normal", "Physical", 30);
            quickattack.basePriority = 1;
            quickattack.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove tailwhip = new BaseMove("Tail Whip", "Lowers target's defense", 0, 100, "Normal", "Special", 40);
            tailwhip.moveScript = @" if user:hits(move, target) then
                                    target:changeStat('Defense', -1)
                                    ShowMessage(target.pokemon.Nickname .. 'has had their defense lowered')
                                    end
                                ";
            BaseMove ember = new BaseMove("Ember", "fires the opponent", 40, 100, "Fire", "Special", 25);
            ember.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove bubble = new BaseMove("Bubble", "waters the opponent", 20, 100, "Water", "Special", 30);
            bubble.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove razorleaf = new BaseMove("Razor Leaf", "Sends razor sharp leaves at the target", 55, 95, "Grass", "Special", 25);
            razorleaf.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";

           
            ActivePokemon newPokemon = new ActivePokemon(BaseStatsList.basestats);
            switch (pokemonname)
            {
                case "Charmander":
                    newPokemon.baseStat = BaseStatsList.GetBaseStats("Charmander");
                    newPokemon.move[0] = new ActiveMove(scratch);
                    newPokemon.move[1] = new ActiveMove(growl);
                    newPokemon.move[2] = new ActiveMove(ember);
                    break;
                case "Mudkip":
                    newPokemon.baseStat = BaseStatsList.GetBaseStats("Piplup");
                    newPokemon.move[0] = new ActiveMove(pound);
                    newPokemon.move[1] = new ActiveMove(growl);
                    newPokemon.move[2] = new ActiveMove(bubble);
                    break;
                case "Chikorita":
                    newPokemon.baseStat = BaseStatsList.GetBaseStats("Chikorita");
                    newPokemon.move[0] = new ActiveMove(tackle);
                    newPokemon.move[1] = new ActiveMove(growl);
                    newPokemon.move[2] = new ActiveMove(razorleaf);
                    break;
                default: 
                    newPokemon.baseStat = BaseStatsList.GetBaseStats("Piplup");
                    newPokemon.move[0] = new ActiveMove(pound);
                    newPokemon.move[1] = new ActiveMove(growl);
                    newPokemon.move[2] = new ActiveMove(bubble);
                    break;
            }

            newPokemon.level = 7;
            newPokemon.currentHP = newPokemon.HP;
            newPokemon.addExp(newPokemon.expAtLevel(newPokemon.level) - 1);
            player.currentPokemon[0] = newPokemon;
        }

        [LuaGlobalAttribute]
        public static void StartRandomEncounter(Player player)
        {
            //This is only a test implementation for now
            //Starts random battle with Starly, Rattata, or HootHoot
            Trainer trainer = new Trainer();


            BaseMove tackle = new BaseMove("Tackle", "hits the opponent hard", 50, 100, "Normal", "Physical", 35);
            tackle.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove scratch = new BaseMove("Scratch", "hits the opponent hard", 40, 100, "Normal", "Physical", 35);
            scratch.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove growl = new BaseMove("Growl", "Lowers target's attack", 0, 100, "Normal", "Special", 40);
            growl.moveScript = @" if user:hits(move, target) then
                                    target:changeStat('Attack', -1)
                                    ShowMessage(target.pokemon.Nickname .. 'has had their attack lowered')
                                    end
                                ";
            BaseMove foresight = new BaseMove("Foresight", "Negates accuracy reduction moves", 0, "Normal", "Status", 40);
            foresight.moveScript = @"target:changeStat('Accuracy', 12)
                                    target:changeStat('Accuracy', -6)
                                    ShowMessage(target.pokemon.Nickname .. 'has had their accuracy reset')
                                ";
            BaseMove quickattack = new BaseMove("Quick Attack", "Always strikes first", 40, 100, "Normal", "Physical", 30);
            quickattack.basePriority = 1;
            quickattack.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove tailwhip = new BaseMove("Tail Whip", "Lowers target's defense", 0, 100, "Normal", "Special", 40);
            tailwhip.moveScript = @" if user:hits(move, target) then
                                    target:changeStat('Defense', -1)
                                    ShowMessage(target.pokemon.Nickname .. 'has had their defense lowered')
                                    end
                                ";


            ActivePokemon enemy = new ActivePokemon(BaseStatsList.basestats);
            Random random = new Random();
            int r = random.Next(3);
            switch (r)
            {
                case 0:
                    enemy.baseStat = BaseStatsList.GetBaseStats("Starly");
                    enemy.move[0] = new ActiveMove(tackle);
                    enemy.move[1] = new ActiveMove(growl);
                    enemy.move[2] = new ActiveMove(quickattack);
                    break;
                case 1:
                    enemy.baseStat = BaseStatsList.GetBaseStats("Hoothoot");
                    enemy.move[0] = new ActiveMove(tackle);
                    enemy.move[1] = new ActiveMove(growl);
                    enemy.move[2] = new ActiveMove(foresight);
                    break;
                case 2:
                    enemy.baseStat = BaseStatsList.GetBaseStats("Rattata"); 
                    enemy.move[0] = new ActiveMove(tackle);
                    enemy.move[1] = new ActiveMove(tailwhip);
                    enemy.move[2] = new ActiveMove(quickattack);
                    break;
                default: return;
            }

            enemy.level = 5;
            enemy.currentHP = enemy.HP;
            enemy.addExp(enemy.expAtLevel(enemy.level) - 1);

            trainer.addPokemon(enemy);

            ScreenHandler.TopScreen.IsVisible = false;
            ScreenHandler.PushScreen(new BattleScreen(graphics, content, font, player, trainer));

        }

        #region NPC
        
        ///I don't even know what this is supposed to be - Ency
        /*
        // Moving/Facing Functions
        [LuaGlobalAttribute]
        public static void Function(byte f, int arg0)
        {
            switch(f)
            {
                #region Facing

                    // Face Down
                case 0x0:
                    GameScreen.ActiveNPC.facing = Trainers.FacingDirection.South;
                    break;
                    // Face Up
                case 0x1:
                    GameScreen.ActiveNPC.facing = Trainers.FacingDirection.North;
                    break;
                    // Face Left
                case 0x2:
                    GameScreen.ActiveNPC.facing = Trainers.FacingDirection.East;
                    break;
                    // Face Right
                case 0x3:
                    GameScreen.ActiveNPC.facing = Trainers.FacingDirection.West;
                    break;
                    // Face Down (Faster)
                case 0x4:
                    Console.WriteLine("Function " + f + " is not implemented yet!");
                    break;
                    // Face Up (Faster)
                case 0x5:
                    Console.WriteLine("Function " + f + " is not implemented yet!");
                    break;
                    // Face Left (Faster)
                case 0x6:
                    Console.WriteLine("Function " + f + " is not implemented yet!");
                    break;
                    // Face Right (Faster)
                case 0x7:
                    Console.WriteLine("Function " + f + " is not implemented yet!");
                    break;
                    // Face Down (Slow/Delayed)
                case 0x8:
                    Console.WriteLine("Function" + f + " is not implemeneted yet!");
                    break;
                    // Face Up (Slow/Delayed)
                case 0x9:
                    Console.WriteLine("Function" + f + " is not implemeneted yet!");
                    break;
                    // Face Left (Slow/Delayed)
                case 0xA:
                    Console.WriteLine("Function" + f + " is not implemeneted yet!");
                    break;
                    // Face Right (Slow/Delayed)
                case 0xB:
                    Console.WriteLine("Function" + f + " is not implemeneted yet!");
                    break;

                #endregion

                #region Stepping

                    // Step Down (Very Slow)
                case 0xC:
                    GameScreen.ActiveNPC.speed = NPC.VERY_SLOW;
                    GameScreen.ActiveNPC.tryToMove("Down");
                    break;
                    // Step Up (Very Slow)
                case 0xD:
                    GameScreen.ActiveNPC.speed = NPC.VERY_SLOW;
                    GameScreen.ActiveNPC.tryToMove("Up");
                    break;
                    // Step Left (Very Slow)
                case 0xE:
                    GameScreen.ActiveNPC.speed = NPC.VERY_SLOW;
                    GameScreen.ActiveNPC.tryToMove("Left");
                    break;
                    // Step Right (Very Slow)
                case 0xF:
                    GameScreen.ActiveNPC.speed = NPC.VERY_SLOW;
                    GameScreen.ActiveNPC.tryToMove("Right");
                    break;
                    // Step Down (Slow)
                case 0x10:
                    GameScreen.ActiveNPC.speed = NPC.SLOW;
                    GameScreen.ActiveNPC.tryToMove("Down");
                    break;
                    // Step Up (Slow)
                case 0x11:
                    GameScreen.ActiveNPC.speed = NPC.SLOW;
                    GameScreen.ActiveNPC.tryToMove("Up");
                    break;
                    // Step Left (Slow)
                case 0x12:
                    GameScreen.ActiveNPC.speed = NPC.SLOW;
                    GameScreen.ActiveNPC.tryToMove("Left");
                    break;
                    // Step Right (Slow)
                case 0x13:
                    GameScreen.ActiveNPC.speed = NPC.SLOW;
                    GameScreen.ActiveNPC.tryToMove("Right");
                    break;
                    // Step Down (Normal)
                case 0x14:
                    GameScreen.ActiveNPC.speed = NPC.NORMAL;
                    GameScreen.ActiveNPC.tryToMove("Down");
                    break;
                    // Step Up (Normal)
                case 0x15:
                    GameScreen.ActiveNPC.speed = NPC.NORMAL;
                    GameScreen.ActiveNPC.tryToMove("Up");
                    break;
                    // Step Left (Normal)
                case 0x16:
                    GameScreen.ActiveNPC.speed = NPC.NORMAL;
                    GameScreen.ActiveNPC.tryToMove("Left");
                    break;
                    // Step Right (Normal)
                case 0x17:
                    GameScreen.ActiveNPC.speed = NPC.NORMAL;
                    GameScreen.ActiveNPC.tryToMove("Right");
                    break;
                    // Step Down (Fast)
                case 0x18:
                    GameScreen.ActiveNPC.speed = NPC.FAST;
                    GameScreen.ActiveNPC.tryToMove("Down");
                    break;
                    // Step Up (Fast)
                case 0x19:
                    GameScreen.ActiveNPC.speed = NPC.FAST;
                    GameScreen.ActiveNPC.tryToMove("Up");
                    break;
                    // Step Left (Fast)
                case 0x1A:
                    GameScreen.ActiveNPC.speed = NPC.FAST;
                    GameScreen.ActiveNPC.tryToMove("Left");
                    break;
                    // Step Right (Fast)
                case 0x1B:
                    GameScreen.ActiveNPC.speed = NPC.FAST;
                    GameScreen.ActiveNPC.tryToMove("Right");
                    break;
                    // Step Down (Very Fast)
                case 0x1C:
                    GameScreen.ActiveNPC.speed = NPC.VERY_FAST;
                    GameScreen.ActiveNPC.tryToMove("Down");
                    break;
                    // Step Up (Very Fast)
                case 0x1D:
                    GameScreen.ActiveNPC.speed = NPC.VERY_FAST;
                    GameScreen.ActiveNPC.tryToMove("Up");
                    break;
                    // Step Left (Very Fast)
                case 0x1E:
                    GameScreen.ActiveNPC.speed = NPC.VERY_FAST;
                    GameScreen.ActiveNPC.tryToMove("Left");
                    break;
                    // Step Right (Very Fast)
                case 0x1F:
                    GameScreen.ActiveNPC.speed = NPC.VERY_FAST;
                    GameScreen.ActiveNPC.tryToMove("Right");
                    break;

                #endregion

                #region Player Running

                    // Run Down
                case 0x20:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    // Run Up
                case 0x21:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    // Run Left
                case 0x22:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    // Run Right
                case 0x23:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                #endregion

                #region Sliding

                // Slide Down (Slow)
                case 0x24:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Up (Slow)
                case 0x25:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Left (Slow)
                case 0x26:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Right (Slow)
                case 0x27:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Down (Normal)
                case 0x28:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Up (Normal)
                case 0x29:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Left (Normal)
                case 0x2A:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Right (Normal)
                case 0x2B:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Down (Fast)
                case 0x2C:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Up (Fast)
                case 0x2D:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Left (Fast)
                case 0x2E:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide Right (Fast)
                case 0x2F:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Right Foot (Down)
                case 0x30:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Right Foot (Up)
                case 0x31:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Right Foot (Left)
                case 0x32:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Right Foot (Right)
                case 0x33:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Left Foot (Down)
                case 0x34:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Left Foot (Up)
                case 0x35:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Left Foot (Left)
                case 0x36:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Slide on Left Foot (Right)
                case 0x37:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;


                #endregion

                #region Player Slide

                    // Slide Running on Right Foot (Down)
                case 0x38:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                    // Slide Running on Right Foot (Up)
                case 0x39:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                    // Slide Running on Right Foot (Left)
                case 0x3A:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    
                    // Slide Running on Right Foot (Right)
                case 0x3B:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    
                    // Slide Running on Left Foot (Down)
                case 0x3C:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    
                    // Slide Running on Left Foot (Up)
                case 0x3D:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    
                    // Slide Running on Left Foot (Left)
                case 0x3E:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                    
                    // Slide Running on Left Foot (Right)
                case 0x3F:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                #endregion

                #region Jumping

                // Jump Down
                case 0x40:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Up
                case 0x41:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Left
                case 0x42:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Right
                case 0x43:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Down)
                case 0x44:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Up)
                case 0x45:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Left)
                case 0x46:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Right)
                case 0x47:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Down/Up)
                case 0x48:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Up/Down)
                case 0x49:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Left/Right)
                case 0x4A:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump in Place (Facing Right/Left)
                case 0x4B:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Facing Left (Down)
                case 0x4C:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Facing Down (Up)
                case 0x4D:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Facing Up (Left)
                case 0x4E:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Facing Left (Right)
                case 0x4F:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Down
                case 0x50:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Up
                case 0x51:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Left
                case 0x52:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Right
                case 0x53:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                #endregion

                #region Player JumpRunning
                // Jump Down Running
                case 0x54:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Up Running
                case 0x55:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Left Running
                case 0x56:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump Right Running
                case 0x57:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Down Running
                case 0x58:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Up Running
                case 0x59:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Right Running
                case 0x5A:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Jump2 Left Running
                case 0x5B:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                #endregion

                // Delay
                case 0x5C:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                #region Emoticons

                // Exclamation Mark (!)
                case 0x5D:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Double Exclamation Mark (!!)
                case 0x5E:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Question Mark (?)
                case 0x5F:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Cross (X)
                case 0x60:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Love (<3)
                case 0x61:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Happy (^_^)
                case 0x62:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                #endregion

                #region Misc

                // Face Player
                case 0x63:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Face Away from Player
                case 0x64:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Lock Sprite Facing
                case 0x65:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Release Sprite Facing
                case 0x66:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Hide Sprite
                case 0x67:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Show Sprite
                case 0x68:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Levitate
                case 0x69: //dohoho funny number
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Stop Levitating
                case 0x6A:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Fly Up Vertically
                case 0x6B:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;
                // Land
                case 0x6C:
                    Console.WriteLine("Function " + f + " not implemented yet!");
                    break;

                #endregion
                
            }
        }
        */

        #endregion

        #region tools

        [LuaGlobalAttribute]
        public static void Print(String text)
        {
            Console.Write(text);
        }

        [LuaGlobalAttribute]
        public static void PrintLine(String text)
        {
            Console.WriteLine(text);
        }

        #endregion
    }

    public static class ScriptVariables
    {
        internal static LuaTable variables;

        internal static void TakeVariables(Lua lua)
        {
            //take the variables straight out of the runtime
            variables = lua.GetTable("var");
        }

        /// <summary>
        /// This puts the persistant variables into the given lua runtime
        /// </summary>
        /// <param name="lua">lua runtime to put variables into</param>
        internal static void PutVariables(Lua lua)
        {
            //create blank table
            lua.NewTable("var");
            LuaTable temp = lua.GetTable("var");

            //fill up table
            if (variables != null)
            {
                foreach (var v in variables.Keys)
                {
                    temp[v] = variables[v];
                }
            }
            //global and var both point to the same table
            lua.DoString("global = var");

        }
    }

    public enum DataType : byte
    {
        DOUBLE,
        STRING,
        BOOLEAN
    }
}
