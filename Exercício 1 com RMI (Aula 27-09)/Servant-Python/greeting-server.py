import Pyro4

@Pyro4.expose
class Classe(object):
    def calcularSalario(self, cargo, salario):

        if cargo == 1:
            salario *=1.2
        if cargo == 2:
            salario *=1.18

        return salario

daemon = Pyro4.Daemon(host="172.31.84.239", port=11154, nathost="54.235.242.223", natport=11154)                # make a Pyro daemon
ns = Pyro4.locateNS()                  
uri = daemon.register(Classe)   
ns.register("classe.salario", uri)   
print("Ready.")
daemon.requestLoop()                   
