using System;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using GestureRecognition;
using Leap;
using PatrickRampage.Metier;
using Frame = Leap.Frame;

namespace PatrickRampage
{


    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ILeapEventDelegate
    {
        /// <summary>
        /// LeapMotion controller
        /// </summary>
        private Controller _controller;

        /// <summary>
        /// Listener used to translate signal sent by the LeapMotion
        /// </summary>
        private LeapEventListener _listener;

        /// <summary>
        /// Used to determine if the window is closing or not.
        /// </summary>
        private Boolean _isClosing;

        /// <summary>
        /// Bouding rectangle of the crosshair
        /// </summary>
        private Rect _crossRect;

        /// <summary>
        /// Music player. Playing the shot sound by default.
        /// </summary>
        private SoundPlayer _player = new SoundPlayer("..\\..\\Laser_Shoot2.wav");

        /// <summary>
        /// Jellyfish fabric initialized in XAML.
        /// </summary>
        private FabJellyfish fabJellyfish;

        /// <summary>
        /// Used to represent if the game is playing or paused.
        /// </summary>
        public bool Playing
        {
            get { return _mPlaying; }
            set
            {
                _mPlaying = value;
                if (!fabJellyfish.Timer.Enabled && value)
                {
                    fabJellyfish.Timer.Start();
                }
                if (!fabJellyfish.Timer.Enabled && !value)
                {
                    fabJellyfish.Timer.Stop();
                }
                PauseGrid.IsEnabled = !value;
                PauseGrid.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }
        private bool _mPlaying;


        /// <summary>
        /// Used to keep track of the score and display it on the screen.
        /// </summary>
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                ScoreTextBlock.Text = value.ToString();
            }
        }
        private int _score;


        /// <summary>
        /// Window and attributes initilization.
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();

            _controller = new Controller();
            _listener = new LeapEventListener(this);
            _controller.AddListener(_listener);
            GestureEngine.GestureRecognized += engine_GestureRecognized;

            fabJellyfish = Resources["Jellyfishes"] as FabJellyfish;
            _crossRect = new Rect((double)CrossHair.GetValue(Canvas.LeftProperty), (double)CrossHair.GetValue(Canvas.BottomProperty), CrossHair.Width, CrossHair.Height);

            Playing = false;
        }


        /// <summary>
        /// EventHandler of the GestureRecognized event thrown by the GestureEngine
        /// </summary>
        /// <param name="sender">Gesture or posture that sent the event</param>
        /// <param name="e"></param>
        private void engine_GestureRecognized(object sender, GestureRecognizedEventArgs e)
        {

            string name = sender.GetType().Name.ToUpper();
            if (Playing)
            {
                switch (name)
                {
                    case "SHOOT":
                        Shoot(_controller.Frame());
                        break;
                    case "HANDLIFTED":
                        Jump(_controller.Frame());
                        break;
                    case "HANDMISSING":
                        Playing = false;
                        break;
                    case "INITSHOOT":
                        MoveCrossHair(_controller.Frame());
                        break;
                }
            }
            else
            {
                if (name == "INITPOSTURE")
                {
                    Playing = true;
                }
            }
        }

        /// <summary>
        /// Makes Patrick jump. Not implemented yet.
        /// </summary>
        /// <param name="frame">Frame captured by the LeapMotion</param>
        private void Jump(Frame frame)
        {
            Debug.WriteLine("Jump!");
        }

        /// <summary>
        /// Shoot using the crosshair position on any overlapping jellyfish
        /// </summary>
        /// <param name="frame">Frame captured by the LeapMotion</param>
        private void Shoot(Frame frame)
        {
            // Play the shot sound
            _player.Play();

            // Remove jellyfish from game when the player's crosshair rectangle is intersecting
            // with the jellyfish rectangle.
            Jellyfish jellyToRemove = null;
            if (fabJellyfish == null) return;
            foreach (Jellyfish jellyfish in fabJellyfish.JellyFishes)
            {
                if (_crossRect.IntersectsWith(jellyfish.RectJelly))
                {
                    jellyToRemove = jellyfish;
                }
            }
            if (jellyToRemove == null) return;
            fabJellyfish.JellyFishes.Remove(jellyToRemove);
            Score++;
        }

        /// <summary>
        /// Stop the Game and display the Lose panel.
        /// </summary>
        private void StopGame()
        {
            Playing = false;
            fabJellyfish.JellyFishes.Clear();
            LooseTextBlock.Text = string.Format("You lost with a score of :{0}", Score);
            LoseGrid.IsEnabled = true;
            LoseGrid.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Move Patrick using the user right palm position.
        /// </summary>
        /// <param name="frame">Frame captured by the LeapMotion</param>
        private void MovePatrick(Frame frame)
        {
            Hand rightHand = HandGetter.GetRightHand(frame);
            if (rightHand == null) return;
            double posLeft = (rightHand.PalmPosition.x - 80) * (ColumnGame.ActualWidth / 130);
            if (posLeft < 0) posLeft = 0;
            if (posLeft > ColumnGame.ActualWidth - Patrick.ActualWidth * 2) posLeft = ColumnGame.ActualWidth - Patrick.ActualWidth * 2;
            Canvas.SetLeft(Patrick, posLeft);
        }

        /// <summary>
        /// Move the crosshair using the user left palm position
        /// </summary>
        /// <param name="frame">Frame captured by the LeapMotion</param>
        private void MoveCrossHair(Frame frame)
        {
            Hand leftHand = HandGetter.GetLeftHand(frame);
            if (leftHand == null) return;
            double posLeft = ((leftHand.WristPosition.x + 125) * ColumnGame.ActualWidth / 150.0) + ColumnGame.ActualWidth / 2;
            if (posLeft < 0) posLeft = 0;
            if (posLeft > ColumnGame.ActualWidth - CrossHair.ActualWidth * 2) posLeft = ColumnGame.ActualWidth - CrossHair.ActualWidth * 2;
            double posBottom = ((leftHand.WristPosition.y - 110) / 220 * GameWindow.ActualHeight);
            _crossRect.X = posLeft;
            _crossRect.Y = posBottom;
            Canvas.SetLeft(CrossHair, posLeft);
            Canvas.SetBottom(CrossHair, posBottom);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainWindow_Closing(object sender, EventArgs e)
        {
            _isClosing = true;
            _controller.RemoveListener(_listener);
            _controller.Dispose();
        }

        /// <summary>
        /// Event send from Quit buttons that quits the application
        /// </summary>
        /// <param name="sender">A quit button</param>
        /// <param name="e"></param>
        private void PauseButtonQuit_OnClick(object sender, RoutedEventArgs e)
        {
            if (fabJellyfish != null)
            {
                fabJellyfish.Timer.Stop();
                fabJellyfish.Timer.Close();
            }
            MainWindow_Closing(sender, e);
            Close();
        }

        /// <summary>
        /// Event sent from the return button. Used to start the game again.
        /// </summary>
        /// <param name="sender">A retry button</param>
        /// <param name="e"></param>
        private void RetryButton_OnClick(object sender, RoutedEventArgs e)
        {
            Score = 0;
            LoseGrid.Visibility = Visibility.Hidden;
            LoseGrid.IsEnabled = false;
            Playing = true;
        }


        /// <summary>
        /// Fire a new LeapEventNotification later on.
        /// </summary>
        /// <param name="eventName"></param>
        delegate void LeapEventDelegate(string eventName);

        /// <summary>
        /// Handle the LeapMotion events if the thread is available. Otherwise will invoke the delegate.
        /// </summary>
        /// <param name="eventName"></param>
        public void LeapEventNotification(string eventName)
        {
            if (CheckAccess())
            {
                switch (eventName)
                {
                    case "onInit":
                        break;
                    case "onConnect":
                        connectHandler();
                        break;
                    case "onFrame":
                        if (!_isClosing)
                            newFrameHandler(_controller.Frame());
                        break;
                    case "onDisconnect":
                        PauseTextBlock.Text = "Please connect your LeapMotion.";

                        break;
                }
            }
            else
            {
                Dispatcher.Invoke(new LeapEventDelegate(LeapEventNotification), eventName);
            }
        }

        /// <summary>
        /// Called when the LeapMotion is detected on the computer. Changes the default pause screen message
        /// to Game Paused instead of "Please connect the LeapMotion"
        /// </summary>
        void connectHandler()
        {
            PauseTextBlock.Text = "Put your hands back above the LeapMotion";
        }

        /// <summary>
        /// Called everytime the LeapMotion captures a frame
        /// </summary>
        /// <param name="frame">Frame captured by the LeapMotion</param>
        void newFrameHandler(Frame frame)
        {


            GestureEngine.TestGesture(frame);

            if (Playing)
            {
                MovePatrick(frame);
            }

            if (fabJellyfish == null) return;
            Jellyfish jellyToRemove = null;

            foreach (Jellyfish jellyfish in fabJellyfish.JellyFishes)
            {
                jellyfish.RectJelly.X = jellyfish.Left;
                var bottomValue = 768 - jellyfish.Top - 130;
                if (bottomValue <= 0)
                {
                    jellyToRemove = jellyfish;
                }
                else
                {
                    jellyfish.RectJelly.Y = bottomValue;
                }
            }
            if (jellyToRemove != null)
            {
                StopGame();
            }

        }
    }

    public interface ILeapEventDelegate
    {
        void LeapEventNotification(string eventName);
    }

    public class LeapEventListener : Listener
    {
        ILeapEventDelegate _eventDelegate;

        public LeapEventListener(ILeapEventDelegate delegateObject)
        {
            _eventDelegate = delegateObject;
        }
        public override void OnInit(Controller controller)
        {
            _eventDelegate.LeapEventNotification("onInit");
        }
        public override void OnConnect(Controller controller)
        {
            _eventDelegate.LeapEventNotification("onConnect");
        }

        public override void OnFrame(Controller controller)
        {
            _eventDelegate.LeapEventNotification("onFrame");
        }
        public override void OnExit(Controller controller)
        {
            _eventDelegate.LeapEventNotification("onExit");
        }
        public override void OnDisconnect(Controller controller)
        {
            _eventDelegate.LeapEventNotification("onDisconnect");
        }

    }

}