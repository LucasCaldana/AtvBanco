using System;
using System.Collections.Generic;
using Npgsql;

class Program
{
    // Conexão com o banco
    static string conexao = "Host=localhost;Username=seu_usuario;Password=sua_senha;Database=seu_banco";

    static void Main()
    {
        // Testando os métodos
        CriarUsuario("Maria", "maria@email.com");
        ListarUsuarios();
        DeletarUsuario(1); // Altere o ID conforme necessário
    }

    // CREATE
    static void CriarUsuario(string nome, string email)
    {
        using var conn = new NpgsqlConnection(conexao);
        conn.Open();

        var cmd = new NpgsqlCommand("INSERT INTO usuarios (nome, email) VALUES (@nome, @email)", conn);
        cmd.Parameters.AddWithValue("nome", nome);
        cmd.Parameters.AddWithValue("email", email);

        int linhasAfetadas = cmd.ExecuteNonQuery();
        Console.WriteLine($"{linhasAfetadas} usuário(s) criado(s).");
    }

    // READ
    static void ListarUsuarios()
    {
        using var conn = new NpgsqlConnection(conexao);
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT id, nome, email FROM usuarios", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader.GetInt32(0)}, Nome: {reader.GetString(1)}, Email: {reader.GetString(2)}");
        }
    }

    // DELETE
    static void DeletarUsuario(int id)
    {
        using var conn = new NpgsqlConnection(conexao);
        conn.Open();

        var cmd = new NpgsqlCommand("DELETE FROM usuarios WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("id", id);

        int linhasAfetadas = cmd.ExecuteNonQuery();
        Console.WriteLine($"{linhasAfetadas} usuário(s) deletado(s).");
    }
}

