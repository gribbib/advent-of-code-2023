namespace tests;

public class UnitTest1
{
    [Fact]
    public void Day6Part1Test()
    {
        // Arrange
        var day = new Day6Part1() { InputString = @"Time:      7  15   30
Distance:  9  40  200" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(288, result);
    }

    [Fact]
    public void Day6Part2Test()
    {
        // Arrange
        var day = new Day6Part2() { InputString = @"Time:      7  15   30
Distance:  9  40  200" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(71503, result);
    }
}