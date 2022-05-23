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
    class usuario
    {
        public string nome;
        public string cpf;
        public string profissao;

        public void cadastroUsuario()
        {
            usuarioDAO cadUsuario = new usuarioDAO();
            cadUsuario.usuario();
            
               
        }


    }
}
