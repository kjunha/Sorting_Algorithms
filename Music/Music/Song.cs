using System;

namespace Music
{
    class Song
    {
        private String title;
        private String artist;
        private int length;

        public Song(String t, String a, int n)
        {
            title = t;
            artist = a;
            length = n;
        }

        public override string ToString()
        {
            return (title + " " + artist + " " + length + " sec.");
        }
    }

    class Program
    {
        public static void Main(String[] args)
        {
            Song a = new Song("mylife", "kj", 180);
            Console.WriteLine(a);
        }
    }
}
