﻿﻿using System;
using System.Collections.Generic;
using System.Text;
using Prototype.GameInformation;
using System.Diagnostics;

namespace Prototype.GameSystem
{
    /// <summary>
    /// 処理のための拡張した盤面を表すクラス
    /// </summary>
    class GameState 
    {
        
        #region  [パブリック変数]
        /// <summary>
        /// ボード上のオブジェクト情報
        /// </summary>
        //private Square[,] m_Boad = new Square[8, 6];
        /// <summary>
        /// 仮
        /// </summary>
        private FieldObject[,] m_Board = new FieldObject[8, 6];
        private GhostType[,] board = new GhostType[6, 6];
        private FieldObject[,] boardState = new FieldObject[6, 6];


        private List<Ghost> p1GhostList = new List<Ghost>();
        private List<Ghost> p2GhostList = new List<Ghost>();

        //protected int maxTurn;
        public Move currentPlayerMove = null;
        public FieldObject currentPlayer = FieldObject.P1;
        public FieldObject notcurrentPlayer = FieldObject.P2;

        private const int THINKTIME = 500;
        private int turnNum = 0;

        #endregion


        #region [アクセサ]

        public FieldObject[,] M_Board
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

        public GhostType[,] Board
        {
            set
            {
                this.board = value;
            }

            get
            {
                return board;
            }
        }

        public FieldObject[,] BoardState
        {
            set { this.boardState = value; }
            get { return this.boardState; }


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

        public Move CurrentPlayerMove
        {
            get { return this.currentPlayerMove; }
            set { this.currentPlayerMove = value; }
        }

        public FieldObject CurrentPlayer
        {
            get { return this.currentPlayer; }
            set { this.currentPlayer = value; }

        }

        public FieldObject NotCurrentPlayer
        {
            get { return this.notcurrentPlayer; }
            set { this.notcurrentPlayer = value; }
        }

        public int TurnNum
        {
            get { return this.turnNum; }
            set { this.turnNum = value; }
        }



        #endregion

        #region [コンストラクタ]
        /// <summary>
        /// VirtualBoardコンストラクタ
        /// </summary>
        /// <param name="gtArray_1">1Pのゴースト初期配置</param>
        /// <param name="gtArray_2">2Pのゴースト初期配置</param>
        public GameState(GhostAttribute[,] gtArray_1, GhostAttribute[,] gtArray_2)
        {

            //1Pは下側
            for (int i = 0; i < gtArray_1.GetLength(0); i++)
            {
                for (int j = 0; j < gtArray_1.GetLength(1); j++)
                {
                    //初期位置に配置
                    if (gtArray_1[i, j] == GhostAttribute.good)
                    {
                        //M_Board[i + 5, j + 1] = Square.P1Good;
                        P1ghostList.Add(new Ghost(GhostAttribute.good, new Position(i + 5, j + 1)));
                    }
                    else if (gtArray_1[i, j] == GhostAttribute.evil)
                    {
                        //M_Board[i + 5, j + 1] = Square.P1Evil;
                        P1ghostList.Add(new Ghost(GhostAttribute.evil, new Position(i + 5, j + 1)));

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
                    if (gtArray_2[i, j] == GhostAttribute.good)
                    {
                        //M_Board[2 - i, 4 - j] = Square.P2Good;
                        P2ghostList.Add(new Ghost(GhostAttribute.good, new Position(2 - i, 4 - j)));

                    }
                    else if (gtArray_2[i, j] == GhostAttribute.evil)
                    {
                        //M_Board[2 - i, 4 - j] = Square.P2Evil;
                        P2ghostList.Add(new Ghost(GhostAttribute.evil, new Position(2 - i, 4 - j)));
                    }
                    else
                    {
                        //M_Board[2 - i, 4 - j] = Square.Blank;
                    }

                }
            }

            ResetGhostPostion();
            SetGhostPostionInVirtual();
            SetBoardState();
            ResetGhostPositionInBoard();
            SetGhostPositionInBoard();

        }
        #endregion

        #region [パブリックメソッド]
        public void SetGhostPostionInVirtual()
        {
            foreach (Ghost g in P1ghostList)
            {
                M_Board[g.P.X, g.P.Y] = FieldObject.P1;
            }

            foreach (Ghost g in P2ghostList)
            {
                M_Board[g.P.X, g.P.Y] = FieldObject.P2;
            }
        }
        /// <summary>
        /// Sets the state of the board.
        /// </summary>
        public void SetBoardState()
        {
            for (int i = 0; i < BoardState.GetLength(0); i++)
            {
                for (int j = 0; j < BoardState.GetLength(1); j++)
                {
                    BoardState[i, j] = M_Board[i + 1, j];
                }

            }
        }


        public void SetGhostPositionInBoard()
        {
            //P1リスト
            foreach (Ghost g in P1ghostList)
            {
                //vb->b変換
                int x = g.P.X - 1;
                int y = g.P.Y;

                //例外処理
                if (x < 0 || y < 0 || x > 5 || y > 5)
                {

                }
                else
                {
                    if (g.Gt.Equals(GhostAttribute.evil))
                    {
                        Board[x, y] = GhostType.P1GhostEvil;
                    }
                    else if (g.Gt.Equals(GhostAttribute.good))
                    {
                        Board[x, y] = GhostType.P1GhostGood;
                    }
                    else
                    {
                        Board[x, y] = GhostType.Blank;
                    }

                }
            }

            foreach (Ghost g in P2ghostList)
            {
                //vb->b変換
                int x = g.P.X - 1;
                int y = g.P.Y;

                //例外処理
                if (x < 0 || y < 0 || x > 5 || y > 5)
                {
                    Debug.WriteLine("out of Board");
                }
                else
                {
                    if (g.Gt.Equals(GhostAttribute.evil))
                    {
                        Board[x, y] = GhostType.P2GhostEvil;
                    }
                    else if (g.Gt.Equals(GhostAttribute.good))
                    {

                        Board[x, y] = GhostType.P2GhostGood;
                    }
                    else
                    {
                        Board[x, y] = GhostType.Blank;
                    }

                }
            }

        }




        public void ResetGhostPostion()
        {
            for (int i = 0; i < M_Board.GetLength(0); i++)
            {
                for (int j = 0; j < M_Board.GetLength(1); j++)
                {
                    M_Board[i, j] = FieldObject.blank;
                }
            }
        }

        public void ResetGhostPositionInBoard()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    Board[i, j] = GhostType.Blank;
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

        public int GetGhostCount(FieldObject o, GhostAttribute gt)
        {
            int count = 0;

            if (o.Equals(FieldObject.P1))
            {
                foreach (Ghost g in P1ghostList)
                {
                    if (g.Gt.Equals(gt))
                    {
                        count++;
                    }
                }
            }
            else if (o.Equals(FieldObject.P2))
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

        public Boolean IsGhostAtExit(FieldObject o)
        {
            //Ghostlistを検索し出口にいないかチェック
            if (o.Equals(FieldObject.P1))
            {

                foreach (Ghost g in P1ghostList)
                {
                    if (g.P.X == 0 && g.P.Y == 0 || g.P.X == 0 && g.P.Y == 5)
                    {
                        P1ghostList.Remove(g);
                        if (g.Gt.Equals((GhostAttribute.good)))
                        {
                            return true;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }
            else if (o.Equals(FieldObject.P2))
            {
                foreach (Ghost g in P2ghostList)
                {
                    if (g.P.X == 7 && g.P.Y == 0 || g.P.X == 7 && g.P.Y == 5)
                    {
                        P2ghostList.Remove(g);

                        if (g.Gt.Equals((GhostAttribute.good)))
                        {
                            return true;
                        }
                        else
                        {
                            break;
                        }
                    }

                }

            }

            return false;
        }

        public GameState Clone()
        {
            GameState cloned = (GameState)MemberwiseClone();
            if (this.Board != null)
            {
                cloned.Board = (GhostType[,])this.Board.Clone();
            }
            if (this.M_Board != null)
            {
                cloned.M_Board = (FieldObject[,])this.M_Board.Clone();
            }
            if(this.boardState!=null){
                cloned.BoardState = (FieldObject[,])this.BoardState.Clone();
            }

            if (this.P1ghostList != null)
                {
                List<Ghost> tmp = new List<Ghost>();   
                foreach(Ghost g in this.P1ghostList)
                {
                    tmp.Add(g.Clone());   
                }
                cloned.P1ghostList = tmp;
                }

            if (this.P2ghostList != null)
            {

                List<Ghost> tmp = new List<Ghost>();   
                foreach(Ghost g in this.P2ghostList)
                {
                    tmp.Add(g.Clone());   
                }
                cloned.P2ghostList = tmp;
                }
			
            return cloned;
        }

        public int ThinkTime
        {
            get { return THINKTIME; }
        }

    }
}
