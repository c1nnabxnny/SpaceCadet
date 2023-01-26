using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.RTree;
using coordinates;

class RtreeExample
{
    static void Main(string[] args)
    {
        // Create an R-tree
        var rtree = new RTree<Coordinate>();

        // import coordinates
        double[][] coordinates = Coordinates.coordinates;
        // Insert items into the R-tree
        for (int i = 0; i < coordinates.GetLength(0); i++)
        {
            rtree.Insert(new Envelope(coordinates[i][0], coordinates[i][1], coordinates[i][2], coordinates[i][3]), new Coordinate(coordinates[i][0], coordinates[i][1], coordinates[i][2]));
        }
        // Now you can use the `rtree` to perform spatial queries.
        Console.WriteLine(rtree);
        //Retrieving the player's current location
        var playerLocation = GetPlayerLocation();
        //Searching by point
        var searchRadius = 10;
        var results = rtree.Search(new Envelope(playerLocation.X - searchRadius, playerLocation.Y - searchRadius, playerLocationZ - searchRadius, playerLocation.X + searchRadius, playerLocation.Y + searchRadius, playerLocationZ + searchRadius));
        Console.WriteLine("Results for search point:" + playerLocation);
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
    // Function to retrieve the player's location
    public static Coordinate GetPlayerLocation()
    {
        //Retrieve player location from game
        return new Coordinate(playerLocationX, playerLocationY, playerLocationZ);
    }
}
