USE AdventureWorks2022;

-- 1. Products cheaper than the average price without any category

SELECT Name, ProductNumber
FROM Production.Product AS product
WHERE ListPrice < (
	SELECT AVG(ListPrice) 
	FROM Production.Product
) AND NOT EXISTS (
	SELECT ProductSubcategoryID
	FROM Production.ProductSubcategory
	WHERE ProductSubcategoryID = product.ProductSubcategoryID
);

-- 2. Employees with the smallest number of vacation and sick leave hours

SELECT FirstName, LastName
FROM Person.Person AS person
WHERE person.BusinessEntityID IN (
	SELECT employee.BusinessEntityID
	FROM HumanResources.Employee AS employee
	WHERE VacationHours + SickLeaveHours = (
		SELECT MIN(VacationHours + SickLeaveHours)
		FROM HumanResources.Employee
	)
);

-- 3. Vendors whose purchase orders have line total above average

SELECT DISTINCT Name, AccountNumber
FROM Purchasing.Vendor AS vendor
	INNER JOIN Purchasing.PurchaseOrderHeader AS header
		ON vendor.BusinessEntityID = header.VendorID
WHERE header.PurchaseOrderID IN (
	SELECT PurchaseOrderID 
	FROM Purchasing.PurchaseOrderDetail AS detail
	WHERE detail.LineTotal > (
		SELECT AVG(LineTotal)
		FROM Purchasing.PurchaseOrderDetail
	)
)
ORDER BY Name;

-- 4. Order cost categorization in Australia

SELECT header.SalesOrderID,
CASE
    WHEN SUM(detail.LineTotal) <= 1000 THEN 'Low cost order'
    WHEN SUM(detail.LineTotal) > 1000 AND SUM(detail.LineTotal) < 5000 THEN 'Medium cost order'
    ELSE 'High cost order'
END AS OrderCostStatus
FROM Sales.SalesOrderHeader AS header
	INNER JOIN Sales.SalesOrderDetail AS detail
		ON header.SalesOrderID = detail.SalesOrderID
WHERE header.TerritoryID IN (
	SELECT TerritoryID 
	FROM Sales.SalesTerritory
	WHERE Name = 'Australia'
)
GROUP BY header.SalesOrderID;

-- 5. Products which have average history price higher than the current one

SELECT Name, SellStartDate
FROM Production.Product AS product
WHERE product.ProductID IN (
	SELECT history.ProductID
	FROM Production.ProductCostHistory AS history
	GROUP BY history.ProductID
	HAVING AVG(history.StandardCost) > (
		SELECT hist.StandardCost
		FROM Production.ProductCostHistory AS hist
		WHERE hist.ProductID = history.ProductID 
			AND hist.EndDate IS NULL
	)
);

-- 6. Product name and its max cost from the transaction history

SELECT product.Name, (
	SELECT MAX(history.ActualCost)
	FROM Production.TransactionHistory AS history
	WHERE history.ProductID = product.ProductID
) AS TransactionMaxProductCost
FROM Production.Product AS product
WHERE (
	SELECT MAX(history.ActualCost)
	FROM Production.TransactionHistory AS history
	WHERE history.ProductID = product.ProductID
) IS NOT NULL
ORDER BY TransactionMaxProductCost DESC;

-- 7. Products and their scrap reasons from the work orders

SELECT DISTINCT product.Name AS Product, reason.Name AS ScrapReason
FROM Production.ScrapReason AS reason
	INNER JOIN Production.WorkOrder AS work_order
		ON work_order.ScrapReasonID = reason.ScrapReasonID
	INNER JOIN Production.Product AS product
		ON product.ProductID = work_order.ProductID
WHERE reason.ScrapReasonID IN (
	SELECT work_order.ScrapReasonID
	FROM Production.WorkOrder AS work_order
	WHERE work_order.ScrapReasonID IS NOT NULL
);

-- 8. Number of female employees who have a particular job title

SELECT COUNT(*) AS EmployeeCount FROM (
	SELECT FirstName, LastName
	FROM Person.Person AS person
		INNER JOIN HumanResources.Employee AS employee
			ON person.BusinessEntityID = employee.BusinessEntityID
	WHERE employee.BusinessEntityID IN (
		SELECT BusinessEntityID
		FROM HumanResources.Employee
		WHERE JobTitle LIKE '%Manager%'
	) AND employee.Gender = 'F'
) AS EmployeeCount;