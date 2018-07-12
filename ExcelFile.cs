using MSExcel = Microsoft.Office.Interop.Excel;
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

        MSExcel.Application excelApp;           // Excel 应用程序变量
        MSExcel.Workbook excelDoc;              // Excel 文档变量

        path = @"F:\xianfeng\MyExcel.xlsx";
        excelApp = new MSExcel.ApplicationClass();

        if (File.Exists((string)path))
        {
            File.Delete((string)path);
        }

        //由于使用的是COM库，因此有许多变量需要用Nothing 代替
        Object Nothing = Missing.Value;
        excelDoc = excelApp.Workbooks.Add(Nothing);

        //WdSaveFormat 为Excel 文档的保存格式
        Object format = MSExcel.XlFileFormat.xlWorkbookDefault;

        //将excelDoc 文档对象的内容保存为xlsx文档
        excelDoc.SaveAs(path, Nothing, Nothing, Nothing, Nothing, Nothing, MSExcel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);

        //关闭excelDoc  文档对象
        excelDoc.Close(Nothing, Nothing, Nothing);

        //关闭excelApp 组件对象
        excelApp.Quit();

        Console.WriteLine(path + " 创建完毕");
    }
}