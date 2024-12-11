﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var text = Encryption.EncryptString(textBox1.Text, Encryption.GenerateKey(textBox2.Text), Encryption.GenerateIV(textBox3.Text));
                richTextBox1.Text += "Encrypted value: '" + text + "'\n";
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "There was an error: " + ex.Message + "\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var text = Encryption.DecryptString(textBox1.Text, Encryption.GenerateKey(textBox2.Text), Encryption.GenerateIV(textBox3.Text));
                richTextBox1.Text += "Decrypted value: '" + text + "'\n";
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "There was an error: " + ex.Message + "\n";
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
