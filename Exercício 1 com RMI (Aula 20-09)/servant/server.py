import Pyro4

@Pyro4.expose
class gerenciador_pag(object):
    def calcularSalario(self, cargo, salario):
        if cargo == 1:
            salario = salario*1.20
        if cargo == 2:
            salario = salario*1.18

        return salario

daemon = Pyro4.Daemon()                
ns = Pyro4.locateNS()                  
uri = daemon.register(gerenciador_pag)   
ns.register("gerenciador_pag", uri) 

print("Ready.")
daemon.requestLoop()                  