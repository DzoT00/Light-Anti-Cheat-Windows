using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LightAnti_CheatWindows
{
    public class RuntimeCompileCode
    {
        public static void compileInMemory(string[] code)
        {
            CompilerParameters compilerParameters = new CompilerParameters();
            string currentDirectory = Directory.GetCurrentDirectory();
            compilerParameters.GenerateInMemory = true;
            compilerParameters.TreatWarningsAsErrors = false;
            compilerParameters.GenerateExecutable = false;
            compilerParameters.CompilerOptions = "/optimize";

            CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();
            CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, code);
            if (compilerResults.Errors.HasErrors)
            {
                string text = "Compile error: ";
                foreach (CompilerError compilerError in compilerResults.Errors)
                {
                    text = text + "\r\n" + compilerError.ToString();
                }
                throw new Exception(text);
            }
            Module module = compilerResults.CompiledAssembly.GetModules()[0];
            Type type = null;
            MethodInfo methodInfo = null;
            if (module != null)
            {
                type = module.GetType("HelloWorld.HelloWorldClass");
            }
            if (type != null)
            {
                methodInfo = type.GetMethod("Main");
            }
            if (methodInfo != null)
            {
                methodInfo.Invoke(null, null);
            }
        }
    }
}
