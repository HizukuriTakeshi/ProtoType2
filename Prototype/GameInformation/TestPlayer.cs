using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.GameInformation
{
    class TestPlayer : AbstractPlayer
    {

        public TestPlayer() : base()
        {
            SetInitialPlacement(new GhostAttribute[2, 4] { { GhostAttribute.evil, GhostAttribute.evil, GhostAttribute.evil, GhostAttribute.evil },
                                                      { GhostAttribute.good, GhostAttribute.good, GhostAttribute.good, GhostAttribute.good }}
                                                   );

        }

        public TestPlayer(string name) : base()
        {
            this.name = name;
            SetInitialPlacement(new GhostAttribute[2, 4] { { GhostAttribute.evil, GhostAttribute.evil, GhostAttribute.evil, GhostAttribute.evil },
                                                      { GhostAttribute.good, GhostAttribute.good, GhostAttribute.good, GhostAttribute.good }}
                                                              );
        }

        public override Move GetMove()
        {
            Move m = new Move(new Position(1,1), GhostMove.Down);
            return m;
        }

        

    }
}
