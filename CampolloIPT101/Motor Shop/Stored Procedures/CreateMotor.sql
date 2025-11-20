CREATE Procedure [dbo].[CreateMotor]
	@MotorName NVARCHAR(50),
	@Brand NVARCHAR(50),
	@Price NVARCHAR(50),
	@Total NVARCHAR(50)
	AS
	BEGIN
         INSERT INTO Motor (MotorName, Brand, Price, Total)
		 VALUES (@MotorName, @Brand, @Price, @Total)
	END
