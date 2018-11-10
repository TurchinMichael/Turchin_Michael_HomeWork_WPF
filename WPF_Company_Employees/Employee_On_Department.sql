﻿select Department.Id,[Department Name].Id as Department_id, [Department Name].Department_Name, Employee.Id as Employee_Id, FullName.First_Name, FullName.Last_Name,FullName.Patronymic from Department inner join Employee on Department.Employee = Employee.Id inner join FullName on Employee.[Full Name]= FullName.Id inner join [Department Name] on Department.[Department Name] = [Department Name].Id where [Department Name] = 1