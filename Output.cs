using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace MotorShowProject.Other
{
    public static class Output
    {
        public static void InWord(Dictionary<string, string> dict, string path)
        {
            if(!File.Exists(AppDomain.CurrentDomain.BaseDirectory + path))
                MessageBox.Show($"Файл шаблона ({AppDomain.CurrentDomain.BaseDirectory + path}) не найден");
            try
            {
                Word.Application app = new Word.Application();
                string source = AppDomain.CurrentDomain.BaseDirectory + path;
                Word.Document doc = app.Documents.Add(source);
                doc.Activate();
                Word.Bookmarks bookmarks = doc.Bookmarks;

                foreach(var b in dict)
                {
                    bookmarks[b.Key].Range.Text = b.Value;
                }

                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Произошла ошибка");
            }
        }
    }
}
