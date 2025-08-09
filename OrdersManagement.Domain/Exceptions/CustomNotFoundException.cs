namespace MyResturants.Domain.Exceptions;

public class CustomNotFoundException(string resourceType , string resourceIdentifier) 
    : Exception($"{resourceType} with Id : {resourceIdentifier} doesn't exist .")
{
}