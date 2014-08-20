using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SocioBoard.Helper
{
    public static class GlobusFileHelper
    {

        public static String ReadStringFromTextfile(string filepath)
        {
            string fileText = "";
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    fileText = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return fileText;
        }

        private static int _bufferSize = 16384;

        public static List<string> ReadFile(string filename)
        {
            List<string> listFileContent = new List<string>();
            try
            {

                StreamReader reader = new StreamReader(filename, System.Text.Encoding.UTF8, true);

                StringBuilder stringBuilder = new StringBuilder();
                using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            char[] fileContents = new char[_bufferSize];
                            int charsRead = streamReader.Read(fileContents, 0, _bufferSize);

                            // Can't do much with 0 bytes
                            if (charsRead == 0)
                                throw new Exception("File is 0 bytes");

                            while (charsRead > 0)
                            {
                                stringBuilder.Append(fileContents);
                                charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                            }

                            string[] contentArray = stringBuilder.ToString().Split(new char[] { '\r', '\n' });
                            foreach (string line in contentArray)
                            {
                                if (line.EndsWith("\0"))
                                {
                                    listFileContent.Add(line.Replace("\0", ""));
                                }
                                else
                                {
                                    listFileContent.Add(line);
                                }
                                //listFileContent.Add(line.Replace("#", ""));
                            }
                            listFileContent.RemoveAll(s => string.IsNullOrEmpty(s));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }

            }
            catch
            {
            }
            return listFileContent;
        }

        public static List<string> ReadFiletoStringList(string filepath)
        {

            List<string> list = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string text = "";
                    while ((text = reader.ReadLine()) != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(text))
                            {
                                list.Add(text);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return list;
        }

        public static List<string> ReadTextFileWithSeparator(string FilePath, string pattern)
        {
            List<string> list = null;
            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    list = new List<string>();
                    string text = "";
                    text = reader.ReadToEnd();
                    // while ((text = reader.ReadLine()) != null)
                    {
                        if (text.Contains(pattern))
                        {
                            string[] arrItem = Regex.Split(text, pattern);
                            foreach (string item in arrItem)
                            {
                                try
                                {
                                    list.Add(item.Replace("\r\n", string.Empty));
                                    list.Remove("");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }

                        }
                        else
                        {
                            list.Add(text.Replace("\r\n", string.Empty));
                            list.Remove("");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return list;
        }

        public static void WriteStringToTextfile(string content, string filepath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    writer.Write(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        public static void WriteStringToTextfileNewLine(String content, string filepath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    using (StringReader reader = new StringReader(content))
                    {
                        while (reader.ReadLine() != null)
                        {
                            try
                            {
                                writer.WriteLine(content);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void AppendStringToTextfileNewLine(String content, string filepath)
        {

            try
            {

                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    using (StringReader reader = new StringReader(content))
                    {
                        string temptext = "";

                        while ((temptext = reader.ReadLine()) != null)
                        {
                            try
                            {
                                writer.WriteLine(temptext);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void AppendStringToTextfileNewLineWithCarat(String content, string filepath)
        {

            try
            {
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    using (StringReader reader = new StringReader(content))
                    {
                        string temptext = "";

                        while ((temptext = reader.ReadLine()) != null)
                        {
                            try
                            {
                                writer.WriteLine(temptext);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                        for (int i = 0; i < 80; i++)
                        {
                            try
                            {
                                writer.Write("-");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }
                    writer.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void WriteListtoTextfile(List<string> list, string filepath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    foreach (string listitem in list)
                    {
                        try
                        {
                            writer.WriteLine(listitem);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        public static List<string> readcsvfile(string filpath)
        {
            List<string> tempdata = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filpath, Encoding.GetEncoding(1250)))
                {
                    string strline = "";
                    int x = 0;
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            x++;
                            strline = sr.ReadLine();
                            tempdata.Add(strline);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return tempdata;
        }

        public static void ReplaceStringFromCsv(string filePath, string searchText, string replaceText)
        {
            try
            {
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    content = reader.ReadToEnd();

                    content = Regex.Replace(content, searchText + "\r\n", replaceText);
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(content);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        static List<string[]> dataToBeWritten = new List<string[]>();

        public static List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
            }

            return parsedData;
        }

        public static void writeCSV(string CSVPath, List<string[]> dataToBeWritten)
        {
            foreach (var strArray in dataToBeWritten)
            {
                string eachProfileData = string.Join(",", strArray);
                GlobusFileHelper.AppendStringToTextfileNewLine(eachProfileData, CSVPath);
            }


        }

        public static void ExportDataCSVFile(string CSV_Header, string CSV_Content, string CSV_FilePath)
        {
            try
            {
                if (!File.Exists(CSV_FilePath))
                {
                    GlobusFileHelper.AppendStringToTextFile(CSV_FilePath, CSV_Header);
                }

                GlobusFileHelper.AppendStringToTextFile(CSV_FilePath, CSV_Content);
            }
            catch (Exception)
            {

            }
        }

        public static void WriteCSVLineByLine(string CSVDestinationFilePath, string commaSeparatedData)
        {
            GlobusFileHelper.AppendStringToTextfileNewLine(CSVDestinationFilePath, commaSeparatedData);
        }

        public static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string DesktopPathAccountChecker = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string DesktopFanFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void AppendStringToTextFile(string FilePath, string content)
        {
            //Encoding encodingOfChoice = Encoding.UTF8;
            //byte[] bytes = encodingOfChoice.GetBytes(content);
            //using (StreamWriter sw = new StreamWriter(FilePath, true, Encoding.UTF8))

            using (StreamWriter sw = new StreamWriter(FilePath, true))
            {
                sw.WriteLine(content);
            }
        }

    }
}