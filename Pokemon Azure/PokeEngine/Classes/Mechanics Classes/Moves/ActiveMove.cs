using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeEngine.Moves
{
    
    public class ActiveMove
    {
        public BaseMove bMove; //Base Move of this move
        public int maxPP; //max PP of this move
        public int currentPP; //current PP remaining of this move
        public byte PPUpUses; //number of times PPUp has been used on this move

        public ActiveMove(BaseMove move)
        {
            bMove = move;
            maxPP = bMove.basePP;
            currentPP = bMove.basePP;
            PPUpUses = 0;
        }

        public void PPUp()
        {
            if (PPUpUses <= 1)
            {
                maxPP = Convert.ToInt32(maxPP + (Convert.ToDouble(PPUpUses + 1) * 0.2) * Convert.ToDouble(bMove.basePP));
                PPUpUses++;
            }
            else
            {
                PPMax();
            }
        }

        public void PPMax()
        {
            maxPP = Convert.ToInt32(Math.Floor(1.6 * Convert.ToDouble(bMove.basePP)));
        }

    }
}
