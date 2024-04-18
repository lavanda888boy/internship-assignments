USE AdventureWorks2022;

-- 1. Add new employee

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewBusinessEntityID INT
	INSERT INTO Person.BusinessEntity(rowguid, ModifiedDate)
	VALUES (NEWID(), GETDATE());
	SET @NewBusinessEntityID = SCOPE_IDENTITY();

	INSERT INTO Person.Person(BusinessEntityID, PersonType, NameStyle, FirstName, LastName, EmailPromotion)
	VALUES (@NewBusinessEntityID, 'EM', 0, 'Mike', 'Ross', 1);

	INSERT INTO HumanResources.Employee(BusinessEntityID, NationalIDNumber, LoginID, JobTitle,
		BirthDate, MaritalStatus, Gender, HireDate, SalariedFlag, VacationHours, SickLeaveHours,
		CurrentFlag)
	VALUES (@NewBusinessEntityID, 999888999, 'adventure-works\mikeross0', 'Quality Assurance Technician',
		'1990-07-20', 'M', 'M', GETDATE(), 1, 80, 50, 1);

	INSERT INTO HumanResources.EmployeeDepartmentHistory(BusinessEntityID, DepartmentID, ShiftID, StartDate)
	VALUES (@NewBusinessEntityID, 1, 1, GETDATE());

	INSERT INTO HumanResources.EmployeePayHistory(BusinessEntityID, RateChangeDate, Rate, PayFrequency)
	VALUES (@NewBusinessEntityID, GETDATE(), 120.00, 2);

	COMMIT TRANSACTION;
	PRINT @NewBusinessEntityID;
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 2. Add employee contact and address information

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewBusinessEntityID INT
	SET @NewBusinessEntityID = 20779;

	INSERT INTO Person.EmailAddress(BusinessEntityID, EmailAddress)
	VALUES (@NewBusinessEntityID, 'mike.ross@adventure-works.com');
	
	DECLARE @NewAddressID INT
	INSERT INTO Person.Address(AddressLine1, City, StateProvinceID, PostalCode)
	VALUES('24 Dacia blv.', 'Chisinau', 25, 'MD-2043');
	SET @NewAddressID = SCOPE_IDENTITY()

	INSERT INTO Person.BusinessEntityAddress(BusinessEntityID, AddressID, AddressTypeID)
	VALUES (@NewBusinessEntityID, @NewAddressID, 2);

	COMMIT TRANSACTION;
	PRINT 'Success';
	PRINT @NewAddressID;
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 3. Modify employee address information

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewBusinessEntityID INT
	SET @NewBusinessEntityID = 20779;

	DECLARE @NewAddressID INT
	SET @NewAddressID = 32522

	UPDATE Person.EmailAddress
	SET EmailAddress = 'michael.ross@adventure-works.com'
	WHERE BusinessEntityID = @NewBusinessEntityID;
	
	UPDATE Person.Address
	SET AddressLine1 = '26 Dacia blv',
		City = 'Balti'
	WHERE AddressID = @NewAddressID;

	COMMIT TRANSACTION;
	PRINT 'Success';
	PRINT 'New email address: michael.ross@adventure-works.com'
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 4. Create new purchase order

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewPurchaseOrderID INT
	INSERT INTO Purchasing.PurchaseOrderHeader(RevisionNumber, Status, EmployeeID, VendorID, ShipMethodID,
		OrderDate, ShipDate, SubTotal, TaxAmt, Freight)
	VALUES (1, 2, 260, 1520, 5, GETDATE(), '2024-04-26', 2500.84, 34.25, 8.52);
	SET @NewPurchaseOrderID = SCOPE_IDENTITY();

	DECLARE @NewPurchaseOrderDetailID INT
	INSERT INTO Purchasing.PurchaseOrderDetail(PurchaseOrderID, DueDate, OrderQty, ProductID, UnitPrice, 
		ReceivedQty, RejectedQty)
	VALUES (@NewPurchaseOrderID, '2024-04-24', 5, 1, 1, 10, 0);
	SET @NewPurchaseOrderDetailID = SCOPE_IDENTITY();

	DECLARE @PurchasedProductID INT
	SET @PurchasedProductID = (
								SELECT TOP 1 product.ProductID 
								FROM Production.Product AS product
								INNER JOIN Purchasing.ProductVendor AS pv
								ON pv.ProductID = product.ProductID
								WHERE product.StandardCost <> 0
							  )

	UPDATE Purchasing.PurchaseOrderDetail
	SET ProductID = @PurchasedProductID,
		UnitPrice = (
					  SELECT StandardCost 
					  FROM Production.Product
					  WHERE ProductID = @PurchasedProductID
					)
	WHERE PurchaseOrderDetailID = @NewPurchaseOrderDetailID;
	
	COMMIT TRANSACTION;
	PRINT 'Success';

	PRINT 'NewPurchaseOrderID:'
	PRINT @NewPurchaseOrderID;

	PRINT 'NewPurchaseOrderDetailID:'
	PRINT @NewPurchaseOrderDetailID;
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 5. Create new production work order

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewWorkOrderID INT
	INSERT INTO Production.WorkOrder(ProductID, OrderQty, ScrappedQty, StartDate, EndDate,
		DueDate, ScrapReasonID)
	VALUES (316, 20, 2, GETDATE(), '2024-05-18', '2024-05-20', 11);
	SET @NewWorkOrderID = SCOPE_IDENTITY();

	INSERT INTO Production.WorkOrderRouting(WorkOrderID, ProductID, OperationSequence, LocationID, ScheduledStartDate,
		ScheduledEndDate, ActualStartDate, ActualEndDate, PlannedCost, ActualCost)
	VALUES (@NewWorkOrderID, 316, 6, 10, '2024-04-17', (
														 SELECT DueDate
														 FROM Production.WorkOrder
														 WHERE WorkOrderID = @NewWorkOrderID
														), (
														     SELECT StartDate
														     FROM Production.WorkOrder
														     WHERE WorkOrderID = @NewWorkOrderID
													        ), (
															     SELECT EndDate
															     FROM Production.WorkOrder
															     WHERE WorkOrderID = @NewWorkOrderID
															    ), 157.86, 155.25);
	
	COMMIT TRANSACTION;
	PRINT 'Success';

	PRINT 'NewWorkOrderID:'
	PRINT @NewWorkOrderID;
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 6. Update production work order details

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewWorkOrderID INT
	SET @NewWorkOrderID = 72595;

	UPDATE Production.WorkOrder
	SET ScrappedQty = 1
	WHERE WorkOrderID = @NewWorkOrderID;

	INSERT INTO Production.WorkOrderRouting(WorkOrderID, ProductID, OperationSequence, LocationID, ScheduledStartDate,
		ScheduledEndDate, ActualStartDate, ActualEndDate, PlannedCost, ActualCost)
	VALUES (@NewWorkOrderID, 316, 7, 10, '2024-05-21', '2024-05-27', '2024-05-21', '2024-05-30', 84.85, 92.16);
	
	COMMIT TRANSACTION;
	PRINT 'Success';

	PRINT 'Updated NewWorkOrderID:'
	PRINT @NewWorkOrderID;
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 7. Delete production work order

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewWorkOrderID INT
	SET @NewWorkOrderID = 72595;

	DELETE FROM Production.WorkOrderRouting
	WHERE WorkOrderID = @NewWorkOrderID;

	DELETE FROM Production.WorkOrder
	WHERE WorkOrderID = @NewWorkOrderID;
	
	COMMIT TRANSACTION;
	PRINT 'Success';

	PRINT 'Deleted NewWorkOrderID:'
	PRINT @NewWorkOrderID;

	PRINT 'Deleted all operation records'
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH

-- 8. Delete purchase order

BEGIN TRANSACTION;
	
BEGIN TRY
	DECLARE @NewPurchaseOrderID INT
	SET @NewPurchaseOrderID = 4015;

	DECLARE @NewPurchaseOrderDetailID INT
	SET @NewPurchaseOrderDetailID = 8848;

	DELETE FROM Purchasing.PurchaseOrderDetail
	WHERE PurchaseOrderDetailID = @NewPurchaseOrderDetailID;

	DELETE FROM Purchasing.PurchaseOrderHeader
	WHERE PurchaseOrderID = @NewPurchaseOrderID;
	
	COMMIT TRANSACTION;
	PRINT 'Success';

	PRINT 'Deleted NewPurchaseOrderID:'
	PRINT @NewPurchaseOrderID;

	PRINT 'Deleted NewPurchaseOrderDetailID:'
	PRINT @NewPurchaseOrderDetailID;
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION;
	SELECT ERROR_MESSAGE();
END CATCH