namespace FightingGame.Inputs
{
    /// <summary>
    /// 入力フレームをバッファに格納するクラス
    /// </summary>
    public sealed class InputBuffer
    {
        private readonly InputFrame[] frames;
        private int writeIndex;
        private int count;

        public int Count => count;
        public int Capacity => frames.Length;

        public InputBuffer(int capacity = 120)
        {
            frames = new InputFrame[capacity];
        }

        /// <summary>
        /// 入力フレームをバッファに追加する
        /// </summary>
        /// <param name="frame"></param>
        public void Add(InputFrame frame)
        {
            frames[writeIndex] = frame;
            writeIndex = (writeIndex + 1) % frames.Length;      // 次の書き込み位置を更新する

            if (count < frames.Length)                          // バッファが満杯でない場合はカウントを増やす
                count++;
        }

        /// <summary>
        /// 指定されたインデックスの入力フレームを取得する
        /// </summary>
        /// <param name="indexFromLatest"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
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

        public void Clear()
        {
            writeIndex = 0;
            count = 0;
        }
    }
}
