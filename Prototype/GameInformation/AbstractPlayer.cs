using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.GameInformation
{

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
        protected GhostType[,] initialPlacement = new GhostType[2, 4];
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

        public GhostType[,] InitialPlacement
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
        /// <summary>
        /// ゴーストの初期配置を設定するメソッド
        /// </summary>
        /// <param name="init">ゴーストの2次元配列</param>
        public void SetInitialPlacement(GhostType[,] init)
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
