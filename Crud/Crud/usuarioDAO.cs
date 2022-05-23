using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using MySql.Data;

namespace Crud
{
    class usuarioDAO : usuario
    {
        string fimDoWhile = "1"; // variavel para finalizar laço de repetição do/while

        

        public void usuario()
        {
            MySqlConnection conexao = new MySqlConnection("server=localhost;charset=utf8;database=crud;uid=root;password=");

            try
            {
                conexao.Open();
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

            do
            {

                int escolhaCase;
                Console.WriteLine("Escolha uma dentre as Opções abaixo  \n1 - Adicionar Usuários \n2 - Listar Usuário \n3 - Atualizar Usuários \n4 - Deletar Usuário \n5 - Sair do Programa");
                escolhaCase = int.Parse(Console.ReadLine());

                Console.Clear();
                switch (escolhaCase)
                {
                    

                    case 1:
                        if (conexao.State == ConnectionState.Open)
                        {

                        }
                        else
                        {
                            conexao.Open();
                        }
                        Console.WriteLine("Digite o Nome: ");
                        cpf = Console.ReadLine();
                        Console.WriteLine("Digite o CPF: ");
                        nome = Console.ReadLine();
                        Console.WriteLine("Digite a Profissão: ");
                        profissao = Console.ReadLine();

                        

                        // inserindo um novo usuario no banco de dados
                        string sqlInsert = "insert into  usuario values ('" + cpf + "','" + nome + "', '" + profissao + "')";

                        MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, conexao);
                        cmdInsert.ExecuteNonQuery();
                        Console.WriteLine("Usuário cadastrado com SUCESSO!!");

                        Console.ReadKey();
                        Console.Clear();
                        conexao.Close();
                        break; // fim adicionar usuarios

                    case 2:
                        conexao.Close();
                        conexao.Open();

                        string sql = "select * from usuario";
                        MySqlCommand cmd = new MySqlCommand(sql, conexao);
                        MySqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Console.WriteLine("Nome: {0} \nCPF: {1} \nProfissão: {2}", rdr["nome"], rdr["cpf"], rdr["profissao"]);
                            }
                            conexao.Close();
                            Console.ReadKey();

                        }
                        else
                        {
                            Console.WriteLine("Nenhum Usuário foi cadastro em nosso banco de dados!!");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        conexao.Close();
                        Console.Clear();
                        break; // fim listar usuarios

                    case 3:
                        conexao.Close();
                        conexao.Open();

                        // atualizando um usuario através de seu cpf
                        Console.WriteLine("Digite o cpf do usuário para altera-lo");
                        cpf = Console.ReadLine();

                        Console.WriteLine("Digite as novas informações: ");
                        Console.WriteLine("Nome: ");
                        nome = Console.ReadLine();
                        Console.WriteLine("Profissão: ");
                        profissao = Console.ReadLine();

                        
                        sql = "update usuario SET nome = @nome,  profissao = @profissao Where cpf = @cpf ";
                        cmd = new MySqlCommand(sql, conexao);
                        cmd.Parameters.AddWithValue("@cpf", cpf);
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@profissao", profissao);
                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Dados do Usuário alterados com sucesso!");

                        Console.ReadKey();
                        Console.Clear();
                        conexao.Close();
                        break; // fim atualizar usuarios

                    case 4:

                        conexao.Close();
                        conexao.Open();

                        // deletar um usuario através de seu cpf
                        sql = "select * from usuario";
                        cmd = new MySqlCommand(sql, conexao);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Console.WriteLine("Nome: {0} CPF: {1} Profissão: {2} ", rdr["nome"], rdr["cpf"], rdr["profissao"]);
                        }

                        Console.WriteLine("\nDigite o cpf do Usuário para deleta-lo");
                        cpf = Console.ReadLine();

                        if (rdr == null)
                        {
                            conexao.Close();
                            conexao.Open();
                            Console.WriteLine("Usuário não encontrado");
                            Console.ReadKey();
                            conexao.Close();

                        }
                        else if (rdr != null)
                        {
                            conexao.Close();
                            conexao.Open();
                            sql = "delete from usuario where cpf = @cpf ";
                            cmd = new MySqlCommand(sql, conexao);
                            cmd.Parameters.AddWithValue("@cpf", cpf);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Dados do Usuário deletados com sucesso");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        conexao.Close();
                        break; // fim deletar pessoas

                    case 5:

                        Environment.Exit(0);
                        Console.Clear();
                        break; // fim sair do programa

                    default:

                        Console.WriteLine("Não temos essa opção em nosso programa!!");
                        break;

                } // fim sistema de case

            } while (fimDoWhile == "1"); // fim sistema de laço de repetição do/while
         
            
            
               
        }
    }
}
