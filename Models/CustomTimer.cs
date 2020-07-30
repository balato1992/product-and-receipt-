using System;
using System.Timers;

namespace product_and_receipt.Models
{
    public class CustomTimer
    {
        public double Interval => LocalTimer.Interval;
        public bool TimerEnabled => LocalTimer.Enabled;
        public int CountOfElapsed { get; private set; }

        protected Timer LocalTimer { get; set; }
        public string Tag { get; protected set; }
        protected event Action PoolingAction;

        // avoid timer event excute at same time
        protected object _ElapsedLocker = new object();
        private bool IsRunning { get; set; }



        /// <summary>
        /// Return Tag and Exception
        /// </summary>
        public event Action<string, Exception> ElapsedException;

        public CustomTimer(string tag, int interval, params Action[] actions)
        {
            Tag = tag;
            CountOfElapsed = 0;

            LocalTimer = new Timer();
            LocalTimer.Enabled = false;
            LocalTimer.Elapsed += TimeElapsedEvent;
            LocalTimer.Interval = interval;

            if (actions != null)
            {
                foreach (Action action in actions)
                {
                    PoolingAction += action;
                }
            }
        }

        public void StartTimer()
        {
            LocalTimer.Enabled = true;
        }
        public void StopTimer()
        {
            LocalTimer.Enabled = false;
        }

        private void TimeElapsedEvent(object source, ElapsedEventArgs e)
        {
            lock (_ElapsedLocker)
            {
                if (IsRunning)
                {
                    return;
                }
                IsRunning = true;
            }

            try
            {
                PoolingAction?.Invoke();
            }
            catch (Exception ex)
            {
                ElapsedException?.Invoke(Tag, ex);
            }
            finally
            {
                lock (_ElapsedLocker)
                {
                    IsRunning = false;
                }

                CountOfElapsed += 1;
                if (CountOfElapsed == int.MaxValue)
                {
                    CountOfElapsed = 0;
                }
            }
        }
    }

}
