using System;
using System.Collections.Generic;
using System.IO;

namespace task8._3_lines
{
    class Text
    {
        private List<string> lines;
        private List<string> sentences;

        public Text(List<string> lines)
        {
            this.lines = new List<string>();
            this.lines = lines;
            this.sentences = new List<string>();
            this.sentences = CreateSentences();
        }
        public Text(string filepath)
        {
            this.lines = new List<string>();
            this.sentences = new List<string>();
            this.lines = ReadFromFile(filepath);
            this.sentences = CreateSentences();

        }

        public List<string> ReadFromFile(string filepath)
        {
            List<string> linesList = new List<string>();
            try
            {
                string[] linesArr = File.ReadAllLines(filepath);
                foreach (string l in linesArr)
                {
                    linesList.Add(l);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filepath} not found.");
            }
            return linesList;
        }

        public List<string> CreateSentences()
        {
            List<string> sent = new List<string>();
            string currentSentence = "";
            foreach (string s in this.lines)
            {
                string[] subs = s.Split(". ");


                for (int i = 0; i < subs.Length; ++i)
                {
                    currentSentence += subs[i];
                    if ((i + 1) != subs.Length)
                    {
                        currentSentence += ".";
                        sent.Add(currentSentence);
                        currentSentence = "";
                    }
                }

            }
            return sent;
        }

        public int[] CountChar()
        {
            int left = 0;
            int right = 0;
            int[] count = new int[this.sentences.Count];
            for (int i = 0; i < this.sentences.Count; ++i)
            {
                string[] subs = this.sentences[i].Split(" ");

                for (int j = 0; j < subs.Length; ++j)
                {

                    for (int k = 0; k < subs[j].Length; ++k)
                    {
                        if (subs[j][k] == '(')
                        {
                            left++;
                        }
                        if (subs[j][k] == ')')
                        {
                            right++;
                        }
                        if (left == right && left > count[i])
                        {
                            count[i] = left;
                            left = 0;
                            right = 0;
                        }
                    }
                }

            }
            return count;
        }
        public string FindMostDeep()
        {
            int[] count = CountChar();
            int max = count[0];
            int maxInd = 0;
            for (int i = 0; i < count.Length; ++i)
            {
                if (count[i] > max)
                {
                    max = count[i];
                    maxInd = i;
                }
            }
            return this.sentences[maxInd];
        }

        public void PrintSent()
        {
            foreach (string s in this.sentences)
            {
                Console.WriteLine(s);
            }
        }

        public int[] GetSentencesLength()
        {
            int[] sntLength = new int[this.lines.Count];
            for (int i = 0; i < this.lines.Count; ++i)
            {
                string[] subs = this.lines[i].Split(" ");
                sntLength[i] = subs.Length;
            }
            return sntLength;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Text t = new Text(@"C:\Users\Asus\source\repos\task8.3\TextFile1.txt");
            t.PrintSent();
            Console.WriteLine("Sentences where the depth of the parentheses is greatest:");
            Console.WriteLine(t.FindMostDeep());
        }
    }
}
