using System;
suing System.Xml;

class Program
{
    static void Main(string[] args)
    {   

        ///<summary>
        ///写入xml实例
        ///</summary>
        string path = @"F:\xianfeng\Hisonic.xml";
        try
        {
            //创建设置i
            XmlWriterSettings mySettings = new XmlWriterSettings();
            mySettings.Indent = true;
            mySettings.IndentChars =("  ");  

            XmlWriter myWriter = XmlWriter.Create(path,mySettings);

            //输入XML 数据
            myWriter.WriteStartElement("people");        //写入根元素
            myWriter.WriteStartAttribute("name");        //写入根元素的属性
            myWriter.WriteValue("Chinese");              //写入根元素的属性值
            myWriter.WriteEndAttribute();
            myWriter.WriteAttributeString("sex","male");   // 写入属性值     写入属性值最简单一般的方法;
            myWriter.WriteElementString("name","zhang");  // 写入一个元素
            myWriter.WriteEndElement();                   //结束写入
            myWriter.Flush();

            //写入属性值
            myWriter.WriteStartElement("people");           //写入根元素
            myWriter.WriteAttributeString("sex","male");    //写入属性值
            myWriter.WriteElementString("name","zhang");   // 写入元素
            myWriter.WriteEndElement();
            myWriter.Flush();

            //写入元素值
            myWriter.WriteStartElement("people");
            myWriter.WriteElementString("name","zhang");
            myWriter.WriteElementString("nation","China");
            myWriter.WriteEndElement();
            myWriter.Flush();


        }
        catch (System.Exception)
        {
            
            throw;
        }        
        Console.ReadLine();

        ///<summary>
        ///xml读取
        ///</summary>
        try
        {
            XmlReaderSetting setting = new XmlReaderSetting();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader myXmlReader = XmlReader.Create(path,settings);   // 实例化一个xmlreader, settings 为可选参数

            /*读取xml元素值*/
            //读取month 节点,输出并结束当前节点
            myXmlReader.ReadStartElement("month");
            var value = myXmlReader.ReadString();
            myXmlReader.ReadEndElement();

            //读取day 节点,
            myXmlReader.ReadStartElement("day");
            Console.WriteLine("day中包含的内容:");
            Console.WriteLine(myXmlReader.ReadString());
            myXmlReader.ReadEndElement();


            /*另一种读取方式 */
            while(myXmlReader.Read())
            {
                if(myXmlReader.IsStartElement())
                {
                    if(myXmlReader.IsEmptyElement)
                    {
                        Console.WriteLine("<{0}/>",myXmlReader.Name);
                    }
                    else
                    {
                        Console.WriteLine("<{0}>",myReader.Name));
                        myXmlReader.Read();
                        if(myreader.IsStartElement())
                        {
                            Console.Write(myXmlReader.Name);
                            Console.WriteLine(myXmlReader.ReadString());

                        }
                    }
                }
            }
            Console.WriteLine("</mail>");


            /* 读取xml 属性值 */
            myXmlReader.IsStartElement("mail");       // 直接跳至 mail 元素
            string date = myXmlReader.GetAttribute("date");   // 获取当前元素mail 的date 属性值

            /*在未知元素和属性名称时,读取整个xml文件 */
            while(myXmlReader.Read())
            {
                //检测当前myXmlReader 是否含有属性
                if(myXmlReader.HasAttributes)
                {
                    Console.WriteLine("<"+ myXmlReader.Name + "> 的属性:");
                    while(myXmlReader.MoveToNextAttribut() ())
                    {
                        Console.WriteLine("{0}={1}",myXmlReader.Name,myXmlReader.Value);
                    }
                }
            }
            
        }
        catch{
            //
        }
    }
}