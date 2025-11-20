CREATE PROCEDURE [dbo].[DeleteMotor]
	@MotorID INT
	AS
	BEGIN
	     DELETE FROM Motor WHERE MotorID = @MotorID
	END