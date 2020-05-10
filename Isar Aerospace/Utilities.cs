using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System.Reflection;
using System.Windows.Shapes;

namespace Isar_Aerospace
{
    public static class Utilities
    {
        public static List<Book> ReadCSV(string path,ref List<string> bindingList)
        {
            List<Book> books = new List<Book>();
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        // Skip the Headers Row
                        if (i != 0)
                        {
                            line = fixPunctuation(line);
                            var values = restorPunctuation(line.Split(';'));

                            books.Add(ConvertValuesToBook(values.ToList(), ref bindingList));
                        }

                        i++;
                    }
                }
            }
            return books;
        }

        private static string fixPunctuation(string line)
        {
            int last = 0;
            while (line.Length > last)
            {
                string strtmp = line.Substring(last);
                int start = strtmp.IndexOf('"');
                if(start >= 0)
                {
                    string endtmp = strtmp.Substring(start+1);
                    int end = endtmp.IndexOf('"');

                    if (end > 0)
                    {
                        string quta = strtmp.Substring(start, (end)+2);
                        quta = quta.Replace(";", "&colon&");
                        quta = quta.Replace("\"", "&qute&");
                        line = line.Replace(line.Substring(start + last, (end) + 1), quta);
                        last = last + end + 1;
                    }
                    else
                    {
                        last = line.Length;
                    }
                    
                }
                else
                {
                    last = line.Length;
                }
            }
            return line;
        }

        private static string[] restorPunctuation(string[] values){
            List<string> result = new List<string>(); 
            foreach (var item in values)
            {
                string tmp;
                tmp = item.Replace("&colon&", ";");
                tmp = tmp.Replace("&qute&", "\"");
                tmp = tmp.Replace("\"\"", "\"");
                result.Add(tmp);
            }
            return result.ToArray();
        }

        public static string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data\\";
            ;
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return "";
        }

        public static Book ConvertValuesToBook(List<string> values, ref List<string> bindingList)
        {
            Book book = new Book();
            // check if the line item(Book) have  7 elements
            if (values.Count() != 7)
                throw new ArgumentOutOfRangeException("Values", "The imported book must have 7 items");
            // Add the binding Item to List if not exists
            if (!bindingList.Contains(values[5]))
                bindingList.Add(values[5]);

            book.Title = values[0];
            book.Author = values[1];
            book.Year = values[2];
            book.Price = values[3];
            book.InStock =values[4] == "yes"?true:false;

            book.Binding = bindingList.IndexOf(values[5]);
            
            book.Description = values[6];
            return book;
        }
    }


}
