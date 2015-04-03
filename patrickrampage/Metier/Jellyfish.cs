using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

namespace PatrickRampage.Metier
{
    class Jellyfish :  Animatable, INotifyPropertyChanged
    {
        public Rect RectJelly;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Jellyfish()
        {
            RectJelly = new Rect(Left, SystemParameters.PrimaryScreenHeight - Top, 100, 130);
        }


        // Using a DependencyProperty as the backing store for Top.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(Jellyfish));

        public double Top
        {
            get
            {
                return (double)GetValue(TopProperty);
            }
            set
            {
                Debug.WriteLine("TopChanged");
                SetValue(TopProperty, value);
                OnPropertyChanged("Top");
            }
        }


        // Using a DependencyProperty as the backing store for Left.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(Jellyfish));

        public double Left
        {
            get
            {
                return (double)GetValue(LeftProperty);
            }
            set
            {
                SetValue(LeftProperty, value);
                OnPropertyChanged("Left");
            }
        }


        public static string Img
        {
            get
            {
                return img;
            }
        }

        private static string img = "/Images/jellyfish.png";


        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
    }
}