using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Security;
using Compartilhados;

namespace AlteraPermissaoPastas
{
    class Program
    {
        static void Main(string[] args)

        {
            var diretorioDeExecucao = System.Reflection.Assembly.GetExecutingAssembly().Location;
            
            diretorioDeExecucao = diretorioDeExecucao.Substring(0, diretorioDeExecucao.LastIndexOf("bin"));
            
            try
            {
                AlteraPermissaoPastas(diretorioDeExecucao + "LOG");
                AlteraPermissaoPastas(diretorioDeExecucao + "App_Data");
                AlteraPermissaoPastas(diretorioDeExecucao + "Loads");


            }catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                Console.ReadKey();
                throw;
             
            }
        }

        private static void AlteraPermissaoPastas(string pasta)
        {
            DirectorySecurity oDirSec = Directory.GetAccessControl(pasta);

            // Define o usuário Everyone (Todos)
            SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            //SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
            NTAccount oAccount = sid.Translate(typeof(NTAccount)) as NTAccount;

            oDirSec.PurgeAccessRules(oAccount);

            FileSystemAccessRule fsAR = new FileSystemAccessRule(oAccount,
            FileSystemRights.Modify,
            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
            PropagationFlags.None,
            AccessControlType.Allow);

            // Atribui a regra de acesso alterada
            oDirSec.SetAccessRule(fsAR);
            Directory.SetAccessControl(pasta, oDirSec);
        }

        

    }

}
