USE AdventureWorks2022;

-- 1. Average salary rate by departments;

SELECT departments.Name, AVG(payments.Rate * payments.PayFrequency) AS AverageEmployeeHourPayment
FROM HumanResources.Department AS departments
	INNER JOIN HumanResources.EmployeeDepartmentHistory AS employee_department
		ON departments.DepartmentID = employee_department.DepartmentID
	INNER JOIN HumanResources.EmployeePayHistory AS payments
		ON employee_department.BusinessEntityID = payments.BusinessEntityID
GROUP BY departments.Name
HAVING AVG(payments.Rate * payments.PayFrequency) > 50.0
ORDER BY AVG(payments.Rate * payments.PayFrequency) DESC;

-- 2. Most common reason for buying products by territory

SELECT territory.Name, reason.Name, COUNT(reason.Name) AS ReasonCount
FROM Sales.SalesTerritory AS territory
	INNER JOIN Sales.SalesOrderHeader AS header
		ON territory.TerritoryID = header.TerritoryID
	INNER JOIN Sales.SalesOrderHeaderSalesReason AS header_reason
		ON header.SalesOrderID = header_reason.SalesOrderID
	INNER JOIN Sales.SalesReason AS reason
		ON header_reason.SalesReasonID = reason.SalesReasonID
GROUP BY territory.Name, reason.Name
ORDER BY territory.Name, ReasonCount DESC;

-- 3. Product with max quantity ordered

SELECT TOP 1 detail.ProductID, product.Name, product.ListPrice, MAX(detail.OrderQty) AS MaxQuantity
FROM Sales.SalesOrderDetail AS detail
	INNER JOIN Production.Product AS product
		ON detail.ProductID = product.ProductID
WHERE product.ListPrice > 1000
GROUP BY detail.ProductID, product.Name, product.ListPrice
ORDER BY MaxQuantity DESC;

-- 4. Best product according to reviews

SELECT TOP 1 product.Name, AVG(review.Rating) AS AverageRating
FROM Production.ProductReview review
    INNER JOIN Production.Product product
        ON product.ProductID = review.ProductID
GROUP BY product.Name
ORDER BY product.Name DESC;

-- 5. Total sum of orders by territories

SELECT territory.Name, ROUND(SUM(detail.LineTotal), 2) AS TotalOrdersSum
FROM Sales.SalesOrderDetail AS detail
	INNER JOIN Sales.SalesOrderHeader AS header
		ON detail.SalesOrderID = header.SalesOrderID
	INNER JOIN Sales.SalesTerritory AS territory
		ON header.TerritoryID = territory.TerritoryID
GROUP BY territory.Name
ORDER BY TotalOrdersSum DESC;

-- 6. Find employee contacts who were working at Adventure Works before

SELECT DISTINCT person.FirstName, person.LastName, address.City, address.AddressLine1 AS Address
FROM Person.Person AS person
	INNER JOIN Person.BusinessEntityAddress AS business_address
		ON person.BusinessEntityID = business_address.BusinessEntityID
	INNER JOIN Person.Address AS address
		ON business_address.AddressID = address.AddressID
	INNER JOIN HumanResources.EmployeeDepartmentHistory AS history
		ON person.BusinessEntityID = history.BusinessEntityID
WHERE history.EndDate IS NOT NULL;

-- 7. Top employees with the maximum number of registered purchase orders

SELECT person.FirstName, person.LastName, employee.JobTitle, COUNT(header.EmployeeID) AS Orders
FROM HumanResources.Employee AS employee
	INNER JOIN Person.Person AS person
		ON employee.BusinessEntityID = person.BusinessEntityID
	INNER JOIN Purchasing.PurchaseOrderHeader AS header
		ON employee.BusinessEntityID = header.EmployeeID
GROUP BY person.BusinessEntityID, person.FirstName, person.LastName, employee.JobTitle
HAVING COUNT(header.EmployeeID) > 300
ORDER BY Orders DESC;

-- 8. Most expensive products which are available and do not have a category

SELECT product.Name, category.Name AS Category, product.ListPrice, product.SellEndDate
FROM Production.Product AS product
	LEFT JOIN Production.ProductSubcategory AS subcategory
		ON product.ProductSubcategoryID = subcategory.ProductSubcategoryID
	LEFT JOIN Production.ProductCategory AS category
		ON subcategory.ProductCategoryID = category.ProductCategoryID
WHERE product.SellEndDate IS NULL
ORDER BY product.ListPrice DESC;