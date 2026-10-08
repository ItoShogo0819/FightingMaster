namespace FightingGame.Inputs
{
    /// <summary>
    /// 入力フレームをリングバッファ（FIFO）形式で一定フレーム分保持するクラス。
    /// コマンド判定や先行入力判定のために過去の入力を遡って取得する機能を提供します。
    /// </summary>
    public sealed class InputBuffer
    {
        private readonly InputFrame[] frames;
        private int writeIndex;
        private int count;

        /// <summary>現在バッファに格納されている有効なフレーム数</summary>
        public int Count => count;

        /// <summary>バッファが保持できる最大フレーム容量</summary>
        public int Capacity => frames.Length;

        /// <summary>
        /// 指定された容量で入力バッファのインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity">保持する最大フレーム数（デフォルトは120フレーム＝2秒分）</param>
        public InputBuffer(int capacity = 120)
        {
            frames = new InputFrame[capacity];
        }

        /// <summary>
        /// 最新の入力フレームをバッファに追加します。
        /// 最大容量を超えた場合は最も古いフレームが上書きされます。
        /// </summary>
        /// <param name="frame">追加する入力フレーム</param>
        public void Add(InputFrame frame)
        {
            frames[writeIndex] = frame;
            writeIndex = (writeIndex + 1) % frames.Length;

            if (count < frames.Length)
                count++;
        }

        /// <summary>
        /// 最新フレームから指定したインデックス分過去に遡った入力フレームを取得します。
        /// </summary>
        /// <param name="indexFromLatest">最新フレームからの遡り数（0が最新、1が1フレーム前）</param>
        /// <param name="frame">取得した入力フレームの出力先</param>
        /// <returns>指定されたインデックスのフレームが存在し、正常に取得できた場合は true</returns>
        public bool TryGetRecent(int indexFromLatest, out InputFrame frame)
        {
            if (indexFromLatest < 0 || indexFromLatest >= count)
            {
                frame = default;
                return false;
            }

            int index = writeIndex - 1 - indexFromLatest;

            if (index < 0)
                index += frames.Length;

            frame = frames[index];
            return true;
        }

        /// <summary>
        /// バッファに格納されているフレーム履歴をクリアします。
        /// </summary>
        public void Clear()
        {
            writeIndex = 0;
            count = 0;
        }
    }
}