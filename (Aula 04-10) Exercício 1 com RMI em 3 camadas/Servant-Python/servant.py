import Pyro5.api
import mysql.connector

Pyro5.api.config.SERVERTYPE = "thread"

@Pyro5.api.expose
class Classe(object):
    def calcularSalario(self, cod):

        cnx = mysql.connector.connect(user='user', password='4227565', host='18.235.9.32', database='empresa')            
        cursor = cnx.cursor()
        query = ("SELECT func, salario FROM empresa.colaborador WHERE cod=%s")
        cursor.execute(query, (cod,))
        row = cursor.fetchall()

        if row[0][0] == "operador":
            salario = row[0][1]
            salario = salario*1.2
            cursor.execute("UPDATE empresa.colaborador SET salario=%s WHERE cod=%s", (salario, cod))
        if row[0][0] == "programador":
            salario = row[0][1]
            salario = salario*1.18
            cursor.execute("UPDATE empresa.colaborador SET salario=%s WHERE cod=%s", (salario, cod))

        cnx.commit()
        cnx.close()

        return salario

daemon = Pyro5.api.Daemon(host="172.31.84.239", port=11154, nathost="54.235.242.223", natport=11154)                # make a Pyro daemon
ns = Pyro5.api.locate_ns(host="3.233.161.61", port=11153)                  
uri = daemon.register(Classe)   
ns.register("classe.salario", uri)   
print("Ready.")
daemon.requestLoop()                   
