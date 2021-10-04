// Needed for pyrolite
import net.razorvine.pyro.*;
import java.io.*;   // Needed (?) for IOException thrown by some pyrolite methods
import java.util.Scanner;

public class Greeting_Client {

    public void run(String arg)
    {
      int cod;
      System.out.println("Conectando-se com o servant...");

      NameServerProxy ns = null;                                                  
       try 
       {
           ns = NameServerProxy.locateNS("3.233.161.61", 11153);
       } 
       catch (IOException e)
       {
           System.out.println("Caught LocateNS IOException: " + e.getMessage());
           return;
       }
       
       PyroProxy remoteobject = null;
       try 
       {
           remoteobject = new PyroProxy(ns.lookup("classe.salario"));
       } 
       catch (IOException e)
       {
              System.out.println("Caught ns.lookup IOException: " + e.getMessage());
              ns.close();
              return;
       }

      Object result = null;
       try
       {
           Scanner sc = new Scanner(System.in);
           System.out.println("Digite o código do funcionário que deseja reajustar o salário:");
           cod = sc.nextInt();
           result = remoteobject.call("calcularSalario", cod);
       } 
       catch (IOException e)
       {
             System.out.println("Caught remoteobject.call IOException: " + e.getMessage());
             ns.close();
             remoteobject.close();
             return;
        }
       System.out.println("Novo salário:");
       System.out.println(result);
       remoteobject.close();
       ns.close();
    }

}
