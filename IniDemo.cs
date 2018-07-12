using System;
using System.Runtime.InteropSevices;
using System.Text;

namespace IniDemo
{
    ///<summary>
    ///IniFile class
    ///read ini file
    ///</summary>
    public class IniFile
    {
        public string path;

        ///<summary>
        ///导入系统中写入ini文件的函数
        ///</summary>
        ///<param name = "section"> 段</param>
        ///<param name = "key">键</param>
        ///<param name = "val">值</param>
        ///<param name = "filePath">路径<param>
        ///<returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);

        ///<summary>
        ///导入系统中读取ini文件的函数
        ///</summary>
        ///<param name ="section">段</param>
        ///<param name ="key">键</param>
        ///<param name ="def">定义</param>
        ///<param name ="retVal">值</param>
        ///<param name ="size">大小</param>
        ///<param name ="filePath">路径</param>
        ///<returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int Size,string filePath);
        
        ///<summary>
        ///构造函数
        ///</summary>
        ///<param name ="iniPath">文件路径</param>
        public IniFile(string iniPath)
        {
            path = iniPath;
        }

        ///<summary>
        ///写入数据
        ///</summary>
        ///<param name ="section"></param>
        ///<param name ="key"></param>
        ///<param name ="value"></param>
        public void IniWriteValue(string section,string key,string value)
        {
            WritePrivateProfileString(section,key,value,this.path);
        }

        ///<summary>
        ///读取数据
        ///</summary>
        ///<param name ="section"></param>
        ///<param name ="key"></param>
        ///<param name ="value"></param>
        public string IniReadValue(string section,string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section,key,"",temp,255,this.path);
            return temp.ToString();
        }
    }
}