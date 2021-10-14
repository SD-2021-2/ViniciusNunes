using System;
public class Funcionario
{
    public string Nome {get;set;}

    public int Idade {get;set;}
    public int TempoServ {get;set;}
    public string Cargo {get;set;}
    public double Salario {get;set;}
    public string Nivel {get;set;}
    public int NumDep {get;set;}

    public double getSalario()
    {
        return this.Salario;
    }
    public string getCargo()
    {
        return this.Cargo;
    }
    public string getNome()
    {
        return this.Nome;
    }

    public int getIdade()
    {
        return this.Idade;
    }
    public int getTempoServ()
    {
        return this.TempoServ;
    }

    public string getNivel()
    {
        return this.Nivel;
    }
    public int getNumDep()
    {
        return this.NumDep;
    }
    public void setSalario(Double salario)
    {
        this.Salario = salario;
    }
    public Funcionario(String nome, String cargo, Double salario, String nivel, int numDep, int idade, int tempoServ)
    {
        this.Salario = salario;
        this.Nome = nome;
        this.Cargo = cargo;
        this.Nivel = nivel;
        this.NumDep = NumDep;
        this.Idade = idade;
        this.TempoServ = tempoServ;
    }
}
