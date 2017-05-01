using System;
using System.Collections.Generic;
using System.Text;
using Prototype.GameSystem;

namespace Prototype.GameInformation
{
    //test
    abstract class AbstractPlayer
    {
        #region [フィールド]
        /// <summary>
        /// プレイヤーの名前
        /// </summary>
        protected string name;
        /// <summary>
        /// プレイヤーの番号
        /// </summary>
        public int playerNumber;
        /// <summary>
        /// ゴーストの初期配置
        /// </summary>
        protected GhostAttribute[,] initialPlacement = new GhostAttribute[2, 4];

        private GameState gameState;

        #endregion

        #region [ アクセサ ]
        /// <summary>
        /// プレイヤー名を取得します
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public int PlayerNumber
        {
            get
            {
                return playerNumber;
            }

            set
            {
                playerNumber = value;
            }
        }

        public GhostAttribute[,] InitialPlacement
        {
            set
            {
                this.initialPlacement = value;
            }

            get
            {
                return this.initialPlacement;
            }
        }

        #endregion

        #region [コンストラクタ]
        /// <summary>
        /// コンストラクタ 名前表示がNonameになります
        /// </summary>
        public AbstractPlayer()
        {
            Name = "Noname";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">表示したい名前</param>
        public AbstractPlayer(string name)
        {
            Name = name;
        }
        #endregion

        #region [パブリックメソッド]

        public void SetGameState(GameState gameState)
        {
            this.gameState = gameState.Clone();
        }

        /// <summary>
        /// 盤面上のゴーストの位置を取得する
        /// </summary>
        /// <returns></returns>
        public FieldObject[,] GetBoardState()
        {
            return gameState.BoardState;
        }

        /// <summary>
        /// 自身のFieldObjectにおける値を取得する
        /// </summary>
        /// <returns>
        /// Type:FieldObject
        /// P1 or P2
        /// </returns>
        public FieldObject GetMyPlayerID()
        {
            return gameState.currentPlayer;
        }

        /// <summary>
        /// 自身のGhostのリストを取得する
        /// </summary>
        /// <returns>
        /// Type:List<Ghost>
        /// </returns>
        public List<Ghost> GetMyGhostList()
        {
            if (gameState.currentPlayer.Equals(FieldObject.P1))
            {
                return gameState.P1ghostList;
            }
            else
            if (gameState.currentPlayer.Equals(FieldObject.P2))
            {
                return gameState.P2ghostList;
            }

            return null;
        }

        /// <summary>
        /// 指定した属性のゴーストのリストを取得
        /// </summary>
        /// <param name="ga"></param>
        /// <returns></returns>
        public List<Ghost> GetMyGoodGhostList(GhostAttribute ga)
        {
            List<Ghost> glist = new List<Ghost>();

            //getmyghostlistをループ
            foreach (Ghost g in GetMyGhostList())
            {
                if (g.Gt.Equals(ga))
                {
                    glist.Add(g);
                }
            }
            return glist;
        }

        /// <summary>
        /// 指定した位置のゴーストの初期位置を取得
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Position GetInitialPlacement(Position p)
        {
            foreach (Ghost g in gameState.P1ghostList)
            {
                if (g.P.X.Equals(p.X) && g.P.Y.Equals(p.Y))
                {
                    return g.InitPos;
                }
            }

            foreach (Ghost g in gameState.P2ghostList)
            {
                if (g.P.X.Equals(p.X) && g.P.Y.Equals(p.Y))
                {
                    return g.InitPos;
                }
            }

            return null;
        }

        /// <summary>
        /// ゴーストの初期配置を設定するメソッド
        /// </summary>
        /// <param name="init">ゴーストの2次元配列</param>
        public void SetInitialPlacement(GhostAttribute[,] init)
        {
            this.initialPlacement = init;
        }

        /// <summary>
        /// 移動させるゴーストと方向を決定するメソッド
        /// </summary>
        /// <returns>移動させるゴーストの位置と方向のメンバを持つクラス Move</returns>
        public abstract Move GetMove();


        #endregion
    }


}
