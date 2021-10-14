using System;
using System.Threading;
using System.Text.Json;
using NetMQ;
using NetMQ.Sockets;
namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random(50);
            using (var pubSocket = new PublisherSocket())
            {
                Console.WriteLine("Publisher socket binding...");
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind("tcp://*:12345");

                Funcionario func1 = new Funcionario("Fabio", "Programador", 1500.0, "A", 3, 31, 10);
                Funcionario func2 = new Funcionario("Marcos", "Operador", 1800.0, "B", 2, 60, 40);
                Funcionario func3 = new Funcionario("Paulo", "Programador", 2000.0, "D", 1, 58, 32);
                string func1Str = JsonSerializer.Serialize(func1);
                string func2Str = JsonSerializer.Serialize(func2);
                string func3Str = JsonSerializer.Serialize(func3);

                Pessoa pessoa1 = new Pessoa("Maria", "Feminino", 18, 1.58, 57);
                Pessoa pessoa2 = new Pessoa("Lucas", "Masculino", 18, 1.65, 68);
                Pessoa pessoa3 = new Pessoa("Pablo", "Masculino", 21, 1.75, 78);
                string pessoa1Str = JsonSerializer.Serialize(pessoa1);
                string pessoa2Str = JsonSerializer.Serialize(pessoa2);
                string pessoa3Str = JsonSerializer.Serialize(pessoa3);


                Aluno aluno1 = new Aluno(10.0, 7.0, 6.0);
                Aluno aluno2 = new Aluno(5.0, 4.0, 5.0);
                Aluno aluno3 = new Aluno(7.0, 9.0, 6.0);
                string aluno1Str = JsonSerializer.Serialize(aluno1);
                string aluno2Str = JsonSerializer.Serialize(aluno2);
                string aluno3Str = JsonSerializer.Serialize(aluno3);

                Cliente cliente = new Cliente(500);
                string clienteStr = JsonSerializer.Serialize(cliente);
            
                while(true)
                {
                    pubSocket.SendMoreFrame("Funcionario").SendFrame(func1Str);
                    pubSocket.SendMoreFrame("Funcionario").SendFrame(func2Str);
                    pubSocket.SendMoreFrame("Funcionario").SendFrame(func3Str);

                    pubSocket.SendMoreFrame("Peso").SendFrame(pessoa1Str);
                    pubSocket.SendMoreFrame("Peso").SendFrame(pessoa2Str);
                    pubSocket.SendMoreFrame("Peso").SendFrame(pessoa3Str);

                    pubSocket.SendMoreFrame("Maioridade").SendFrame(pessoa1Str);
                    pubSocket.SendMoreFrame("Maioridade").SendFrame(pessoa2Str);
                    pubSocket.SendMoreFrame("Maioridade").SendFrame(pessoa3Str);

                    pubSocket.SendMoreFrame("Aluno").SendFrame(aluno1Str);
                    pubSocket.SendMoreFrame("Aluno").SendFrame(aluno2Str);
                    pubSocket.SendMoreFrame("Aluno").SendFrame(aluno3Str);
                
                    pubSocket.SendMoreFrame("Esporte").SendFrame(pessoa1Str);
                    pubSocket.SendMoreFrame("Esporte").SendFrame(pessoa2Str);
                    pubSocket.SendMoreFrame("Esporte").SendFrame(pessoa3Str);

                    pubSocket.SendMoreFrame("Credito").SendFrame(clienteStr);

                    Thread.Sleep(1000);
                }
            }
        }

    }
}