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
    /// ��DataTable������д�뵽CSV�ļ���
    /// </summary>
    /// <param name="dt">�ṩ�������ݵ�DataTable</param>
    /// <param name="fileName">CSV���ļ�·��</param>
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
        //д��������
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            data += dt.Columns[i].ColumnName.ToString();
            if (i < dt.Columns.Count - 1)
            {
                data += ",";
            }
        }
        sw.WriteLine(data);
        //д����������
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            data = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string str = dt.Rows[i][j].ToString();
                str = str.Replace("\"", "\"\"");//�滻Ӣ��ð�� Ӣ��ð����Ҫ��������ð��
                if (str.Contains(",") || str.Contains("\"") 
                    || str.Contains("\r") || str.Contains("\n")) //������ ð�� ���з�����Ҫ�ŵ�������
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
        DialogResult result = MessageBox.Show("CSV�ļ�����ɹ���");
        if (result == DialogResult.OK)
        {
            ;
        }
    }

    /// <summary>
    /// ��CSV�ļ������ݶ�ȡ��DataTable��
    /// </summary>
    /// <param name="fileName">CSV�ļ�·��</param>
    /// <returns>���ض�ȡ��CSV���ݵ�DataTable</returns>
    public static DataTable OpenCSV_DataTable(string filePath)
    {
        Encoding encoding = Encoding.ASCII; // Common.GetType(filePath); //Encoding.ASCII;//
        DataTable dt = new DataTable();
        FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        
        //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
        StreamReader sr = new StreamReader(fs, encoding);
        //string fileContent = sr.ReadToEnd();
        //encoding = sr.CurrentEncoding;
        //��¼ÿ�ζ�ȡ��һ�м�¼
        string strLine = "";
        //��¼ÿ�м�¼�еĸ��ֶ�����
        string[] aryLine = null;
        string[] tableHead = null;
        //��ʾ����
        int columnCount = 0;
        //��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
        bool IsFirst = true;
        //���ж�ȡCSV�е�����
        while ((strLine = sr.ReadLine()) != null)
        {
            //strLine = Common.ConvertStringUTF8(strLine, encoding);
            //strLine = Common.ConvertStringUTF8(strLine);

            if (IsFirst == true)
            {
                tableHead = strLine.Split(',');
                IsFirst = false;
                columnCount = tableHead.Length;
                //������
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
    /// ��CSV�ļ������ݶ�ȡ��DataTable��
    /// </summary>
    /// <param name="fileName">CSV�ļ�·��</param>
    /// <returns>���ض�ȡ��CSV���ݵ�DataTable</returns>
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
        //��¼ÿ�ζ�ȡ��һ�м�¼
        string strLine = "";
        //��¼ÿ�м�¼�еĸ��ֶ�����
        string[] aryLine = null;
        string[] tableHead = null;
        //��ʾ����
        int columnCount = 0;
        //��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
        bool IsFirst = true;
        //���ж�ȡCSV�е�����
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