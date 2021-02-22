using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace MovieLibrary_A4
{
    class Program
    {  
        public static List<string> movieList = new List<string>();
        public static int movieID = 164981;

        static void displayMenu() {
            Console.WriteLine("");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1.) Add a Movie.");
            Console.WriteLine("2.) Display Movies in Pages");
            Console.WriteLine("3.) Display The Whole List of Movies");
            Console.WriteLine("4.) End Program.");
            Console.Write(": ");
        }

        static void addMovie() {
            //helpful tip to self: adding true allows for appending, avoiding overwriting the csv
            StreamWriter sw = new StreamWriter("movies.csv", true);
            List<String> tempGenreList = new List<String>(); 

            Console.WriteLine("Name of The Movie?: ");
            string movieName = Console.ReadLine();

            //found this on some forum, i couldnt get it to work however
            //System.IO.Directory.Delete("movies.csv").Where(s => s.Contains(movieName));

            Console.WriteLine("Year Movie was Released?: ");
            string movieYear = Console.ReadLine();

            Console.WriteLine("How Many Genres are present?: ");
            string genresPresent = Console.ReadLine();

            //loop to go through all genres that can be in a movie
            for (int l = 1; l <= int.Parse(genresPresent); l++) 
            {
                Console.WriteLine($"Name of Genre {l}:"); 
                string genreInput = Console.ReadLine();
                tempGenreList.Add(genreInput);
            }

            //gets the pipe delimiter for genres
            string genresToString = String.Join("|", tempGenreList);
            //Concats and formats users movie
            string movie = ($"{movieID},{movieName} ({movieYear}),{genresToString}");

            //adds users movie into the csv file
            sw.WriteLine(movie); 
            //increments id
            movieID++;
            //closes stream writer
            sw.Close();

            for (int i = 0; i < movieList.Capacity; i++) {
                if(movieList.Contains(movieName)) {
                    movieList.RemoveAt(i);
                }
            }
        }

        static void readAllData() {
            //new stream reader and list for items being read
            var reader = new StreamReader(File.OpenRead("smallSampleTest.csv"));
            List<string> readMoviesList = new List<string>();

            while (!reader.EndOfStream)
            {
                //iterates through each movie until theres nothing to stream.
                var movieRow = reader.ReadLine();
                readMoviesList.Add(movieRow);
            }
            //writes every single movie to console
            Console.WriteLine(readMoviesList);
        }

        static void pageData() {
            Console.WriteLine("How many movies would you like one page to contain? ");
            double pageEnd = Convert.ToDouble(Console.ReadLine());

            List<String> moviesPageList = new List<String>(); 

            //loop for each movie in the csv
            //create second array that holds *i movies, specified by user
            //create menu that asks if users wants to move one page up/down
            //displays each mini page array in a larger array when specified page is reached

        }        
        static void Main(string[] args){
            //Logger log = LogManager.GetCurrentClassLogger();

            //exception handling incase movies.csv doesnt exist
            try
            {
                if (!File.Exists("movies.csv"))
                throw new FileNotFoundException();
            }
                catch(FileNotFoundException) { 
                    Console.WriteLine("File was not found!");
                    Console.WriteLine("Want to create a file? y/n");
                    string wantNewFile = Console.ReadLine();
                    
                    //creates file for user if they want it
                    if (wantNewFile.Equals("y")) {
                        string file = "movies.csv";
                        StreamWriter sw = new StreamWriter(file);
                    }
                }
            //defaults to 0 so first loop can initiate
            string choice = "0";

            //will run a loop until the user chooses 4, breaking the loop
            while (choice != "4") 
            {
                //Displays menu
                displayMenu();
                
                // input response
                choice = Console.ReadLine();
            
                //if statements for user input
                if (choice == "1")
                {
                    addMovie();
                }
                else if (choice == "2")
                {
                    pageData();
                }
                else if (choice == "3")
                {
                    readAllData();
                }
                else if (choice == "4")
                {
                    Console.WriteLine("Program Has Closed.");
                    break;
                }
            }
        }  
    }
} 
