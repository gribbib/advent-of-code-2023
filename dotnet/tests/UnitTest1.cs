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

    [Fact]
    public void Day7Part1Test()
    {
        // Arrange
        var day = new Day7Part1() { InputString = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(6440, result);
    }

    [Theory]
    [InlineData(@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483",5905)]
    [InlineData(@"2345A 1
Q2KJJ 13
Q2Q2Q 19
T3T3J 17
T3Q33 11
2345J 3
J345A 2
32T3K 5
T55J5 29
KK677 7
KTJJT 34
QQQJA 31
JJJJJ 37
JAAAA 43
AAAAJ 59
AAAAA 61
2AAAA 23
2JJJJ 53
JJJJ2 41", 6839)]
    public void Day7Part2Test(string inputData, int resultData)
    {
        // Arrange
        var day = new Day7Part2() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(resultData, result);
    }

    [Theory]
    [InlineData(@"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)",2)]
    [InlineData(@"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)", 6)]
    public void Day8Part1Test(string inputData, int resultData)
    {
        // Arrange
        var day = new Day8Part1() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(resultData, result);
    }

    [Theory]
    [InlineData(@"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)",2)]
    [InlineData(@"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)", 6)]
    public void Day8Part2Test(string inputData, int resultData)
    {
        // Arrange
        var day = new Day8Part1() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(resultData, result);
    }
}