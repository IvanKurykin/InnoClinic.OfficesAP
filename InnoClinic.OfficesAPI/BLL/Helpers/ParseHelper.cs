using System.Diagnostics.CodeAnalysis;
using BLL.Exceptions;
using MongoDB.Bson;

namespace BLL.Helpers;

public static class ParseHelper
{
    public static ObjectId ParseId(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId)) throw new InvalidIdException();

        return objectId;
    }
}