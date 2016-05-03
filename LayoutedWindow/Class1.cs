
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Reflection;
//using System.IO;
//using System.Windows.Forms;

//namespace LayoutedWindow
//{
//    namespace testdd
//    {
//        public class Class1
//        {
//            private bool IsTiny(MethodBody mbd)
//            {
//                if (mbd.MaxStackSize > 8)
//                    return false;//
//                //if(mbd.LocalSignatureMetadataToken != 0)
//                //    return false;
//                if (mbd.LocalVariables.Count > 0)
//                    return false;
//                if (mbd.ExceptionHandlingClauses.Count > 0)
//                    return false;
//                if (mbd.GetILAsByteArray().Length > 63)
//                    return false;
//                return true;
//            }

//            private bool IsSEHTiny(MethodBody mb)
//            {
//                int n = mb.ExceptionHandlingClauses.Count;
//                int datasize = n * 12 + 4;
//                if (datasize > 255)
//                    return false;
//                foreach (ExceptionHandlingClause ehc in mb.ExceptionHandlingClauses)
//                {
//                    if (ehc.HandlerLength > 255)
//                        return false;
//                    if (ehc.TryLength > 255)
//                        return false;
//                    if (ehc.TryOffset > 65535)
//                        return false;
//                    if (ehc.HandlerOffset > 65535)
//                        return false;
//                }
//                return true;
//            }
//            private void WriteHeader(BinaryWriter bw, MethodBody mb)
//            {
//                int codesize = mb.GetILAsByteArray().Length;
//                if (IsTiny(mb))
//                {
//                    byte bt = 2;
//                    bt = (byte)(bt + codesize * 4);
//                    bw.Write(bt);
//                    return;
//                }
//                //fat mode here
//                byte fg = 3;//fat flag
//                if (mb.LocalVariables.Count > 0 && mb.InitLocals)
//                    fg |= 0x10;
//                if (mb.ExceptionHandlingClauses.Count > 0)
//                    fg |= 0x8;
//                bw.Write(fg);// byte 1            
//                bw.Write((byte)0x30);//byte 2
//                bw.Write((ushort)mb.MaxStackSize);// byte 3, 4
//                bw.Write(codesize);//byte 5-8
//                bw.Write(mb.LocalSignatureMetadataToken);//byte 9-12
//            }
//            private void WriteILCode(BinaryWriter bw, MethodBody mb)
//            {
//                int codesize = mb.GetILAsByteArray().Length;
//                bw.Write(mb.GetILAsByteArray());

//                //对齐 4 bytes
//                int ig = codesize & 3;
//                if (ig == 0)
//                    return;
//                if (mb.ExceptionHandlingClauses.Count == 0)
//                    return;//无SEH;
//                ig = 4 - ig;
//                for (int i = 0; i < ig; i++)
//                {
//                    bw.Write((byte)0);
//                }
//            }
//            private void WriteTinySEHHeader(BinaryWriter bw, MethodBody mb)
//            {
//                int n = mb.ExceptionHandlingClauses.Count;
//                int datasize = n * 12 + 4;
//                bw.Write((byte)1);
//                bw.Write((byte)datasize);
//                bw.Write((byte)0);
//                bw.Write((byte)0);
//            }
//            private void WriteFatSEHHeader(BinaryWriter bw, MethodBody mb)
//            {
//                int n = mb.ExceptionHandlingClauses.Count;
//                int datasize = n * 24 + 4;
//                datasize = datasize * 0x100 + 0x41;
//                bw.Write(datasize);
//            }
//            private void WriteSeHTinyRow(BinaryWriter bw, ExceptionHandlingClause ehc)
//            {
//                ushort flag = 0;

//                if (ehc.Flags == ExceptionHandlingClauseOptions.Filter)
//                    flag += 1;
//                if (ehc.Flags == ExceptionHandlingClauseOptions.Fault)
//                    flag += 4;
//                if (ehc.Flags == ExceptionHandlingClauseOptions.Finally)
//                    flag += 2;
//                bw.Write(flag);

//                bw.Write((ushort)ehc.TryOffset);
//                bw.Write((byte)ehc.TryLength);

//                bw.Write((ushort)ehc.HandlerOffset);
//                bw.Write((byte)ehc.HandlerLength);
//                object obj = new object();
//                if (ehc.Flags == ExceptionHandlingClauseOptions.Clause /*|| ehc.CatchType != obj.GetType()*/)
//                    bw.Write(GetTypeToken(ehc.CatchType));
//                else
//                    bw.Write(ehc.FilterOffset);

//            }

//            private void WriteSeHFatRow(BinaryWriter bw, ExceptionHandlingClause ehc)
//            {
//                int flag = 0;

//                if (ehc.Flags == ExceptionHandlingClauseOptions.Filter)
//                    flag += 1;
//                if (ehc.Flags == ExceptionHandlingClauseOptions.Fault)
//                    flag += 4;
//                if (ehc.Flags == ExceptionHandlingClauseOptions.Finally)
//                    flag += 2;
//                bw.Write(flag);//

//                bw.Write(ehc.TryOffset);
//                bw.Write(ehc.TryLength);

//                bw.Write(ehc.HandlerOffset);
//                bw.Write(ehc.HandlerLength);
//                object obj = new object();
//                if (ehc.Flags == ExceptionHandlingClauseOptions.Clause /*|| ehc.CatchType != obj.GetType()*/)
//                    bw.Write(GetTypeToken(ehc.CatchType));
//                else
//                    bw.Write(ehc.FilterOffset);


//            }

//            private void WriteSEH(BinaryWriter bw, MethodBody mb)
//            {
//                if (mb.ExceptionHandlingClauses.Count == 0)
//                    return;
//                bool bTiny = IsSEHTiny(mb);
//                if (bTiny)
//                    WriteTinySEHHeader(bw, mb);
//                else
//                    WriteFatSEHHeader(bw, mb);
//                foreach (ExceptionHandlingClause ehc in mb.ExceptionHandlingClauses)
//                {
//                    if (bTiny)
//                        WriteSeHTinyRow(bw, ehc);
//                    else
//                        WriteSeHFatRow(bw, ehc);
//                }
//            }


//            public static void Dump()
//            {
//                Class1 cls = new Class1();
//                cls.DoIt();
//            }
//            public Class1()
//            {
//                //nil
//                int i = 0;
//                try
//                {
//                    string s = "";
//                    if (s == "")
//                        i = 2;

//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("err" + ex.ToString());
//                }
//            }

//            protected void DoIt()
//            {
//                Assembly ass = Assembly.GetEntryAssembly();
//                DumpAssembly(ass, @"D:\4.0.1.0\dumped.exe");

//            }

//            /// <summary>
//            /// Dump程序集的 IL字节代码到指定目录；
//            /// </summary>
//            /// <param name="ass"></param>
//            /// <param name="path"></param>
//            private void DumpAssembly(Assembly ass, string path)
//            {
//                //////////////////////////////////////////////////////////////////////////
//                if (!testdd.com.WrapperClass.MetaInit(ass.Location))
//                {
//                    MessageBox.Show("error meta");
//                    return;
//                }
//                FileStream fs = new FileStream(path, System.IO.FileMode.Open, FileAccess.Write);
//                BinaryWriter bw = new BinaryWriter(fs);

//                Type[] tps = ass.GetTypes();
//                for (int i = 0; i < tps.Length; i++)
//                {
//                    DumpType(tps[i], bw);
//                }
//                bw.Flush();
//                bw.Close();
//                bw = null;
//                fs.Close();
//                fs = null;
//                MessageBox.Show("ok");
//            }
//            private void DumpType(Type tp, BinaryWriter sw)
//            {
//                BindingFlags bf = BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
//                   BindingFlags.Public | BindingFlags.Static
//                   | BindingFlags.Instance;


//                MemberInfo[] mbis = tp.GetMembers(bf);
//                for (int i = 0; i < mbis.Length; i++)
//                {
//                    MemberInfo mbi = mbis[i];

//                    try
//                    {
//                        if (mbi.MemberType == MemberTypes.Method || mbi.MemberType == MemberTypes.Constructor)
//                        {
//                            DumpMethod((MethodBase)mbi, sw);
//                        }
//                    }
//                    catch (Exception)
//                    {

//                    }

//                }

//            }

//            private void DumpMethod(MethodBase mb, BinaryWriter sw)
//            {
//                MethodBody mbd = mb.GetMethodBody();
//                if (mbd == null)
//                    return;
//                SetOffset(sw, mb.MetadataToken);

//                WriteHeader(sw, mbd);

//                WriteILCode(sw, mbd);

//                WriteSEH(sw, mbd);

//            }
//            private int GetTypeToken(Type tp)
//            {
//                if (tp.Assembly == Assembly.GetEntryAssembly())
//                    return tp.MetadataToken;
//                Assembly ass = Assembly.GetEntryAssembly();
//                uint tk = testdd.com.WrapperClass.GetTypeToken(tp);
//                if (tk == 0)
//                {
//                    MessageBox.Show("error tk");
//                    return 0x100005f;
//                }
//                return (int)tk;
//            }
//            private void SetOffset(BinaryWriter bw, int mbtk)
//            {
//                uint token = (uint)mbtk;
//                uint offsetrva = testdd.com.WrapperClass.GetMehodRVA(token);
//                int offsetra = (int)(offsetrva - 0x1000);
//                bw.Seek(offsetra, SeekOrigin.Begin);
//            }
//        }


//    }
//}
