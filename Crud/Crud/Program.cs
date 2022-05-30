using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;


namespace Crud
{
    class Program
    {
        static void Main(string[] args)
        {
            Dao cadUsuario = new Dao();
            cadUsuario.CadastroFinal();
        }
    }
}
