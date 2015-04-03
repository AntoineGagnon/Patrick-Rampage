using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Media.Animation;

namespace PatrickRampage.Metier
{
    class FabJellyfish
    {
        /// <summary>
        /// Is used to determine a random horizontal position for the Jellyfish
        /// </summary>
        Random rdm = new Random ();

        /// <summary>
        /// Is used to create Jellyfish objects
        /// </summary>
        public Timer Timer
        {
            get
            {
                return mTimer;
            }
        }
        private Timer mTimer = new Timer();

        /// <summary>
        /// Animate the y position of a Jellyfish
        /// </summary>
        DoubleAnimation verticalAnim;

        /// <summary>
        /// Animate the x position of a Jellyfish
        /// </summary>
        DoubleAnimation horizontalAnim;

        /// <summary>
        /// Collection of Jellyfish objects
        /// </summary>
        public ObservableCollection<Jellyfish> JellyFishes
        {
            get
            {
                return jellyfishes;
            }
        }
        private ObservableCollection<Jellyfish> jellyfishes = new ObservableCollection<Jellyfish>();

        /// <summary>
        /// Construtor  
        /// </summary>
        public FabJellyfish () {
            // Sets the verticalAnim
            verticalAnim = new DoubleAnimation
            {
                By = 1000,
                SpeedRatio = 0.02
            };

            //Sets the horizontalAnim
            horizontalAnim = new DoubleAnimation
            {
                By = 30,
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                SpeedRatio = 0.5
            };

            // Sets the timer interval
            Timer.Interval = 500;

            // Subscribe the event Elapsed of the timer to the method CreateJellyfish
            Timer.Elapsed += CreateJellyfish;
        }

        /// <summary>
        /// Create Jellyfish objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateJellyfish(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("CreateJellyfish");
            //if (rdm.Next(5) != 1) return;
            if (JellyFishes.Count >= 1) return;
            Jellyfish j = (Jellyfish) Application.Current.Dispatcher.Invoke(new Func<Jellyfish>(() => new Jellyfish { Top = 0, Left = rdm.Next(0, 870) }));
            Application.Current.Dispatcher.Invoke(new Action(() => jellyfishes.Add(j)));
            Application.Current.Dispatcher.Invoke(new Action(() => j.BeginAnimation(Jellyfish.LeftProperty, horizontalAnim)));
            Application.Current.Dispatcher.Invoke(new Action(() =>j.BeginAnimation(Jellyfish.TopProperty, verticalAnim)));
        }
    }
}