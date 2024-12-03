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
QQQJA 483", 5905)]
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
ZZZ = (ZZZ, ZZZ)", 2)]
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
ZZZ = (ZZZ, ZZZ)", 2)]
    [InlineData(@"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)", 6)]
    [InlineData(@"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)", 6)]
    public void Day8Part2Test(string inputData, int resultData)
    {
        // Arrange
        var day = new Day8Part2() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(resultData, result);
    }

    [Theory]
    [InlineData(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45", 114)]
    public void Day9Part1Test(string inputData, int resultData)
    {
        // Arrange
        var day = new Day9Part1() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(resultData, result);
    }

    [Theory]
    [InlineData(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45", 2)]
    public void Day9Part2Test(string inputData, int resultData)
    {
        // Arrange
        var day = new Day9Part2() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(resultData, result);
    }

    [Fact]
    public void Day202401Part1Test()
    {
        // Arrange
        var day = new Day202401Part1() { InputString = @"3   4
4   3
2   5
1   3
3   9
3   3" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(11, result);
    }

    [Fact]
    public void Day202401Part2Test()
    {
        // Arrange
        var day = new Day202401Part2() { InputString = @"3   4
4   3
2   5
1   3
3   9
3   3" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(31, result);
    }

    [Fact]
    public void Day202402Part1Test()
    {
        // Arrange
        var day = new Day202402Part1() { InputString = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
7 6 4 2 3
7 6 4 2 6
5 6 4 2 1
10 6 4 2 1" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Day202402Part2Test()
    {
        // Arrange
        var day = new Day202402Part2() { InputString = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
7 6 4 2 3
7 6 4 2 6
5 6 4 2 1
10 6 4 2 1" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(8, result);
    }

    [Theory]
    [InlineData("10 6 4 2 1")]
    [InlineData("5 6 4 2 1")]
    [InlineData("7 6 4 2 6")]
    [InlineData("7 6 4 2 3")]
    [InlineData("1 3 6 7 9")]
    [InlineData("8 6 4 4 1")]
    [InlineData("1 3 2 4 5")]
    [InlineData("7 6 4 2 1")]
    [InlineData("1 2 3 4 5")]
    [InlineData("1 3 5 7 9")]
    [InlineData("1 4 7 10 13")]
    [InlineData("5 4 3 2 1")]
    [InlineData("9 7 5 3 1")]
    [InlineData("13 10 7 4 1")]
    [InlineData("2 1 3 4 5")]
    [InlineData("2 3 1 4 5")]
    [InlineData("2 3 4 1 5")]
    [InlineData("2 3 4 5 1")]
    [InlineData("1 5 6 7 8")]
    [InlineData("5 1 6 7 8")]
    [InlineData("5 6 1 7 8")]
    [InlineData("5 6 7 1 8")]
    [InlineData("5 6 7 8 1")]
    [InlineData("31 34 36 38 40 43 46 44")]
    [InlineData("13 12 11 9 11")]
    [InlineData("2 1 3 5 8")]
    [InlineData("16 13 15 13 12 11 9 6")]
    [InlineData("17 16 13 15 13 12 11 9 6")]
    [InlineData("80 79 78 75 72 70 71 70")]
    public void Day202402Part2TestSave(string inputString)
    {
        // Arrange
        var day = new Day202402Part2() { InputString = inputString };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData("1 2 7 8 9")]
    [InlineData("9 7 6 2 1")]
    [InlineData("9 7 6 8 7")]
    [InlineData("22 19 18 19 12")]
    [InlineData("67 69 67 67 64 63 61")]
    [InlineData("66 69 69 66 63 61 60 57")]
    public void Day202402Part2TestUnsave(string inputString)
    {
        // Arrange
        var day = new Day202402Part2() { InputString = inputString };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Day202403Part1Test()
    {
        // Arrange
        var day = new Day202403Part1() { InputString = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))" };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(161, result);
    }

    [Theory]
    [InlineData("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48)]
    [InlineData("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))mul(8,5)", 88)]
    [InlineData("xmul(1,1)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(1,1))mul(1,1)", 3)]
    [InlineData("xmul(1,1)&mul[3,7]!^mul(1,1)don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(1,1))mul(1,1)", 4)]
    [InlineData("xmul(1,1)&mul[3,7]!^mul(1,1)don't()_mul(5,5)+mul(32,64](mul(11,8)unmul(1,1)do()?mul(1,1))mul(1,1)", 4)]
    [InlineData("xmul(1,1)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(1,1))mul(1,1)don't()mul(1,1)", 3)]
    //TODO: multiline testcase missing
    public void Day202403Part2Test(string inputData, int expectedResult)
    {
        // Arrange
        var day = new Day202403Part2() { InputString = inputData };

        // Act
        var result = day.Run();
        // Assert
        Assert.Equal(expectedResult, result);
    }
}