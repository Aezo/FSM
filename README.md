# FSM

Match any kind of text based pattern in the input text.

Suppose you need to match "the quick brown fox" in the following lines of text -

```
I am a quick brown fox.
I can be anywhere, you don't know.
You can't find me.
I am the quick brown fox.
```

The normal text matching can happen using Regular Expressions. But often, regex doesn't have the capability to have complex conditional logic. This is where finite state machines (FSM) can help.

In a finite state machine, you can define multiple states, and their transition logic. This way you can create any kind of string matching pattern you want.

For the above example, we can create a finite state machine like this -

[!finite state machine example one](docs/example_01.png)