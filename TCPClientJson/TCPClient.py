import json
from socket import *
serverName = 'localhost'
serverPort = 13000
clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((serverName, serverPort))

UserInput = input("Enter the method and numbers (e.g. 'Add 5 10): ")

method, num1, num2 = UserInput.split()
num1 = int(num1)
num2 = int(num2)

jsonInput = json.dumps({"method": method, "num1": num1, "num2": num2})
print(f"Sending to server: {jsonInput}")

jsonObj = json.loads(jsonInput)
clientSocket.send(f"{json.dumps(jsonObj)}\n".encode())

result = clientSocket.recv(1024).decode()
result_json = json.loads(result)
print(f"Result = ", result_json) 

clientSocket.close() 

