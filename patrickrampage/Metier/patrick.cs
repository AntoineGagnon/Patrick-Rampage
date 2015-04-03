namespace PatrickRampage.Metier
{
    class Patrick
    {
        public int x {
            get;
            set;
        }

        public int y {
            get;
            set;
        }

        public string toLeftImg {
            get;
            private set;
        }

        public string toRightImg {
            get;
            private set;
        }

        Patrick(string toLeftImg, string toRightImg) {
            this.toLeftImg = toLeftImg;
            this.toRightImg = toRightImg;
        }
    }
}