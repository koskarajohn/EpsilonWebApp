IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 1')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 1', N'Test Address 1', '1111', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 2')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 2', N'Test Address 2', '12222', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 3')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 3', N'Test Address 3', '33333', GETUTCDATE() )


IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 4')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 4', N'Test Address 4', '4444', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 5')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 5', N'Test Address 5', '5555', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 6')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 6', N'Test Address 6', '66666', GETUTCDATE() )



IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 7')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 7', N'Test Address 7', '7777', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 8')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 8', N'Test Address 8', '88888', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 9')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 9', N'Test Address 9', '99999', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 10')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 10', N'Test Address 19', '1234567', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 11')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 11', N'Test Address 11', '232323223', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 12')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 12', N'Test Address 12', '3434343434', GETUTCDATE() )

  IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 13')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 13', N'Test Address 13', '56565656', GETUTCDATE() )

IF NOT EXISTS ( SELECT * FROM Customer WHERE ContactName = N'Test Customer 14')
  INSERT INTO Customer ( Id, ContactName, Address, Phone, CreatedDate) VALUES ( NEWID(), N'Test Customer 14', N'Test Address 14', '8989898989', GETUTCDATE() )