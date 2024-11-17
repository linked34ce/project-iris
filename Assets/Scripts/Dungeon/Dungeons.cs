using System.Collections.Generic;

public class Dungeons
{
    public static Dictionary<string, Walls[][][]> Maps { get; } = new(){
       {
            "To-o Gakuen",
            new Walls[][][]{
                // 1F
                new Walls[][] {
                    // 1
                    new Walls[] {
                        new(0, 1, 1, 0), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 0),
                        new(0, 1, 0, 0), new(0, 1, 0, 1), new(0, 1, 0, 1), new(1, 1, 0, 1), new(1, 1, 1, 0),
                    },
                    // 2
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 1, 0), new(1, 1, 0, 0), new(1, 1, 1, 0), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 1, 1, 0), new(0, 1, 1, 0), new(1, 1, 0, 0), new(1, 0, 1, 0),
                    },
                    // 3
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 0, 1, 1), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 0, 1, 1), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 0, 1, 0),
                    },
                    // 4
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 1), new(0, 0, 0, 1), new(0, 1, 0, 1), new(0, 0, 0, 0),
                        new(0, 0, 0, 0), new(0, 1, 0, 1), new(0, 0, 0, 1), new(1, 0, 0, 1), new(1, 0, 1, 0),
                    },
                    // 5
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 0, 0), new(0, 1, 0, 1), new(1, 1, 0, 1), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(0, 1, 1, 1), new(0, 1, 0, 1), new(1, 1, 0, 1), new(1, 0, 1, 0),
                    },
                    // 6
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 1, 0), new(0, 1, 0, 0), new(0, 1, 0, 1), new(0, 0, 0, 0),
                        new(0, 0, 0, 0), new(0, 1, 0, 1), new(0, 1, 0, 0), new(1, 1, 0, 0), new(1, 0, 1, 0),
                    },
                    // 7
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 1, 1, 0), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 1, 1, 0), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 0, 1, 0),
                    },
                    // 8
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 1), new(1, 0, 0, 1), new(1, 0, 1, 1), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 0, 1, 1), new(0, 0, 1, 1), new(1, 0, 0, 1), new(1, 0, 1, 0),
                    },
                    // 9
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 1, 1), new(0, 1, 2, 1), new(0, 1, 0, 1), new(0, 0, 0, 1),
                        new(0, 0, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(1, 1, 0, 1), new(1, 0, 1, 0),
                    },
                    // 10
                    new Walls[] {
                        new(0, 0, 1, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1),
                        new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(1, 0, 0, 1),
                    },
                },
                // 2F
                new Walls[][] {
                    // 1
                    new Walls[] {
                        new(0, 1, 1, 0), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 0),
                        new(0, 1, 0, 0), new(0, 1, 0, 1), new(0, 1, 0, 1), new(1, 1, 0, 1), new(1, 1, 1, 0),
                    },
                    // 2
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 1, 0), new(1, 1, 0, 0), new(1, 1, 1, 0), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 1, 1, 0), new(0, 1, 1, 0), new(1, 1, 0, 0), new(1, 0, 1, 0),
                    },
                    // 3
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 0, 1, 1), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 0, 1, 1), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 0, 1, 0),
                    },
                    // 4
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 1), new(0, 0, 0, 1), new(0, 1, 0, 1), new(0, 0, 0, 0),
                        new(0, 0, 0, 0), new(0, 1, 0, 1), new(0, 0, 0, 1), new(1, 0, 0, 1), new(1, 0, 1, 0),
                    },
                    // 5
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 0, 0), new(0, 1, 0, 1), new(1, 1, 0, 1), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(0, 1, 1, 1), new(0, 1, 0, 1), new(1, 1, 0, 1), new(1, 0, 1, 0),
                    },
                    // 6
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 1, 0), new(0, 1, 0, 0), new(0, 1, 0, 1), new(0, 0, 0, 0),
                        new(0, 0, 0, 0), new(0, 1, 0, 1), new(0, 1, 0, 0), new(1, 1, 0, 0), new(1, 0, 1, 0),
                    },
                    // 7
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 1, 1, 0), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 1, 1, 0), new(0, 0, 1, 0), new(1, 0, 0, 0), new(1, 0, 1, 0),
                    },
                    // 8
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 0, 1, 1), new(1, 0, 0, 1), new(1, 0, 1, 1), new(0, 0, 1, 0),
                        new(1, 0, 0, 0), new(1, 0, 1, 1), new(0, 0, 1, 1), new(1, 0, 0, 1), new(1, 0, 1, 0),
                    },
                    // 9
                    new Walls[] {
                        new(1, 0, 1, 0), new(0, 1, 1, 1), new(0, 1, 2, 1), new(0, 1, 0, 1), new(0, 0, 0, 1),
                        new(0, 0, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(1, 1, 0, 1), new(1, 0, 1, 0),
                    },
                    // 10
                    new Walls[] {
                        new(0, 0, 1, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1),
                        new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(0, 1, 0, 1), new(1, 0, 0, 1),
                    },
                },
            }
        },
    };

    public static Dictionary<string, float> EncountRates { get; } = new(){
        {"To-o Gakuen", 0.1f},
    };

    public static Dictionary<string, int[][]> InitialPositions { get; } = new(){
        {
            "To-o Gakuen",
            new int[][]{
                new int[]{9, 0}, new int[]{9, 0},
            }
        }
    };

    public static Dictionary<string, string> DisplayNames { get; } = new() {
        {"To-o Gakuen", "桃鳳学園 旧校舎"},
    };
}
