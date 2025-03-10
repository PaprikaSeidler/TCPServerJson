import json
from socket import *
serverName = 'localhost'
serverPort = 13000
clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((serverName, serverPort))

jsonInput = input("Enter the method and numbers in JSON format: ")

jsonObj = json.loads(jsonInput)
clientSocket.send(f"{json.dumps(jsonObj)}\n".encode())

result = clientSocket.recv(1024).decode()
result_json = json.loads(result)
print(f"Result = ", result_json)

clientSocket.close()

