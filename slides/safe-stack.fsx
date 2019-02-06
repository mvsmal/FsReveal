(**
- title : Web development with SAFE stack 
- description : Presentation about web devepment in F# with SAFE stack
- author : Mikhail Smal
- theme : beige
- transition : default

***

## Web development with SAFE stack

![SAFE](images/safe_logo.png)

Mikhail Smal @ F(by) 2019

[@mvsmal](https://twitter.com/mvsmal)

***

My name is Mikhail
and
# I ❤️ F#

***

## What about you?

<aside class="notes">
    Who is working with F#?
    Who is Full stack developer? Backend? Frontend?
</aside>

***

## Why F#

<ul>
    <li class="fragment">
        Functional language
        <ul>
            <li>Immutability</li>
            <li>Pattern matching</li>
            <li>Static typing</li>
            <li>Algebraic data-types</li>
        </ul>
    </li>
    <li class="fragment">Domain Driven Design</li>
</ul>

<br/> <br/> <br/> <br/>

---

### Domain Modeling Made Functional

<img src="images/dmmf_wlaschin.jpg" alt="DMMF Scott Wlaschin" width="400"/>

**Scott Wlaschin**

---

### Why F# is the best enterprise language
https://fsharpforfunandprofit.com/posts/fsharp-is-the-best-enterprise-language/

***

## Why F#

<ul>
    <li>
        Functional language
        <ul>
            <li>Immutability</li>
            <li>Pattern matching</li>
            <li>Static typing</li>
            <li>Algebraic data-types</li>
        </ul>
    </li>
    <li>Domain Driven Design</li>
    <li class="fragment">Cross-platform .NET Core</li>
    <li class="fragment">Single language for Backend/Frontend</li>
    <li class="fragment">Awesome community ️❤️</li>
</ul>

<aside class="notes">
    FP devs not willing to consider F# (.NET from MS)
</aside>

***

<img src="images/safe_small.png" alt="SAFE" class="no-border" width="400" />

The SAFE stack is an **open-source, free, flexible** end-to-end, **functional-first** stack for **cloud-ready web applications** that emphasizes **type-safe programming**.

<table class="safe-stack">
    <tr>
        <td><img src="images/saturn_logo.png" alt="Saturn" width="150"/></td>
        <td><img src="images/azure_logo.png" alt="Azure" width="150"/></td>
        <td><img src="images/fable_logo.png" alt="Fable" width="150"/></td>
        <td><img src="images/elmish_logo.png" alt="Elmish" width="150"/></td>
    </tr>
    <tr>
        <td><h3><u>S</u>aturn</h3></td>
        <td><h3><u>A</u>zure</h3></td>
        <td><h3><u>F</u>able</h3></td>
        <td><h3><u>E</u>lmish</h3></td>
    </tr>
</table>

<aside class="notes">
    The SAFE stack is an **open-source, free, flexible** end-to-end, **functional-first** stack for **cloud-ready web applications** that emphasizes **type-safe programming**.<br>
    <br>
    Is not smth special, not a custom implementation of some framework. It is not a framework. It is just a set of tools which simplifies end to end web development with the focus on type safety. With SAFE we just take an existing technology and use it from F#.
    <br>
    libuv - cross-platform async IO libraby
</aside>

***

<div>
    <h2 class="flex vertical-middle align-center">
        <img src="images/saturn_logo.png" alt="Saturn" width="80" class="no-border margin-right-2rem" />
        Saturn
    </h2>
    A modern web framework that focuses on developer productivity, performance, and maintainability
    <div class="margin-top-2rem flex flex-horizontal">
        <div class="flex1">
            <h3>Rings</h3>
            <ul>
                <li>Kestrel and ASP.NET Core</li>
                <li>Giraffe</li>
            </ul>
        </div>
        <div class="flex1">
                <h3>Moons</h3>
                <ul>
                    <li>Dapper</li>
                    <li>Simple.Migrations</li>
                </ul>
        </div>
    </div>
</div>


<div class="fragment fade-up darken-bg padding-md margin-top-2rem">
    <h3>Alternatives</h3>
    <div class="flex flex-horizontal">
        <div class="flex1">
            <ul>
                <li>Giraffe</li>
                <li>ASP.NET Core</li>
            </ul>
        </div>
        <div class="flex1">
            <ul>
                <li>Suave</li>
                <li>Freya</li>
            </ul>
        </div>
    </div>
</div>

<aside class="notes">
    A modern web framework that focuses on developer productivity, performance, and maintainability
    <p>
        Simple and performant object mapper for .NET
    </p>
    <p>
        Super simple framework for database versioning. Doesn't generate SQL, just provides a set of composable tools for integrating migrations into your application
    </p>
</aside>

---

### Saturn framework

Heavily inspired by __Elixir's Phoenix__

<br>

Familiar abstractions:

* `application { }`
* `router (*scope*) { }`
* `controller { }`

---

### Saturn template

```shell
dotnet new -i Saturn.Template
dotnet new saturn -lang F#
```

### Saturn cli-tool

```shell
dotnet saturn gen Book Books id:string title:string author:string
dotnet saturn migration
```

<aside class="notes">
    This will create Books folder with Models with these properties,
    controller with CRUD, db access, migration, view
</aside>

***

<h2 class="flex vertical-middle align-center">
    <img src="images/azure_logo.png" alt="Azure" width="80" class="no-border margin-right-2rem" />
    Microsoft Azure
</h2>

* Top 3 Cloud hosting platforms
* Native .NET environment

<div class="fragment fade-up darken-bg padding-md margin-top-2rem">
    <h3>Alternatives</h3>
    <div class="flex flex-horizontal">
        <div class="flex1">
            <ul>
                <li>Amazon Web Services<br/>(AWS)</li>
                <li>Google Cloud Platform<br/>(GCP)</li>
            </ul>
        </div>
        <div class="flex1">
            <ul>
                <li>Any other hosting</li>
            </ul>
        </div>
    </div>
</div>

---

## SAFE in Docker

* `microsoft/dotnet:2.2-sdk`
* `microsoft/dotnet:2.2-aspnetcore-runtime`
* `microsoft/dotnet:2.2-runtime`
* `-`
* `microsoft/dotnet:2.2-sdk-alpine`
* `microsoft/dotnet:2.2-aspnetcore-runtime-alpine`
* `microsoft/dotnet:2.2-runtime-alpine`

***

<h2 class="flex vertical-middle align-center">
    <img src="images/fable_logo.png" alt="Fable" width="80" class="no-border margin-right-2rem" />
    Fable
</h2>

The compiler that emits JavaScript you can be proud of!

* F# -> JavaScript compiler
* JavaScript as runtime
* Simple interop with JavaScript
* ReactJS and React Native
* SSR w/o Node.js

<div class="fragment fade-up darken-bg padding-md margin-top-2rem">
    <h3>Alternatives</h3>
    <ul>
        <li>WebSharper</li>
    </ul>
</div>

---

## Fable compiler

<div class="fragment fade-down fsharp">F#</div>
<div class="fragment fade-down fable">Fable</div>
<div class="fragment fade-down babel">Babel</div>
<div class="fragment fade-down javascript">ECMAScript 5<div>

***

<h2 class="flex vertical-middle align-center">
    <img src="images/elmish_logo.png" alt="Elmish" width="80" class="no-border margin-right-2rem" />
    Elmish
</h2>

* Borrowed from Elm language
* MVU Architecture
* Hot module replacement

<aside class="notes">
    HRM thanks to webpack
</aside>

---

<h2 class="flex vertical-middle align-center">
    Model-View-Update
</h2>

<img src="images/mvu.png" alt="Model View Update" class="no-border" style="width: 90%" />

***

## Client-Server communtication

---

## Fable.Remoting

Type-safe communication layer for F# Apps

* RPC-style
* Adapters for all SAFE servers and more

---

## Elmish.Bridge

A bridge between server and client using websockets

* Web sockets
* Bi-directional communication

***

## SAFE Pre-requisites

* .NET Core SDK 2.x
* Node.js 8.x+
* Yarn/NPM
* FAKE 5 _as a dotnet global tool_
* Mono _(for macOS/Linux only)_

***

## SAFE Template

```shell
dotnet new -i SAFE.Template
dotnet new SAFE
dotnet new SAFE --server giraffe --communication remoting
dotnet new SAFE --layout fulma-landing
dotnet new SAFE --deploy docker
dotnet new SAFE --js-deps npm
```

<aside class="notes">
    <b>Servers</b>: saturn(d), giraffe, suave<br>
    <b>Layout</b>: Fulma-basic(d), admin, cover, hero, landing, login<br>
    <b>Communication</b>: none(d) & remoting<br>
    <b>Deploy</b>: none(d), docker, azure(Azure Resource manager template for AppService and AI), gcp (AppEngine)<br>
    <b>JS Deps</b>: yarn(d) & npm (need npx)
</aside>

***

# DEMO

***

![percent](./images/percent.png)

***

## Wrapping up

* Performance thanks to .NET Core
* Full-stack out-of-box
* Single type-safe functional language

***

# QUESTIONS
# ???

***

![](./images/thanks.jpg)

## Mikhail Smal
[@mvsmal](https://twitter.com/mvsmal)

*)