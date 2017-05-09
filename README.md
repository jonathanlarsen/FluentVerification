# FluentVerification
A tool to write complicated assertions. 

Currently made for NUnit. All failures throw NUnit AssertionExceptions.

[![Build Status](https://travis-ci.org/LucasObendorf/FluentVerification.svg?branch=master)](https://travis-ci.org/LucasObendorf/FluentVerification)

Examples
==========

Simple Check
------------
~~~csharp
var item = "Test";

Check.That(item)
     .NotNull()
     .EqualTo("Test");
~~~

Object Check
------------
~~~csharp
var item = new {
    A = 1,
    B = "Test"
};

Check.That(item)
     .NotNull()
     .That(x => x.A, a => a.EqualTo(1))
     .That(x => x.B, b => b.NotNull()
                           .EqualTo("Test"));

//Or with a "that" variable

var itemThat = Check.That(item);

itemThat.NotNull();

var aThat = itemThat.That(x => x.A);

aThat.EqualTo(1);

var bThat = itemThat.That(x => x.B);

bThat.NotNull()
     .EqualTo("Test");     
~~~

Lists
-----

### Checking If Empty ###
~~~csharp
var list = new [] { 1, 2, 3 };

Check.That(list)
     .NotNull()
     .NotEmpty();
~~~

### Reasoning About Contents Using With ###
~~~csharp
var list = new [] { "A", "B", "C" };

Check.That(list)
     .With(x => x.AtMost(1, item => item.EqualTo("A"))
                 .All(item => item.NotNull())
                 .None(item => item.EqualTo("D")));

//Or with a "with" variable

var listThat = Check.That(list);

var listWith = listThat.With();

listWith.AtMost(1, item => item.EqualTo("A"));
listWith.All(item => item.NotNull());
listWith.None(item => item.EqualTo("D"));
~~~