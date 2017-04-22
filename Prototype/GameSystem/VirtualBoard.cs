using System;
using System.Collections.Generic;
using System.Text;
using Prototype.GameInformation;

namespace Prototype.GameSystem
{
    /// <summary>
    /// 処理のための拡張した盤面を表すクラス
    /// </summary>
    class VirtualBoard
    {
        #region  [パブリック変数]
        /// <summary>
        /// ボード上のオブジェクト情報
        /// </summary>
        //private Square[,] m_Boad = new Square[8, 6];
        /// <summary>
        /// 仮
        /// </summary>
        private Object[,] m_Board = new Object[8, 6];

        private List<Ghost> p1GhostList = new List<Ghost>();
        private List<Ghost> p2GhostList = new List<Ghost>();


        #endregion


        #region　[アクセサ]
        /// <summary>
        /// Objectのアクセサ
        /// </summary>
        //public Square[,] M_Board
        //{
        //    set
        //    {
        //        this.m_Boad = value;
        //    }

        //    get
        //    {
        //        return this.m_Boad;
        //    }
        //}

        public Object[,] M_Board
        {
            set
            {
                this.m_Board = value;
            }

            get
            {
                return this.m_Board;
            }
        }

        public List<Ghost> P1ghostList
        {
            set
            {
                p1GhostList = value;
            }
            get
            {
                return p1GhostList;
            }
        }

        public List<Ghost> P2ghostList
        {
            get
            {
                return p2GhostList;
            }
            set
            {
                p2GhostList = value;
            }
        }
        #endregion

        #region [コンストラクタ]
        /// <summary>
        /// VirtualBoardコンストラクタ
        /// </summary>
        /// <param name="gtArray_1">1Pのゴースト初期配置</param>
        /// <param name="gtArray_2">2Pのゴースト初期配置</param>
        public VirtualBoard(GhostType[,] gtArray_1, GhostType[,] gtArray_2)
        {

            //1Pは下側
            for (int i = 0; i < gtArray_1.GetLength(0); i++)
            {
                for (int j = 0; j < gtArray_1.GetLength(1); j++)
                {
                    //初期位置に配置
                    if (gtArray_1[i, j] == GhostType.good)
                    {
                        //M_Board[i + 5, j + 1] = Square.P1Good;
                        P1ghostList.Add(new Ghost(GhostType.good, new Position(i + 5, j + 1)));
                    }
                    else if (gtArray_1[i, j] == GhostType.evil)
                    {
                        //M_Board[i + 5, j + 1] = Square.P1Evil;
                        P1ghostList.Add(new Ghost(GhostType.evil, new Position(i + 5, j + 1)));

                    }
                    else
                    {
                        //M_Board[i + 5, j + 1] = Square.Blank;
                    }
                }
            }

            //2Pは上側なので反転して代入
            for (int i = 0; i < gtArray_2.GetLength(0); i++)
            {
                for (int j = 0; j < gtArray_2.GetLength(1); j++)
                {
                    if (gtArray_2[i, j] == GhostType.good)
                    {
                        //M_Board[2 - i, 4 - j] = Square.P2Good;
                        P2ghostList.Add(new Ghost(GhostType.good, new Position(2 - i, 4 - j)));

                    }
                    else if (gtArray_2[i, j] == GhostType.evil)
                    {
                        //M_Board[2 - i, 4 - j] = Square.P2Evil;
                        P2ghostList.Add(new Ghost(GhostType.evil, new Position(2 - i, 4 - j)));
                    }
                    else
                    {
                        //M_Board[2 - i, 4 - j] = Square.Blank;
                    }

                }
            }

            resetGhostPostion();
            setGhostPostion();

        }
        #endregion

        #region [パブリックメソッド]
        public void setGhostPostion()
        {
            foreach (Ghost g in P1ghostList)
            {
                M_Board[g.P.X, g.P.Y] = Object.P1;
            }

            foreach (Ghost g in P2ghostList)
            {
                M_Board[g.P.X, g.P.Y] = Object.P2;
            }
        }
        public void resetGhostPostion()
        {
            for (int i = 0; i < M_Board.GetLength(0); i++)
            {
                for (int j = 0; j < M_Board.GetLength(1); j++)
                {
                    M_Board[i, j] = Object.blank;
                }
            }
        }


        public int GetSamePosGhostIndex(List<Ghost> glist, Position p)
        {
            int index = -1;
            foreach (Ghost g in glist)
            {
                if (g.P.X == p.X && g.P.Y == p.Y)
                {
                    index = glist.IndexOf(g);
                    break;
                }
            }
            return index;

            #endregion
        }

        public int GetGhostCount(Object o, GhostType gt)
        {
            int count = 0;

            if (o.Equals(Object.P1))
            {
                foreach (Ghost g in P1ghostList)
                {
                    if (g.Gt.Equals(gt))
                    {
                        count++;
                    }
                }
            }
            else if (o.Equals(Object.P2))
            {
                foreach (Ghost g in P2ghostList)
                {
                    if (g.Gt.Equals(gt))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public Boolean IsGhostAtExit(Object o)
        {
            //Ghostlistを検索し出口にいないかチェック
            if (o.Equals(Object.P1))
            {

                foreach (Ghost g in P1ghostList)
                {
                    if (g.P.X == 0 && g.P.Y == 0 || g.P.X == 0 && g.P.Y == 5)
                    {
                        return true;
                    }
                }
            }
            else if (o.Equals(Object.P2))
            {
                foreach (Ghost g in P2ghostList)
                {
                    if (g.P.X == 7 && g.P.Y == 0 || g.P.X == 7 && g.P.Y == 5)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

    }
}
