using MetroSet_UI.Enums;
using System;
using System.Windows.Forms;

namespace MetroSet_UI.Animates
{
    // interpolation animate
    public abstract class Animate<T> : IDisposable
    {
        // interface for custom animate
        public delegate void UpdateEventHandler(T value);

        // update event for user to do anything they want
        public event UpdateEventHandler Update;

        #region Internal Vars
        // a bad way to record time...
        private DateTime _lastUpdateTime;
        // I use timer instead of thread, so you can modify control without Control.Invoke
        private Timer _animateTimer;
        // reverse animate
        private bool _reverse;
        // ......
        protected EventHandler _complated;
        #endregion

        #region Constructors
        // choose best interval for yourself
        public Animate(int updateInterval = 16)
        {
            _animateTimer = new Timer()
            {
                Interval = updateInterval,
                Enabled = false,
            };
            _animateTimer.Tick += this.OnFrameUpdate;
            _reverse = false;
            Alpha = 0.0;
        }
        #endregion

        #region Functions
        // just set once, and use start, back or reverse to play animate
        public void Setting(int duration, T initial, T end, EasingType easing = EasingType.Linear, EventHandler complated = null)
        {
            InitialValue = initial;
            EndValue = end;
            EasingType = easing;
            Duration = duration;
            _complated = complated;
        }

        // start animate with default setting
        public void Start()
        {
            _reverse = false;
            Alpha = 0.0;
            Play();
        }

        // back animate with default setting
        public void Back()
        {
            _reverse = true;
            Alpha = 1.0;
            Play();
        }

        // start animate with default setting
        public void Start(int duration)
        {
            _reverse = false;
            Alpha = 0.0;
            Duration = duration;
            Play();
        }

        // back animate with default setting
        public void Back(int duration)
        {
            _reverse = true;
            Alpha = 1.0;
            Duration = duration;
            Play();
        }

        // reverse animate
        public void Reverse()
        {
            Reverse(!_reverse);
        }

        // reverse animate
        public void Reverse(bool val)
        {
            _reverse = val;

            if (!Active)
                Play();
        }

        // play animate
        public void Play()
        {
            _lastUpdateTime = DateTime.Now;
            Active = true;
            _animateTimer.Enabled = true;
            _animateTimer.Start();
        }

        public void Pause()
        {
            _animateTimer.Stop();
            _animateTimer.Enabled = false;
            Active = false;
        }

        public void Stop()
        {
            Pause();
            Alpha = _reverse ? 1.0 : 0.0;
        }

        // start animate with specific setting
        public void Start(int duration, T initial, T end, EasingType easing = EasingType.Linear, EventHandler callback = null)
        {
            Setting(duration, initial, end, easing, callback);
            Start();
        }

        // back animate with specific setting
        public void Back(int duration, T initial, T end, EasingType easing = EasingType.Linear, EventHandler callback = null)
        {
            Setting(duration, initial, end, easing, callback);
            Back();
        }
        #endregion

        #region Events
        // process frame
        private void OnFrameUpdate(object sender, EventArgs e)
        {
            DateTime updateTime = DateTime.Now;
            double elapsed;

            if (Duration == 0)
                elapsed = 1.0;
            else
                elapsed = (updateTime - _lastUpdateTime).TotalMilliseconds / Duration;

            _lastUpdateTime = updateTime;
            Alpha = Math.Max(0.0, Math.Min(Alpha + (_reverse ? -elapsed : elapsed), 1.0));

            if (Update != null)
                Update.Invoke(Value);

            if (Alpha == 0.0 || Alpha == 1.0)
            {
                Pause();
                if (_complated != null)
                    _complated.Invoke(this, null);
            }
        }
        #endregion

        #region Properties
        // progress. value between 0 and 1
        public double Alpha { get; set; }

        // animate duration 
        //     recorded for calculating elapsed alpha
        //     if you use reverse animate when animate avtiving
        //     the real duration will different with the duration you set
        public int Duration { get; set; }

        // initial state of value
        public T InitialValue { get; private set; }

        public abstract T Value { get; }

        // final state of value
        public T EndValue { get; private set; }

        // easing type
        public EasingType EasingType { get; private set; }

        // active if the animate is running
        public bool Active { get; private set; }

        // store you own variable here
        public object Tag { get; set; }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _animateTimer.Dispose();
        }
        #endregion
    }
}
