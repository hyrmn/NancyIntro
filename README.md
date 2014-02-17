Nancy Intro
============

This contains my slides and demo applications from my intro to Nancy talk at the local .net user group meeting.

Rough outline while I work on slides....

- Nancy is a lightweight web framework for .net
- It has sensible defaults and conventions designed to stay out of your way; it's low-ceremony (meaning you write less code)
- It's also pluggable and its defaults can be overridden with your own
- It supports multiple view engines. It comes with its own super simple view engine but you can easily replace it with your own (or add multiple!).
- Content negotiation
- Can be self-hosted or it can run on OWIN or asp.net + IIS
- It can even be hosted via scriptcs
- Testing is a first-level concern. This means you can do test-driven development from the client forward (this is a huge painpoint in asp.net and asp.net mvc. You can test controllers there but you can't easily test all of the other 'stuff' that gets instantiated within the context of a request)
- Dependency Injection is a first-level concern. Nancy comes with its own lightweight IoC container for dependency injection but you can easily replace it with your own.

Sample app we'll be building up:

WhatTheNancy; a single-purpose website for generating commit messages.

- Home page that shows a random commit message; should render as HTML, json, text, xml
- About page
- Secured page for adding messages

Modules...

Your modules will inherit from the NancyModule. You can call these anything you want. Common practice (but not necessary) is to append Module. Like controllers in asp.net, these are where you'll do most of your request handling.

Testing

It's generally a good idea anyway to keep your tests in a different assembly than your production code. This is especially true in Nancy since, by default, it auto-discovers types and you don't want your test junk and production junk mixed together.

Nancy.Testing comes with a very useful class called Browser. We'll use this to simulate requests to Nancy. Note: The requests aren't going over HTTP, they're hitting Nancy at the earliest point it handles the request.

The benefit of this is that it... 1) makes the tests very fast (no HTTP) while 2) exercises the entire request pipeline within Nancy (think to how you might test an asp.net mvc controller that has action attributes on it)

