import socket
import struct 
HOST = '127.0.0.1'     # Endereco IP do Servidor
PORT = 11000           # Porta que o Servidor esta
tcp = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
dest = (HOST, PORT)
tcp.connect(dest)

data = tcp.recv(1024)
print(repr(data))
name = input()
name_bytes = str.encode(name)
tcp.send(name_bytes)

data = tcp.recv(1024)
print(repr(data))
cargo = input()
cargo_bytes = str.encode(cargo)
tcp.send(cargo_bytes)

data = tcp.recv(1024)
print(repr(data))
salary = float(input())
salary_bytes = bytearray(struct.pack("d", salary))
tcp.send(salary_bytes)
novo_salario_bytes = tcp.recv(1024)
print(name)
novo_salario = struct.unpack('d', novo_salario_bytes)[0]
print(novo_salario)
tcp.close()