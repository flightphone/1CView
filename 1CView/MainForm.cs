using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using OpenMcdf;
using System.Text.RegularExpressions;
using System.IO.Compression;


namespace _1CView
{
    

    public partial class MainForm : Form
    {

        private struct CodeName
        {
            public String Cod;
            public String Name;
        }

        private StringBuilder sb;

        public MainForm()
        {
            InitializeComponent();
        }

        private String GetNumberText(String S)
        {
            Regex rg = new Regex(@"(\d+)");
            String Res = "";
            if (rg.IsMatch(S))
            { Res = rg.Match(S).Groups[1].Value; }
            return Res;
            
        }

        private CodeName GetCodeName8(String s)
        {
            CodeName cn;
            String cod = GetNumberText(s);
            if (String.IsNullOrEmpty(cod))
            {
                cn.Cod = "";
                cn.Name = "";
            }
            else
            {
                if (s.IndexOf(cod) == 0)
                {
                    cn.Cod = "";
                    cn.Name = "";
                }
                else
                {
                    cn.Cod = cod;
                    cn.Name = s.Substring(0, s.IndexOf(cod));
                }
            }
            return cn;
        }

        private String Parce1Name(String S, DataTable tl)
        {
            
            CodeName cn = GetCodeName8(S);
            if (String.IsNullOrEmpty(cn.Cod))
                return S;
            DataRow[] rw = tl.Select("CodName='" + cn.Name + "' and CodNUM = '" + cn.Cod + "'");

            if (rw.Length == 0)
                return S;

            String Res = S.Replace(cn.Cod, "_" + rw[0]["CodWord"].ToString());
            return Res;
        }


        private String Parse8Name(String S, DataTable tl)
        {
            String[] sep = {"_"};
            String[] buf = S.Split(sep, StringSplitOptions.None);
            String Res = buf[0];
            for (int i = 1; i < buf.Length; i++)
            {
                Res = Res + "_" + Parce1Name(buf[i], tl);
            }
            return Res;
        }

        private bool isNumberText(String S)
        {
            if (String.IsNullOrEmpty(S))
            {return false;}
            String r = GetNumberText(S);
            return (S == r);
        }

        private SortedList ParseObject(String FName)
        {
            String[] ResLine = File.ReadAllLines(FName, Encoding.GetEncoding(1251));
            SortedList Res = new SortedList();
            for (int i = 0; i < ResLine.Length; i++)
            {
                String key = ResLine[i].Substring(0, 10).Trim();
                String val = ResLine[i].Substring(11).Trim();
                if (!Res.ContainsKey(key))
                    Res.Add(key, val);
            }
            return Res;
        }

        private void CreateSQL(String ConnectString, String F1C, String FSQL, String Base, bool useDDS)
        {
            //try
            //{

            if (!String.IsNullOrEmpty(Base))
            { Base = Base + ".."; }

            SortedList meta;
            if (useDDS)
            {
                meta = GetDDSFile(F1C);
            }
            else
            {
                meta = GetMainStream(F1C); //ParseObject(F1C);
            }
            DataTable TabList = new DataTable();
            DataTable ColList = new DataTable();

            String sql = "select [name], id from sysobjects where xtype = 'U' and category = 0 order by 1";

            //(left([name], 1) <> '_' )
            
                SqlDataAdapter da = new SqlDataAdapter(sql, ConnectString);
                da.Fill(TabList);
                sql = "select [name], id from syscolumns (nolock)";
                da.SelectCommand.CommandText = sql;
                da.Fill(ColList);
            
            String ResSQL = "";
            String val = "";
            String key = "";
            String[] vl;
            String[] sep = {"."};
            String pref = "";

            
            pbar.Minimum = 0;
            pbar.Maximum = TabList.Rows.Count;
            pbar.Step = 1;
            pbar.Value = 0;

            ArrayList atabval = new ArrayList();

            for (int i = 0; i < TabList.Rows.Count; i++)
            {
                if (useDDS)
                {
                    key = TabList.Rows[i]["name"].ToString();
                }
                else
                {
                    key = GetNumberText(TabList.Rows[i]["name"].ToString());
                    if (String.IsNullOrEmpty(key) | key == "1")
                        key = "XXX";
                }

                pref = TabList.Rows[i]["name"].ToString().Substring(0, 2);
                if (meta[key]==null)
                {
                    val = TabList.Rows[i]["name"].ToString() + "_V"; 
                }
                else
                {
                    val = meta[key].ToString().Replace(".", "_");
                }


                if (pref == "DT")
                { val = val + "_Детали"; }


                if (pref =="RG")
                { val = val + "_Период"; }


                if (atabval.IndexOf(val) > -1)
                {
                    val = val + i.ToString();
                }
                else
                { atabval.Add(val); }


                String CView = "if (exists(select * from sysobjects where [name] = '" + val + "')) drop view [" + val + "]";

                if (!chDel.Checked)  //Только удаляем таблицы
                {
                    CView = CView + "\r\nGO\r\nCREATE VIEW [" + val + "]\r\nAS\r\nSELECT";

                    DataRow[] cols = ColList.Select("id=" + TabList.Rows[i]["id"].ToString());

                    ArrayList aval = new ArrayList();

                    for (int j = 0; j < cols.Length; j++)
                    {
                        if (useDDS)
                        {
                            key = cols[j]["name"].ToString();
                        }
                        else
                        {
                            key = GetNumberText(cols[j]["name"].ToString());
                            if (String.IsNullOrEmpty(key))
                                key = "XXX";
                        }

                        if (meta[key] == null)
                        {
                            val = cols[j]["name"].ToString();
                        }
                        else
                        {
                            val = meta[key].ToString();
                        }
                        if (String.IsNullOrEmpty(val))
                            val = cols[j]["name"].ToString();

                        vl = val.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        val = vl[vl.Length - 1];

                        if (aval.IndexOf(val) > -1)
                        {
                            val = val + j.ToString();
                        }
                        else
                        { aval.Add(val); }

                        CView = CView + "\r\n\t" + cols[j]["name"].ToString() + " AS [" + val + "]";
                        if (j < cols.Length - 1)
                        { CView = CView + ","; }
                    }

                    CView = CView + "\r\nFROM " + Base + TabList.Rows[i]["name"].ToString() + "(NOLOCK)";
                }
                
                CView = CView + "\r\nGO\r\n";
                ResSQL = ResSQL + CView;
                pbar.PerformStep();
                Application.DoEvents();
            }

                HelpBox.Text = ResSQL; 
                File.WriteAllText(FSQL, ResSQL, Encoding.GetEncoding(1251));
            //}
            //catch (Exception ex)
            //{
            //    HelpBox.Text = ex.Message;
            //    return;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OD.ShowDialog() == DialogResult.OK)
            {
                FileObject.Text = OD.FileName;
                FileInfo fi = new FileInfo(FileObject.Text);
                FileSQL.Text = fi.DirectoryName + @"\1CView.sql";
                String DBAName = fi.DirectoryName + @"\1Cv7.dba";
                FileDDS.Text = fi.DirectoryName + @"\1Cv7.DDS";
                
                if (File.Exists(DBAName))
                {
                    String Res = DBADetect(DBAName);

                    Res = Res.Substring(1, Res.Length - 2);
                    List<String> par = GetDirs(Res);
                    String[] sep = {","};
                    String[] bf;
                    foreach (String pr in par)
                    {
                        String p = pr.Replace(@"""", "");
                        bf = p.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        if (bf[0] == "Server")
                            ServerName.Text = bf[1];
                        if (bf[0] == "DB")
                            BaseName.Text = bf[1];
                        if (bf[0] == "UID")
                            SQLLogin.Text = bf[1];
                        if (bf[0] == "PWD")
                            SQLPassword.Text = bf[1];
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SD.ShowDialog() == DialogResult.OK)
                FileSQL.Text = SD.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FileObject.Text))
            {
                MessageBox.Show("Не указан файл метаданных 1С!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty(FileSQL.Text))
            {
                MessageBox.Show("Не указан файл скрипта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (String.IsNullOrEmpty(BaseName.Text) | 
                String.IsNullOrEmpty(ServerName.Text) | 
                String.IsNullOrEmpty(SQLLogin.Text) |
                String.IsNullOrEmpty(SQLPassword.Text) )
            {
                MessageBox.Show("Не указаны параметры соединения с базой 1С!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            

            
            String[] Par = {ServerName.Text, SQLLogin.Text, SQLPassword.Text, BaseName.Text};
            String cn = String.Format("data source={0};User ID={1};Password={2};database={3}", Par);

            String BName = "";
            if (chBase.Checked)
            { BName = BaseName.Text; }

            pbar.Value = 0;
            pbar.Visible = true;
            Application.DoEvents();

            String File1C;
            if (chDDS.Checked)
            { File1C = FileDDS.Text; }
            else
            { File1C = FileObject.Text; }


            CreateSQL(cn, File1C, FileSQL.Text, BName, chDDS.Checked );
            pbar.Visible = false;
            MessageBox.Show("Скрипт успешно создан!", "Внимание!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private String RepName(String Res)
        {
            return Res.Replace("TaskItem", "Задача").Replace("GenJrnlFldDef", "ОбщийРеквизитДокумента").
                  Replace("DocSelRefObj", "ГрафаОтбора").
                  Replace("DocNumDef", "Нумератор").
                  Replace("Consts", "Константа").
                  Replace("SbCnts", "Справочник").
                  Replace(".Params", "").
                  Replace("Registers", "Регистр").
                  Replace(".Props", "")
                  .Replace(".Figures", "")
                  .Replace(".Flds", "")
                  .Replace("Documents", "Документ")
                  .Replace(".Head Fields", "")
                  .Replace(".Table Fields", "")
                  .Replace("Journalisters", "Журнал")
                  .Replace(".JournalFld", "")
                  .Replace("EnumList", "Перечисление")
                  .Replace(".EnumVal", "")
                  .Replace("ReportList", "Отчет")
                  .Replace("CalcVars", "Обработка")
                  .Replace("Document Streams", "Последовательность")
                  .Replace("Buh", "Бухгалтерия"); 
        }

        private SortedList GetMainStream(String FName)
        {
            SortedList meta = new SortedList();
            CompoundFile cf = null;
            cf = new CompoundFile(FName);
            CFStorage r = cf.RootStorage;

            r = r.GetStorage("Metadata");
            CFStream st = r.GetStream("Main MetaData Stream");
            byte[] rbyte = st.GetData();
            String Res = Encoding.GetEncoding(1251).GetString(rbyte, 10, rbyte.Length-10);
            sb = new StringBuilder();

            List<String> Dirs = GetDirs(Res);

            pbar.Minimum = 0;
            pbar.Maximum = 2000;
            pbar.Step = 1;
            pbar.Value = 0;

            foreach (String sd in Dirs)
            {
                ParseMainStream("", sd, meta);
            }

            return meta;
        }


        private int GetOpenTag(String S, int istart)
        {
            return S.IndexOf(@"{", istart);
        }

        private int GetCloseTag(String S, int istart)
        {
            int nopen = 1;
            int nclose = 0;
            int istop = -1;
            for (int i = istart + 1; i < S.Length; i++)
            {
                if (S.Substring(i, 1) == "{")
                    nopen++;
                if (S.Substring(i, 1) == "}")
                    nclose++;

                if (nclose == nopen)
                {
                    istop = i;
                    break;
                }
            }
            return istop;
        }

        private List<String> GetDirs(String S)
        {
            List<String> Res = new List<String>();
            int istart = 0;
            int istop = 0;
            istart = GetOpenTag(S, istop);
            while (istart > -1)
            {
                istop = GetCloseTag(S, istart);
                if (istop > -1)
                {
                    Res.Add(S.Substring(istart + 1, istop - istart - 1));
                    istart = GetOpenTag(S, istop);
                }
                else
                {
                    istart = -1;
                }
            }
            return Res;
        }


        private CodeName  GetCodeNameDir(String S)
        {
            S = S.Replace(@"""", "");
            CodeName cn;
            String[] Sep = {","};
            String[] r = S.Split(Sep, StringSplitOptions.None);
            if (r.Length == 1 | String.IsNullOrEmpty(r[0]))
            {
                cn.Cod = "";
                cn.Name = "";
            }
            else
            { 
                
                if (isNumberText(r[0]))
                {
                    cn.Cod = r[0];
                    cn.Name = r[1];
                }
                else
                    if (isNumberText(r[1]))
                    {
                        cn.Cod = r[1];
                        cn.Name = r[0];
                    }
                    else 
                    {
                        cn.Name = r[0];
                        cn.Cod = "";
                    }
            }
            return cn;
        }

        private SortedList GetDDSFile(String FileName)
        {
            SortedList meta = new SortedList();
            String[] lines = File.ReadAllLines(FileName, Encoding.GetEncoding(1251));
            String[] sep = { "|" };
            for (int i = 0; i < lines.Length; i++)
            {
                String s = lines[i];
                if (s.Length > 2)
                {
                    String pref = s.Substring(0, 2);
                    if (pref == "T=" | pref == "F=")
                    {
                        String[] buf = s.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                        String cod;
                        if (pref=="T=")
                        {
                            cod = buf[2].Trim();
                            if (cod.Length < 3)
                                cod = buf[0].Trim().Replace(pref, "");

                        }
                        else
                        {
                            cod = buf[0].Trim().Replace(pref, "");
                        }
                        String name = buf[1].Trim().Replace(" ", "_").Replace("(", "").Replace(")", "").Replace(".", "");
                        if (!meta.ContainsKey(cod))
                        {
                            meta.Add(cod, name);
                        }
                    }
                }
            }
            return meta;
        }


        private void ParseMainStream(String Root, String S, SortedList meta)
        {
            CodeName cn = GetCodeNameDir(S);
            String rstr = "";
            if (!String.IsNullOrEmpty(cn.Cod))
            {
                if (!String.IsNullOrEmpty(cn.Name))
                    rstr = RepName(Root + cn.Name);
                else
                    rstr = RepName(Root);

                if (!meta.ContainsKey(cn.Cod))
                {
                    
                    meta.Add(cn.Cod, rstr);
                    rstr = cn.Cod + (char)9 + rstr;
                    sb.AppendLine(rstr);
                    pbar.PerformStep();
                    Application.DoEvents();
                }
            }
            

            if (!String.IsNullOrEmpty(cn.Name))
            { 
                Root = Root + cn.Name + ".";
            }
                List<String> Dirs = GetDirs(S);

                foreach (String sd in Dirs)
                {
                    ParseMainStream(Root, sd, meta); 
                }
            
        }




        private String LoadTab(String FileName, String ConnectStr)
        {
            String Res = "";

            

            String[] Sep = new String[4];
            Sep[0] = "\nGO ";
            Sep[1] = "\nGO";
            Sep[2] = "\ngo ";
            Sep[3] = "\ngo";
            StreamReader sread = new StreamReader(FileName, Encoding.GetEncoding(1251));
            String so = sread.ReadToEnd();
            sread.Close();
            String[] sb = so.Split(Sep, StringSplitOptions.RemoveEmptyEntries);



            SqlConnection cn = new SqlConnection(ConnectStr);

            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            pbar.Minimum = 0;
            pbar.Maximum = sb.Length;
            pbar.Step = 1;
            pbar.Value = 0;

            foreach (String s in sb)
            {
                try
                {
                    cm.CommandText = s;
                    cm.ExecuteNonQuery();
                    pbar.PerformStep();
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    if (Res == "")
                        Res = s + (char)13 + (char)10 + ex.Message;
                    else
                        Res = Res + s + (char)13 + (char)10 + (char)13 + (char)10 + ex.Message;



                }
            }


            cn.Close();

            return Res;

        }


        private void button5_Click(object sender, EventArgs e)
        {
            

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FileSQL.Text))
            {
                MessageBox.Show("Не указан файл скрипта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            
            if (String.IsNullOrEmpty(BaseName.Text) |
                String.IsNullOrEmpty(ServerName.Text) |
                String.IsNullOrEmpty(SQLLogin.Text) |
                String.IsNullOrEmpty(SQLPassword.Text))
            {
                MessageBox.Show("Не указаны параметры соединения с базой 1С!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            String[] Par = { ServerName.Text, SQLLogin.Text, SQLPassword.Text, BaseName.Text };
            String cn = String.Format("data source={0};User ID={1};Password={2};database={3}", Par);

            pbar.Value = 0;
            pbar.Visible = true;
            Application.DoEvents();
            HelpBox.Text = LoadTab(FileSQL.Text, cn);
            pbar.Visible = false;
            MessageBox.Show("Скрипт успешно выполнен!", "Внимание!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AboutProg ap = new AboutProg();
            ap.ShowDialog();
        }


        private String DBADetect(String FName)
        {
            byte[] dba = File.ReadAllBytes(FName);
            String SQLKey = "19465912879oiuxc ensdfaiuo3i73798kjl";
            byte[] sqlb = Encoding.GetEncoding(1251).GetBytes(SQLKey);
            byte[] resb = new byte[dba.Length];

            for (int i = 0; i < dba.Length; i++)
            {

                resb[i] = (byte)((int)dba[i] ^ (int)sqlb[i % 36]);
            }

            String Res = Encoding.GetEncoding(1251).GetString(resb);
            return Res;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SortedList meta = GetMainStream(FileObject.Text);
            StringBuilder s = new StringBuilder();
            foreach (Object k in meta.Keys)
            {
                String l = (String)k + (char)9 + (String)meta[k];
                s.AppendLine(l);
            }
            HelpBox.Text = s.ToString();
            
            /*
            CompoundFile cf = null;
            cf = new CompoundFile(FileObject.Text);
            CFStorage r = cf.RootStorage;

            r = r.GetStorage("Metadata");
            CFStream st = r.GetStream("Main MetaData Stream");
            byte[] rbyte = st.GetData();
            HelpBox.Text = Encoding.GetEncoding(1251).GetString(rbyte, 10, rbyte.Length - 10);
             */ 
        }




        private void button8_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(FileSQL.Text))
            {
                MessageBox.Show("Не указан файл скрипта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            if (String.IsNullOrEmpty(BaseName.Text) |
                String.IsNullOrEmpty(ServerName.Text) |
                String.IsNullOrEmpty(SQLLogin.Text) |
                String.IsNullOrEmpty(SQLPassword.Text))
            {
                MessageBox.Show("Не указаны параметры соединения с базой 1С!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            String[] Par = { ServerName.Text, SQLLogin.Text, SQLPassword.Text, BaseName.Text };
            String cn = String.Format("data source={0};User ID={1};Password={2};database={3}", Par);

            String BName = "";
            if (chBase.Checked)
            { BName = BaseName.Text; }

            pbar.Value = 0;
            pbar.Visible = true;
            Application.DoEvents();
            String Res = CreateSQL8(cn, BName);
            HelpBox.Text = Res;
            File.WriteAllText(FileSQL.Text, Res, Encoding.GetEncoding(1251));
            pbar.Visible = false;
            MessageBox.Show("Скрипт успешно создан!", "Внимание!");
        }

        private String RepName8(String S)
        {
            return S.Replace("_Fld", "");
        }

        private String CreateSQL8(String cn, String BName)
        {
            try
            {
                if (!String.IsNullOrEmpty(BName))
                { BName = BName + ".."; }

                String tsql = "select BinaryData  from Config where [FileName] = 'root'";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(tsql, cn);
                da.Fill(dt);
                byte[] buf = (byte[])dt.Rows[0][0];
                buf = decomp(buf);
                String binData = Encoding.UTF8.GetString(buf);
                String[] sep = { "," };
                String[] res = binData.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                String code = res[1] + ",00000000-0000-0000-0000-000000000000";

                //Читаем список полей
                dt.Rows.Clear();
                tsql = "select BinaryData  from Params where [FileName] = 'DBNames'";
                da = new SqlDataAdapter(tsql, cn);
                da.Fill(dt);
                buf = (byte[])dt.Rows[0][0];
                buf = decomp(buf);
                String CodeList = Encoding.UTF8.GetString(buf);
                String WordList = "";


                dt.Rows.Clear();
                tsql = "select BinaryData  from Params where [FileName] not in ('DBNames', 'DBNamesVersion', 'evlogparams.inf', 'ibparams.inf', 'locale.inf', 'log.inf', 'siVersions', 'users.usr')";
                da = new SqlDataAdapter(tsql, cn);
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    buf = (byte[])dt.Rows[i][0];
                    buf = decomp(buf);
                    binData = Encoding.UTF8.GetString(buf);
                    if (binData.IndexOf(code) > -1)
                    {
                        WordList = binData;
                        break;
                    }
                }

                DataTable FldList = CreateTable8(CodeList, WordList);





                DataTable TabList = new DataTable();
                DataTable ColList = new DataTable();

                String sql = "select [name], id, ' ' C8Name from sysobjects where xtype = 'U' and category = 0 order by 1";

                //(left([name], 1) <> '_' )
                try
                {
                    da = new SqlDataAdapter(sql, cn);
                    da.Fill(TabList);
                    sql = "select [name], id, ' ' C8Name from syscolumns (nolock)";
                    da.SelectCommand.CommandText = sql;
                    da.Fill(ColList);
                }
                catch (Exception ex)
                {
                    HelpBox.Text = ex.Message;
                    return "";
                }


                for (int i = 0; i < TabList.Rows.Count; i++)
                {
                    TabList.Rows[i]["C8Name"] = Parse8Name(TabList.Rows[i]["name"].ToString(), FldList);
                }


                for (int i = 0; i < ColList.Rows.Count; i++)
                {
                    ColList.Rows[i]["C8Name"] = Parse8Name(ColList.Rows[i]["name"].ToString(), FldList);
                }



                String Res = CreateText8(TabList, ColList, BName);

                return Res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        
        }


        private String CreateText8(DataTable TabList, DataTable ColList, String Base)
        {

            String ResSQL = "";
            String val = "";
            

            


            DataRow[] Rows = TabList.Select("name <> C8Name");

            pbar.Minimum = 0;
            pbar.Maximum = Rows.Length;
            pbar.Step = 1;
            pbar.Value = 0;

            for (int i = 0; i < Rows.Length; i++)
            {
                val = RepName8(Rows[i]["C8Name"].ToString());
                String CView = "if (exists(select * from sysobjects where [name] = '" + val + "')) drop view [" + val + "]";

                if (!chDel.Checked)  //Только удаляем таблицы
                {
                    CView = CView + "\r\nGO\r\nCREATE VIEW [" + val + "]\r\nAS\r\nSELECT";

                    DataRow[] cols = ColList.Select("id=" + Rows[i]["id"].ToString());

                    ArrayList aval = new ArrayList();
                    for (int j = 0; j < cols.Length; j++)
                    {
                     
                        val = RepName8(cols[j]["C8Name"].ToString());
                        if (aval.IndexOf(val) > -1)
                        {
                            val = val + j.ToString();
                        }
                        else
                        { aval.Add(val); }

                        CView = CView + "\r\n\t" + cols[j]["name"].ToString() + " AS [" + val + "]";
                        if (j < cols.Length - 1)
                        { CView = CView + ","; }
                    }

                    CView = CView + "\r\nFROM " + Base + Rows[i]["name"].ToString() + "(NOLOCK)";
                }

                CView = CView + "\r\nGO\r\n";
                ResSQL = ResSQL + CView;
                pbar.PerformStep();
                Application.DoEvents();
                
            }

            return ResSQL;
        }

        private DataTable CreateTable8(String CodeList, String WordList)
        {
            DataTable CodeTab = new DataTable();
            DataColumn cl = new DataColumn("CodGUID", Type.GetType("System.String"));
            CodeTab.Columns.Add(cl);

            cl = new DataColumn("CodName", Type.GetType("System.String"));
            CodeTab.Columns.Add(cl);

            cl = new DataColumn("CodNUM", Type.GetType("System.String"));
            CodeTab.Columns.Add(cl);


            cl = new DataColumn("CodWord", Type.GetType("System.String"));
            CodeTab.Columns.Add(cl);



            DataTable WordTab = new DataTable();
            
            cl = new DataColumn("CodGUID", Type.GetType("System.String"));
            WordTab.Columns.Add(cl);

            cl = new DataColumn("WordName", Type.GetType("System.String"));
            WordTab.Columns.Add(cl);

            String[] sep = { "\n", "\r" };
            String[] sep1 = { "," };
            String[] bf;

            CodeList = CodeList.Replace("{", @"""").Replace("}", @"""").Replace(@"""", "");
            String[] cods = CodeList.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cods.Length; i++)
            {
                    bf = cods[i].Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                    if (bf.Length == 3)
                        if (bf[0]!="00000000-0000-0000-0000-000000000000")
                            {
                            DataRow rw = CodeTab.NewRow();
                            rw["CodGUID"] = bf[0];
                            rw["CodName"] = bf[1];
                            rw["CodNUM"] = bf[2];
                            CodeTab.Rows.Add(rw);
                            }
                
            }
            WordList = WordList.Replace(@"""", "");
            cods = WordList.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cods.Length; i++)
            {
                int k = cods[i].IndexOf("},");
                if ((k > -1) & (k < cods[i].Length - 2 ))
                {
                    cods[i] = cods[i].Substring(k);
                    bf = cods[i].Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                    if (bf.Length == 7)
                        {
                            DataRow rw = WordTab.NewRow();
                            rw["CodGUID"] = bf[3];
                            rw["WordName"] = bf[6];
                            WordTab.Rows.Add(rw);
                        }
                }
            }

            for (int i = 0; i < CodeTab.Rows.Count; i++)
            {
                DataRow[] rw = WordTab.Select("CodGUID='" + CodeTab.Rows[i]["CodGUID"].ToString() + "'");
                if (rw.Length > 0)
                {
                    CodeTab.Rows[i]["CodWord"] = rw[0]["WordName"].ToString().Replace("_", "");
                }
            }

            return CodeTab;
        }



        private byte[] decomp(byte[] srcbyte)
        {
            MemoryStream msrc = new MemoryStream(srcbyte);
            MemoryStream mdst = new MemoryStream();
            try
            {
                DeflateStream dfl = new DeflateStream(msrc, CompressionMode.Decompress);
                byte[] buf = new byte[1024];
                int n = dfl.Read(buf, 0, buf.Length);
                while (n > 0)
                {
                    mdst.Write(buf, 0, n);
                    n = dfl.Read(buf, 0, buf.Length);
                }
                dfl.Close();
                msrc.Close();
                mdst.Close();
                byte[] res = mdst.ToArray();
                return res;

            }
            catch
            {
                return srcbyte;
            }
        
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (OD.ShowDialog() == DialogResult.OK)
            {
                FileDDS.Text = OD.FileName;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*
            if (!File.Exists(@"\\nissa.ru\dfs\Nissa_soft\NissaReport.exe"))
            {
                MessageBox.Show("Нелегальная копия программы!", "Внимание!");
                Application.Exit();
            }
             */ 

        }


    }
}
