namespace _1CView
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SQLLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SQLPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FileObject = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.FileSQL = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.HelpBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.OD = new System.Windows.Forms.OpenFileDialog();
            this.SD = new System.Windows.Forms.SaveFileDialog();
            this.chBase = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.chDel = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.FileDDS = new System.Windows.Forms.TextBox();
            this.chDDS = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сервер:";
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(192, 93);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(321, 20);
            this.ServerName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "База 1С:";
            // 
            // BaseName
            // 
            this.BaseName.Location = new System.Drawing.Point(192, 119);
            this.BaseName.Name = "BaseName";
            this.BaseName.Size = new System.Drawing.Size(321, 20);
            this.BaseName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "SQL логин:";
            // 
            // SQLLogin
            // 
            this.SQLLogin.Location = new System.Drawing.Point(192, 145);
            this.SQLLogin.Name = "SQLLogin";
            this.SQLLogin.Size = new System.Drawing.Size(321, 20);
            this.SQLLogin.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "SQL пароль:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // SQLPassword
            // 
            this.SQLPassword.Location = new System.Drawing.Point(192, 171);
            this.SQLPassword.Name = "SQLPassword";
            this.SQLPassword.Size = new System.Drawing.Size(321, 20);
            this.SQLPassword.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Файл конфигурации 1Cv7.MD:";
            // 
            // FileObject
            // 
            this.FileObject.Location = new System.Drawing.Point(192, 9);
            this.FileObject.Margin = new System.Windows.Forms.Padding(0);
            this.FileObject.Name = "FileObject";
            this.FileObject.Size = new System.Drawing.Size(297, 20);
            this.FileObject.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(489, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 10;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Файл скрипта:";
            // 
            // FileSQL
            // 
            this.FileSQL.Location = new System.Drawing.Point(192, 35);
            this.FileSQL.Margin = new System.Windows.Forms.Padding(0);
            this.FileSQL.Name = "FileSQL";
            this.FileSQL.Size = new System.Drawing.Size(297, 20);
            this.FileSQL.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(489, 35);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 13;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HelpBox
            // 
            this.HelpBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpBox.Location = new System.Drawing.Point(16, 260);
            this.HelpBox.Multiline = true;
            this.HelpBox.Name = "HelpBox";
            this.HelpBox.ReadOnly = true;
            this.HelpBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.HelpBox.Size = new System.Drawing.Size(627, 142);
            this.HelpBox.TabIndex = 14;
            this.HelpBox.Text = "1.\tДля  1С версии 7 укажите путь к файлу 1Cv7.md и параметры соединения с базой 1" +
                "С на MS SQL Server\r\n2.\tДля 1С версии 8 укажите только параметры соединения с баз" +
                "ой 1С на MS SQL Server";
            this.HelpBox.WordWrap = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(525, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Создать 1Cv7";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(525, 88);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 16;
            this.button4.Text = "Отмена";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // OD
            // 
            this.OD.Filter = "Файл конфигурации 1С (*.MD)|*.MD|Все файлы|*.*";
            // 
            // chBase
            // 
            this.chBase.AutoSize = true;
            this.chBase.Location = new System.Drawing.Point(192, 208);
            this.chBase.Name = "chBase";
            this.chBase.Size = new System.Drawing.Size(228, 17);
            this.chBase.TabIndex = 17;
            this.chBase.Text = "Добавлять имя базы в имя таблицы 1С";
            this.chBase.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(525, 60);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(121, 23);
            this.button5.TabIndex = 18;
            this.button5.Text = "Выполнить";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(16, 231);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(627, 23);
            this.pbar.TabIndex = 19;
            this.pbar.Visible = false;
            // 
            // chDel
            // 
            this.chDel.AutoSize = true;
            this.chDel.Location = new System.Drawing.Point(505, 208);
            this.chDel.Name = "chDel";
            this.chDel.Size = new System.Drawing.Size(138, 17);
            this.chDel.TabIndex = 21;
            this.chDel.Text = "Только удаление view";
            this.chDel.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(525, 145);
            this.button6.Name = "button6";
            this.button6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button6.Size = new System.Drawing.Size(121, 23);
            this.button6.TabIndex = 22;
            this.button6.Text = "О программе...";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(525, 116);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(121, 23);
            this.button7.TabIndex = 23;
            this.button7.Text = "MainData";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(525, 32);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(121, 23);
            this.button8.TabIndex = 24;
            this.button8.Text = "Создать 1Cv8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Файл DDS:";
            // 
            // button9
            // 
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.Location = new System.Drawing.Point(489, 62);
            this.button9.Margin = new System.Windows.Forms.Padding(0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(24, 23);
            this.button9.TabIndex = 27;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // FileDDS
            // 
            this.FileDDS.Location = new System.Drawing.Point(192, 62);
            this.FileDDS.Margin = new System.Windows.Forms.Padding(0);
            this.FileDDS.Name = "FileDDS";
            this.FileDDS.Size = new System.Drawing.Size(297, 20);
            this.FileDDS.TabIndex = 26;
            // 
            // chDDS
            // 
            this.chDDS.AutoSize = true;
            this.chDDS.Location = new System.Drawing.Point(27, 208);
            this.chDDS.Name = "chDDS";
            this.chDDS.Size = new System.Drawing.Size(154, 17);
            this.chDDS.TabIndex = 28;
            this.chDDS.Text = "Использовать файл DDS";
            this.chDDS.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 413);
            this.Controls.Add(this.chDDS);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.FileDDS);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.chDel);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chBase);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.HelpBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.FileSQL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FileObject);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SQLPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SQLLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BaseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание view для базы 1С";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox BaseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SQLLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SQLPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox FileObject;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox FileSQL;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox HelpBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog OD;
        private System.Windows.Forms.SaveFileDialog SD;
        private System.Windows.Forms.CheckBox chBase;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.CheckBox chDel;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox FileDDS;
        private System.Windows.Forms.CheckBox chDDS;
    }
}

