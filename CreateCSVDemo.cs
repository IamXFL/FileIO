using MSWord = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;
using System;

/*需要为项目添加对 Microsoft Excel X Object Library 的引用，X 对应版本号 */
/*引用的库位于 "COM" 选项卡下，添加成功后，引用项自动多出三个引用： Microsoft.Office.Core, Microsoft.Office.Interop.Excel,VBIDE */
/*如果提示无法嵌入互操作类型,解决方法:
  选中项目中引入的三个引用,鼠标右键,选择属性,把互操作类型设置为false;
     */
class Program
{
    static void Main(string[] args)
    {
        object path;
        string strContent;          // 文本内容变量

        MSWord.Application wordApp; // word 应用程序变量
        MSWord.Document wordDoc;    // word 文档变量

        path = @"F:\xianfeng\MyWord.docx";
        wordApp = new MSWord.ApplicationClass();

        if (File.Exists((string)path))
        {
            File.Delete((string)path);
        }

        //由于使用的是COM 库，因此有许多变量需要用Misssing.Value 代替
        Object Nothing = Missing.Value;
        wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

        //strContent = "您好！\n";
        //wordDoc.Paragraphs.Last.Range.Text = strContent;

        //strContent = "hello world";
        //wordDoc.Paragraphs.Last.Range.Text = strContent;

        //WdSaveFormat 为word 2007 文档的保存格式
        object format = MSWord.WdSaveFormat.wdFormatDocumentDefault;

        //将wordDoc 文档对象保存为docx文档
        wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

        //关闭wordDoc 文档对象
        wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);

        //关闭wordApp组件对象
        wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);

        Console.WriteLine(path + " 创建完毕");
    }
}