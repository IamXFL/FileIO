using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using HiSonic.Entities;
using System;

public class CSVFileOpera
{
    /// <summary>
    /// 将DataTable中数据写入到CSV文件中
    /// </summary>
    /// <param name="dt">提供保存数据的DataTable</param>
    /// <param name="fileName">CSV的文件路径</param>
    public static void SaveCSV(DataTable dt, string fullPath)
    {
        FileInfo fi = new FileInfo(fullPath);
        if (!fi.Directory.Exists)
        {
            fi.Directory.Create();
        }
        FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        string data = "";
        //写出列名称
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            data += dt.Columns[i].ColumnName.ToString();
            if (i < dt.Columns.Count - 1)
            {
                data += ",";
            }
        }
        sw.WriteLine(data);
        //写出各行数据
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            data = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string str = dt.Rows[i][j].ToString();
                str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                if (str.Contains(",") || str.Contains("\"") 
                    || str.Contains("\r") || str.Contains("\n")) //含逗号 冒号 换行符的需要放到引号中
                {
                    str = string.Format("\"{0}\"", str);
                }

                data += str;
                if (j < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
        }
        sw.Close();
        fs.Close();
        DialogResult result = MessageBox.Show("CSV文件保存成功！");
        if (result == DialogResult.OK)
        {
            ;
        }
    }

    /// <summary>
    /// 将CSV文件的数据读取到DataTable中
    /// </summary>
    /// <param name="fileName">CSV文件路径</param>
    /// <returns>返回读取了CSV数据的DataTable</returns>
    public static DataTable OpenCSV_DataTable(string filePath)
    {
        Encoding encoding = Encoding.ASCII; // Common.GetType(filePath); //Encoding.ASCII;//
        DataTable dt = new DataTable();
        FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        
        //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
        StreamReader sr = new StreamReader(fs, encoding);
        //string fileContent = sr.ReadToEnd();
        //encoding = sr.CurrentEncoding;
        //记录每次读取的一行记录
        string strLine = "";
        //记录每行记录中的各字段内容
        string[] aryLine = null;
        string[] tableHead = null;
        //标示列数
        int columnCount = 0;
        //标示是否是读取的第一行
        bool IsFirst = true;
        //逐行读取CSV中的数据
        while ((strLine = sr.ReadLine()) != null)
        {
            //strLine = Common.ConvertStringUTF8(strLine, encoding);
            //strLine = Common.ConvertStringUTF8(strLine);

            if (IsFirst == true)
            {
                tableHead = strLine.Split(',');
                IsFirst = false;
                columnCount = tableHead.Length;
                //创建列
                for (int i = 0; i < columnCount; i++)
                {
                    DataColumn dc = new DataColumn(tableHead[i]);
                    dt.Columns.Add(dc);
                }
            }
            else
            {
                aryLine = strLine.Split(',');
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columnCount; j++)
                {
                    dr[j] = aryLine[j];
                }
                dt.Rows.Add(dr);
            }
        }
        if (aryLine != null && aryLine.Length > 0)
        {
            dt.DefaultView.Sort = tableHead[0] + " " + "asc";
        }
        
        sr.Close();
        fs.Close();
        return dt;
    }

    /// <summary>
    /// 将CSV文件的数据读取到DataTable中
    /// </summary>
    /// <param name="fileName">CSV文件路径</param>
    /// <returns>返回读取了CSV数据的DataTable</returns>
    public static Image OpenCSV_Image(string filePath, ref float[][] value)
    {
        Bitmap bitmap = new Bitmap(800, 1000);
        value = new float[bitmap.Width][];
        for (int i = 0; i < bitmap.Width; i++)
        {
            value[i] = new float[bitmap.Height];
        }

        

        Encoding encoding = Encoding.ASCII; // Common.GetType(filePath); //Encoding.ASCII;//
        FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

        //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
        StreamReader sr = new StreamReader(fs, encoding);
        //string fileContent = sr.ReadToEnd();
        //encoding = sr.CurrentEncoding;
        //记录每次读取的一行记录
        string strLine = "";
        //记录每行记录中的各字段内容
        string[] aryLine = null;
        string[] tableHead = null;
        //标示列数
        int columnCount = 0;
        //标示是否是读取的第一行
        bool IsFirst = true;
        //逐行读取CSV中的数据
        int row = 0;
        while ((strLine = sr.ReadLine()) != null)
        {
            //strLine = Common.ConvertStringUTF8(strLine, encoding);
            //strLine = Common.ConvertStringUTF8(strLine);

            aryLine = strLine.Split(',');
            for (int col = 0; col < bitmap.Width; col++)
            {
                value[col][row] = (float)((Convert.ToDouble(aryLine[col]) + 8));
            }
            row++;
        }

        sr.Close();
        fs.Close();

        for (int j = 0; j < bitmap.Height; j++)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                int gray_idx = (int)(value[i][j] * 16);
                if (gray_idx < 0)
                {
                    gray_idx = 0;
                }
                if (gray_idx > 255)
                {
                    gray_idx = 255; 
                }
                bitmap.SetPixel(i, j, ColorEntry.Instance.Convert_Gray_to_Color((byte)gray_idx));
            }
        }


        return bitmap;
    }
}