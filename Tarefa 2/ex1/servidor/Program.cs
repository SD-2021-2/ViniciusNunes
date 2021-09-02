using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
  
// Socket Listener acts as a server and listens to the incoming   
// messages on the specified port and protocol.  
public class SocketListener  
{  
    public static int Main(String[] args)  
    {  
        StartServer();  
        return 0;  
    }  
  
     
    public static void StartServer()  
    {  
        // Get Host IP Address that is used to establish a connection  
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
        // If a host has multiple addresses, you will get a list of addresses  
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[1];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);     
  
            // Create a Socket that will use Tcp protocol      
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
            // A Socket must be associated with an endpoint using the Bind method  
            listener.Bind(localEndPoint);  
            // Specify how many requests a Socket can listen before it gives Server busy response.  
            // We will listen 10 requests at a time  
            listener.Listen(10);  
  
            Console.WriteLine("Waiting for a connection...");  
            Socket handler = listener.Accept();

            Console.WriteLine("Connected");

            calcularReajuste(handler);
    }

    public static void calcularReajuste(Socket handler)
    {
        string data, nome_func, cargo_func;
        double salario;
        int num_bytes;
        byte[] msg_bytes_rcv = new byte[1024]; 
        byte[] msg_bytes_send = null;

        handler.Blocking = true;

        data = "Digite o nome do funcionario";
        msg_bytes_send = Encoding.ASCII.GetBytes(data);
        handler.Send(msg_bytes_send);
        num_bytes = handler.Receive(msg_bytes_rcv, 0, msg_bytes_rcv.Length-0, SocketFlags.None);
        nome_func = Encoding.ASCII.GetString(msg_bytes_rcv, 0, num_bytes);

        msg_bytes_rcv = new byte[1024];

        data = "Digite o cargo do funcionario";
        msg_bytes_send = Encoding.ASCII.GetBytes(data);
        handler.Send(msg_bytes_send);
        num_bytes = handler.Receive(msg_bytes_rcv, 0, msg_bytes_rcv.Length-0, SocketFlags.None);
        cargo_func = Encoding.ASCII.GetString(msg_bytes_rcv, 0, num_bytes);

        msg_bytes_rcv = new byte[1024];

        data = "Digite o salario do funcionario";
        msg_bytes_send = Encoding.ASCII.GetBytes(data); 
        handler.Send(msg_bytes_send);
        handler.Receive(msg_bytes_rcv, 0, msg_bytes_rcv.Length-0, SocketFlags.None);
        salario = BitConverter.ToDouble(msg_bytes_rcv, 0);

        if(cargo_func.Equals("operador"))
            salario += (salario * 0.20);
        else if(cargo_func.Equals("programador"))
            salario += (salario * 0.18);

        msg_bytes_send = BitConverter.GetBytes(salario);
        handler.Send(msg_bytes_send);

        handler.Close();

    }

}          
