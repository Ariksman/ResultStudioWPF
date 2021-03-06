﻿using System.IO;
using System.Linq;
using CSharpFunctionalExtensions;

namespace ResultStudioWPF.Domain.DomainModel.ValueObjects
{
  public class FilePath : ValueObject<FilePath>
  {
    public string Value { get; }

    private FilePath(string value)
    {
      Value = value;
    }

    public static Result<FilePath> Create(Maybe<string> filePathOrNothing)
    {
      return filePathOrNothing.ToResult("File path should not be empty")
        .Map(filePath => filePath)
        .Ensure(filePath => File.Exists(filePath), "File does not exists")
        .Ensure(filePath => ContainsOnlyValidCharacters(filePath), "File path contains illegal characters")
        .Map(filePath => new FilePath(filePath));
    }

    public static Result<FilePath> CreateTest(Maybe<string> filePathOrNothing)
    {
      return filePathOrNothing.ToResult("File path should not be empty")
        .Map(filePath => filePath)
        .Ensure(filePath => ContainsOnlyValidCharacters(filePath), "File path contains illegal characters")
        .Map(filePath => new FilePath(filePath));
    }

    protected override bool EqualsCore(FilePath other)
    {
      return Value == other.Value;
    }

    protected override int GetHashCodeCore()
    {
      return Value.GetHashCode();
    }

    /// <summary>
    /// following is valid: 
    /// FilePath path = GetPath(); 
    /// string pathString = path;
    /// </summary>
    /// <param name="filePath"></param>
    public static implicit operator string(FilePath filePath)
    {
      return filePath.Value;
    }


    /// <summary>
    /// may fail, usage can throw exception
    /// string pathString = GetPath();
    /// FilePath path = (FilePath)pathString
    /// </summary>
    /// <param name="filePath"></param>
    public static explicit operator FilePath(string filePath)
    {
      return Create(filePath).Value;
    }

    private static bool ContainsOnlyValidCharacters(string path)
    {
      return Path.GetInvalidPathChars().All(invalidChar => path.Contains(invalidChar) != true);
    }
  }
}