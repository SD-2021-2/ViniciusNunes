// Needed for pyrolite
import net.razorvine.pyro.*;
import java.io.*;   // Needed (?) for IOException thrown by some pyrolite methods
import java.util.Scanner;

public class Greeting_Client {

    public void run(String arg)
    {
      int cargo;
      float salario;
      System.out.println("Conectando-se com o servant...");

      NameServerProxy ns = null;                                                  
       try 
       {
           ns = NameServerProxy.locateNS("54.235.242.223", 11153, null);
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
           System.out.println("Cargo do funcionário: 1 - Operador | 2 - Programador");
           cargo = sc.nextInt();
           System.out.println("Salário:");
           salario = sc.nextFloat();
           result = remoteobject.call("calcularSalario", cargo, salario);
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
