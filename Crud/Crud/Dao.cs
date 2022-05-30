using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace Crud
{
    class Dao : Program
    {

        public string nome;
        public string cpf;
        public string profissao;
        private MySqlConnection Conexao;

        public void ConexaoBanco()
        {
            Conexao = new MySqlConnection("server=localhost;database=crud;uid=root;password=");            

        }

        public void ConexaoInicar()
        {
            try
            {
                Conexao.Open();
                Console.WriteLine("Conexão realizada com sucesso!!");
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Erro ao acessar o banco de dados, contate o administrador!!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public void Insert()
        {
            if (Conexao.State == ConnectionState.Open)
            {

            }
            else
            {
                Conexao.Open();
            }
            Console.WriteLine("Digite o Nome: ");
            cpf = Console.ReadLine();
            Console.WriteLine("Digite o CPF: ");
            nome = Console.ReadLine();
            Console.WriteLine("Digite a Profissão: ");
            profissao = Console.ReadLine();



            // inserindo um novo usuario no banco de dados
            string sqlInsert = "insert into  usuario values ('" + cpf + "','" + nome + "', '" + profissao + "')";

            MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, Conexao);
            cmdInsert.ExecuteNonQuery();
            Console.WriteLine("Usuário cadastrado com SUCESSO!!");

            Console.ReadKey();
            Console.Clear();
            Conexao.Close();
        }

        public void List()
        {
            if (this.Conexao.State == ConnectionState.Open)
            {

            }
            else
            {
                try
                {
                    this.Conexao.Open();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Erro ao tentar abrir a conexao com o banco de dados");
                    Console.Clear();            
                }
            }

            string sql = "select * from usuario";
            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Console.WriteLine("Nome: {0} \nCPF: {1} \nProfissão: {2}", rdr["nome"], rdr["cpf"], rdr["profissao"]);
                }
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Nenhum Usuário foi cadastro em nosso banco de dados!!");
                Console.ReadKey();
                Console.Clear();
            }
            Conexao.Close();
            Console.Clear();
        }

        public void Update()
        {
            if (Conexao.State == ConnectionState.Open)
            {

            }
            else
            {
                try
                {
                    Conexao.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Erro ao tentar abrir a conexao com o banco de dados");
                    Console.Clear();
                }
            }

            // atualizando um usuario através de seu cpf
            Console.WriteLine("Digite o cpf do usuário para altera-lo");
            cpf = Console.ReadLine();

            Console.WriteLine("Digite as novas informações: ");
            Console.WriteLine("Nome: ");
            nome = Console.ReadLine();
            Console.WriteLine("Profissão: ");
            profissao = Console.ReadLine();


            string sqlUpdate = "update usuario SET nome = @nome,  profissao = @profissao Where cpf = @cpf ";
            MySqlCommand cmd = new MySqlCommand(sqlUpdate, Conexao);
            cmd.Parameters.AddWithValue("@cpf", cpf);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@profissao", profissao);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Dados do Usuário alterados com sucesso!");

            Console.ReadKey();
            Console.Clear();
            Conexao.Close();
        }

        public void Delete()
        {
            if (Conexao.State == ConnectionState.Open)
            {

            }
            else
            {
                try
                {
                    Conexao.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Erro ao tentar abrir a conexao com o banco de dados");
                    Console.Clear();
                }
            }

            // deletar um usuario através de seu cpf
            string sql = "select * from usuario";
            MySqlCommand cmd = new MySqlCommand(sql, Conexao);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("Nome: {0} CPF: {1} Profissão: {2} ", rdr["nome"], rdr["cpf"], rdr["profissao"]);
            }

            Console.WriteLine("\nDigite o cpf do Usuário para deleta-lo");
            cpf = Console.ReadLine();

            if (rdr == null)
            {
                Conexao.Close();
                Conexao.Open();
                Console.WriteLine("Usuário não encontrado");
                Console.ReadKey();
                Conexao.Close();

            }
            else if (rdr != null)
            {
                Conexao.Close();
                Conexao.Open();
                string sqlDelete= "delete from usuario where cpf = @cpf ";
                MySqlCommand cmdDelete = new MySqlCommand(sqlDelete, Conexao);
                cmdDelete.Parameters.AddWithValue("@cpf", cpf);
                cmdDelete.ExecuteNonQuery();
                Console.WriteLine("Dados do Usuário deletados com sucesso");
            }
            Console.ReadKey();
            Console.Clear();
            Conexao.Close();
        }

        public void CadastroFinal()
        {
            ConexaoBanco();
            ConexaoInicar();
            string fimDoWhile = "1"; // variavel para finalizar laço de repetição do/while
            int escolhaCase; // variavel para escolha de sistema de case
            do
            {
                Console.WriteLine("Escolha uma dentre as Opções abaixo  \n1 - Adicionar Usuários \n2 - Listar Usuário \n3 - Atualizar Usuários \n4 - Deletar Usuário \n5 - Sair do Programa");
                escolhaCase = int.Parse(Console.ReadLine());

                Console.Clear();
                switch (escolhaCase)
                {
                    case 1:
                        Insert();
                        break;

                    case 2:
                        List();
                        break;

                    case 3:
                        Update();
                        break;

                    case 4:
                        Delete();
                        break;

                    case 5:

                        Environment.Exit(0);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Não temos essa opção em nosso programa!!");
                        break;
                }

            } while (fimDoWhile == "1");
        }

    }
}
