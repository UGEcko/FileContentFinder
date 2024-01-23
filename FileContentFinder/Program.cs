﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Runtime.CompilerServices;
using System.Data;
using System.Threading;

namespace FileContentFinder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            void RAWTC(string dir, string keyword, Boolean log)
            {
                if (log)
                {
                    if(File.Exists(dir))
                    {
                        if (dir.Contains(keyword))
                        {
                            Console.WriteLine(dir + " contains " + keyword + " (true)");
                        }
                        else
                        {
                            Console.WriteLine("None found");
                        }
                    } else
                    {
                        Console.WriteLine(dir + " doesnt exist!");
                    }
                    
                }

            }
            void typeDetermine(string operation, string rawDir)
            {
                if (operation.ToUpper() == "FILES")
                {
                    List<string> temp = getAllDirEntries(rawDir, returnType.Files,true);
                    foreach(string file in temp.ToArray())
                    {
                        Console.WriteLine(file);
                    }
                }
                else if (operation.ToUpper() == "FOLDERS")
                {
                    List<string> temp = getAllDirEntries(rawDir, returnType.Folders,true);
                    foreach(string folder in temp.ToArray())
                    {
                        Console.WriteLine(folder);
                    }
                }
                else if (operation.ToUpper() == "ENTRIES")
                {
                    List<string> temp = getAllDirEntries(rawDir, returnType.Entries,true);
                    foreach(string entry in temp.ToArray())
                    {
                        Console.WriteLine(entry);
                    }
                }
                else if (operation.ToUpper() == "KEYWORDS")
                {
                    Console.WriteLine("Please enter a keyword : ");
                    string keyword = Console.ReadLine();
                    Console.WriteLine("Finding " + keyword);
                    List<string> files = getAllDirEntries(rawDir, returnType.Files, false);
                    foreach(string file in files.ToArray())
                    {
                        Console.WriteLine(file);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please enter either: FILES, FOLDERS, ENTRIES, or KEYWORDS.");
                    typeDetermine(operation, rawDir);
                }
            }
            void fcf()
            {

                Console.WriteLine("Please input a VALID path : ");
                string rawDir = Console.ReadLine();
                if (Directory.Exists(rawDir))
                {
                    Console.WriteLine("Found '" + rawDir + "'");

                    Console.WriteLine("Find FILES, FOLDERS, or ENTRIES (Both) within a base directory | Find KEYWORDS in files within a base directory. Please type your choice  (The Captitalized words)");
                    string operation = Console.ReadLine();

                    typeDetermine(operation, rawDir);

                    Console.WriteLine(); // Space out 
                    Console.WriteLine("Would you like to do another directory? Y or N.");
                    string usrAgain = Console.ReadLine();
                    if (usrAgain.ToUpper() == "Y")
                    {
                        fcf();
                    } else if (usrAgain.ToUpper() == "N")
                    {
                        Environment.Exit(0);
                    } else
                    {
                        Console.WriteLine("Please input Y or N.");
                    }

                }
                else
                {
                    Console.WriteLine(rawDir + " doesnt exist!");
                    fcf();
                }
            }
            Console.WriteLine("`..     `..`........`..      `..          `....     \r\n`..     `..`..      `..      `..        `..    `..  \r\n`..     `..`..      `..      `..      `..        `..\r\n`...... `..`......  `..      `..      `..        `..\r\n`..     `..`..      `..      `..      `..        `..\r\n`..     `..`..      `..      `..        `..     `.. \r\n`..     `..`........`........`........    `....     \r\n                                                    ");
            Console.WriteLine("Welcome to FCF");
            fcf();
        }
            public enum returnType{
            Folders,
            Files,
            Entries
            }

        static List<string> getAllDirEntries(string directory, returnType rT, bool log)
            {
            
            List<string> returnArray = new List<string>();
            if (Directory.Exists(directory))
            {
                string[] baseSubDirectories = Directory.GetDirectories(directory); // For if the function needs to run again to get the next dir entries
                string[] baseSubFiles = Directory.GetFiles(directory); // For whatever
                string[] baseEntries = Directory.GetFileSystemEntries(directory); // Mainly for listing folders and files in their order (not seperate)

                
                if (rT == returnType.Folders)
                {

                    if (baseSubDirectories.Length > 0)
                    {
                        foreach (string subDirectory in baseSubDirectories)
                        {
                            if(log)
                            {
                                Console.WriteLine(subDirectory);
                            }
                            returnArray.Add(subDirectory);
                        }
                        foreach (string dir in baseSubDirectories)
                        {
                            if(log)
                            {
                                getAllDirEntries(dir, returnType.Folders, true);
                            } else
                            {
                                getAllDirEntries(dir, returnType.Folders, false);
                            }
                            
                        }
                        
                    }
                }
                else if (rT == returnType.Files)
                {
                    if (baseSubFiles.Length > 0)
                    {
                        foreach (string file in baseSubFiles)
                        {
                            if(log)
                            {
                                Console.WriteLine($"{file}");
                            }
                            returnArray.Add(file);
                        }
                    }
                    if (baseSubDirectories.Length > 0)
                    {
                        foreach (string subDirectory in baseSubDirectories)
                        {
                            if(log)
                            {
                                getAllDirEntries(subDirectory, returnType.Files,true);
                            } else
                            {
                                getAllDirEntries(subDirectory, returnType.Files,false);
                            }
                            
                        }
                    }
                }
                else if (rT == returnType.Entries)
                {
                    foreach (string entry in baseEntries)
                    {
                        if(log)
                        {
                            Console.WriteLine(entry);
                        }
                        
                        returnArray.Add(entry);
                    }
                    if (baseSubDirectories.Length > 0)
                    {
                        foreach (string dir in baseSubDirectories)
                        {
                            if(log)
                            {
                                getAllDirEntries(dir, returnType.Entries,true);
                            } else
                            {
                                getAllDirEntries(dir, returnType.Entries,false);
                            }
                            
                        }
                    }
                }
                return returnArray;
            } else
            {
                Console.WriteLine("Directory doesnt exist.");
                return returnArray;
            }
    
        }
    }
}
