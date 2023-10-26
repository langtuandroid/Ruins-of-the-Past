using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class TextUtilityTests
{
    [Test]
    public void IsWhitespaceShouldBeTrueSpace()
    {
        Assert.IsTrue(' '.isWhitespace());
    }

    [Test]
    public void IsWhitespaceShouldBeTrueAscii()
    {
        Assert.IsTrue('\u0020'.isWhitespace());
    }

    [Test]
    public void IsWhitespaceShouldBeFalseCharacter()
    {
        Assert.IsFalse('a'.isWhitespace());
    }

    [Test]
    public void IsWhitespaceShouldBeFalseSymbol()
    {
        Assert.IsFalse('�'.isWhitespace());
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeEmptyString() {
        var result = "!@#$%^&*()".RemoveSpecialCharacters();
        Assert.IsTrue(result.Length == 0);
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeSame()
    {
        var result = "abcDEF123".RemoveSpecialCharacters();
        Assert.IsTrue(result == "abcDEF123");
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeRemoved()
    {
        var result = "test{�#&^".RemoveSpecialCharacters();
        Assert.IsTrue(result == "test");
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeWhitespace()
    {
        var result = " ".RemoveSpecialCharacters();
        Assert.IsTrue(result == " ");
    }

    [Test]
    public void RemoveWhitespacesShouldBeEmpty()
    {
        var result = "        ".RemoveWhitespaces();
        Assert.IsTrue(result.Length == 0);
    }

    [Test]
    public void RemoveWhitespacesShouldBeSame()
    {
        var result = "testing".RemoveWhitespaces();
        Assert.IsTrue(result == "testing");
    }

    [Test]
    public void RemoveWhitespacesShouldRemove()
    {
        var result = "this is a test".RemoveWhitespaces();
        Assert.IsTrue(result == "thisisatest");
    }
}
