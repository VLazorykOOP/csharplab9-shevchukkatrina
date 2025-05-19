using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab9
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }

        public Song(string title, string artist, TimeSpan duration)
        {
            Title = title;
            Artist = artist;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"{Title} — {Artist} ({Duration:mm\\:ss})";
        }
    }

    public class MusicDisc
    {
        public string DiscName { get; private set; }
        private ArrayList songs;

        public MusicDisc(string discName)
        {
            DiscName = discName;
            songs = new ArrayList();
        }

        public void AddSong(Song song) => songs.Add(song);

        public bool RemoveSong(string songTitle)
        {
            for (int i = 0; i < songs.Count; i++)
            {
                Song s = (Song)songs[i];
                if (s.Title.Equals(songTitle, StringComparison.OrdinalIgnoreCase))
                {
                    songs.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void ShowSongs()
        {
            Console.WriteLine($"Диск: {DiscName}, Пісні:");
            if (songs.Count == 0)
            {
                Console.WriteLine("  Пісень немає.");
                return;
            }
            foreach (Song song in songs)
                Console.WriteLine("  " + song);
        }

        public List<Song> FindSongsByArtist(string artist)
        {
            List<Song> result = new List<Song>();
            foreach (Song song in songs)
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    result.Add(song);
            }
            return result;
        }
    }

    public class MusicCatalog
    {
        private Hashtable discs = new Hashtable();

        public bool AddDisc(MusicDisc disc)
        {
            if (discs.ContainsKey(disc.DiscName))
            {
                Console.WriteLine($"Диск '{disc.DiscName}' вже існує.");
                return false;
            }
            discs.Add(disc.DiscName, disc);
            return true;
        }

        public bool RemoveDisc(string discName)
        {
            if (discs.ContainsKey(discName))
            {
                discs.Remove(discName);
                return true;
            }
            return false;
        }

        public bool AddSongToDisc(string discName, Song song)
        {
            if (discs.ContainsKey(discName))
            {
                ((MusicDisc)discs[discName]).AddSong(song);
                return true;
            }
            Console.WriteLine($"Диск '{discName}' не знайдено.");
            return false;
        }

        public bool RemoveSongFromDisc(string discName, string songTitle)
        {
            if (discs.ContainsKey(discName))
            {
                return ((MusicDisc)discs[discName]).RemoveSong(songTitle);
            }
            Console.WriteLine($"Диск '{discName}' не знайдено.");
            return false;
        }

        public void ShowCatalog()
        {
            if (discs.Count == 0)
            {
                Console.WriteLine("Каталог порожній.");
                return;
            }

            Console.WriteLine("Каталог музичних дисків:");
            foreach (DictionaryEntry entry in discs)
            {
                ((MusicDisc)entry.Value).ShowSongs();
                Console.WriteLine();
            }
        }

        public void ShowDisc(string discName)
        {
            if (discs.ContainsKey(discName))
                ((MusicDisc)discs[discName]).ShowSongs();
            else
                Console.WriteLine($"Диск '{discName}' не знайдено.");
        }

        public void FindSongsByArtist(string artist)
        {
            Console.WriteLine($"Пошук пісень виконавця '{artist}':");
            bool foundAny = false;
            foreach (DictionaryEntry entry in discs)
            {
                List<Song> songs = ((MusicDisc)entry.Value).FindSongsByArtist(artist);
                if (songs.Count > 0)
                {
                    Console.WriteLine($"Диск: {((MusicDisc)entry.Value).DiscName}");
                    foreach (Song s in songs)
                        Console.WriteLine("  " + s);
                    foundAny = true;
                }
            }
            if (!foundAny)
                Console.WriteLine("Пісень цього виконавця не знайдено.");
        }
    }

    public class Lab9T5
    {
        public void Run()
        {
            MusicCatalog catalog = new MusicCatalog();

            // Додаємо диски
            catalog.AddDisc(new MusicDisc("Rock Classics"));
            catalog.AddDisc(new MusicDisc("Pop Hits"));

            // Додаємо пісні
            catalog.AddSongToDisc("Rock Classics", new Song("Bohemian Rhapsody", "Queen", TimeSpan.FromMinutes(5.55)));
            catalog.AddSongToDisc("Rock Classics", new Song("Stairway to Heaven", "Led Zeppelin", TimeSpan.FromMinutes(8.02)));
            catalog.AddSongToDisc("Pop Hits", new Song("Thriller", "Michael Jackson", TimeSpan.FromMinutes(5.12)));
            catalog.AddSongToDisc("Pop Hits", new Song("Billie Jean", "Michael Jackson", TimeSpan.FromMinutes(4.54)));

            // Показуємо каталог
            catalog.ShowCatalog();

            // Пошук пісень по виконавцю
            catalog.FindSongsByArtist("Michael Jackson");

            // Видалення пісні
            catalog.RemoveSongFromDisc("Pop Hits", "Thriller");
            Console.WriteLine("\nПісля видалення пісні Thriller:");
            catalog.ShowDisc("Pop Hits");

            // Видалення диска
            catalog.RemoveDisc("Rock Classics");
            Console.WriteLine("\nПісля видалення диска Rock Classics:");
            catalog.ShowCatalog();
        }
    }
}
