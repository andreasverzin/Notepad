﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotepadCSharp
{
    public partial class blank : Form
    {
        
        public string DocName = "";
        private string BufferText = "";
        public bool IsSaved = false;
        public blank()
        {
            InitializeComponent();
        }

        // Вырезание текста
        public void Cut()
        {
            this.BufferText = richTextBox1.SelectedText;
            richTextBox1.SelectedText = "";
        }
        // Копирование текста
        public void Copy()
        {
            this.BufferText = richTextBox1.SelectedText;
        }
        // Вставка
        public void Paste()
        {
            richTextBox1.SelectedText = this.BufferText;
        }
        // Выделение всего текста — используем свойство SelectAll элемента управления RichTextBox
        public void SelectAll()
        {
            richTextBox1.SelectAll();
        }
        // Удаление
        public void Delete()
        {
            richTextBox1.SelectedText = "";
            this.BufferText = "";
        }

        //Создаем метод Open, в качестве параметра объявляем строку адреса файла.
        public void Open(string OpenFileName)
        {
            //Если файл не выбран, возвращаемся назад (появится встроенное предупреждение)
            if (OpenFileName == "")
            {
                return;
            }
            else
            {
                //Создаем новый объект StreamReader и передаем ему переменную OpenFileName
                StreamReader sr = new StreamReader(OpenFileName);
                //Читаем весь файл и записываем его в richTextBox1
                richTextBox1.Text = sr.ReadToEnd();
                // Закрываем поток
                sr.Close();
                //Переменной DocName присваиваем адресную строку.
                DocName = OpenFileName;
            }
        }

        //Создаем метод Save, в качестве параметра объявляем строку адреса файла.
        public void Save(string SaveFileName)
        {
            //Если файл не выбран, возвращаемся назад (появится встроенное предупреждение)
            if (SaveFileName == "")
            {
                return;
            }
            else
            {
                //Создаем новый объект StreamWriter и передаем ему переменную //OpenFileName
                StreamWriter sw = new StreamWriter(SaveFileName);
                //Содержимое richTextBox1 записываем в файл
                sw.WriteLine(richTextBox1.Text);
                //Закрываем поток
                sw.Close();
                //Устанавливаем в качестве имени документа название сохраненного файла
                DocName = SaveFileName;
            }
        }

        private void Blank_Load(object sender, EventArgs e)
        {

        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void CmnuCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void CmnuCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void CmnuPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void CmnuDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void CmnuSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void Blank_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Blank_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Если переменная IsSaved имеет значение true, т. е. новый документ
            //был сохранен (Save As) или в открытом документе были сохранены изменения (Save), то //выполняется условие
            if (IsSaved == true)
                //Появляется диалоговое окно, предлагающее сохранить документ.
                if (MessageBox.Show("Do you want save changes in " + this.DocName + "?",
                "Message", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                //Если была нажата кнопка Yes, вызываем метод Save
                {
                    this.Save(this.DocName);
                }
        }
    }
}
