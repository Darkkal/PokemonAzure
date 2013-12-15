using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Moves;


namespace PokeEngine.Moves
{
    /// <summary>
    /// This is a list which holds every move
    /// There is an add method which allows more moves to be created
    /// NOTE: SortedList IS AWESOMESAUCE!!!
    /// </summary>
    
    public class MoveList
    {
        public static List<BaseMove> move;

        public static int numberOfMoves
        {
            get { return move.Count; }
        }

        public MoveList()
        {
            //Keys for moves are always in upper case, this is automatic
            move = new List<BaseMove>();
        }

        /// <summary>
        /// Adds the specified base more to the move list
        /// NOTE: Will overwrite any move with the same name
        /// </summary>
        /// <param name="newMove">instance of base move</param>
        public static void addMove( BaseMove newMove)
        {
            int index = getIndex(newMove.name);

            if (index != -1)
            {
                move[index] = newMove;
            }
            else
            {
                move.Add(newMove);
            }
        }

        /// <summary>
        /// returns a BaseMove object from the list with the given name
        /// </summary>
        /// <param name="moveName">string containing move name you wish to find</param>
        /// <returns>BaseMove OR null if unsuccessful</returns>
        public static BaseMove getMove( String moveName)
        {
            BaseMove temp = null;
            int index = getIndex(moveName);

            if (index != -1)
            {
                temp = move[index];
            }

            return temp;
        }

        /// <summary>
        /// Returns the index that the move is at in the list
        /// or -1 if not in the list
        /// </summary>
        /// <param name="moveName">name of move to look for</param>
        /// <returns>index of move</returns>
        public static int getIndex(String moveName)
        {
            int index = -1;
            for (int i = 0; i < move.Count; i++)
            {
                if(move[i].name.Equals(moveName))
                {
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// Removes a move with the specified name
        /// </summary>
        /// <param name="moveName">string of the move's name</param>
        public static void removeMove(String moveName)
        {
            for (int i = 0; i < move.Count; i++)
            {
                if (move[i].name.Equals(moveName))
                    move.RemoveAt(i);
            }
        }

        /// <summary>
        /// Removes the specified Base move
        /// </summary>
        /// <param name="moveName">Basemove you wish to remove</param>
        public static void removeMove(BaseMove inMove)
        {
            for (int i = 0; i < move.Count; i++)
            {
                if (move[i].name.Equals(inMove.name))
                    move.RemoveAt(i);
            }
        }
    }
}
