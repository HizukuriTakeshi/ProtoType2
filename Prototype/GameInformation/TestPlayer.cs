using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.GameInformation
{
    class TestPlayer : AbstractPlayer
    {

        public TestPlayer() : base()
        {
            SetInitialPlacement(new GhostType[2, 4] { { GhostType.evil, GhostType.evil, GhostType.evil, GhostType.evil },
                                                      { GhostType.good, GhostType.good, GhostType.good, GhostType.good }}
                                                   );

        }

        public TestPlayer(string name) : base()
        {
            this.name = name;
            SetInitialPlacement(new GhostType[2, 4] { { GhostType.evil, GhostType.evil, GhostType.evil, GhostType.evil },
                                                      { GhostType.good, GhostType.good, GhostType.good, GhostType.good }}
                                                              );
        }

        public override Move GetMove()
        {
            Move m = new Move(new Position(1,1), GhostMove.Down);
            return m;
        }

        

    }
}
