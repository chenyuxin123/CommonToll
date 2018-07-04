using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Web.Services;
using System.Web.Services.Description;
using Microsoft.CSharp;
using System;

namespace CommonToll.Toll
{
    /// <summary>
    /// 动态调用WebService
    /// </summary>
    public static class WebServiceHelper
    {
        /// <summary>  
        /// 动态调用WebService  
        /// </summary>  
        /// <param name="url">WebService地址</param>  
        /// <param name="methodname">WebService方法名</param>  
        /// <param name="args">参数列表</param>  
        /// <returns>返回object</returns>  
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(url, null, methodname, args);
        }
        /// <summary>  
        /// 动态调用WebService  
        /// </summary>  
        /// <param name="url">WebService地址</param>  
        /// <param name="classname">类名</param>  
        /// <param name="methodname">WebService方法名</param>  
        /// <param name="args">参数列表</param>  
        /// <returns>返回object</returns>  
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "ServiceBase.WebService.DynamicWebLoad";
            if (string.IsNullOrEmpty(classname))
            {
                classname = WebServiceHelper.GetClassName(url);
            }
            //获取服务描述语言(WSDL)  
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);
            //生成客户端代码类代码  
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            //CSharpCodeProvider csc = new CSharpCodeProvider();
            //ICodeCompiler icc = csc.CreateCompiler();
            CSharpCodeProvider icc = new CSharpCodeProvider();
            //设定编译器的参数  
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类  
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString() + System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            // 生成代理实例并调用方法  
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);
            return mi.Invoke(obj, args);
        }

        /// <summary>  
        /// 得到URL中的WebService名称  
        /// </summary>  
        /// <param name="url">URL地址</param>  
        /// <returns>如http://wwww.baidu.com/service.asmx 则返回service</returns>  
        private static string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string url = "http://localhost:2697/Service1.asmx";
        //    object b = InvokeWebService.WebServiceHelper.InvokeWebService(url, "HelloWorld", null);
        //    MessageBox.Show(b.ToString());
        //}
    }
}
