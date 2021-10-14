using System;
public class Pessoa
{
    public string Nome {get;set;}
    public string Sexo {get;set;}
    public int Idade {get;set;}
    public double Altura {get;set;}
    public double Peso {get;set;}
    public int getIdade()
    {
        return this.Idade;
    }

    public double getAltura()
    {
        return this.Altura;
    }
    public double getPeso()
    {
        return this.Peso;
    }
    public string getSexo()
    {
        return this.Sexo;
    }
    public string getNome()
    {
        return this.Nome;
    }
    public Pessoa()
    {
    }
    public Pessoa(String nome, String sexo, int idade, double altura, double peso)
    {
        this.Idade = idade;
        this.Nome = nome;
        this.Sexo = sexo;
        this.Altura = altura;
        this.Peso = peso;
    }

}
