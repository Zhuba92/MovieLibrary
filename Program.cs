using System;
using System.IO;
using System.Collections.Generic;
using NLog.Web;
using System.Linq;
using System.Collections;

namespace MovieLibrary 
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            int choice;
            List<String> movieList = new List<string>();
            
            do
            {
                Console.WriteLine("Type '1' to see all the movies" + "\n" + "Type '2' to add a movie to the list" + "\n" + "Type '0' to exit");
                if(!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Not a valid choice!");
                }
                string file = "movies.csv";

                if(!File.Exists(file))
                {
                    logger.Error("File does not exist");
                }
                else
                {
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
                            movieList.Add(movieTitle);

                            Console.Write($"ID: {movieID}, Title: {movieTitle}, Genre(s): ");

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
                            string movieTitle2 = Console.ReadLine();
                            if(movieList.Contains(movieTitle2))
                            {
                                Console.WriteLine("Movie exists already");
                            }
                            else
                            {
                                var movieGenres = new ArrayList();
                                Console.WriteLine("How many genres does this movie fit into? ");
                                int numGenres;
                                if(!int.TryParse(Console.ReadLine(), out numGenres))
                                {
                                    Console.WriteLine("Not a number");
                                }

                                for(int i = 0; i < numGenres; i++)
                                {
                                    Console.WriteLine("Please enter the name of the genre:");
                                    string addToGenres = Console.ReadLine();
                                    movieGenres.Add(addToGenres);
                                }
                                sw.WriteLine("");
                                sw.Write($"{id},{movieTitle2},");

                                for(int i = 0; i < movieGenres.Count; i++)
                                {
                                    if(i < movieGenres.Count - 1)
                                    {
                                        sw.Write(movieGenres[i] + "|");
                                    }
                                    else
                                    {
                                        sw.Write(movieGenres[i]);
                                    }
                                }
                                Console.WriteLine("");
                                sw.Close();
                            }
                        } 
                    }
                }
            }while(choice != 0);
        }
    }
}

