using System;
public class Aluno
{

    public double N1{get;set;}
    public double N2{get;set;}
    public double N3{get;set;}
    public double getN1()
    {
        return this.N1;
    }
    public double getN2()
    {
        return this.N2;
    }
    public double getN3()
    {
        return this.N3;
    }
    public Aluno(double n1, double n2, double n3)
    {
        this.N1 = n1;
        this.N2 = n2;
        this.N3 = n3;

    }
}