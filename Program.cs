﻿using System;
using System.IO;
using System.Collections;

namespace MovieLibrary 
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Type '1' to see all the movies" + "\n" + "Type '2' to add a movie to the list" + "\n" + "Type '0' to exit");
                choice = Convert.ToInt32(Console.ReadLine());
                string file = "movies.csv";
                
                if(choice == 1)
                {
                    StreamReader sr = new StreamReader(file);
                    
                    while(!sr.EndOfStream)
                    {
                        string movieInfo = sr.ReadLine();
                        string[] movieInfoParts = movieInfo.Split(",");
                        string movieID = movieInfoParts[0];
                        string movieTitle = movieInfoParts[1];
                        string[] genres = movieInfoParts[2].Split("|");

                        Console.Write($"ID: {movieID}, Title: '{movieTitle}', Genre(s): ");

                        for(int i = 0; i < genres.Length; i++)
                        {
                            if(i < genres.Length - 1)
                            {
                                Console.Write(genres[i] + ", ");
                            }
                            else
                            {
                                Console.Write(genres[i]);
                            }
                        }
                        Console.WriteLine("");
                    }
                    sr.Close();
                } 
                else if(choice == 2)
                {                    
                    if(System.IO.File.Exists(file))
                    {
                        StreamWriter sw = new StreamWriter(@"movies.csv", true);

                        Console.WriteLine("Enter the id:");
                        string id = Console.ReadLine();

                        Console.WriteLine("What is the title of the movie?");
                        string movieTitle = Console.ReadLine();

                        var movieGenres = new ArrayList();
                        Console.WriteLine("How many genres does this movie fit into? ");
                        int numGenres = Convert.ToInt32(Console.ReadLine());

                        for(int i = 0; i < numGenres; i++)
                        {
                            Console.WriteLine("Please enter the name of the genre:");
                            string addToGenres = Console.ReadLine();
                            movieGenres.Add(addToGenres);
                        }

                        sw.Write($"ID: {id}, Title: '{movieTitle}', Genre(s): ");

                        for(int i = 0; i < movieGenres.Count; i++)
                        {
                            if(i < movieGenres.Count - 1)
                            {
                                Console.Write(movieGenres[i] + ", ");
                            }
                            else
                            {
                                Console.Write(movieGenres[i]);
                            }
                        }
                        Console.WriteLine("");
                        sw.Close();
                    } 
                }
            }while(choice != 0);
        }
    }
}
