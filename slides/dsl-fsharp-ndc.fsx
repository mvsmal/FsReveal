(**
- title : Write Your Own Domain Specific Language with F#
- description : Presentation about how readable the code could be
- author : Mikhail Smal
- theme : night
- transition : default

***

## Write Your Own 
## Domain Specific Language
## with F#

<br />
<br />

**Mikhail Smal**

https://smal.dev

@ NDC Oslo 2019

***

My name is Mikhail
and
# I ❤️ F#

---

## What about you?

<aside class="notes">
    Who is working with F#?
    .NET? Java?
</aside>

***



# <img src="images/fsharp_logo.png" class="no-border" width="120" style="margin-bottom: -10px;" /> F#

<ul>
    <li class="fragment">Functional-first language</li>
    <li class="fragment">ADT + pattern matching</li>
    <li class="fragment">Cross-platform thx to .NET Core</li>
    <li class="fragment">Compiles to JS thx to <a href="https://fable.io" target="_blank">Fable</a></li>
</ul>

***

## Why even bother?

---

## Reading the code

## We want to understand the intention

---

### Uncertainity

```csharp
public class OrderController : Controller
{
    public IActionResult Get()
    {
        // Null? Exception?
        List<Order> orders = _ordersService.GetAllOrders();
        return Json(orders);
    }
}

```

---

### Nulls

```csharp
public void CreateOrder(Order order, string userId)
{
    if (order == null)
    {
        throw new ArgumentNullException(nameof(order));
    }

    if (userId == null)
    {
        throw new ArgumentNullException(nameof(userId));
    }

    // Business logic. Finally...
}

```

---

### Keywords noise

<ul>
    <li class="fragment">public</li>
    <li class="fragment">static</li>
    <li class="fragment">readonly</li>
    <li class="fragment">void</li>
    <li class="fragment">class</li>
    <li class="fragment">struct</li>
</ul>

---

### Types

```csharp
public static IEnumerable<TResult> GroupBy<TSource,TKey,TElement,TResult> (
    this IEnumerable<TSource> source,
    Func<TSource,TKey> keySelector,
    Func<TSource,TElement> elementSelector, 
    Func<IEnumerable<TElement>,TResult> resultSelector);
```

***

<img src="images/oop_world.jpg" alt="OOP World" width="800"/>

http://bonkersworld.net/object-world

<aside class="notes">
    OOP patterns, inheritance, abstract factories, DI
</aside>

***

## Too much

```csharp
AbstractToastHeaterGeneratorFactoryInterfaceImplementer
    abstractToastHeaterGeneratorFactoryInterfaceImplementer = 
    new AbstractToastHeaterGeneratorFactoryInterfaceImplementer(
        AbstractToastHeaterGeneratorFactoryInterfaceImplementer.DEFAULT_PARAMS, 0, NULL);
```
<img src="images/abstract_factory.gif" />

***

# So what?

# We are programmers!

***

# There is something wrong...

***

### Simple(?) example

```JavaScript
{
    "thankYou": "Thank you",
    "supportWillContactYou": "Our support team will contact you shortly",
    "welcomeToSurvey": "Welcome to ticket survey",
    "send": "Send",
}
```

---

### Technical naming

<ul>
    <li class="fragment">float?</li>
    <li class="fragment">integer?</li>
    <li class="fragment">Guid?</li>
    <li class="fragment">string?</li>
</ul>

---

## WE SPEAK 

# DIFFERENT LANGUAGUES 

## WITH BUSINESS PEOPLE

---

# BUT DO WE HAVE TO?

<h1 class="fragment" data-fragment-index="1">NO.</h1>

<h2 class="fragment" data-fragment-index="2"><i>But then we need a better programming language</i></h2>

***

## An expressive language

<img src="images/Olga_FSharp_expressive.png" alt="Olga F# as text" />

***

# F# in real life

---

## Modeling with types

---

## Discriminated unions
### aka Sum types

```fsharp
type Currency =
    | USD
    | EUR
    | NOK

let currency = NOK // Currency
```

---

## DUs with extra data

```fsharp
open FSharp.Data.UnitSystems.SI.UnitSymbols

type Precipitation =
    | Rain
    | Snow
    | Hail

type Weather =
    | Sunny
    | Precipitation of Precipitation
    | Windy of float<m/s>

let sunnyWeather = Sunny // Weather
let rainyWeather = Precipitation Rain // Weather
let windyWeather = Windy 15.0<m/s> // Weather
```

---

## Pattern matching

```fsharp
let printWeather weather = // Weather -> unit
    match weather with
    | Sunny -> printfn "Sunny weather!"
    | Precipitation p -> printfn "Weather with %A" p
    | Windy speed -> printfn "Wind speed: %.1f m/s" speed
```

---

## Single case DUs

```fsharp
type CardNumber = CardNumber of string

let cardNumber = CardNumber "XXXX-XXXX-XXXX-XXXX"
```

---

## Records
#### aka Product types

* Reference types compared by value
* Non-nullable
* Must be initialized
* Immutable

---

## Record example

```fsharp
type Person =
    { FirstName : string
      LastName : string
      Age : int }

let person =
    { FirstName = "Mikhail"
      LastName = "Smal"
      Age = 30 }
```

---

### DUs + Records

```fsharp
type CardType = // Union type aka Sum type
    | Visa
    | Mastercard
type CheckNumber = CheckNumber of int
type CardNumber = CardNumber of string

type CreditCardInfo = // Record type aka Product type
    { CardType : CardType
      CardNumber : CardNumber }

type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CreditCardInfo

type PaymentAmount = PaymentAmount of decimal
type Currency = EUR | USD

type Payment =
    { Amount : PaymentAmount
      Currency : Currency
      Method : PaymentMethod }
```

---

### DUs + Records

```fsharp
type [<Measure>] g
type [<Measure>] inch
type Gramms = Gramms of int<g>
type Inches = Inches of float<inch>

type GadgetName = GadgetName of string

type PhoneCode = PhoneCode of string // 5 characters starting with T
type TabletCode = TabletCode of int // 10000 < code < 100000

type GadgetCode =
    | PhoneCode of PhoneCode
    | TabletCode of TabletCode

type Gadget =
    { Code : GadgetCode
      Name : GadgetName
      Weight : Gramms
      ScreenSize : Inches }
```

***

## Different states - Different types

---

## Unvalidated - Validated

```fsharp
type UnvalidatedOrder =
    { ...
      ShippingAddress : UnvalidatedAddress
      ... }

type ValidatedOrder =
    { ...
      ShippingAddress : ValidatedAddress
      ... }

type AddressValidationService = UnvalidatedAddress -> ValidatedAddress option

type Option<'a> =
    | Some of 'a
    | None
```

---

## Explicitly specified Result

```fsharp
type Result<'Success,'Failure> =
    | Ok of 'Success
    | Error of 'Failure

type PaymentError =
    | CardTypeNotRecognized
    | PaymentRejected
    | PaymentProviderOffline

type PayInvoice = UnpaidInvoice -> Payment -> Result<PaidInvoice,PaymentError>

```

---

## Collections in result

```fsharp
type ValidateOrder = UnvalidatedOrder -> Result<ValidatedOrder,ValidationError list>
and ValidationError = {
    FieldName : string
    ErrorDescription : string
}
```

---

## DUs for different states

```fsharp
namespace Cart

type Item = { ... }
type ActiveCartData = { UnpaidItems: Item list }
type PaidCartData = { PaidItems: Item list; Payment: Payment }
type ShoppingCart =
    | EmptyCart // no data
    | ActiveCart of ActiveCartData
    | PaidCart of PaidCartData

let addItem cart item = // ShoppingCart -> Item -> ShoppingCart
    match cart with
    | EmptyCart ->
        // create a new active cart with one item
        ActiveCart { UnpaidItems = [item] }
    | ActiveCart { UnpaidItems = existingItems } ->
        // create a new ActiveCart with the item added
        ActiveCart { UnpaidItems = item :: existingItems }
    | PaidCart _ ->
        // ignore
        cart
```

---

## Functions, not classes

---

## Type inference

```fsharp
let add arg1 arg2 = // int -> int -> int
    arg1 + arg2 // no return keyword

let result = add 100 200 // 300

let addFloats (arg1 : float) (arg2 : float) : float = // float -> float -> float
    arg1 + arg2

type AddFloats = float -> float -> float

let addFloats : AddFloats =
    fun a1 a2 ->
        a1 + a2

let result = addFloats 10. 20.

```

---

## Explicit function signatures

```fsharp
type ValidateOrder = UnvalidatedOrder -> Result<ValidatedOrder, ValidationError>
type PriceOrder = ValidatedOrder -> Result<PricedOrder, PricingError>
type AcknowledgeOrder = PricedOrder -> OrderAcknowledgmentSent option
type CreateEvents = PricedOrder -> OrderAcknowledgmentSent option -> PlaceOrderEvent list

let validateOrder : ValidateOrder =
    fun unvalidatedOrder ->
        ...

let priceOrder : PriceOrder =
    fun validatedOrder ->
        ...

let acknowledgeOrder : AcknowledgeOrder =
    fun pricedOrder ->
        ...

let createEvents : CreateEvents =
    fun pricedOrder aknowledgement ->
        ...
```

---

## Pipe operator
## |>

```fsharp
let optionalAddress : Address option = Some address

Option.isSome optionalAddress
// the same as
optionalAddress |> Option.isSome
```

---

### Composition

```fsharp
let validateOrder unvalidateOrder =
    ...
    Ok validatedOrder

let priceOrder validatedOrder =
    ...
    Ok pricedOrder

let acknowledgeOrder pricedOrder =
    ...
    acknowledgement

let createEvents aknowledgement =
    ...
    events

let placeOrder unvalidatedOrder =
    unvalidatedOrder
    |> validateOrder
    |> priceOrder
    |> acknowledgeOrder
    |> createEvents

```

---

## Binding

```fsharp
let placeOrder unvalidatedOrder =
    unvalidatedOrder
    |> validateOrderAdapted
    |> Result.bind priceOrderAdapted
    |> Result.map acknowledgeOrder
    |> Result.map createEvents
```

---

## Partial application

```fsharp
let add a1 a2 = // int -> int -> int
    a1 + a2

let addTen = add 10 // int -> int

let result = addTen 20 // 30
```

---

### Partial application + Pipe operator

```fsharp
type CheckProductCodeExists = UnvalidatedOrder -> Result<ProductCodeExists, ValidationError>
let checkProductCodeExists : CheckProductCodeExists 
    fun unvalidatedOrder -> ...

type CheckAddressExists = UnvalidatedOrder -> Result<AddressExists, ValidationError>
let checkAddressExists : CheckAddressExists
    fun unvalidatedOrder -> ...

type ValidateOrder =
    CheckProductCodeExists -> CheckAddressExists -> UnvalidatedOrder 
        -> Result<ValidatedOrder, ValidationError list>
let validateOrder : ValidateOrder
    fun checkProductCodeExistsFun checkAddressExistsFun unvalidatedOrder -> ...

let placeOrder unvalidatedOrder =
    // UnvalidatedOrder -> Result<ValidatedOrder, ValidationError list>
    let validateOrder = validateOrder checkProductCodeExists checkAddressExists 
    unvlidatedOrder
    |> validatedOrder
    |> ...
    |> ...
```

---

## Imperative style

```fsharp
type PlaceOrderWorkflow = UnvalidatedOrder -> PlaceOrderEvent list

let placeOrder : PlaceOrderWorkflow =
    fun unvalidatedOrder ->
        let validatedOrder = unvalidatedOrder |> validateOrder
        let pricedOrder = validatedOrder |> priceOrder
        let acknowledgementOption = pricedOrder |> acknowledgeOrder
        let events = createEvents pricedOrder acknowledgementOption
        events
```

***

### Domain Modeling Made Functional

<img src="images/dmmf_wlaschin.jpg" alt="DMMF Scott Wlaschin" width="400"/>

**Scott Wlaschin**

---

### Why F# is the best enterprise language
https://fsharpforfunandprofit.com/posts/fsharp-is-the-best-enterprise-language/

---

<img alt="SAFE" src="images/safe_logo.png" style="background-color: white" />

https://safe-stack.github.io/

***

## Thank you
## Takk

Mikhail Smal

https://smal.dev

*)