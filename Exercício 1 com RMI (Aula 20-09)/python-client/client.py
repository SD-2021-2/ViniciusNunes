
import Pyro4

print("Cargo: 1-Operador | 2-Programador")
cargo = int(input())
print("Salario:")
salario = int(input())

calcula_salario = Pyro4.Proxy("PYRONAME:gerenciador_pag")    # use name server object lookup uri shortcut
print(calcula_salario.calcularSalario(cargo,salario))