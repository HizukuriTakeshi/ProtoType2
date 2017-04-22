using System;
using System.Collections.Generic;
using System.Text;
using Prototype.GameSystem;

namespace Prototype.GameSystem
{
    class ProtoTypeUI
    {
        #region [フィールド]

        #endregion

        /// <summary>
        /// ゲームの処理を書く
        /// </summary>
        /// <param name="gameManager"></param>
        #region [コンストラタ]
        public ProtoTypeUI(GameManager gameManager)
        {
            gameManager.DisplayVirtualBoard();
            for (int i = 0; i < gameManager.FinalTurn; i++)
            {
                gameManager.MoveGhost(gameManager.tmpMove());
                gameManager.DisplayVirtualBoard();
                if (gameManager.VorDCheck())
                {
                    break;
                }
                gameManager.NextTurn();
            }
            Console.ReadLine();
        }


        #endregion
    }
}
