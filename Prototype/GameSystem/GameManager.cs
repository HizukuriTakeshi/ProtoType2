using System;
using System.Collections.Generic;
using System.Text;
using Prototype.GameInformation;
using Prototype.GameSystem;

namespace Prototype.GameSystem
{
    class GameManager
    {
        #region [フィールド]
        public VirtualBoard Vb
        {
            get;
            set;
        }

        public AbstractPlayer P1
        {
            get;
            set;
        }

        public AbstractPlayer P2
        {
            get;
            set;
        }

        public int FinalTurn
        {
            get;
            set;
        }

        public int Turn
        {
            get;
            set;
        }

        public Object TurnPlayer
        {
            get;
            set;
        }

        public Object NotTurnPlayer
        {
            get;
            set;
        }

        public Square Good
        {
            get;
            set;
        }

        public Square Evil
        {
            get;
            set;
        }

        public int P1_GoodGhostNum
        {
            get;
            set;
        }

        public int P1_EvilGhostNum
        {
            get;
            set;
        }

        public int P2_GoodGhostNum
        {
            get;
            set;
        }

        public int P2_EvilGhostNum
        {
            get;
            set;
        }

        #endregion


        #region [コンストラクタ]
        public GameManager(AbstractPlayer p1, AbstractPlayer p2, int finalturn)
        {
            P1 = p1;
            P2 = p2;
            FinalTurn = finalturn;
            Turn = 0;
            TurnPlayer = Object.P1;
            NotTurnPlayer = Object.P2;

            P1.PlayerNumber = 1;
            P2.PlayerNumber = -1;

            Evil = Square.P1Evil;
            Good = Square.P1Good;

            P1_EvilGhostNum = 4;
            P1_GoodGhostNum = 4;

            P2_EvilGhostNum = 4;
            P2_GoodGhostNum = 4;

            Vb = new VirtualBoard(P1.InitialPlacement, P2.InitialPlacement);
        }
        #endregion

        #region [メソッド]
        public void NextTurn()
        {

            if ((Turn % 2).Equals(1))
            {
                Evil = Square.P1Evil;
                Good = Square.P1Good;
                TurnPlayer = Object.P1;
                NotTurnPlayer = Object.P2;
                Console.WriteLine("Next Turn:1");
            }
            else
            {
                Evil = Square.P2Evil;
                Good = Square.P2Good;
                TurnPlayer = Object.P2;
                NotTurnPlayer = Object.P1;
                Console.WriteLine("Next Turn:2");
            }
            Turn++;
        }


        public void MoveGhost(Move m)
        {
            if (TurnPlayer.Equals(Object.P1))
            {
                //Borad変換

                //移動可能性
                if (IsGhostMovable(m))
                {

                    //移動(ゴーストリストのpositionを書き換える)
                    //移動先に相手のゴーストがいるときはそのゴーストをリストから消す
                    int _tmp;
                    switch (m.GhostM)
                    {

                        case GhostMove.Down:
                            //xとｙがm.posと同じリストの中の要素のインデックスを一つ取得する
                            Vb.P1ghostList[Vb.GetSamePosGhostIndex(Vb.P1ghostList, m.Pos)].P = new Position(m.Pos.X + 1, m.Pos.Y);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P2ghostList, new Position(m.Pos.X + 1, m.Pos.Y));
                            if (_tmp >= 0)
                            {
                                Vb.P2ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X + 1, m.Pos.Y] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;

                        case GhostMove.Left:
                            Vb.P1ghostList[Vb.GetSamePosGhostIndex(Vb.P1ghostList, m.Pos)].P = new Position(m.Pos.X, m.Pos.Y - 1);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P2ghostList, new Position(m.Pos.X, m.Pos.Y - 1));
                            if (_tmp >= 0)
                            {
                                Vb.P2ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X, m.Pos.Y - 1] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;

                        case GhostMove.Right:
                            Vb.P1ghostList[Vb.GetSamePosGhostIndex(Vb.P1ghostList, m.Pos)].P = new Position(m.Pos.X, m.Pos.Y + 1);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P2ghostList, new Position(m.Pos.X, m.Pos.Y + 1));
                            if (_tmp >= 0)
                            {
                                Vb.P2ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X, m.Pos.Y + 1] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;
                        case GhostMove.Up:
                            Vb.P1ghostList[Vb.GetSamePosGhostIndex(Vb.P1ghostList, m.Pos)].P = new Position(m.Pos.X - 1, m.Pos.Y);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P2ghostList, new Position(m.Pos.X - 1, m.Pos.Y));
                            if (_tmp >= 0)
                            {
                                Vb.P2ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X - 1, m.Pos.Y] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;
                        default:
                            break;
                    }

                }

            }
            else
            {
                //Borad変換

                //移動可能性
                if (IsGhostMovable(m))
                {

                    //移動(ゴーストリストのpositionを書き換える)
                    //移動先に相手のゴーストがいるときはそのゴーストをリストから消す
                    int _tmp;
                    switch (m.GhostM)
                    {

                        case GhostMove.Down:
                            //xとｙがm.posと同じリストの中の要素のインデックスを一つ取得する
                            Vb.P2ghostList[Vb.GetSamePosGhostIndex(Vb.P2ghostList, m.Pos)].P = new Position(m.Pos.X + 1, m.Pos.Y);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P1ghostList, new Position(m.Pos.X + 1, m.Pos.Y));
                            if (_tmp >= 0)
                            {
                                Vb.P1ghostList.RemoveAt(_tmp);

                            }
                            //Vb.M_Board[m.Pos.X + 1, m.Pos.Y] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;

                        case GhostMove.Left:
                            Vb.P2ghostList[Vb.GetSamePosGhostIndex(Vb.P2ghostList, m.Pos)].P = new Position(m.Pos.X, m.Pos.Y - 1);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P1ghostList, new Position(m.Pos.X, m.Pos.Y - 1));
                            if (_tmp >= 0)
                            {
                                Vb.P1ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X, m.Pos.Y - 1] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;

                        case GhostMove.Right:
                            Vb.P2ghostList[Vb.GetSamePosGhostIndex(Vb.P2ghostList, m.Pos)].P = new Position(m.Pos.X, m.Pos.Y + 1);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P1ghostList, new Position(m.Pos.X, m.Pos.Y + 1));
                            if (_tmp >= 0)
                            {
                                Vb.P1ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X, m.Pos.Y + 1] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;
                        case GhostMove.Up:
                            Vb.P2ghostList[Vb.GetSamePosGhostIndex(Vb.P2ghostList, m.Pos)].P = new Position(m.Pos.X - 1, m.Pos.Y);
                            _tmp = Vb.GetSamePosGhostIndex(Vb.P1ghostList, new Position(m.Pos.X - 1, m.Pos.Y));
                            if (_tmp >= 0)
                            {
                                Vb.P1ghostList.RemoveAt(_tmp);
                            }
                            //Vb.M_Board[m.Pos.X - 1, m.Pos.Y] = Vb.M_Board[m.Pos.X, m.Pos.Y];
                            break;
                        default:
                            break;
                    }

                }
            }

            Vb.resetGhostPostion();
            Vb.setGhostPostion();

        }

        /// <summary>
        /// ゴーストが移動できるか判定
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public Boolean IsGhostMovable(Move m)
        {
            ///ゴーストがいるか判定
            if (GhostExists(m.Pos))
            {
                //下に移動するとき
                if (m.GhostM.Equals(GhostMove.Down))
                {
                    //移動先が盤面内か
                    if (GhostIsInBoard(new Position(m.Pos.X + 1, m.Pos.Y)) || IsExit(m.Pos))
                    {
                        //移動先に自分のゴーストがいないか
                        if (!GhostExists(new Position(m.Pos.X + 1, m.Pos.Y)))
                        {
                            return true;
                        }
                    }

                }

                //左に移動するとき
                if (m.GhostM.Equals(GhostMove.Left))
                {
                    //移動先が盤面内か
                    if (GhostIsInBoard(new Position(m.Pos.X, m.Pos.Y - 1)) || IsExit(m.Pos))
                    {
                        //移動先に自分のゴーストがいないか
                        if (!GhostExists(new Position(m.Pos.X, m.Pos.Y - 1)))
                        {
                            Console.WriteLine("here");
                            return true;
                        }
                    }

                }

                //右に移動するとき
                if (m.GhostM.Equals(GhostMove.Right))
                {
                    //移動先が盤面内か
                    if (GhostIsInBoard(new Position(m.Pos.X, m.Pos.Y + 1)) || IsExit(m.Pos))
                    {
                        //移動先に自分のゴーストがいないか
                        if (!GhostExists(new Position(m.Pos.X, m.Pos.Y + 1)))
                        {
                            return true;
                        }
                    }

                }

                //上に移動するとき
                if (m.GhostM.Equals(GhostMove.Up))
                {
                    //移動先が盤面内か
                    if (GhostIsInBoard(new Position(m.Pos.X - 1, m.Pos.Y)) || IsExit(m.Pos))
                    {
                        //移動先に自分のゴーストがいないか
                        if (!GhostExists(new Position(m.Pos.X - 1, m.Pos.Y)))
                        {
                            return true;
                        }
                    }

                }

            }

            Console.WriteLine("already exist:{0} {1}", m.Pos.X, m.Pos.Y);
            return false;
        }

        /// <summary>
        /// 引数のpositionにターンプレイヤーのゴーストがいるか判定
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Boolean GhostExists(Position p)
        {
            //pが不正な値でないか判定
            if (GhostIsInBoard(p))
            {
                //位置pにゴーストが存在するか判定
                if (Vb.M_Board[p.X, p.Y].Equals(TurnPlayer))
                {
                    return true;
                }
            }
            Console.WriteLine("Ghost not exist:{0} {1}", p.X, p.Y);
            return false;
        }

        /// <summary>
        /// pが不正な値でないか判定
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Boolean GhostIsInBoard(Position p)
        {

            //盤面内にあるとき
            //test
            if ((0 < p.X && p.X < 7) && (0 <= p.Y && p.Y <= 5))
            {
                return true;
            }

            if (TurnPlayer.Equals(Object.P1))
            {


                if (p.X == 0 && p.Y == 0 || p.X == 0 && p.Y == 5)
                {
                    return true;
                }

            }
            else if (TurnPlayer.Equals(Object.P2))
            {
                if (p.X == 7 && p.Y == 0 || p.X == 7 && p.Y == 5)
                {
                    return true;
                }


            }

            Console.WriteLine("out of Board:{0} {1}", p.X, p.Y);
            return false;
        }
        public Boolean IsExit(Position p)
        {
            ////出口にあるとき
            if ((p.X.Equals(0) && p.Y.Equals(0)) || (p.X.Equals(0) && p.Y.Equals(5)) || (p.X.Equals(7) && p.Y.Equals(5)) || (p.X.Equals(7) && p.Y.Equals(0)))
            {
                return true;
            }
            return false;
        }

        public Boolean VorDCheck()
        {
            //ゲームの終了条件を確認
            //ゴースト数での終了条件
            Console.WriteLine("good_{0} {1}", Vb.GetGhostCount(NotTurnPlayer, GhostType.good), NotTurnPlayer);
            if (Vb.GetGhostCount(NotTurnPlayer, GhostType.good).Equals(0))
            {
                Console.WriteLine("{0} {1}", NotTurnPlayer, Vb.GetGhostCount(NotTurnPlayer, GhostType.good));
                Console.WriteLine("{0} Win!", TurnPlayer);
                return true;
            }

            Console.WriteLine("evil_{0} {1}", Vb.GetGhostCount(NotTurnPlayer, GhostType.evil), NotTurnPlayer);
            if (Vb.GetGhostCount(NotTurnPlayer, GhostType.evil).Equals(0))
            {
                Console.WriteLine("{0} Win!", NotTurnPlayer);
                return true;
            }

            //ゴーストの位置での終了条件
            //Vb.IsGhostAtExit(TurnPlayer);
            return false;
        }


        /// <summary>
        /// コンソール画面にボード情報を出力
        /// </summary>
        public void DisplayVirtualBoard()
        {
            for (int i = 0; i < Vb.M_Board.GetLength(1); i++)
            {
                Console.Write("{0,11} ", i);
            }
            Console.WriteLine();
            for (int i = 0; i < Vb.M_Board.GetLength(0); i++)
            {
                Console.Write("{0} ", i);
                for (int j = 0; j < Vb.M_Board.GetLength(1); j++)
                {
                    Console.Write("{0,11} ", Vb.M_Board[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// テスト用Move入力関数(コンソールから)
        /// </summary>
        public Move tmpMove()
        {
            GhostMove gm = new GhostMove();

            Console.WriteLine("x");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("y");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine("gm");
            string gmString = Console.ReadLine();

            switch (gmString)
            {
                case "u":
                    gm = GhostMove.Up;
                    break;

                case "d":
                    gm = GhostMove.Down;
                    break;

                case "l":
                    gm = GhostMove.Left;
                    break;

                case "r":
                    gm = GhostMove.Right;
                    break;
                default:
                    gm = GhostMove.Up;
                    break;
            }

            return new Move(new Position(x, y), gm);
        }
        #endregion


    }
}
