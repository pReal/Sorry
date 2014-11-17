using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PieceLocation
    {
        public SpaceType SpaceType { get; private set; }
        public int SpaceNumber { get; private set; }

        public PieceLocation()
        {
            SpaceType = SpaceType.Standard;
            SpaceNumber = 0;
        }

        public PieceLocation(SpaceType spaceType, int spaceNumber)
        {
            SpaceType = spaceType;
            SpaceNumber = spaceNumber;
        }
    }
}
