CREATE PROCEDURE [dbo].[UpdateMotor]
	@MotorID INT,
    @MotorName NVARCHAR (50),
    @Brand NVARCHAR (50),
    @Price NVARCHAR (50),
    @Total NVARCHAR(50)
    AS
    BEGIN
    UPDATE Motor
    SET MotorName = @MotorName,
        Brand = @Brand,
        Price = @Price,
        Total = @Total
    Where MotorID = @MotorID
    END


