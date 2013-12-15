using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//These are for scripting

namespace PokeEngine.Moves
{
    /// <summary>
    /// This is a base move, it stores basic information for a move. PP will be stored in an ActiveMove class and will be aggregated by the pokemon
    /// </summary>
    
    public class BaseMove
    {
        public String name;
        public String description;
        public int power;
        public int accuracy;
        public String moveType; //Move type - Fighting, Fire, etc, also used for STAB
        public String moveKind; //Physical, Special, or Status
        public int basePP;
        public int basePriority;

        public String moveScript; //script that the move uses
        public String effectScript; //the effect used in the moveScript

        //TODO find out if this class needs a way to track volatile status effects (confustion, perish song, etc)

        /// <summary>
        /// Creates an instance of a move given all the parameters
        /// </summary>
        /// <param name="name">Name of the move</param>
        /// <param name="description">Short description of the move</param>
        /// <param name="power">power of the move (ususally multiples of 5)</param>
        /// <param name="accuracy">accuracy of the move (between 30 and 100 usually), -1 for NA</param>
        /// <param name="moveType">Type of move - Fire, Water, Ghost etc</param>
        /// <param name="moveKind">Kind of move - Physical, Special, or Status</param>
        /// <param name="basePP">Base PP of the move</param>
        public BaseMove(String name, String description, int power, int accuracy, String moveType, String moveKind, int basePP)
        {
            this.name = name;
            this.description = description;
            this.power = power;
            this.accuracy = accuracy;
            if (accuracy > 100) this.accuracy = 100;

            this.moveType = moveType;
            this.moveKind = moveKind;
            this.basePP = basePP;
            basePriority = 0;
        }

        /// <summary>
        /// Creates an instance of a move given all parameters except accuracy
        /// </summary>
        /// <param name="name">Name of the move</param>
        /// <param name="description">Short description of the move</param>
        /// <param name="power">power of the move (ususally multiples of 5)</param>
        /// <param name="moveType">Type of move - Fire, Water, Ghost etc</param>
        /// <param name="moveKind">Kind of move - Physical, Special, or Status</param>
        /// <param name="basePP">Base PP of the move</param>
        public BaseMove(String name, String description, int power, String moveType, String moveKind, int basePP)
        {
            this.name = name;
            this.description = description;
            this.power = power;
            this.accuracy = -1;
            if (accuracy > 100) this.accuracy = 100;

            this.moveType = moveType;
            this.moveKind = moveKind;
            this.basePP = basePP;
            basePriority = 0;
        }

        public BaseMove()
        {
            name = "Default Name";
            description = "Default Description";
            power = 0;
            accuracy = 0;
            moveType = "Normal";
            moveKind = "Status";
            basePP = 15;
            basePriority = 0;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
