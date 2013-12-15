using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
Here's a library that lists every single move in Pokemon.
Have fun.

Copyright (C) 2010 The Int'l Association of Pokemon Leagues

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

Credits-
bigplrbear
Minute

You can find more code for the PokeEngine at http://code.google.com/p/pokeengine,
post on the forums at http://pokeleagues.org, or chat with us
in the IRC at irc.rizon.net #PokeEngine.

Feel free to use our code for whatever you need, but please give us credit!
*/

namespace PokeEngine.Pokemon_Library.Moves
{
    public enum MoveType
    {
        Normal, Fire, Fighting, Water, Flying,
        Grass, Poison, Electric, Ground, Psychic,
        Rock, Ice, Bug, Dragon, Ghost, Dark, Steel
    };
    public enum Category
    {
        Physical, Special, Status
    };
    public class Move
    {
        private MoveType type;
        private string name;
        private Category category;
        private string effect; //Description of what the move does
        private string machine; // TM or HM
        private int pp;
        private int power; //How much damage the attack does
        private int accuracy; //How often is it going to hit
        private int probability; //the probability of the secondary effect

        public Move(string name, MoveType type, Category category, int power, int accuracy, int pp, string machine, string effect, int probability)
        {
            this.name = name;
            this.effect = effect;
            this.machine = machine;
            this.type = type;
            this.category = category;
            this.pp = pp;
            this.power = power;
            this.accuracy = accuracy;
            this.probability = probability;
        }

        public MoveType Type { get { return type; } set { type = value; } }
        public string Name { get { return name; } set { name = value; } }
        public Category Categories { get { return category; } set { category = value; } }
        public string Effect { get { return effect; } set { effect = value; } }
        public string Machine { get { return machine; } set { machine = value; } }
        public int PP { get { return pp; } set { pp = value; } }
        public int Power { get { return power; } set { power = value; } }
        public int Probablity { get { return probability; } set { probability = value; } }
        public int Accuracy { get { return accuracy; } set { accuracy = value; } }

        public void printMoveTypes()
        {
            Array values = Enum.GetValues(typeof(MoveType));

            foreach (MoveType val in values)
            {
                Console.WriteLine(String.Format("{0}: {1}", Enum.GetName(typeof(MoveType), val), val));
            }
        }
        public void printCategoryType()
        {
            Array values = Enum.GetValues(typeof(Category));

            foreach (Category val in values)
            {
                Console.WriteLine(String.Format("{0}: {1}", Enum.GetName(typeof(Category), val), val));
            }
        }
        public override string ToString()
        {
            String result = "Name: " + name + "\n" +
                            "Move type: " + type + "\n" +
                            "Category: " + category + "\n" +
                            "Power: " + power + "\n" +
                            "Accuracy: %" + accuracy + "\n" +
                            "PP: " + pp + "\n" +
                            "TM/HM: " + machine + "\n" +
                            "Effect: " + effect + "\n" +
                            "Probabilty: " + probability + "\n";
            return result;
        }
    }

    public class MoveList
    {
        // -1 represents that the stat does not apply to the move
        public static Move Absorb = new Move("Absorb", MoveType.Grass, Category.Special, 20, 100, 25, "-", "User recovers half of the HP inflicted on opponent.", -1);
        public static Move Acid = new Move("Acid", MoveType.Poison, Category.Special, 40, 100, 30, "-", "May lower opponent's Defense or Special Defense one stage.", 10);
        public static Move AcidArmor = new Move("Acid Armor", MoveType.Poison, Category.Status, -1, -1, 40, "-", "Increases user's Defense by two stages.", -1);
        public static Move AcidBomb = new Move("Acid Bomb", MoveType.Poison, Category.Special, 40, 100, 20, "-", "Lowers the target's Special Defense by one stage.", 100);
        public static Move Acrobat = new Move("Acrobat", MoveType.Flying, Category.Physical, 55, 100, 15, "-", "Stronger when the user does not have a held item.", -1);
        public static Move Acupressure = new Move("Acupressure", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Randomly raises one of the user's stats by two stages.", -1);
        public static Move AerialAce = new Move("Aerial Ace", MoveType.Flying, Category.Physical, 60, 8, 20, "TM40", "Cannot miss, regardless of Accuracy or Evasiveness.", -1);
        public static Move Aeroblast = new Move("Aeroblast", MoveType.Flying, Category.Special, 100, 95, 5, "-", "High Critical-Hit ratio.", -1);
        public static Move AfroBreak = new Move("Afro Break", MoveType.Normal, Category.Physical, 120, 100, 15, "-", "User receives 1/4 the damage it inflicts in recoil.", -1);
        public static Move Agility = new Move("Agility", MoveType.Psychic, Category.Status, -1, -1, 30, "-", "Raises the user's Speed by two stages.", -1);
        public static Move AirCutter = new Move("Air Cutter", MoveType.Flying, Category.Special, 55, 95, 25, "-", "High Critical-Hit ratio.", -1);
        public static Move AirSlash = new Move("Air Slash", MoveType.Flying, Category.Special, 75, 95, 20, "-", "May cause flinching.", 30);
        public static Move Amnesia = new Move("Amnesia", MoveType.Psychic, Category.Status, -1, -1, 20, "-", "Raises user's Special Defense by two stages.", -1);
        public static Move Ancient = new Move("Ancient Song", MoveType.Normal, Category.Special, 75, 100, 10, "-", "May put the target to sleep.", 10);
        public static Move AncientPower = new Move("Ancient Power", MoveType.Rock, Category.Special, 60, 100, 5, "-", "Might raise all of the user's stats by one stage.", 10);
        public static Move AnkleSweep = new Move("Ankle Sweep", MoveType.Fighting, Category.Physical, 60, 100, 20, "-", "Lowers the target's Speed by one level.", 100);
        public static Move AquaJet = new Move("Aqua Jet", MoveType.Water, Category.Physical, 40, 100, 20, "-", "User goes first.", -1);
        public static Move AquaRing = new Move("Aqua Ring", MoveType.Water, Category.Status, -1, -1, 20, "-", "Restores a little HP each turn.", -1);
        public static Move AquaTail = new Move("Aqua Tail", MoveType.Water, Category.Physical, 90, 90, 10, "-", "", -1);
        public static Move ArmThrust = new Move("Arm Thrust", MoveType.Fighting, Category.Physical, 15, 100, 20, "-", "Hits 2-5 times.", -1);
        public static Move Aromatherapy = new Move("Aromatherapy", MoveType.Grass, Category.Status, -1, -1, 5, "-", "Cures all Status problems in your party.", -1);
        public static Move Assist = new Move("Assist", MoveType.Normal, Category.Status, -1, -1, 20, "-", "In a Double Battle, user randomly attacks with a partner's move.", -1);
        public static Move AssistPower = new Move("Assist Power", MoveType.Psychic, Category.Special, 20, 100, 10, "-", "Power rises the more the user's stats have been raised.", -1);
        public static Move Assurance = new Move("Assurance", MoveType.Dark, Category.Physical, 50, 100, 10, "-", "Power doubles if opponent already took damage in the same turn.", -1);
        public static Move Astonish = new Move("Astonish", MoveType.Ghost, Category.Physical, 30, 100, 15, "-", "May cause flinching.", 30);
        public static Move AttackOrder = new Move("Attack Order", MoveType.Bug, Category.Physical, 90, 100, 15, "-", "High Critical-Hit ratio.", -1);
        public static Move Attract = new Move("Attract", MoveType.Normal, Category.Status, -1, 100, 15, "TM45", "If opponent is the opposite gender, it's less likely to attack.", -1);
        public static Move AuraSphere = new Move("Aura Sphere", MoveType.Fighting, Category.Special, 90, 8, 20, "-", "Cannot miss, regardless of Accuracy or Evasiveness.", -1);
        public static Move AuroraBeam = new Move("Aurora Beam", MoveType.Ice, Category.Special, 65, 100, 20, "-", "Might lower opponent's Attack.", 10);
        public static Move Avalanche = new Move("Avalanche", MoveType.Ice, Category.Physical, 60, 100, 10, "TM72", "Power doubles if user took damage first.", -1);
        public static Move BackOut = new Move("Back Out", MoveType.Dark, Category.Special, 55, 95, 15, "-", "May lower the target's Special Attack by one level.", -1);
        public static Move Barrage = new Move("Barrage", MoveType.Normal, Category.Physical, 15, 85, 20, "-", "Hits 2-5 times.", -1);
        public static Move Barrier = new Move("Barrier", MoveType.Psychic, Category.Status, -1, -1, 30, "-", "Raises user's Defense by two stages.", -1);
        public static Move BatonPass = new Move("Baton Pass", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Allows user to switch but gives stat changes to the new Pokemon.", -1);
        public static Move BeatUp = new Move("Beat Up", MoveType.Dark, Category.Physical, 10, 100, 30, "-", "Each Pokemon in your party attacks.", -1);
        public static Move Befriend = new Move("Befriend", MoveType.Normal, Category.Status, -1, 100, 15, "-", "User copies the target's nature.", -1);
        public static Move BellyDrum = new Move("Belly Drum", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User loses 50% of its max HP, but Attack raises to maximum.", -1);
        public static Move Bide = new Move("Bide", MoveType.Normal, Category.Physical, -1, -1, 10, "-", "User takes damage for two turns and strikes back double on 3rd.", -1);
        public static Move Bind = new Move("Bind", MoveType.Normal, Category.Physical, 15, 75, 20, "-", "Damages opponent for 2-5 turns, opponent cannot escape/switch.", -1);
        public static Move Bite = new Move("Bite", MoveType.Dark, Category.Physical, 60, 100, 25, "-", "May cause flinching.", 30);
        public static Move BlastBurn = new Move("Blast Burn", MoveType.Fire, Category.Special, 150, 90, 5, "-", "User cannot use a move next turn.", -1);
        public static Move BlazeJudgement = new Move("Blaze Judgement", MoveType.Fire, Category.Special, 100, 100, 5, "-", "May burn the target.", 30);
        public static Move BlazeKick = new Move("Blaze Kick", MoveType.Fire, Category.Physical, 85, 90, 10, "-", "High Critical-Hit ratio. May cause a burn.", 10);
        public static Move Blizzard = new Move("Blizzard", MoveType.Ice, Category.Special, 120, 70, 5, "TM14", "May cause freezing.", 10);
        public static Move Block = new Move("Block", MoveType.Normal, Category.Status, -1, -1, 5, "-", "Opponent cannot flee or switch.", -1);
        public static Move BlueFlame = new Move("Blue Flame", MoveType.Fire, Category.Special, 130, 85, 5, "-", "May burn the target.", 20);
        public static Move BodyPurge = new Move("Body Purge", MoveType.Steel, Category.Status, -1, -1, 15, "-", "Raises the user's Speed by two stages.", -1);
        public static Move BodySlam = new Move("Body Slam", MoveType.Normal, Category.Physical, 85, 100, 15, "-", "May cause paralysis.", 30);
        public static Move BoilingWater = new Move("Boiling Water", MoveType.Water, Category.Special, 80, 100, 15, "-", "May burn the target.", 30);
        public static Move BoneClub = new Move("Bone Club", MoveType.Ground, Category.Physical, 65, 85, 20, "-", "May cause flinching.", 10);
        public static Move BoneRush = new Move("Bone Rush", MoveType.Ground, Category.Physical, 25, 80, 10, "-", "Hits 2-5 times.", -1);
        public static Move Bonemerang = new Move("Bonemerang", MoveType.Ground, Category.Physical, 50, 90, 10, "-", "Hits twice.", -1);
        public static Move Bounce = new Move("Bounce", MoveType.Flying, Category.Physical, 85, 85, 5, "-", "Flies up on the first turn, hits on the 2nd. May cause paralysis.", 30);
        public static Move BraveBird = new Move("Brave Bird", MoveType.Flying, Category.Physical, 120, 100, 15, "-", "User receives recoil damage.", -1);
        public static Move BrickBreak = new Move("Brick Break", MoveType.Fighting, Category.Physical, 75, 100, 30, "TM31", "Destroys Reflect, Light Screen, and Barrier.", -1);
        public static Move Brine = new Move("Brine", MoveType.Water, Category.Special, 65, 100, 10, "TM55", "Power doubles if opponent's HP is less than 50%.", -1);
        public static Move Bubble = new Move("Bubble", MoveType.Water, Category.Special, 20, 100, 30, "-", "May lower opponent's Speed by one stage.", 10);
        public static Move Bubblebeam = new Move("Bubblebeam", MoveType.Water, Category.Special, 65, 100, 20, "-", "May lower opponent's Speed by one stage.", 10);
        public static Move BugBite = new Move("Bug Bite", MoveType.Bug, Category.Physical, 60, 100, 20, "-", "Receives the effect from the opponent's held berry.", -1);
        public static Move BugBuzz = new Move("Bug Buzz", MoveType.Bug, Category.Special, 90, 100, 10, "-", "May lower opponent's Special Defense by one stage.", 10);
        public static Move BugRepellant = new Move("Bug Repellant", MoveType.Bug, Category.Special, 30, 100, 20, "-", "May lower the target's Special Attack by one level.", 100);
        public static Move BulkUp = new Move("Bulk Up", MoveType.Fighting, Category.Status, -1, -1, 20, "TM08", "Increases user's Attack and Defense by one stage.", -1);
        public static Move BulletPunch = new Move("Bullet Punch", MoveType.Steel, Category.Physical, 40, 100, 30, "-", "User goes first.", -1);
        public static Move BulletSeed = new Move("Bullet Seed", MoveType.Grass, Category.Physical, 10, 100, 30, "TM09", "Hits 2-5 times.", -1);
        public static Move ButterflyDance = new Move("Butterfly Dance", MoveType.Bug, Category.Status, -1, -1, 20, "-", "Raises the user's Special Attack, Special Defense, and Speed by one stage.", -1);
        public static Move CalmMind = new Move("Calm Mind", MoveType.Psychic, Category.Status, -1, -1, 20, "TM04", "Increases user's Special Attack and Special Defense by one stage.", -1);
        public static Move Camouflage = new Move("Camouflage", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Changes user's type according to the location.", -1);
        public static Move Captivate = new Move("Captivate", MoveType.Normal, Category.Status, -1, 100, 20, "TM78", "Foe with opposite gender has its Sp. Attack lowered by 2 stages.", -1);
        public static Move Charge = new Move("Charge", MoveType.Electric, Category.Status, -1, -1, 20, "-", "Power of your Electric-type attacks increase next turn.", -1);
        public static Move ChargeBeam = new Move("Charge Beam", MoveType.Electric, Category.Special, 50, 90, 10, "TM57", "Likely to lower opponent's Special Attack by one stage.", 70);
        public static Move Charm = new Move("Charm", MoveType.Normal, Category.Status, -1, 100, 20, "-", "Lowers opponent's Attack by two stages.", -1);
        public static Move Chatter = new Move("Chatter", MoveType.Flying, Category.Special, 60, 100, 20, "-", "Likely to confuse the opponent.", 50);
        public static Move CheerUp = new Move("Cheer Up", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Raises the user's Attack and MSpecial Attack by one stage.", -1);
        public static Move Clamp = new Move("Clamp", MoveType.Water, Category.Physical, 35, 75, 10, "-", "Damages opponent for 2-5 turns, oponent cannot escape/switch.", -1);
        public static Move ClawSharpen = new Move("Claw Sharpen", MoveType.Dark, Category.Status, -1, -1, 15, "-", "Raises the user's Attack and accuracy by one stage.", -1);
        public static Move ClearSmog = new Move("Clear Smog", MoveType.Poison, Category.Special, 50, -1, 15, "-", "Removes all of the target's stat changes.", -1);
        public static Move CloseCombat = new Move("Close Combat", MoveType.Fighting, Category.Physical, 120, 100, 5, "-", "Decreases user's Defense and Special Defense by one stage.", -1);
        public static Move ColdFlare = new Move("Cold Flare", MoveType.Ice, Category.Special, 140, 90, 5, "-", "Charges for a turn before attacking. May burn the target.", 30);
        public static Move CometPunch = new Move("Comet Punch", MoveType.Normal, Category.Physical, 18, 85, 15, "-", "Hits 2-5 times.", -1);
        public static Move ConfuseRay = new Move("Confuse Ray", MoveType.Ghost, Category.Status, -1, 100, 10, "-", "Confuses opponent.", -1);
        public static Move Confusion = new Move("Confusion", MoveType.Psychic, Category.Special, 50, 100, 25, "-", "May confuse opponent.", 10);
        public static Move Constrict = new Move("Constrict", MoveType.Normal, Category.Physical, 10, 100, 35, "-", "May lower opponent's Speed by one stage.", 10);
        public static Move Conversion = new Move("Conversion", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Changes user's type to a type of one of its own moves.", -1);
        public static Move Conversion2 = new Move("Conversion 2", MoveType.Normal, Category.Status, -1, -1, 30, "-", "User changes type to become resistant to opponent's last move.", -1);
        public static Move Copycat = new Move("Copycat", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Copies opponent's last move.", -1);
        public static Move CosmicPower = new Move("Cosmic Power", MoveType.Psychic, Category.Status, -1, -1, 20, "-", "Increase user's Defense and Special Defense by one stage.", -1);
        public static Move CottonGuard = new Move("Cotton Guard", MoveType.Grass, Category.Status, -1, -1, 10, "-", "Raises the user's Defense by two stages.", -1);
        public static Move CottonSpore = new Move("Cotton Spore", MoveType.Grass, Category.Status, -1, 85, 40, "-", "Opponent's Speed decreases by two stages.", -1);
        public static Move Counter = new Move("Counter", MoveType.Fighting, Category.Physical, -1, 100, 20, "-", "When hit by a Physical Attack, user strikes back with 2x power.", -1);
        public static Move Covet = new Move("Covet", MoveType.Normal, Category.Physical, 40, 100, 40, "-", "Opponent's item is stolen by the user if user isn't holding one.", -1);
        public static Move Crabhammer = new Move("Crabhammer", MoveType.Water, Category.Physical, 90, 85, 10, "-", "High Critical-Hit ratio.", -1);
        public static Move CrossChop = new Move("Cross Chop", MoveType.Fighting, Category.Physical, 100, 80, 5, "-", "High Critical-Hit ratio.", -1);
        public static Move CrossFlame = new Move("Cross Flame", MoveType.Fire, Category.Special, 100, 100, 5, "-", "Power increases if Cross Thunder is used in the same turn.", -1);
        public static Move CrossPoison = new Move("Cross Poison", MoveType.Poison, Category.Physical, 70, 100, 20, "-", "Likely to land a Critical Hit, may Poison opponent.", 10);
        public static Move CrossThunder = new Move("Cross Thunder", MoveType.Electric, Category.Physical, 100, 100, 5, "-", "Power increases if Cross Flame is used in the same turn.", -1);
        public static Move Crunch = new Move("Crunch", MoveType.Dark, Category.Physical, 80, 100, 15, "-", "May decrease opponent's Defense by one stage.", 20);
        public static Move CrushClaw = new Move("Crush Claw", MoveType.Normal, Category.Physical, 75, 95, 10, "-", "May decrease opponent's Defense by one stage.", 50);
        public static Move CrushGrip = new Move("Crush Grip", MoveType.Normal, Category.Physical, -1, 100, 5, "-", "Attack power depends on opponent's remaining HP.", -1);
        public static Move Curse = new Move("Curse", MoveType.Ghost, Category.Status, -1, -1, 10, "-", "Lose 50% of max HP, opponent loses 25% of max HP at the end of each turn. Non-Ghosts -1 Raises user's Attack and Defense one stage each, but lowers Speed by one stage.", -1);
        public static Move Cut = new Move("Cut", MoveType.Normal, Category.Physical, 50, 95, 30, "HM01", "", -1);
        public static Move DarkPulse = new Move("Dark Pulse", MoveType.Dark, Category.Special, 80, 100, 15, "TM79", "May cause flinching.", 20);
        public static Move DarkVoid = new Move("Dark Void", MoveType.Dark, Category.Status, -1, 80, 10, "-", "Puts all Pokemon on the field to sleep.", -1);
        public static Move DefendOrder = new Move("Defend Order", MoveType.Bug, Category.Status, -1, -1, 10, "-", "Increases user's Defense and Special Defense by one stage each.", -1);
        public static Move DefenseCurl = new Move("Defense Curl", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Increases user's Defense by one stage. Rollout is now stronger.", -1);
        public static Move Defog = new Move("Defog", MoveType.Flying, Category.Status, -1, -1, 15, "HM05", "Lowers opponent's Evasiveness by one stage.", -1);
        public static Move DestinyBond = new Move("Destiny Bond", MoveType.Ghost, Category.Status, -1, -1, 5, "-", "If the user faints, the opponent also faints.", -1);
        public static Move Detect = new Move("Detect", MoveType.Fighting, Category.Status, -1, -1, 5, "-", "Opponent's attack doesn't affect you, but may fail if used often.", -1);
        public static Move Dig = new Move("Dig", MoveType.Ground, Category.Physical, 80, 100, 10, "TM28", "Digs down first turn, attacks in the 2nd. Acts like an Escape Rope on the field.", -1);
        public static Move Disable = new Move("Disable", MoveType.Normal, Category.Status, -1, 80, 20, "-", "Opponent can't use its last attack for a few turns.", -1);
        public static Move Discharge = new Move("Discharge", MoveType.Electric, Category.Special, 80, 100, 15, "-", "May cause paralysis.", 30);
        public static Move Dive = new Move("Dive", MoveType.Water, Category.Physical, 80, 100, 10, "-", "Dives down for the first turn, attacks on the second turn.", -1);
        public static Move DizzyPunch = new Move("Dizzy Punch", MoveType.Normal, Category.Physical, 70, 100, 10, "-", "May cause confusion.", 20);
        public static Move Doom = new Move("Doom Desire", MoveType.Steel, Category.Special, 120, 85, 5, "-", "Damage occurs 2 turns later.", -1);
        public static Move DoubleChop = new Move("Double Chop", MoveType.Dragon, Category.Physical, 40, 90, 15, "-", "Hits twice in one turn.", -1);
        public static Move DoubleHit = new Move("Double Hit", MoveType.Normal, Category.Physical, 35, 90, 10, "-", "Hits twice.", -1);
        public static Move DoubleKick = new Move("Double Kick", MoveType.Fighting, Category.Physical, 30, 100, 30, "-", "Hits twice.", -1);
        public static Move DoubleTeam = new Move("Double Team", MoveType.Normal, Category.Status, -1, -1, 15, "TM32", "Increases user's Evasiveness by one stage.", -1);
        public static Move DoubleEdge = new Move("Double-Edge", MoveType.Normal, Category.Physical, 120, 100, 15, "-", "User receives recoil damage equal to 1/8th the damage inflicted.", -1);
        public static Move DoubleSlap = new Move("Double Slap", MoveType.Normal, Category.Physical, 15, 85, 10, "-", "Hits 2-5 times.", -1);
        public static Move DracoMeteor = new Move("Draco Meteor", MoveType.Dragon, Category.Special, 140, 90, 5, "-", "Decreases user's Special Attack by 2 stages.", -1);
        public static Move DragonClaw = new Move("Dragon Claw", MoveType.Dragon, Category.Physical, 80, 100, 15, "TM02", "", -1);
        public static Move DragonDance = new Move("Dragon Dance", MoveType.Dragon, Category.Status, -1, -1, 20, "-", "Increases user's Attack and Speed by one stage each.", -1);
        public static Move DragonPulse = new Move("Dragon Pulse", MoveType.Dragon, Category.Special, 90, 100, 10, "TM59", "", -1);
        public static Move DragonRage = new Move("Dragon Rage", MoveType.Dragon, Category.Special, -1, 100, 10, "-", "Always inflicts 40 HP.", -1);
        public static Move DragonRush = new Move("Dragon Rush", MoveType.Dragon, Category.Physical, 100, 75, 10, "-", "May cause flinching.", 20);
        public static Move DragonTail = new Move("Dragon Tail", MoveType.Dragon, Category.Physical, 60, 90, 10, "-", "In battles, the opponent switches. In the wild, the Pokemon runs.", -1);
        public static Move Dragonbreath = new Move("Dragonbreath", MoveType.Dragon, Category.Special, 60, 100, 20, "-", "May cause paralysis.", -1);
        public static Move DrainPunch = new Move("Drain Punch", MoveType.Fighting, Category.Physical, 60, 100, 5, "TM60", "User recovers half of the damage inflicted on opponent.", -1);
        public static Move DreamEater = new Move("Dream Eater", MoveType.Psychic, Category.Special, 100, 100, 15, "TM85", "Can only be used on a sleeping target. User recovers half of the damage inflicted on opponent.", -1);
        public static Move DrillLiner = new Move("Drill Liner", MoveType.Ground, Category.Physical, 80, 95, 10, "-", "High Critical-Hit ratio.", -1);
        public static Move DrillPeck = new Move("Drill Peck", MoveType.Flying, Category.Physical, 80, 100, 20, "-", "", -1);
        public static Move Dynamicpunch = new Move("Dynamicpunch", MoveType.Fighting, Category.Physical, 100, 50, 5, "-", "Causes confusion, if it hits.", 100);
        public static Move EarthPower = new Move("Earth Power", MoveType.Ground, Category.Special, 90, 100, 10, "-", "May decrease opponent's Category.Special Defense by on stage.", 10);
        public static Move Earthquake = new Move("Earthquake", MoveType.Ground, Category.Physical, 100, 100, 10, "TM26", "Power is doubled if opponent is underground from using Dig.", -1);
        public static Move EchoVoice = new Move("Echo Voice", MoveType.Normal, Category.Special, 40, 100, 15, "-", "Power increases each turn if multiple Pokemon in the team also use it.", -1);
        public static Move EggBomb = new Move("Egg Bomb", MoveType.Normal, Category.Physical, 100, 75, 10, "-", "", -1);
        public static Move ElectricBall = new Move("Electric Ball", MoveType.Electric, Category.Special, -1, 100, 10, "-", "The faster the user, the stronger the attack.", -1);
        public static Move ElectricNet = new Move("Electric Net", MoveType.Electric, Category.Special, 55, 95, 15, "-", "Lowers the target's Speed by one level.", 100);
        public static Move Embargo = new Move("Embargo", MoveType.Dark, Category.Status, -1, 100, 15, "TM63", "Opponent cannot use hold items.", -1);
        public static Move Ember = new Move("Ember", MoveType.Fire, Category.Special, 40, 100, 25, "-", "May cause a burn.", 10);
        public static Move Encore = new Move("Encore", MoveType.Normal, Category.Status, -1, 100, 5, "-", "Forces opponent to keep using its last more for 2-6 turns.", -1);
        public static Move Endeavor = new Move("Endeavor", MoveType.Normal, Category.Physical, -1, 100, 5, "-", "Reduces opponent's HP to same as user's.", -1);
        public static Move Endure = new Move("Endure", MoveType.Normal, Category.Status, -1, -1, 10, "TM58", "Always left with at least 1 HP, but may fail if used in a row.", -1);
        public static Move EnergyBall = new Move("Energy Ball", MoveType.Grass, Category.Special, 80, 100, 10, "TM53", "May decrease opponent's Special Defense by one stage.", -1);
        public static Move Eruption = new Move("Eruption", MoveType.Fire, Category.Special, 150, 100, 5, "-", "Stronger when the user's HP is higher.", -1);
        public static Move EvilEye = new Move("Evil Eye", MoveType.Ghost, Category.Special, 50, 100, 10, "-", "Inflicts more damage if the target has a Status condition.", -1);
        public static Move Explosion = new Move("Explosion", MoveType.Normal, Category.Status, 250, 100, 5, "TM64", "User faints.", -1);
        public static Move ExtraSensory = new Move("Extra Sensory", MoveType.Psychic, Category.Special, 80, 100, 30, "-", "May cause flinching.", 10);
        public static Move ExtremeSpeed = new Move("Extreme Speed", MoveType.Normal, Category.Physical, 80, 100, 5, "-", "User attacks first.", -1);
        public static Move Facade = new Move("Facade", MoveType.Normal, Category.Physical, 70, 100, 20, "TM42", "Stronger if user is Burned, Poisoned, or Paralyzed.", -1);
        public static Move FaintAttack = new Move("Faint Attack", MoveType.Dark, Category.Physical, 60, 8, 20, "-", "Cannot miss, regardless of Accuracy or Evasiveness.", -1);
        public static Move FakeOut = new Move("Fake Out", MoveType.Normal, Category.Physical, 40, 100, 10, "-", "Can only be used in first turn. User goes first, foe flinches.", 100);
        public static Move FakeTears = new Move("Fake Tears", MoveType.Dark, Category.Status, -1, 100, 20, "-", "Decreases opponent's Special Defense by two stages.", -1);
        public static Move FalseSwipe = new Move("False Swipe", MoveType.Normal, Category.Physical, 40, 100, 40, "TM54", "Always leaves opponent with at least 1 HP.", -1);
        public static Move FastGuard = new Move("Fast Guard", MoveType.Fighting, Category.Status, -1, -1, 15, "-", "Fast moves won't damage the user or its teammates.", -1);
        public static Move Featherdance = new Move("Featherdance", MoveType.Flying, Category.Status, -1, 100, 15, "-", "Decreases opponent't Attack by two stages.", -1);
        public static Move Feint = new Move("Feint", MoveType.Normal, Category.Physical, 50, 100, 10, "-", "Only hits if opponent uses Protect or Detect in the same turn.", -1);
        public static Move FireBlast = new Move("Fire Blast", MoveType.Fire, Category.Special, 120, 85, 5, "TM38", "May cause a burn.", 10);
        public static Move FireDance = new Move("Fire Dance", MoveType.Fire, Category.Special, 80, 100, 10, "-", "May raise the user's Special Attack by one level.", 50);
        public static Move FireFang = new Move("Fire Fang", MoveType.Fire, Category.Physical, 65, 95, 15, "-", "May cause a burn or flinching.", 10);
        public static Move FireOath = new Move("Fire Oath", MoveType.Fire, Category.Special, 50, 100, 10, "-", "Added effects appear if preceded by Grass Oath or succeeded by Water Oath.", -1);
        public static Move FirePunch = new Move("Fire Punch", MoveType.Fire, Category.Physical, 75, 100, 15, "-", "May cause a burn.", 10);
        public static Move FireSpin = new Move("Fire Spin", MoveType.Fire, Category.Special, 15, 70, 15, "-", "Damages opponent each turn, opponent cannot escape/switch.", -1);
        public static Move Fissure = new Move("Fissure", MoveType.Ground, Category.Physical, -1, 30, 5, "-", "An instant 1-hit KO, if it hits.", -1);
        public static Move Flail = new Move("Flail", MoveType.Normal, Category.Physical, -1, 100, 15, "-", "The lower the user's HP, the higher the power.", -1);
        public static Move FlameBurst = new Move("Flame Burst", MoveType.Fire, Category.Special, 70, 100, 15, "-", "May also injure nearby Pokemon.", -1);
        public static Move FlameWheel = new Move("Flame Wheel", MoveType.Fire, Category.Physical, 60, 100, 25, "-", "May cause a burn.", 10);
        public static Move Flamethrower = new Move("Flamethrower", MoveType.Fire, Category.Special, 95, 100, 15, "TM35", "May cause a burn.", 10);
        public static Move FlareBlitz = new Move("Flare Blitz", MoveType.Fire, Category.Physical, 120, 100, 15, "-", "User receives recoil damage.", -1);
        public static Move Flash = new Move("Flash", MoveType.Normal, Category.Status, -1, 100, 20, "TM70", "Decreases opponent's Accuracy by one stage.", -1);
        public static Move FlashCannon = new Move("Flash Cannon", MoveType.Steel, Category.Special, 80, 100, 10, "TM91", "May decrease opponent's Sp. Attack or Sp. Defense by one stage.", 10);
        public static Move Flatter = new Move("Flatter", MoveType.Dark, Category.Status, -1, 100, 15, "-", "Confuses opponent, but raises its pecial Attack by two stages.", -1);
        public static Move Fling = new Move("Fling", MoveType.Dark, Category.Physical, -1, 100, 10, "TM56", "Power depends on held item.", -1);
        public static Move Fly = new Move("Fly", MoveType.Flying, Category.Physical, 90, 95, 15, "HM02", "Flies up on the first turn, attacks on the second turn.", -1);
        public static Move FocusBlast = new Move("Focus Blast", MoveType.Fighting, Category.Special, 120, 70, 5, "TM52", "May decrease opponent's Special Defense by one stage.", 10);
        public static Move FocusEnergy = new Move("Focus Energy", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Increases Critical-Hit ratio by one stage.", -1);
        public static Move FocusPunch = new Move("Focus Punch", MoveType.Fighting, Category.Physical, 150, 100, 20, "TM01", "Pokemon waits on the first turn, attacks on the second. If the Pokemon is hit between this, the Pokemon flinches instead of attacking.", -1);
        public static Move FollowMe = new Move("Follow Me", MoveType.Normal, Category.Status, -1, -1, 20, "-", "In Double Battle, the user takes all the attacks.", -1);
        public static Move ForcePalm = new Move("Force Palm", MoveType.Fighting, Category.Physical, 60, 100, 10, "-", "May cause paralysis.", 30);
        public static Move Foresight = new Move("Foresight", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Resets opponent's Evasiveness, Normal-type and Fighting-type attacks can now hit Ghosts, and Ghost-type attacks hit Normal.", -1);
        public static Move Freefall = new Move("Freefall", MoveType.Flying, Category.Physical, 60, 100, 10, "-", "Tosses the target into the air on first turn; Drops them on the second.", -1);
        public static Move FreezeBolt = new Move("Freeze Bolt", MoveType.Ice, Category.Physical, 140, 90, 5, "-", "Charges for a turn before attacking. May paralyze the target.", 30);
        public static Move FrenzyPlant = new Move("Frenzy Plant", MoveType.Grass, Category.Special, 150, 90, 5, "-", "User cannot public static Move in the following turn.", -1);
        public static Move FrozenWorld = new Move("Frozen World", MoveType.Ice, Category.Special, 65, 95, 10, "-", "Lowers the target's Speed.", 100);
        public static Move Frustration = new Move("Frustration", MoveType.Normal, Category.Physical, -1, 100, 20, "TM21", "Power raises with the more the user dislikes its trainer.", -1);
        public static Move FuryAttack = new Move("Fury Attack", MoveType.Normal, Category.Physical, 15, 85, 20, "-", "Hits 2-5 times.", -1);
        public static Move FuryCutter = new Move("Fury Cutter", MoveType.Bug, Category.Physical, 10, 95, 20, "-", "Power increases with each consecutive hit.", -1);
        public static Move FurySwipes = new Move("Fury Swipes", MoveType.Normal, Category.Physical, 18, 80, 15, "-", "Hits 2-5 times.", -1);
        public static Move FutureSight = new Move("Future Sight", MoveType.Psychic, Category.Special, 80, 90, 15, "-", "Damage occurs 2 turs later.", -1);
        public static Move GastroAcid = new Move("Gastro Acid", MoveType.Poison, Category.Status, -1, 100, 10, "-", "Cancels out the effect of the opponent's Ability.", -1);
        public static Move GearChange = new Move("Gear Change", MoveType.Steel, Category.Status, -1, -1, 10, "-", "Raises the user's Attack and Speed by one stage.", -1);
        public static Move GearSaucer = new Move("Gear Saucer", MoveType.Steel, Category.Physical, 50, 85, 15, "-", "Hits twice in one turn.", -1);
        public static Move GiftPass = new Move("Gift Pass", MoveType.Normal, Category.Status, -1, -1, 15, "-", "Gives the user's held item to the target.", -1);
        public static Move GigaDrain = new Move("Giga Drain", MoveType.Grass, Category.Special, 60, 100, 10, "TM19", "User recovers 50% of the damage inflicted on the opponent.", -1);
        public static Move GigaImpact = new Move("Giga Impact", MoveType.Normal, Category.Physical, 150, 90, 5, "TM68", "User cannot public static Move in the following turn.", -1);
        public static Move Glare = new Move("Glare", MoveType.Normal, Category.Status, -1, 75, 30, "-", "Causes paralysis, if it hits.", -1);
        public static Move GrassKnot = new Move("Grass Knot", MoveType.Grass, Category.Special, -1, 100, 20, "TM86", "The heavier the opponent, the stronger the attack.", -1);
        public static Move GrassMixer = new Move("Grass Mixer", MoveType.Grass, Category.Special, 65, 90, 10, "-", "May lower the target's Accuracy by one level.", 50);
        public static Move GrassOath = new Move("Grass Oath", MoveType.Grass, Category.Special, 50, 100, 10, "-", "Added effects appear if preceded by Water Oath or succeeded by Fire Oath.", -1);
        public static Move GrassWhistle = new Move("Grass Whistle", MoveType.Grass, Category.Status, -1, 55, 15, "-", "Puts opponent to sleep, if it hits.", -1);
        public static Move Gravity = new Move("Gravity", MoveType.Psychic, Category.Status, -1, -1, 5, "-", "Prevents moves like Fly and Bounce and the Ability Levitate for 5 turns.", -1);
        public static Move Growl = new Move("Growl", MoveType.Normal, Category.Status, -1, 100, 40, "-", "Decreases opponent's Attack by one stage.", -1);
        public static Move Growth = new Move("Growth", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Increases user's Special Attack by one stage.", -1);
        public static Move Grudge = new Move("Grudge", MoveType.Ghost, Category.Status, -1, -1, 5, "-", "If the users faints after using this move, the PP for the opponent's last move is depleted.", -1);
        public static Move GuardShare = new Move("Guard Share", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Averages Defense and Special Defense with the target.", -1);
        public static Move GuardSwap = new Move("Guard Swap", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "User and opponent swap Defense and Special Defense.", -1);
        public static Move Guillotine = new Move("Guillotine", MoveType.Normal, Category.Physical, -1, 30, 5, "-", "An instant 1-hit KO, if it hits.", -1);
        public static Move GunkShot = new Move("Gunk Shot", MoveType.Poison, Category.Physical, 120, 70, 5, "-", "May cause Poison.", -1);
        public static Move Gust = new Move("Gust", MoveType.Flying, Category.Special, 40, 100, 35, "-", "", -1);
        public static Move GyroBall = new Move("Gyro Ball", MoveType.Steel, Category.Physical, -1, 100, 5, "TM74", "The slower the opponent, the stronger the attack.", -1);
        public static Move HailIce = new Move("Hail", MoveType.Ice, Category.Status, -1, -1, 10, "TM07", "All Pokemon except Ice-types are damaged for 5 turns.", -1);
        public static Move HammerArm = new Move("Hammer Arm", MoveType.Fighting, Category.Physical, 100, 90, 10, "-", "Decreases the user's Speed by one stage.", -1);
        public static Move HardRoller = new Move("Hard Roller", MoveType.Bug, Category.Physical, 65, 100, 20, "-", "May make the target flinch.", 30);
        public static Move Harden = new Move("Harden", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Increases the user's Defense by one stage.", -1);
        public static Move Haze = new Move("Haze", MoveType.Ice, Category.Status, -1, -1, 30, "-", "Resets are stat changes.", -1);
        public static Move HeadSmash = new Move("Head Smash", MoveType.Rock, Category.Physical, 150, 80, 5, "-", "User receives recoil damage.", -1);
        public static Move Headbutt = new Move("Headbutt", MoveType.Normal, Category.Physical, 70, 100, 15, "-", "May cause flinching.", 30);
        public static Move HealBell = new Move("Heal Bell", MoveType.Normal, Category.Status, -1, -1, 5, "-", "Heals the user's party's Status conditions.", -1);
        public static Move HealBlock = new Move("Heal Block", MoveType.Psychic, Category.Status, -1, -1, 15, "-", "Prevents the opponent from restoring HP for 5 turns.", -1);
        public static Move HealOrder = new Move("Heal Order", MoveType.Bug, Category.Status, -1, -1, 10, "-", "Recovers half of the user's max HP.", -1);
        public static Move HealingBeam = new Move("Healing Beam", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Restores half the target's max HP.", -1);
        public static Move HealingWish = new Move("Healing Wish", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "The user faints and the next Pokemon released is fully healed.", -1);
        public static Move HeartStamp = new Move("Heart Stamp", MoveType.Psychic, Category.Physical, 60, 100, 25, "-", "May make the target flinch.", 30);
        public static Move HeartSwap = new Move("Heart Swap", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Stat changes are swapped with the opponent.", -1);
        public static Move HeatStamp = new Move("Heat Stamp", MoveType.Fire, Category.Physical, -1, 100, 10, "-", "The heavier the user, the stronger the attack.", -1);
        public static Move HeatWave = new Move("Heat Wave", MoveType.Fire, Category.Special, 100, 90, 10, "-", "May cause a burn.", 10);
        public static Move HeavyBomber = new Move("Heavy Bomber", MoveType.Steel, Category.Physical, -1, 100, 10, "-", "The heavier the user, the stronger the attack.", -1);
        public static Move HelpingHand = new Move("Helping Hand", MoveType.Normal, Category.Status, -1, -1, 20, "-", "In Double Battles, boosts the power of the partner's move.", -1);
        public static Move HiJumpKick = new Move("Hi Jump Kick", MoveType.Fighting, Category.Physical, 100, 90, 20, "-", "If this attack misses, the user takes 1/8 of the would-be damage.", -1);
        public static Move HiddenPower = new Move("Hidden Power", MoveType.Normal, Category.Special, -1, 100, 15, "TM10", "Type and power depends on user's IV's.", -1);
        public static Move HornAttack = new Move("Horn Attack", MoveType.Normal, Category.Physical, 65, 100, 25, "-", "", -1);
        public static Move HornDrill = new Move("Horn Drill", MoveType.Normal, Category.Physical, -1, 30, 5, "-", "An instant 1-hit KO, if it hits.", -1);
        public static Move Howl = new Move("Howl", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Increases the user's Attack by one stage.", -1);
        public static Move Hurricane = new Move("Hurricane", MoveType.Flying, Category.Special, 120, 70, 10, "-", "May confuse the target.", 30);
        public static Move HydroCannon = new Move("Hydro Cannon", MoveType.Water, Category.Special, 150, 90, 5, "-", "User cannot public static Move in the following turn.", -1);
        public static Move HydroPump = new Move("Hydro Pump", MoveType.Water, Category.Special, 120, 80, 5, "-", "", -1);
        public static Move HyperBeam = new Move("Hyper Beam", MoveType.Normal, Category.Special, 150, 90, 5, "TM15", "User cannot Move on the following turn.", -1);
        public static Move HyperFang = new Move("Hyper Fang", MoveType.Normal, Category.Physical, 80, 90, 15, "-", "May cause flinching.", 10);
        public static Move HyperVoice = new Move("Hyper Voice", MoveType.Normal, Category.Special, 90, 100, 10, "-", "", -1);
        public static Move Hypnosis = new Move("Hypnosis", MoveType.Psychic, Category.Status, -1, 60, 20, "-", "Put opponent to sleep, if it hits.", -1);
        public static Move IceBall = new Move("Ice Ball", MoveType.Ice, Category.Physical, 30, 90, 20, "-", "An attack lasting for 5 turns that grows in power.", -1);
        public static Move IceBeam = new Move("Ice Beam", MoveType.Ice, Category.Special, 95, 100, 10, "TM13", "May cause freezing.", 10);
        public static Move IceBreath = new Move("Ice Breath", MoveType.Ice, Category.Special, 40, 90, 10, "-", "Always results in a critical hit.", 100);
        public static Move IceFang = new Move("Ice Fang", MoveType.Ice, Category.Physical, 65, 95, 15, "-", "May cause freezing and flinching.", 10);
        public static Move IcePunch = new Move("Ice Punch", MoveType.Ice, Category.Physical, 75, 100, 15, "-", "May cause freezing.", 10);
        public static Move IceShard = new Move("Ice Shard", MoveType.Ice, Category.Physical, 40, 100, 30, "-", "User attacks first.", -1);
        public static Move IcicleDrop = new Move("Icicle Drop", MoveType.Ice, Category.Physical, 85, 90, 10, "-", "May make the target flinch.", 30);
        public static Move IcicleSpear = new Move("Icicle Spear", MoveType.Ice, Category.Physical, 10, 100, 30, "-", "Hits 2-5 times.", -1);
        public static Move IcyWind = new Move("Icy Wind", MoveType.Ice, Category.Special, 55, 95, 15, "-", "Decreases opponent's Speed by one stage.", 100);
        public static Move Imprison = new Move("Imprison", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Opponent is unable to use moves that the user also knows.", -1);
        public static Move Incinerate = new Move("Incinerate", MoveType.Fire, Category.Special, 30, 100, 15, "-", "Destroys the target's held berry.", -1);
        public static Move Ingrain = new Move("Ingrain", MoveType.Grass, Category.Status, -1, -1, 20, "-", "User restores HP each turn. User cannot escape/switch.", -1);
        public static Move IronDefense = new Move("Iron Defense", MoveType.Steel, Category.Status, -1, -1, 15, "-", "Increases the user's Defense by two stages.", -1);
        public static Move IronHead = new Move("Iron Head", MoveType.Steel, Category.Physical, 80, 100, 15, "-", "May cause flinching.", 30);
        public static Move IronTail = new Move("Iron Tail", MoveType.Steel, Category.Physical, 100, 75, 15, "TM23", "May decrease opponent's Defense by one stage.", 30);
        public static Move Judgment = new Move("Judgment", MoveType.Normal, Category.Special, 100, 100, 10, "-", "Type depends on the Arceus Plate being held.", -1);
        public static Move JumpKick = new Move("Jump Kick", MoveType.Fighting, Category.Physical, 85, 95, 25, "-", "If this attack misses, the user takes 1/8 of the would-be damage.", -1);
        public static Move KarateChop = new Move("Karate Chop", MoveType.Fighting, Category.Physical, 50, 100, 25, "-", "High Critical-Hit ratio.", -1);
        public static Move Kinesis = new Move("Kinesis", MoveType.Psychic, Category.Status, -1, 80, 15, "-", "Decreases opponent's Accuracy by one stage, if it hits.", -1);
        public static Move KnockDown = new Move("Knock Down", MoveType.Rock, Category.Physical, 50, 100, 15, "-", "Makes Flying-type pokemon vulnerable to Ground moves.", 100);
        public static Move KnockOff = new Move("Knock Off", MoveType.Dark, Category.Physical, 20, 100, 20, "-", "Opponent cannot use its hold item for the duration of the battle.", -1);
        public static Move LastResort = new Move("Last Resort", MoveType.Normal, Category.Physical, 130, 100, 5, "-", "Can only be used after all over moves are used.", -1);
        public static Move LavaPlume = new Move("Lava Plume", MoveType.Fire, Category.Special, 80, 100, 15, "-", "May cause a burn.", 30);
        public static Move LeafBlade = new Move("Leaf Blade", MoveType.Grass, Category.Physical, 90, 100, 15, "-", "High Critcal-Hit ratio.", -1);
        public static Move LeafStorm = new Move("Leaf Storm", MoveType.Grass, Category.Special, 140, 90, 5, "-", "Decreases user's Special Attack by two stages.", -1);
        public static Move LeechLife = new Move("Leech Life", MoveType.Bug, Category.Physical, 20, 100, 15, "-", "User recovers half of the damage inflicted on the opponent.", -1);
        public static Move LeechSeed = new Move("Leech Seed", MoveType.Grass, Category.Status, -1, 90, 10, "-", "User steals HP from opponent each turn.", -1);
        public static Move Leer = new Move("Leer", MoveType.Normal, Category.Status, -1, 100, 30, "-", "Decreases opponent's Defense by one stage.", -1);
        public static Move LevelField = new Move("Level Field", MoveType.Ground, Category.Physical, 60, 100, 20, "-", "May lower the target's Speed by one level.", 100);
        public static Move Lick = new Move("Lick", MoveType.Ghost, Category.Physical, 20, 100, 30, "-", "May cause paralysis.", 30);
        public static Move LifeGamble = new Move("Life Gamble", MoveType.Fighting, Category.Special, -1, 100, 5, "-", "Inflicts damage equal to the user's remaining HP. User faints.", -1);
        public static Move LightScreen = new Move("Light Screen", MoveType.Psychic, Category.Status, -1, -1, 30, "TM16", "User's party receives 1/2 damage from Sp. Attacks for 5 turns.", -1);
        public static Move LightningStrike = new Move("Lightning Strike", MoveType.Electric, Category.Physical, 130, 85, 5, "-", "May paralyze the target.", 20);
        public static Move LittleByLittle = new Move("Little By Little", MoveType.Normal, Category.Physical, 70, 100, 20, "-", "Ignores the target's stat changes.", -1);
        public static Move Lockon = new Move("Lock-on", MoveType.Normal, Category.Status, -1, -1, 5, "-", "The next Move the user uses is guaranteed to hit.", -1);
        public static Move LovelyKiss = new Move("Lovely Kiss", MoveType.Normal, Category.Status, -1, 75, 10, "-", "Puts the opponent to sleep, if it hits.", -1);
        public static Move LowKick = new Move("Low Kick", MoveType.Fighting, Category.Status, -1, 100, 20, "-", "The heavier the opponent, the stronger the attack.", -1);
        public static Move LuckyChant = new Move("Lucky Chant", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Opponent cannot land Critical Hits for 5 turns.", -1);
        public static Move LunarDance = new Move("Lunar Dance", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "The user faints but the next Pokemon released is fully healed.", -1);
        public static Move LusterPurge = new Move("Luster Purge", MoveType.Psychic, Category.Special, 70, 100, 5, "-", "May decrease opponent's Special Defense by one stage.", -1);
        public static Move MachPunch = new Move("Mach Punch", MoveType.Fighting, Category.Physical, 40, 100, 30, "-", "User goes first.", -1);
        public static Move MagicCoat = new Move("Magic Coat", MoveType.Psychic, Category.Status, -1, -1, 15, "-", "Any Special public static Move is reflected back to the attacker.", -1);
        public static Move MagicRoom = new Move("Magic Room", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Suppresses the effects of held items for five turns.", -1);
        public static Move MagicalLeaf = new Move("Magical Leaf", MoveType.Grass, Category.Special, 60, 8, 20, "-", "Cannot miss, regardless of Accuracy or Evasiveness.", -1);
        public static Move MagmaStorm = new Move("Magma Storm", MoveType.Fire, Category.Special, 120, 70, 5, "-", "Opponent cannot escape/switch.", -1);
        public static Move MagnetBomb = new Move("Magnet Bomb", MoveType.Steel, Category.Physical, 60, 8, 20, "-", "Cannot miss, regardless of Accuracy or Evasiveness.", -1);
        public static Move MagnetRise = new Move("Magnet Rise", MoveType.Electric, Category.Status, -1, -1, 10, "-", "User becomes immune to Ground-type moves for 5 turns.", -1);
        public static Move Magnitude = new Move("Magnitude", MoveType.Ground, Category.Physical, -1, 100, 30, "-", "Hits with random power.", -1);
        public static Move MeFirst = new Move("Me First", MoveType.Normal, Category.Status, -1, -1, 20, "-", "User goes first with the opponent's attack at 1.5x power.", -1);
        public static Move MeanLook = new Move("Mean Look", MoveType.Normal, Category.Status, -1, -1, 5, "-", "Opponent cannot escape/switch.", -1);
        public static Move Meditate = new Move("Meditate", MoveType.Psychic, Category.Status, -1, -1, 40, "-", "Increases user's Attack by one stage.", -1);
        public static Move MegaDrain = new Move("Mega Drain", MoveType.Grass, Category.Special, 40, 100, 15, "-", "User recovers 1/2 of the damage inflicted onto the opponent.", -1);
        public static Move MegaKick = new Move("Mega Kick", MoveType.Normal, Category.Physical, 120, 75, 5, "-", "", -1);
        public static Move MegaPunch = new Move("Mega Punch", MoveType.Normal, Category.Physical, 80, 85, 20, "-", "", -1);
        public static Move Megahorn = new Move("Megahorn", MoveType.Bug, Category.Physical, 120, 85, 10, "-", "", -1);
        public static Move Memento = new Move("Memento", MoveType.Dark, Category.Status, -1, 100, 10, "-", "User faints, opponent's Attack is dropped to the lowest stage.", -1);
        public static Move MetalBurst = new Move("Metal Burst", MoveType.Steel, Category.Physical, -1, 100, 10, "-", "Deals damage equal to 1.5x opponent's attack.", -1);
        public static Move MetalClaw = new Move("Metal Claw", MoveType.Steel, Category.Physical, 50, 95, 35, "-", "May increase the user's Attack by one stage.", 10);
        public static Move MetalSound = new Move("Metal Sound", MoveType.Steel, Category.Status, -1, 85, 40, "-", "Decreases opponent's Special Defense by two stages.", -1);
        public static Move MeteorMash = new Move("Meteor Mash", MoveType.Steel, Category.Physical, 100, 85, 10, "-", "May increase user's Attack by one stage.", 20);
        public static Move Metronome = new Move("Metronome", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User performs any Move in the game at random.", -1);
        public static Move MilkDrink = new Move("Milk Drink", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User recoves 50% of its maximum HP.", -1);
        public static Move Mimic = new Move("Mimic", MoveType.Normal, Category.Status, -1, -1, 10, "-", "Replaces this public static Move with the opponent's last move, for the battle.", -1);
        public static Move MindReader = new Move("Mind Reader", MoveType.Psychic, Category.Status, -1, -1, 5, "-", "User's next attack is guaranteed to hit.", -1);
        public static Move Minimize = new Move("Minimize", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Increases user's Evasiveness by one stage.", -1);
        public static Move MiracleEye = new Move("Miracle Eye", MoveType.Psychic, Category.Status, -1, -1, 40, "-", "Resets opponent's Evasiveness, removes Dark's Psychic immunity.", -1);
        public static Move MirrorCoat = new Move("Mirror Coat", MoveType.Psychic, Category.Special, -1, 100, 20, "-", "When hit by a Special Attack, user strikes back with 2x power.", -1);
        public static Move MirrorMove = new Move("Mirror Move", MoveType.Flying, Category.Status, -1, -1, 20, "-", "User performs the opponent's last move.", -1);
        public static Move MirrorShot = new Move("Mirror Shot", MoveType.Steel, Category.Special, 65, 85, 10, "-", "May decrease opponent's Accuracy by one stage.", -1);
        public static Move MirrorType = new Move("Mirror Type", MoveType.Normal, Category.Status, -1, -1, 15, "-", "User becomes the target's type.", -1);
        public static Move Mist = new Move("Mist", MoveType.Ice, Category.Status, -1, -1, 30, "-", "User's stats cannot be changed for a period of time.", -1);
        public static Move MistBall = new Move("Mist Ball", MoveType.Psychic, Category.Special, 70, 100, 5, "-", "Likely to decrease opponent's Special Attack by one stage.", 50);
        public static Move Moonlight = new Move("Moonlight", MoveType.Normal, Category.Status, -1, -1, 5, "-", "User recovers HP. Amount varies with the weather.", -1);
        public static Move MorningSun = new Move("Morning Sun", MoveType.Normal, Category.Status, -1, -1, 5, "-", "User recovers HP. Amount varies with the weather.", -1);
        public static Move MountainStorm = new Move("Mountain Storm", MoveType.Fighting, Category.Physical, 40, 100, 10, "-", "Always results in a critical hit.", -1);
        public static Move MudBomb = new Move("Mud Bomb", MoveType.Ground, Category.Special, 65, 85, 10, "-", "May decrease opponent's Accuracy by one stage.", 30);
        public static Move MudShot = new Move("Mud Shot", MoveType.Ground, Category.Special, 55, 95, 15, "-", "Decreases opponent's Speed by one stage.", 100);
        public static Move MudSport = new Move("Mud Sport", MoveType.Ground, Category.Status, -1, -1, 15, "-", "Weakens the power of Electric-type moves.", -1);
        public static Move MudSlap = new Move("Mud-Slap", MoveType.Ground, Category.Special, 20, 100, 10, "-", "Lowers opponent's accuracy by one stage.", 100);
        public static Move MuddyWater = new Move("Muddy Water", MoveType.Water, Category.Special, 95, 85, 10, "-", "May decrease opponent's Accuracy by one stage.", 30);
        public static Move MysterySword = new Move("Mystery Sword", MoveType.Fighting, Category.Special, 85, 100, 10, "-", "Inflicts damage based on the target's Defense, not Category.Special Defense.", -1);
        public static Move NastyPlot = new Move("Nasty Plot", MoveType.Dark, Category.Status, -1, -1, 20, "-", "Increases the user's Special Attack by two stages.", -1);
        public static Move NaturalGift = new Move("Natural Gift", MoveType.Normal, Category.Physical, -1, 100, 15, "TM83", "The type and power depends on the user's held berry.", -1);
        public static Move NaturePower = new Move("Nature Power", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Uses a certain public static Move based on the current location.", -1);
        public static Move NeedleArm = new Move("Needle Arm", MoveType.Grass, Category.Physical, 60, 100, 15, "-", "May cause flinching.", 30);
        public static Move NightBurst = new Move("Night Burst", MoveType.Dark, Category.Special, 85, 95, 10, "-", "May lower the target's Accuracy by one level.", 40);
        public static Move NightShade = new Move("Night Shade", MoveType.Ghost, Category.Special, -1, 100, 15, "-", "Inflicts damage equal to user's level.", -1);
        public static Move NightSlash = new Move("Night Slash", MoveType.Dark, Category.Physical, 70, 100, 15, "-", "High Critical-Hit ratio.", -1);
        public static Move Nightmare = new Move("Nightmare", MoveType.Ghost, Category.Special, -1, 100, 15, "-", "The sleeping opponent loses 25% of its max HP each turn.", -1);
        public static Move NitroCharge = new Move("Nitro Charge", MoveType.Fire, Category.Physical, 50, 100, 20, "-", "Inflicts regular damage. Raises the user's Speed by one stage.", 100);
        public static Move Octazooka = new Move("Octazooka", MoveType.Water, Category.Special, 65, 85, 10, "-", "Likely to decrease opponent's Accuracy by one stage.", 50);
        public static Move OdorSleuth = new Move("Odor Sleuth", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Resets opponent's Evasiveness, Normal-type and Fighting-type attacks can now hit Ghosts, and Ghost-type attacks hit Normal.", -1);
        public static Move OminousWind = new Move("Ominous Wind", MoveType.Ghost, Category.Special, 60, 100, 5, "-", "May increase all stats of the user by one stage each.", 10);
        public static Move Outrage = new Move("Outrage", MoveType.Dragon, Category.Physical, 120, 100, 15, "-", "User attacks for 2-3 turns and then becomes confused.", -1);
        public static Move OverheadThrow = new Move("Overhead Throw", MoveType.Fighting, Category.Physical, 60, 90, 10, "-", "In battles, the opponent switches. In the wild, the Pokemon runs.", -1);
        public static Move Overheat = new Move("Overheat", MoveType.Fire, Category.Special, 140, 90, 5, "TM50", "Decreases user's Special Attack by two stages.", -1);
        public static Move PainSplit = new Move("Pain Split", MoveType.Normal, Category.Status, -1, -1, 20, "-", "The user's and opponent's HP becomes the average of both.", -1);
        public static Move PayDay = new Move("Pay Day", MoveType.Normal, Category.Physical, 40, 100, 20, "-", "A small amount of money is gained after the battle resolves.", -1);
        public static Move Payback = new Move("Payback", MoveType.Dark, Category.Physical, 50, 100, 10, "TM66", "Power doubles if the user was attacked first.", -1);
        public static Move Peck = new Move("Peck", MoveType.Flying, Category.Physical, 35, 100, 35, "-", "", -1);
        public static Move PerishSong = new Move("Perish Song", MoveType.Normal, Category.Status, -1, -1, 5, "-", "Any Pokemon in play when this attack is used faints in 3 turns.", -1);
        public static Move PetalDance = new Move("Petal Dance", MoveType.Grass, Category.Special, 90, 100, 20, "-", "User attacks for 2-3 turns and then becomes confused.", -1);
        public static Move PinMissile = new Move("Pin Missile", MoveType.Bug, Category.Physical, 14, 85, 20, "-", "Hits 2-5 times.", -1);
        public static Move Pluck = new Move("Pluck", MoveType.Flying, Category.Physical, 60, 100, 20, "TM88", "If the opponent is holding a berry, its effect is stolen by user.", -1);
        public static Move PoisonFang = new Move("Poison Fang", MoveType.Poison, Category.Physical, 50, 100, 15, "-", "May cause Poison that gets worse each turn.", 30);
        public static Move PoisonGas = new Move("Poison Gas", MoveType.Poison, Category.Status, -1, 55, 40, "-", "Poisons the opponent, if it hits.", -1);
        public static Move PoisonJab = new Move("Poison Jab", MoveType.Poison, Category.Physical, 80, 100, 20, "TM84", "May Poison the opponent.", 30);
        public static Move PoisonSting = new Move("Poison Sting", MoveType.Poison, Category.Physical, 15, 100, 35, "-", "May Poison the opponent.", 30);
        public static Move PoisonTail = new Move("Poison Tail", MoveType.Poison, Category.Physical, 50, 100, 25, "-", "High Critical-Hit ratio. May Poison the opponent.", 10);
        public static Move Poisonpowder = new Move("Poisonpowder", MoveType.Poison, Category.Status, -1, 75, 35, "-", "Poisons the opponent, if it hits.", -1);
        public static Move Postpone = new Move("Postpone", MoveType.Dark, Category.Status, -1, 100, 15, "-", "Makes the target act last this turn.", -1);
        public static Move Pound = new Move("Pound", MoveType.Normal, Category.Physical, 40, 100, 35, "-", "", -1);
        public static Move PowderRage = new Move("Powder Rage", MoveType.Bug, Category.Status, -1, -1, 20, "-", "Redirects the target's single-target effects to the user.", -1);
        public static Move PowderSnow = new Move("Powder Snow", MoveType.Ice, Category.Special, 40, 100, 25, "-", "May cause freezing.", 10);
        public static Move PowerGem = new Move("Power Gem", MoveType.Rock, Category.Special, 70, 100, 20, "-", "", -1);
        public static Move PowerShare = new Move("Power Share", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Averages Attack and Special Attack with the target.", -1);
        public static Move PowerSwap = new Move("Power Swap", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "User and opponent swap Attack and Special Attack.", -1);
        public static Move PowerTrick = new Move("Power Trick", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "User's own Attack and Defense switch.", -1);
        public static Move PowerWhip = new Move("Power Whip", MoveType.Grass, Category.Physical, 120, 85, 10, "-", "", -1);
        public static Move Present = new Move("Present", MoveType.Normal, Category.Physical, -1, 90, 15, "-", "Either deals damage or heals.", -1);
        public static Move Protect = new Move("Protect", MoveType.Normal, Category.Status, -1, -1, 10, "TM17", "User is not affected by opponent's move. May fail if used often.", -1);
        public static Move Psybeam = new Move("Psybeam", MoveType.Psychic, Category.Special, 65, 100, 20, "-", "May confuse the opponent.", 10);
        public static Move PsychUp = new Move("Psych Up", MoveType.Normal, Category.Status, -1, -1, 10, "TM77", "Copies the opponent's stat changes.", -1);
        public static Move Psychic = new Move("Psychic", MoveType.Psychic, Category.Special, 90, 100, 10, "TM29", "May decreases the opponent's Special Defense by one stage.", 10);
        public static Move PsychoBoost = new Move("Psycho Boost", MoveType.Psychic, Category.Special, 140, 90, 5, "-", "Decreases user's Special Attack by two stages.", -1);
        public static Move PsychoBreak = new Move("Psycho Break", MoveType.Psychic, Category.Special, 100, 100, 10, "-", "Inflicts damage based on the target's Defense, not Special Defense.", -1);
        public static Move PsychoCut = new Move("Psycho Cut", MoveType.Psychic, Category.Physical, 70, 100, 20, "-", "High Critical-Hit ratio.", -1);
        public static Move PsychoShift = new Move("Psycho Shift", MoveType.Psychic, Category.Status, -1, 90, 10, "-", "Gives the opponent the user's Category.Status condition, if it hits.", -1);
        public static Move PsychoShock = new Move("Psycho Shock", MoveType.Psychic, Category.Special, 80, 100, 10, "-", "Inflicts damage based on the target's Defense, not Special Defense.", -1);
        public static Move Psywave = new Move("Psywave", MoveType.Psychic, Category.Special, -1, 80, 15, "-", "Inflicts damage 50-150% of user's level.", -1);
        public static Move Punishment = new Move("Punishment", MoveType.Dark, Category.Physical, -1, 100, 5, "-", "Power depends on opponent's stats.", -1);
        public static Move Purgatory = new Move("Purgatory", MoveType.Fire, Category.Special, 100, 50, 5, "-", "May burn the target.", 100);
        public static Move Pursuit = new Move("Pursuit", MoveType.Dark, Category.Physical, 40, 100, 20, "-", "Double power if the opponent is switching out.", -1);
        public static Move QuickAttack = new Move("Quick Attack", MoveType.Normal, Category.Physical, 40, 100, 30, "-", "User goes first.", -1);
        public static Move Rage = new Move("Rage", MoveType.Normal, Category.Physical, 20, 100, 20, "-", "Increases user's Attack by one stage when hit.", -1);
        public static Move RainDance = new Move("Rain Dance", MoveType.Water, Category.Status, -1, -1, 5, "TM18", "Causes the weather to change to rain for 5 turns.", -1);
        public static Move RapidSpin = new Move("Rapid Spin", MoveType.Normal, Category.Physical, 20, 100, 40, "-", "Removes the effects of trap moves", -1);
        public static Move RazorLeaf = new Move("Razor Leaf", MoveType.Grass, Category.Physical, 55, 95, 25, "-", "High Critical-Hit ratio.", -1);
        public static Move RazorWind = new Move("Razor Wind", MoveType.Normal, Category.Special, 80, 100, 10, "-", "Immobile on the first turn, attacks on the second turn. High Critical-Hit ratio.", -1);
        public static Move Recover = new Move("Recover", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User recovers 50% of its max HP.", -1);
        public static Move Recycle = new Move("Recycle", MoveType.Normal, Category.Status, -1, -1, 10, "TM67", "User's used hold item is restored.", -1);
        public static Move Reflect = new Move("Reflect", MoveType.Psychic, Category.Status, -1, -1, 20, "TM33", "User's party receives 1/2 damage from Ph. Attacks for 5 turns.", -1);
        public static Move Refresh = new Move("Refresh", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Cures paralysis, Poison, and burns.", -1);
        public static Move Rest = new Move("Rest", MoveType.Psychic, Category.Status, -1, -1, 10, "TM44", "User sleeps for 2 turns, but user is fully healed.", -1);
        public static Move Return = new Move("Return", MoveType.Normal, Category.Physical, -1, 100, 20, "TM27", "Power increases with user's Happiness.", -1);
        public static Move Revenge = new Move("Revenge", MoveType.Fighting, Category.Physical, 60, 100, 10, "-", "Power increases if user was hit first.", -1);
        public static Move Reversal = new Move("Reversal", MoveType.Fighting, Category.Physical, -1, 100, 15, "-", "The lower the user's HP, the higher the power.", -1);
        public static Move Roar = new Move("Roar", MoveType.Normal, Category.Status, -1, 100, 20, "TM05", "In battles, the opponent switches. In the wild, the Pokemon runs.", -1);
        public static Move RoarofTime = new Move("Roar of Time", MoveType.Dragon, Category.Special, 150, 90, 5, "-", "The user cannot Move in the following turn.", -1);
        public static Move RockBlast = new Move("Rock Blast", MoveType.Rock, Category.Physical, 25, 80, 10, "-", "Hits 2-5 times.", -1);
        public static Move RockClimb = new Move("Rock Climb", MoveType.Normal, Category.Physical, 90, 85, 20, "HM08", "May cause confusion.", 20);
        public static Move RockPolish = new Move("Rock Polish", MoveType.Rock, Category.Status, -1, -1, 20, "TM69", "Increases the user's Speed by two stages.", -1);
        public static Move RockSlide = new Move("Rock Slide", MoveType.Rock, Category.Physical, 75, 90, 10, "TM80", "May cause flinching, if it hits.", 30);
        public static Move RockSmash = new Move("Rock Smash", MoveType.Fighting, Category.Physical, 40, 100, 15, "HM06", "Likely to decrease the opponent's Defense by one stage.", 50);
        public static Move RockThrow = new Move("Rock Throw", MoveType.Rock, Category.Physical, 50, 90, 15, "-", "", -1);
        public static Move RockTomb = new Move("Rock Tomb", MoveType.Rock, Category.Physical, 50, 80, 10, "TM39", "Decreases the opponent's Speed by one stage, if it hits.", 100);
        public static Move RockWrecker = new Move("Rock Wrecker", MoveType.Rock, Category.Physical, 150, 90, 5, "-", "The user cannot Move in the following turn.", -1);
        public static Move RolePlay = new Move("Role Play", MoveType.Psychic, Category.Status, -1, -1, 15, "-", "User copies the opponent's Ability.", -1);
        public static Move RollingKick = new Move("Rolling Kick", MoveType.Fighting, Category.Physical, 60, 85, 15, "-", "May cause flinching.", 30);
        public static Move Rollout = new Move("Rollout", MoveType.Rock, Category.Physical, 30, 90, 20, "-", "An attack lasting for 5 turns that grows in power.", -1);
        public static Move Roost = new Move("Roost", MoveType.Flying, Category.Status, -1, -1, 10, "TM51", "User recovers 50% of its max HP. Flying-type users are not immune to Ground-type moves immediately following this move.", -1);
        public static Move SacredFire = new Move("Sacred Fire", MoveType.Fire, Category.Physical, 100, 95, 5, "-", "Likely to cause a burn.", 50);
        public static Move SacredSword = new Move("Sacred Sword", MoveType.Fighting, Category.Physical, 90, 100, 20, "-", "Ignores the target's stat changes.", -1);
        public static Move Safeguard = new Move("Safeguard", MoveType.Normal, Category.Status, -1, -1, 25, "TM20", "The user's party is protected from Status conditions.", -1);
        public static Move SandTomb = new Move("Sand Tomb", MoveType.Ground, Category.Physical, 15, 70, 15, "-", "Damages opponent for 2-5 turns, opponent cannot switch/escape.", -1);
        public static Move Sandattack = new Move("Sand-attack", MoveType.Ground, Category.Status, -1, 100, 15, "-", "Decreases the opponent's Accuracy by one stage.", -1);
        public static Move Sandstorm = new Move("Sandstorm", MoveType.Rock, Category.Status, -1, -1, 10, "TM37", "Damages all Pokemon except Rock-, Ground-, and Steel-types.", -1);
        public static Move ScaryFace = new Move("Scary Face", MoveType.Normal, Category.Status, -1, 90, 10, "-", "Decreases the opponent's Speed by two stages, if it hits.", -1);
        public static Move Scratch = new Move("Scratch", MoveType.Normal, Category.Physical, 40, 100, 35, "-", "", -1);
        public static Move Screech = new Move("Screech", MoveType.Normal, Category.Status, -1, 85, 40, "-", "Decreases the opponent's Defense by two stages, if it hits.", -1);
        public static Move SecretPower = new Move("Secret Power", MoveType.Normal, Category.Physical, 70, 100, 20, "TM43", "Effects of the attack vary with the location.", 30);
        public static Move SeedBomb = new Move("Seed Bomb", MoveType.Grass, Category.Physical, 80, 100, 15, "-", "", -1);
        public static Move SeedFlare = new Move("Seed Flare", MoveType.Grass, Category.Special, 120, 85, 5, "-", "May decrease the opponent's Special Defense by one stage.", 40);
        public static Move SeismicToss = new Move("Seismic Toss", MoveType.Fighting, Category.Physical, -1, 100, 20, "-", "Inflicts damage equal to user's level.", -1);
        public static Move Selfdestruct = new Move("Selfdestruct", MoveType.Normal, Category.Physical, 200, 100, 5, "-", "User faints.", -1);
        public static Move ShadowBall = new Move("Shadow Ball", MoveType.Ghost, Category.Special, 80, 100, 15, "TM30", "May decrease opponent's Special Defense.", 20);
        public static Move ShadowClaw = new Move("Shadow Claw", MoveType.Ghost, Category.Physical, 70, 100, 15, "TM65", "High Critical-Hit ratio.", -1);
        public static Move ShadowForce = new Move("Shadow Force", MoveType.Ghost, Category.Physical, 120, 100, 5, "-", "Disappears on the 1st turn, attacks on the 2nd turn.", -1);
        public static Move ShadowPunch = new Move("Shadow Punch", MoveType.Ghost, Category.Physical, 60, 8, 20, "-", "Cannot miss, regardless of Accuracy and Evasiveness.", -1);
        public static Move ShadowSneak = new Move("Shadow Sneak", MoveType.Ghost, Category.Physical, 40, 100, 30, "-", "User goes first.", -1);
        public static Move Sharpen = new Move("Sharpen", MoveType.Normal, Category.Status, -1, -1, 30, "-", "Increases the user's Attack by one stage.", -1);
        public static Move SheerCold = new Move("Sheer Cold", MoveType.Ice, Category.Special, -1, 30, 5, "-", "An instant 1-hit KO, if it hits.", -1);
        public static Move ShellBlade = new Move("Shell Blade", MoveType.Water, Category.Physical, 75, 95, 10, "-", "May lower the target's Defense by one level.", 50);
        public static Move ShellSmash = new Move("Shell Smash", MoveType.Normal, Category.Status, -1, -1, 15, "-", "Raises user's Speed by one stage. Lower user's Defense by one stage.", -1);
        public static Move ShockWave = new Move("Shock Wave", MoveType.Electric, Category.Special, 60, 8, 20, "TM34", "Cannot miss, regardless of Accuracy and Evasiveness.", -1);
        public static Move SideChange = new Move("Side Change", MoveType.Psychic, Category.Status, -1, -1, 15, "-", "User switches places with a random teammate.", -1);
        public static Move SignalBeam = new Move("Signal Beam", MoveType.Bug, Category.Special, 75, 100, 15, "-", "May cause confusion.", 10);
        public static Move SilverWind = new Move("Silver Wind", MoveType.Bug, Category.Special, 60, 100, 5, "TM62", "May increase all of the user's stats by one stage each.", 10);
        public static Move SimpleBeam = new Move("Simple Beam", MoveType.Normal, Category.Status, -1, 100, 15, "-", "Changes target's ability to Simple.", -1);
        public static Move Sing = new Move("Sing", MoveType.Normal, Category.Status, -1, 55, 15, "-", "Puts the opponent to sleep, if it hits.", 100);
        public static Move SingARound = new Move("Sing-A-Round", MoveType.Normal, Category.Special, 60, 100, 15, "-", "Power increases each turn if multiple Pokemon in the team also use it.", -1);
        public static Move Sketch = new Move("Sketch", MoveType.Normal, Category.Status, -1, -1, 1, "-", "Permanently copies the opponent's last move.", -1);
        public static Move SkillSwap = new Move("Skill Swap", MoveType.Psychic, Category.Status, -1, -1, 10, "TM48", "The user swaps Abilities with the opponent.", -1);
        public static Move SkullBash = new Move("Skull Bash", MoveType.Normal, Category.Physical, 100, 100, 15, "-", "Increases user's Defense one stage in the 1st turn, attacks in the 2nd turn.", 100);
        public static Move SkyAttack = new Move("Sky Attack", MoveType.Flying, Category.Physical, 140, 90, 5, "-", "User cannot public static Move in the 1st turn, attacks 2nd turn. May cause flinching.", 30);
        public static Move SkyUppercut = new Move("Sky Uppercut", MoveType.Fighting, Category.Physical, 85, 90, 15, "-", "Hits the opponent, even during Fly.", -1);
        public static Move SlackOff = new Move("Slack Off", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User recovers 50% of its max HP.", -1);
        public static Move Slam = new Move("Slam", MoveType.Normal, Category.Physical, 80, 75, 20, "-", "", -1);
        public static Move Slash = new Move("Slash", MoveType.Normal, Category.Physical, 70, 100, 20, "-", "High Critical-Hit ratio.", -1);
        public static Move SleepPowder = new Move("Sleep Powder", MoveType.Grass, Category.Status, -1, 75, 15, "-", "Puts the opponent to sleep, if it hits.", -1);
        public static Move SleepTalk = new Move("Sleep Talk", MoveType.Normal, Category.Status, -1, -1, 10, "TM82", "User performs one of its own moves while sleeping.", -1);
        public static Move Sludge = new Move("Sludge", MoveType.Poison, Category.Special, 65, 100, 20, "-", "May Poison the opponent.", 30);
        public static Move SludgeBomb = new Move("Sludge Bomb", MoveType.Poison, Category.Special, 90, 100, 10, "TM36", "May Poison the opponent.", 30);
        public static Move SludgeWave = new Move("Sludge Wave", MoveType.Poison, Category.Special, 95, 100, 10, "-", "May Poison the target.", 10);
        public static Move Smellingsalt = new Move("Smellingsalt", MoveType.Normal, Category.Special, 60, 100, 10, "-", "Power increases when used on a paralyzed opponent, but cures it.", -1);
        public static Move Smog = new Move("Smog", MoveType.Poison, Category.Special, 20, 70, 20, "-", "May Poison the opponent.", 40);
        public static Move Smokescreen = new Move("Smokescreen", MoveType.Normal, Category.Status, -1, 100, 20, "-", "Decreases the opponent's Accuracy by one stage.", -1);
        public static Move SnakeCoil = new Move("Snake Coil", MoveType.Poison, Category.Status, -1, -1, 20, "-", "Raises the user's Attack, Defense, and accuracy by one stage.", -1);
        public static Move Snatch = new Move("Snatch", MoveType.Dark, Category.Status, -1, -1, 10, "TM49", "User steals the effects of the opponent's next move.", -1);
        public static Move Snore = new Move("Snore", MoveType.Normal, Category.Special, 40, 100, 15, "-", "Can only be used while the user is sleeping.", -1);
        public static Move Softboiled = new Move("Softboiled", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User recovers 50% of its max HP.", -1);
        public static Move Solarbeam = new Move("Solarbeam", MoveType.Grass, Category.Special, 120, 100, 10, "TM22", "User cannot Move in the 1st turn, attacks in the 2nd turn.", -1);
        public static Move Sonicboom = new Move("Sonicboom", MoveType.Normal, Category.Special, -1, 90, 20, "-", "Always inflicts 20 HP.", -1);
        public static Move SpacialRend = new Move("Spacial Rend", MoveType.Dragon, Category.Special, 100, 95, 5, "-", "High Critical-Hit ratio.", -1);
        public static Move Spark = new Move("Spark", MoveType.Electric, Category.Physical, 65, 100, 20, "-", "May cause paralysis.", 30);
        public static Move SpiderWeb = new Move("Spider Web", MoveType.Bug, Category.Status, -1, -1, 10, "-", "Opponent cannot escape/switch.", -1);
        public static Move SpikeCannon = new Move("Spike Cannon", MoveType.Normal, Category.Physical, 20, 100, 15, "-", "Hits 2-5 times.", -1);
        public static Move Spikes = new Move("Spikes", MoveType.Ground, Category.Status, -1, -1, 20, "-", "Hurts opponents when they switch into battle.", -1);
        public static Move SpitUp = new Move("Spit Up", MoveType.Normal, Category.Special, -1, 100, 10, "-", "Power depends on how many times the user performed Stockpile.", -1);
        public static Move Spite = new Move("Spite", MoveType.Ghost, Category.Status, -1, 100, 10, "-", "The opponent's last public static Move loses 2-5 PP.", -1);
        public static Move Splash = new Move("Splash", MoveType.Normal, Category.Status, -1, -1, 40, "-", "Doesn't do ANYTHING.", -1);
        public static Move Spore = new Move("Spore", MoveType.Grass, Category.Status, -1, 100, 15, "-", "Puts the opponent to sleep.", 100);
        public static Move StealthRock = new Move("Stealth Rock", MoveType.Rock, Category.Status, -1, -1, 20, "TM76", "Damages opponent when they switch into battle.", -1);
        public static Move SteelWing = new Move("Steel Wing", MoveType.Steel, Category.Physical, 70, 90, 25, "TM47", "May increases user's Defense by one stage.", 10);
        public static Move Stockpile = new Move("Stockpile", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Temporarily increases user's Defense by one stage. To be used with Spit Up and Swallow.", -1);
        public static Move Stomp = new Move("Stomp", MoveType.Normal, Category.Physical, 65, 100, 20, "-", "May cause flinching.", 30);
        public static Move StoneEdge = new Move("Stone Edge", MoveType.Rock, Category.Physical, 100, 80, 5, "TM71", "High Critical-Hit ratio.", -1);
        public static Move Strength = new Move("Strength", MoveType.Normal, Category.Physical, 80, 100, 15, "HM04", "", -1);
        public static Move StringShot = new Move("String Shot", MoveType.Bug, Category.Status, -1, 95, 40, "-", "Decreases opponent's Speed by one stage.", -1);
        public static Move Struggle = new Move("Struggle", MoveType.Normal, Category.Physical, 50, 100, -1, "-", "Only usable when all PP are gone. Hurts the user.", -1);
        public static Move StunSpore = new Move("Stun Spore", MoveType.Grass, Category.Status, -1, 75, 30, "-", "Causes paralysis, if it hits.", -1);
        public static Move Submersion = new Move("Submersion", MoveType.Water, Category.Status, -1, 100, 20, "-", "Changes the target's type to Water.", -1);
        public static Move Submission = new Move("Submission", MoveType.Fighting, Category.Physical, 80, 80, 25, "-", "User receives recoil damage.", -1);
        public static Move Substitute = new Move("Substitute", MoveType.Normal, Category.Status, -1, -1, 10, "TM90", "User loses 1/4 of its max HP, but can take hits without losing HP for awhile.", -1);
        public static Move SuckerPunch = new Move("Sucker Punch", MoveType.Dark, Category.Physical, 80, 100, 5, "-", "User goes first, but won't work if opponent doesn't use an attacking move.", -1);
        public static Move SunnyDay = new Move("Sunny Day", MoveType.Fire, Category.Status, -1, -1, 5, "TM11", "Changes the weather to sunny.", -1);
        public static Move SuperFang = new Move("Super Fang", MoveType.Normal, Category.Physical, -1, 90, 10, "-", "Always takes off half of the opponent's HP.", -1);
        public static Move Superpower = new Move("Superpower", MoveType.Fighting, Category.Physical, 120, 100, 5, "-", "Decreases user's Attack and Defense by one stage each.", -1);
        public static Move Supersonic = new Move("Supersonic", MoveType.Normal, Category.Status, -1, 55, 20, "-", "Causes confusion, if it hits.", -1);
        public static Move Surf = new Move("Surf", MoveType.Water, Category.Special, 95, 100, 15, "HM03", "", -1);
        public static Move Swagger = new Move("Swagger", MoveType.Normal, Category.Status, -1, 90, 15, "TM87", "Opponent becomes confused, but its Attack is raised by 2 stages.", -1);
        public static Move Swallow = new Move("Swallow", MoveType.Normal, Category.Status, -1, -1, 10, "-", "The more times the user has performed Stockpile, the more HP is recovered.", -1);
        public static Move SweepSlap = new Move("Sweep Slap", MoveType.Normal, Category.Physical, 25, 85, 10, "-", "Hits 2-5 times in one turn.", -1);
        public static Move SweetKiss = new Move("Sweet Kiss", MoveType.Normal, Category.Status, -1, 75, 10, "-", "Causes confusion, if it hits.", -1);
        public static Move SweetScent = new Move("Sweet Scent", MoveType.Normal, Category.Status, -1, -1, 20, "-", "Decreases opponent's Evasiveness by one stage.", -1);
        public static Move Swift = new Move("Swift", MoveType.Normal, Category.Special, 60, 8, 20, "-", "Cannot miss, regardless of Accuracy and Evasiveness.", -1);
        public static Move Switcheroo = new Move("Switcheroo", MoveType.Dark, Category.Status, -1, 100, 15, "-", "Swaps held items with the opponent.", -1);
        public static Move SwordsDance = new Move("Swords Dance", MoveType.Normal, Category.Status, -1, -1, 30, "TM75", "Increases the user's Attack by two stages.", -1);
        public static Move Synchronise = new Move("Synchronise", MoveType.Psychic, Category.Special, 70, 100, 15, "-", "Hits any Pokemon that shares a type with the user.", -1);
        public static Move Synthesis = new Move("Synthesis", MoveType.Grass, Category.Status, -1, -1, 5, "-", "User recovers HP. Amount varies with the weather.", -1);
        public static Move Tackle = new Move("Tackle", MoveType.Normal, Category.Physical, 35, 95, 35, "-", "", -1);
        public static Move TailGlow = new Move("Tail Glow", MoveType.Bug, Category.Status, -1, -1, 20, "-", "Increases user's Special Attack by two stages.", -1);
        public static Move TailWhip = new Move("Tail Whip", MoveType.Normal, Category.Status, -1, 100, 30, "-", "Decreases opponent's Defense by one stage.", -1);
        public static Move Tailwind = new Move("Tailwind", MoveType.Flying, Category.Status, -1, -1, 30, "-", "The user's party's Speed is increased by one stage for 5 turns.", -1);
        public static Move TakeDown = new Move("Take Down", MoveType.Normal, Category.Physical, 90, 85, 20, "-", "User receives recoil damage.", -1);
        public static Move Taunt = new Move("Taunt", MoveType.Dark, Category.Status, -1, 100, 20, "TM12", "Opponent can only use moves that attack.", -1);
        public static Move TechnoBuster = new Move("Techno Buster", MoveType.Normal, Category.Special, 85, 100, 5, "-", "If the user is holding a plate, the damage inflicted will match it.", -1);
        public static Move TeeterDance = new Move("Teeter Dance", MoveType.Normal, Category.Status, -1, 100, 20, "-", "All Pokemon on the field become confused.", -1);
        public static Move Telekinesis = new Move("Telekinesis", MoveType.Psychic, Category.Status, -1, -1, 15, "-", "Increases accuracy for 3 turns.", -1);
        public static Move Teleport = new Move("Teleport", MoveType.Psychic, Category.Status, -1, -1, 20, "-", "Allows user to switch out. In the wild, the battle ends.", -1);
        public static Move Thief = new Move("Thief", MoveType.Dark, Category.Physical, 40, 100, 10, "TM46", "May steal opponent's held item.", 40);
        public static Move Thrash = new Move("Thrash", MoveType.Normal, Category.Physical, 90, 100, 20, "-", "User attacks for 2-3 turns, but becomes confused.", -1);
        public static Move Thunder = new Move("Thunder", MoveType.Electric, Category.Special, 120, 70, 10, "TM25", "May cause paralysis.", 30);
        public static Move ThunderFang = new Move("Thunder Fang", MoveType.Electric, Category.Physical, 65, 95, 15, "-", "May cause paralysis and flinching.", 10);
        public static Move ThunderWave = new Move("Thunder Wave", MoveType.Electric, Category.Status, -1, 100, 20, "TM73", "Causes paralysis.", 100);
        public static Move Thunderbolt = new Move("Thunderbolt", MoveType.Electric, Category.Special, 95, 100, 15, "TM24", "May cause paralysis.", 10);
        public static Move Thunderpunch = new Move("Thunderpunch", MoveType.Electric, Category.Physical, 75, 100, 15, "-", "May cause paralysis.", 10);
        public static Move Thundershock = new Move("Thundershock", MoveType.Electric, Category.Special, 40, 100, 30, "-", "May cause paralysis.", 10);
        public static Move Tickle = new Move("Tickle", MoveType.Normal, Category.Status, -1, 100, 20, "-", "Decreases the opponent's Attack and Defense by one stage each.", -1);
        public static Move Torment = new Move("Torment", MoveType.Dark, Category.Status, -1, 100, 15, "TM41", "Opponent cannot use the same public static Move in a row.", -1);
        public static Move Toxic = new Move("Toxic", MoveType.Poison, Category.Status, -1, 85, 10, "TM06", "Causes Poison that doubles in damage each turn.", -1);
        public static Move ToxicSpikes = new Move("Toxic Spikes", MoveType.Poison, Category.Status, -1, -1, 20, "-", "Hurts opponents when they switch into battle.", -1);
        public static Move Transform = new Move("Transform", MoveType.Normal, Category.Status, -1, -1, 10, "-", "User takes on the form and attacks of the opponent.", -1);
        public static Move TriAttack = new Move("Tri Attack", MoveType.Normal, Category.Special, 80, 100, 10, "-", "May cause paralysis, freezing, or a burn.", 20);
        public static Move Trick = new Move("Trick", MoveType.Psychic, Category.Status, -1, 100, 10, "-", "Swaps held items with the opponent.", -1);
        public static Move TrickRoom = new Move("Trick Room", MoveType.Psychic, Category.Status, -1, -1, 5, "TM92", "Slower Pokemon public static Move first in the turn for 5 turns.", -1);
        public static Move Trickery = new Move("Trickery", MoveType.Dark, Category.Physical, 95, 100, 15, "-", "More powerful against Fighting-type Pokemon.", -1);
        public static Move TripleKick = new Move("Triple Kick", MoveType.Fighting, Category.Physical, 10, 90, 10, "-", "Hits 3 times, growing in power each with each hit.", -1);
        public static Move TrumpCard = new Move("Trump Card", MoveType.Normal, Category.Special, -1, 100, 5, "-", "The lower the PP, the higher the power.", -1);
        public static Move Twineedle = new Move("Twineedle", MoveType.Bug, Category.Status, 25, 100, 20, "-", "Hits twice. May cause Poison.", 20);
        public static Move Twister = new Move("Twister", MoveType.Dragon, Category.Special, 40, 100, 20, "-", "May cause flinching. 2x damage against an opponent using Fly.", 20);
        public static Move Uturn = new Move("U-turn", MoveType.Bug, Category.Physical, 70, 100, 20, "TM89", "User switches out immediately after attacking.", -1);
        public static Move Uproar = new Move("Uproar", MoveType.Normal, Category.Special, 50, 100, 10, "-", "User attacks for 2-5 turns and cannot sleep during that time.", -1);
        public static Move VGenerate = new Move("V-Generate", MoveType.Fire, Category.Physical, 180, 95, 5, "-", "Lowers the user's Defense, Special Defense, and Speed by one stage.", 100);
        public static Move VacuumWave = new Move("Vacuum Wave", MoveType.Fighting, Category.Special, 40, 100, 30, "-", "User goes first.", -1);
        public static Move Vengeance = new Move("Vengeance", MoveType.Normal, Category.Physical, 70, 100, 5, "-", "Inflicts double damage if a teammate fainted on the last turn.", -1);
        public static Move VenomShock = new Move("Venom Shock", MoveType.Poison, Category.Special, 65, 100, 10, "-", "Inflicts double damage if the target is Poisoned.", -1);
        public static Move Vicegrip = new Move("Vicegrip", MoveType.Normal, Category.Physical, 55, 100, 30, "-", "", -1);
        public static Move VineWhip = new Move("Vine Whip", MoveType.Grass, Category.Physical, 35, 100, 15, "-", "", -1);
        public static Move VitalThrow = new Move("Vital Throw", MoveType.Fighting, Category.Physical, 70, 8, 10, "-", "User attacks last, but cannot miss.", -1);
        public static Move VoltChange = new Move("Volt Change", MoveType.Electric, Category.Special, 70, 100, 20, "-", "User must switch out after attacking.", -1);
        public static Move VoltTackle = new Move("Volt Tackle", MoveType.Electric, Category.Physical, 120, 100, 15, "-", "User receives recoil damage.", -1);
        public static Move WakeupSlap = new Move("Wake-up Slap", MoveType.Fighting, Category.Physical, 60, 100, 10, "-", "Power doubles if used on a sleeping opponent, but wakes it up.", -1);
        public static Move WaterGun = new Move("Water Gun", MoveType.Water, Category.Special, 40, 100, 25, "-", "", -1);
        public static Move WaterOath = new Move("Water Oath", MoveType.Water, Category.Special, 50, 100, 10, "-", "Added effects appear if preceded by Fire Oath or succeeded by Grass Oath.", -1);
        public static Move WaterPulse = new Move("Water Pulse", MoveType.Water, Category.Special, 60, 100, 20, "TM03", "May cause confusion.", 20);
        public static Move WaterSport = new Move("Water Sport", MoveType.Water, Category.Status, -1, -1, 15, "-", "Increases the user's Fire resistance.", -1);
        public static Move WaterSpout = new Move("Water Spout", MoveType.Water, Category.Special, 150, 100, 5, "-", "The higher the user's HP, the higher the damage caused.", -1);
        public static Move Waterfall = new Move("Waterfall", MoveType.Water, Category.Physical, 80, 100, 15, "HM07", "May cause flinching.", 20);
        public static Move WeatherBall = new Move("Weather Ball", MoveType.Normal, Category.Special, 50, 100, 10, "-", "Move's power and type changes with the weather.", -1);
        public static Move Whirlpool = new Move("Whirlpool", MoveType.Water, Category.Special, 15, 70, 15, "-", "Damages opponent for 2-5 turns, opponent cannot switch/escape.", -1);
        public static Move Whirlwind = new Move("Whirlwind", MoveType.Normal, Category.Status, -1, 100, 20, "-", "In battles, the opponent switches. In the wild, the Pokemon runs.", -1);
        public static Move WideGuard = new Move("Wide Guard", MoveType.Rock, Category.Status, -1, -1, 10, "-", "Protects the user's teammates from attacks for one turn.", -1);
        public static Move WildBolt = new Move("Wild Bolt", MoveType.Electric, Category.Physical, 90, 100, 15, "-", "User receives 1/4 the damage it inflicts in recoil.", -1);
        public static Move Willowisp = new Move("Will-o-wisp", MoveType.Fire, Category.Status, -1, 75, 15, "TM61", "Causes a burn, if it hits.", 100);
        public static Move WingAttack = new Move("Wing Attack", MoveType.Flying, Category.Physical, 60, 100, 35, "-", "", -1);
        public static Move Wish = new Move("Wish", MoveType.Normal, Category.Status, -1, -1, 10, "-", "The user recovers HP in the following turn.", -1);
        public static Move Withdraw = new Move("Withdraw", MoveType.Water, Category.Status, -1, -1, 40, "-", "Increases the user's Defense by one stage.", -1);
        public static Move WonderRoom = new Move("Wonder Room", MoveType.Psychic, Category.Status, -1, -1, 10, "-", "Swaps every Pokemon's Defense and Special Defense for 5 turns.", -1);
        public static Move WoodHammer = new Move("Wood Hammer", MoveType.Grass, Category.Physical, 120, 100, 15, "-", "User receives recoil damage.", -1);
        public static Move WoodHorn = new Move("Wood Horn", MoveType.Grass, Category.Physical, 75, 100, 10, "-", "Heals the user by half the damage inflicted.", -1);
        public static Move WorrySeed = new Move("Worry Seed", MoveType.Grass, Category.Status, -1, 100, 10, "-", "Changes the opponent's Ability to Insomnia.", -1);
        public static Move Wrap = new Move("Wrap", MoveType.Normal, Category.Physical, 15, 85, 20, "-", "Damages opponent for 2-5 turns, opponent cannot switch/escape.", -1);
        public static Move WringOut = new Move("Wring Out", MoveType.Normal, Category.Special, 120, 100, 5, "-", "The higher the opponent's HP, the higher the damage.", -1);
        public static Move XScissor = new Move("X-Scissor", MoveType.Bug, Category.Physical, 80, 100, 15, "TM81", "", -1);
        public static Move Yawn = new Move("Yawn", MoveType.Normal, Category.Status, -1, -1, 10, "-", "Puts the opponent to sleep in the following turn.", -1);
        public static Move YouFirst = new Move("You First", MoveType.Normal, Category.Status, -1, -1, 15, "-", "Copies non-damaging moves but makes the user go last.", -1);
        public static Move ZapCannon = new Move("Zap Cannon", MoveType.Electric, Category.Special, 120, 50, 5, "-", "Guaranteed to paralyze target, if it hits.", 100);
        public static Move ZenHeadbutt = new Move("Zen Headbutt", MoveType.Psychic, Category.Physical, 80, 90, 15, "-", "May cause flinching.", 20);

        public static Move[] MoveArray =  
        {
            Absorb, Acid, AcidArmor, AcidBomb, Acrobat, Acupressure, AerialAce, Aeroblast, AfroBreak, Agility, AirCutter, AirSlash, Amnesia, Ancient, AncientPower, AnkleSweep, 
            AquaJet, AquaRing, AquaTail, ArmThrust, Aromatherapy, Assist, AssistPower, Assurance, Astonish, AttackOrder, Attract, AuraSphere, AuroraBeam, Avalanche, BackOut, Barrage, 
            Barrier, BatonPass, BeatUp, Befriend, BellyDrum, Bide, Bind, Bite, BlastBurn, BlazeJudgement, BlazeKick, Blizzard, Block, BlueFlame, BodyPurge, BodySlam, BoilingWater, BoneClub, 
            BoneRush, Bonemerang, Bounce, BraveBird, BrickBreak, Brine, Bubble, Bubblebeam, BugBite, BugBuzz, BugRepellant, BulkUp, BulletPunch, BulletSeed, ButterflyDance, CalmMind, 
            Camouflage, Captivate, Charge, ChargeBeam, Charm, Chatter, CheerUp, Clamp, ClawSharpen, ClearSmog, CloseCombat, ColdFlare, CometPunch, ConfuseRay, Confusion, Constrict, Conversion, 
            Conversion2, Copycat, CosmicPower, CottonGuard, CottonSpore, Counter, Covet, Crabhammer, CrossChop, CrossFlame, CrossPoison, CrossThunder, Crunch, CrushClaw, CrushGrip, Curse, Cut, 
            DarkPulse, DarkVoid, DefendOrder, DefenseCurl, Defog, DestinyBond, Detect, Dig, Disable, Discharge, Dive, DizzyPunch, Doom, DoubleChop, DoubleHit, DoubleKick, DoubleTeam, DoubleEdge, 
            DoubleSlap, DracoMeteor, DragonClaw, DragonDance, DragonPulse, DragonRage, DragonRush, DragonTail, Dragonbreath, DrainPunch, DreamEater, DrillLiner, DrillPeck, Dynamicpunch, EarthPower, 
            Earthquake, EchoVoice, EggBomb, ElectricBall, ElectricNet, Embargo, Ember, Encore, Endeavor, Endure, EnergyBall, Eruption, EvilEye, Explosion, ExtraSensory, ExtremeSpeed, Facade, 
            FaintAttack, FakeOut, FakeTears, FalseSwipe, FastGuard, Featherdance, Feint, FireBlast, FireDance, FireFang, FireOath, FirePunch, FireSpin, Fissure, Flail, FlameBurst, FlameWheel, 
            Flamethrower, FlareBlitz, Flash, FlashCannon, Flatter, Fling, Fly, FocusBlast, FocusEnergy, FocusPunch, FollowMe, ForcePalm, Foresight, Freefall, FreezeBolt, FrenzyPlant, FrozenWorld, 
            Frustration, FuryAttack, FuryCutter, FurySwipes, FutureSight, GastroAcid, GearChange, GearSaucer, GiftPass, GigaDrain, GigaImpact, Glare, GrassKnot, GrassMixer, GrassOath, GrassWhistle, 
            Gravity, Growl, Growth, Grudge, GuardShare, GuardSwap, Guillotine, GunkShot, Gust, GyroBall, HailIce, HammerArm, HardRoller, Harden, Haze, HeadSmash, Headbutt, HealBell, HealBlock, HealOrder, 
            HealingBeam, HealingWish, HeartStamp, HeartSwap, HeatStamp, HeatWave, HeavyBomber, HelpingHand, HiJumpKick, HiddenPower, HornAttack, HornDrill, Howl, Hurricane, HydroCannon, HydroPump, 
            HyperBeam, HyperFang, HyperVoice, Hypnosis, IceBall, IceBeam, IceBreath, IceFang, IcePunch, IceShard, IcicleDrop, IcicleSpear, IcyWind, Imprison, Incinerate, Ingrain, IronDefense, IronHead, 
            IronTail, Judgment, JumpKick, KarateChop, Kinesis, KnockDown, KnockOff, LastResort, LavaPlume, LeafBlade, LeafStorm, LeechLife, LeechSeed, Leer, LevelField, Lick, LifeGamble, LightScreen, 
            LightningStrike, LittleByLittle, Lockon, LovelyKiss, LowKick, LuckyChant, LunarDance, LusterPurge, MachPunch, MagicCoat, MagicRoom, MagicalLeaf, MagmaStorm, MagnetBomb, MagnetRise, 
            Magnitude, MeFirst, MeanLook, Meditate, MegaDrain, MegaKick, MegaPunch, Megahorn, Memento, MetalBurst, MetalClaw, MetalSound, MeteorMash, Metronome, MilkDrink, Mimic, MindReader, 
            Minimize, MiracleEye, MirrorCoat, MirrorMove, MirrorShot, MirrorType, Mist, MistBall, Moonlight, MorningSun, MountainStorm, MudBomb, MudShot, MudSport, MudSlap, MuddyWater, MysterySword, 
            NastyPlot, NaturalGift, NaturePower, NeedleArm, NightBurst, NightShade, NightSlash, Nightmare, NitroCharge, Octazooka, OdorSleuth, OminousWind, Outrage, OverheadThrow, Overheat, 
            PainSplit, PayDay, Payback, Peck, PerishSong, PetalDance, PinMissile, Pluck, PoisonFang, PoisonGas, PoisonJab, PoisonSting, PoisonTail, Poisonpowder, Postpone, Pound, PowderRage, PowderSnow, 
            PowerGem, PowerShare, PowerSwap, PowerTrick, PowerWhip, Present, Protect, Psybeam, PsychUp, Psychic, PsychoBoost, PsychoBreak, PsychoCut, PsychoShift, PsychoShock, Psywave, Punishment, 
            Purgatory, Pursuit, QuickAttack, Rage, RainDance, RapidSpin, RazorLeaf, RazorWind, Recover, Recycle, Reflect, Refresh, Rest, Return, Revenge, Reversal, Roar, RoarofTime, RockBlast, RockClimb, 
            RockPolish, RockSlide, RockSmash, RockThrow, RockTomb, RockWrecker, RolePlay, RollingKick, Rollout, Roost, SacredFire, SacredSword, Safeguard, SandTomb, Sandattack, Sandstorm, ScaryFace, Scratch, 
            Screech, SecretPower, SeedBomb, SeedFlare, SeismicToss, Selfdestruct, ShadowBall, ShadowClaw, ShadowForce, ShadowPunch, ShadowSneak, Sharpen, SheerCold, ShellBlade, ShellSmash, ShockWave, 
            SideChange, SignalBeam, SilverWind, SimpleBeam, Sing, SingARound, Sketch, SkillSwap, SkullBash, SkyAttack, SkyUppercut, SlackOff, Slam, Slash, SleepPowder, SleepTalk, Sludge, SludgeBomb, 
            SludgeWave, Smellingsalt, Smog, Smokescreen, SnakeCoil, Snatch, Snore, Softboiled, Solarbeam, Sonicboom, SpacialRend, Spark, SpiderWeb, SpikeCannon, Spikes, SpitUp, Spite, Splash, 
            Spore, StealthRock, SteelWing, Stockpile, Stomp, StoneEdge, Strength, StringShot, Struggle, StunSpore, Submersion, Submission, Substitute, SuckerPunch, SunnyDay, SuperFang, Superpower, 
            Supersonic, Surf, Swagger, Swallow, SweepSlap, SweetKiss, SweetScent, Swift, Switcheroo, SwordsDance, Synchronise, Synthesis, Tackle, TailGlow, TailWhip, Tailwind, TakeDown, Taunt, TechnoBuster, 
            TeeterDance, Telekinesis, Teleport, Thief, Thrash, Thunder, ThunderFang, ThunderWave, Thunderbolt, Thunderpunch, Thundershock, Tickle, Torment, Toxic, ToxicSpikes, Transform, TriAttack, 
            Trick, TrickRoom, Trickery, TripleKick, TrumpCard, Twineedle, Twister, Uturn, Uproar, VGenerate, VacuumWave, Vengeance, VenomShock, Vicegrip, VineWhip, VitalThrow, VoltChange, 
            VoltTackle, WakeupSlap, WaterGun, WaterOath, WaterPulse, WaterSport, WaterSpout, Waterfall, WeatherBall, Whirlpool, Whirlwind, WideGuard, WildBolt, Willowisp, WingAttack, Wish, Withdraw, 
            WonderRoom, WoodHammer, WoodHorn, WorrySeed, Wrap, WringOut, XScissor, Yawn, YouFirst, ZapCannon, ZenHeadbutt
        };
    }
}