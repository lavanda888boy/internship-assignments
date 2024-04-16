USE AdventureWorks2022;

SELECT FirstName, LastName, BusinessEntityID AS Employee_id
FROM Person.Person
ORDER BY LastName;

SELECT FirstName, LastName, phone.PhoneNumber, person.BusinessEntityID AS Employee_id 
FROM Person.Person AS person
	INNER JOIN Person.PersonPhone AS phone 
		ON person.BusinessEntityID = phone.BusinessEntityID 
ORDER BY LastName, FirstName;

SELECT person.LastName, SalesYTD, person_address.PostalCode, COUNT(*) AS GroupNumber
FROM Sales.SalesPerson As sales_person
	INNER JOIN Person.Person AS person
		ON sales_person.BusinessEntityID = person.BusinessEntityID
	INNER JOIN Person.BusinessEntityAddress AS business_address
		ON sales_person.BusinessEntityID = business_address.BusinessEntityID
	INNER JOIN Person.Address AS person_address
		ON business_address.AddressID = person_address.AddressID
WHERE TerritoryID IS NOT NULL
	AND SalesYTD <> 0
GROUP BY person.LastName, SalesYTD, person_address.PostalCode;

SELECT SalesOrderID, SUM(LineTotal) AS TotalCost
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID
HAVING SUM(LineTotal) > 100000;
