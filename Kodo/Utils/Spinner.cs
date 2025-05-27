namespace Kodo.Utils
{
    public class Spinner : IDisposable
    {
        private readonly string[] _frames = [
            "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
            "██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
            "████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
            "██████▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
            "▒▒██████▒▒▒▒▒▒▒▒▒▒▒▒",
            "▒▒▒▒██████▒▒▒▒▒▒▒▒▒▒",
            "▒▒▒▒▒▒██████▒▒▒▒▒▒▒▒",
            "▒▒▒▒▒▒▒▒██████▒▒▒▒▒▒",
            "▒▒▒▒▒▒▒▒▒▒██████▒▒▒▒",
            "▒▒▒▒▒▒▒▒▒▒▒▒██████▒▒",
            "▒▒▒▒▒▒▒▒▒▒▒▒▒▒██████",
            "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒████",
            "▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██",
        ];

        private readonly string _message;
        private readonly int _delay;
        private int _frameIndex;
        private bool _active;
        private readonly Thread _thread;

        public Spinner(string message = "Processando", int delay = 100)
        {
            _message = message;
            _delay = delay;
            _frameIndex = 0;
            _active = true;

            _thread = new Thread(Spin);
            _thread.Start();
        }

        private void Spin()
        {
            while (_active)
            {
                Console.Write($"\r{_message}... {_frames[_frameIndex]}");
                _frameIndex = (_frameIndex + 1) % _frames.Length;
                Thread.Sleep(_delay);
            }
        }

        public void Dispose()
        {
            _active = false;
            _thread.Join();
            Console.WriteLine();
        }
    }

}
