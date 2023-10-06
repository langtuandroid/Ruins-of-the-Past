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
        Assert.IsTrue(TextUtility.isWhitespace(' '));
    }

    [Test]
    public void IsWhitespaceShouldBeTrueAscii()
    {
        Assert.IsTrue(TextUtility.isWhitespace('\u0020'));
    }

    [Test]
    public void IsWhitespaceShouldBeFalseCharacter()
    {
        Assert.IsFalse(TextUtility.isWhitespace('a'));
    }

    [Test]
    public void IsWhitespaceShouldBeFalseSymbol()
    {
        Assert.IsFalse(TextUtility.isWhitespace('„'));
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeEmptyString() {
        string result = TextUtility.RemoveSpecialCharacters("!@#$%^&*()");
        Assert.IsTrue(result.Length == 0);
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeSame()
    {
        string result = TextUtility.RemoveSpecialCharacters("abcDEF123");
        Assert.IsTrue(result == "abcDEF123");
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeRemoved()
    {
        string result = TextUtility.RemoveSpecialCharacters("test{€#&^");
        Assert.IsTrue(result == "test");
    }

    [Test]
    public void RemoveSpecialCharactersShouldBeWhitespace()
    {
        string result = TextUtility.RemoveSpecialCharacters(" ");
        Assert.IsTrue(result == " ");
    }

    [Test]
    public void RemoveWhitespacesShouldBeEmpty()
    {
        string result = TextUtility.RemoveWhitespaces("        ");
        Assert.IsTrue(result.Length == 0);
    }

    [Test]
    public void RemoveWhitespacesShouldBeSame()
    {
        string result = TextUtility.RemoveWhitespaces("testing");
        Assert.IsTrue(result == "testing");
    }

    [Test]
    public void RemoveWhitespacesShouldRemove()
    {
        string result = TextUtility.RemoveWhitespaces("this is a test");
        Assert.IsTrue(result == "thisisatest");
    }
}
