using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using NetMQ;
using NetMQ.Sockets;
namespace SubscriberA
{
    class Program
    {
        static void Main(string[] args)
        {
            string topic = " ";
            String opcao;
            int opcaoNum;

            Console.WriteLine("Qual tópico deseja se inscrever?");
            Console.WriteLine("1-Funcionario");
            Console.WriteLine("2-Maioridade");
            Console.WriteLine("3-Esporte");
            Console.WriteLine("4-Peso");
            Console.WriteLine("5-Aluno");
            Console.WriteLine("6-Credito");
            opcao = Console.ReadLine();
            int.TryParse(opcao, out opcaoNum);
            if(opcaoNum == 1) topic = "Funcionario";
            if(opcaoNum == 2) topic = "Maioridade";
            if(opcaoNum == 3) topic = "Esporte";
            if(opcaoNum == 4) topic = "Peso";
            if(opcaoNum == 5) topic = "Aluno";
            if(opcaoNum == 6) topic = "Credito";
            
            Console.WriteLine("Inscrito no tópico : {0}", topic);
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://localhost:12345");
                subSocket.Subscribe(topic);
                Console.WriteLine("Subscriber socket connecting...");

                string messageTopicReceived;
                string messageReceived;
                
                messageTopicReceived = subSocket.ReceiveFrameString();
                messageReceived = subSocket.ReceiveFrameString();

                Console.WriteLine(messageReceived);
                if(topic == "Funcionario")
                {
                    Funcionario func = JsonSerializer.Deserialize<Funcionario>(messageReceived);

                    Console.WriteLine("O que deseja fazer?");
                    Console.WriteLine("1-Calcular reajuste de salário");
                    Console.WriteLine("2-Calcular salário líquido");
                    Console.WriteLine("3-Possibilidade de aposentadoria");
                    opcao = Console.ReadLine();
                    int.TryParse(opcao, out opcaoNum);
                    if(opcaoNum == 1) calcularSalario(func);
                    if(opcaoNum == 2) calcularSalarioLiq(func);
                    if(opcaoNum == 3) podeAposentar(func);
                }

                if(topic == "Maioridade" || topic == "Peso" || topic == "Esporte")
                {
                    
                    
                    Pessoa pessoa = JsonSerializer.Deserialize<Pessoa>(messageReceived);

                    if(topic == "Maioridade")
                        ehMaiorDeIdade(pessoa);
                    if(topic == "Peso")
                        calcPesoIdeal(pessoa);
                    if(topic == "Esporte")
                        classifNadador(pessoa);    
                }

                if(topic == "Credito")
                {
                    Cliente cliente = JsonSerializer.Deserialize<Cliente>(messageReceived);
                    calcularCred(cliente);
                }
                if(topic == "Aluno")
                {
                    Aluno aluno = JsonSerializer.Deserialize<Aluno>(messageReceived);
                    calcularNota(aluno);
                }

            }
        }

        static void calcularSalario(Funcionario func)
        {
            if(func.getCargo() == "Operador")
            {
                var salario = func.getSalario();
                salario *= 1.18;
                func.setSalario(salario);
            }
            if(func.getCargo() == "Programador")
            {
                var salario = func.getSalario();
                salario *= 1.2;
                func.setSalario(salario);
            }

            Console.WriteLine("Nome: "+func.getNome());
            Console.WriteLine("Cargo: "+func.getCargo());
            Console.WriteLine("Salario: "+func.getSalario());
                
        }
        static void ehMaiorDeIdade(Pessoa pessoa)
        {
            if(pessoa.getSexo() == "Masculino")
            {
                if(pessoa.getIdade() >= 18)
                    Console.WriteLine(pessoa.getNome() + " é maior de idade");
                else
                    Console.WriteLine(pessoa.getNome() + " não é maior de idade");

            }
            if(pessoa.getSexo() == "Feminino")
            {
                if(pessoa.getIdade() >= 21)
                    Console.WriteLine(pessoa.getNome() + " é maior de idade");
            
                else
                Console.WriteLine(pessoa.getNome() + " não é maior de idade");

            }
                
        }
        static void calcPesoIdeal(Pessoa pessoa)
        {
            var pesoIdeal = 0.0;
            if(pessoa.getSexo() == "Masculino")
                pesoIdeal = (72.7*pessoa.getAltura()) - 58;
            if(pessoa.getSexo() == "Feminino")
                pesoIdeal = (62.1*pessoa.getAltura()) - 44.7;

            Console.WriteLine("Peso ideal de"+pessoa.getNome()+": "+pesoIdeal);
        }
        static void classifNadador(Pessoa pessoa)
        {
            if(pessoa.getIdade() >= 5 && pessoa.getIdade() <= 7)
                Console.WriteLine("Infantil A");
            else if(pessoa.getIdade() >= 8 && pessoa.getIdade() <= 10)
                Console.WriteLine("Infantil B");
            else if(pessoa.getIdade() >= 11 && pessoa.getIdade() <= 13)
                Console.WriteLine("Juvenial A");
            else if(pessoa.getIdade() >= 14 && pessoa.getIdade() <= 17)
                Console.WriteLine("Juvenial B");
            else
                Console.WriteLine("Adulto");
                
        }

        static void calcularSalarioLiq(Funcionario func)
        {
            double salarioLiq = 0.0;
            if(func.getNivel() == "A")
            {
                if(func.getNumDep() > 0)
                    salarioLiq = func.getSalario()-(0.08*func.getSalario());
                else
                    salarioLiq = func.getSalario()-(0.03*func.getSalario());
            }
            if(func.getNivel() == "B")
            {
                if(func.getNumDep() > 0)
                    salarioLiq = func.getSalario()-(0.10*func.getSalario());
                else
                    salarioLiq = func.getSalario()-(0.05*func.getSalario());
            }
            if(func.getNivel() == "C")
            {
                if(func.getNumDep() > 0)
                    salarioLiq = func.getSalario()-(0.15*func.getSalario());
                else
                    salarioLiq = func.getSalario()-(0.08*func.getSalario());
            }
            if(func.getNivel() == "D")
            {
                if(func.getNumDep() > 0)
                    salarioLiq = func.getSalario()-(0.17*func.getSalario());
                else
                    salarioLiq = func.getSalario()-(0.10*func.getSalario());
            }
            Console.WriteLine("Nome: "+func.getNome());
            Console.WriteLine("Nivel: "+func.getNivel());
            Console.WriteLine("Salario: "+salarioLiq);

        }
        static void podeAposentar(Funcionario func)
        {
            if((func.getIdade() >= 65 || func.getTempoServ() >= 30) || (func.getIdade()>=60 && func.getTempoServ()>=25))
                Console.WriteLine("Nome: "+func.getNome()+" já pode se aposentar");
            else
                Console.WriteLine("Nome: "+func.getNome()+" não pode se aposentar");
                
        }

        static void calcularCred(Cliente cliente)
        {
            double cred = 0.0;

            if(cliente.getSaldoMedio() < 200)
            {
                Console.WriteLine("Cliente sem crédito");
                return;
            }
            else if(cliente.getSaldoMedio() >= 201 && cliente.getSaldoMedio() < 400)
                cred = cliente.getSaldoMedio() * 0.2;
            else if(cliente.getSaldoMedio() >= 401 && cliente.getSaldoMedio() < 600)
                cred = cliente.getSaldoMedio() * 0.3;
            else
                cred = cliente.getSaldoMedio() * 0.4;
            
            Console.WriteLine("Este cliente tem "+cred+" de crédito");

        }

        static void calcularNota(Aluno aluno)
        {
            double media = (aluno.getN1()+aluno.getN2())/2;

            if(media >= 7.0)
                Console.WriteLine("Aprovado");
            else if(media >= 3.0 && media < 7.0)
            {
                media = (media + aluno.getN3())/2;
                if(media >= 5.0)
                    Console.WriteLine("Aprovado");
                else
                    Console.WriteLine("Reprovado");
            }
            else
                Console.WriteLine("Reprovado");
        }
    }
}