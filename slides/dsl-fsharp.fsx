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

@ DevDays Europe 2019

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

***

<img src="images/oop_world.jpg" alt="OOP World" width="800"/>

http://bonkersworld.net/object-world

<aside class="notes">
    OOP patterns, inheritance, abstract factories, DI
</aside>

***

```csharp
AbstractToastHeaterGeneratorFactoryInterfaceImplementer
    abstractToastHeaterGeneratorFactoryInterfaceImplementer = 
    new AbstractToastHeaterGeneratorFactoryInterfaceImplementer(
        AbstractToastHeaterGeneratorFactoryInterfaceImplementer.DEFAULT_PARAMS, 0, NULL);
```
<img src="images/abstract_factory.gif" />

***

```JavaScript
{
    "thankYou": "Thank you",
    "supportWillContactYou": "Our support team will contact you shortly",
    "welcomeToSurvey": "Welcome to ticket survey",
    "send": "Send",
}
```

---

### Different languages

<ul>
    <li class="fragment">float?</li>
    <li class="fragment">integer?</li>
    <li class="fragment">Guid?</li>
    <li class="fragment">string?</li>
</ul>

***

<img src="images/Olga_FSharp_expressive.png" alt="Olga F# as text" />

***

## Modeling with types

---

```fsharp
type CardType = // 'OR' type
    | Visa
    | Mastercard
type CheckNumber = CheckNumber of int
type CardNumber = CardNumber of string

type CreditCardInfo = { // 'AND' type (record)
    CardType : CardType
    CardNumber : CardNumber
}

type PaymentMethod =
    | Cash
    | Check of CheckNumber
    | Card of CreditCardInfo

type PaymentAmount = PaymentAmount of decimal
type Currency = EUR | USD

type Payment = {
    Amount : PaymentAmount
    Currency : Currency
    Method : PaymentMethod
}
```

---

```fsharp
namespace Domain

type [<Measure>] g
type [<Measure>] inch
type Gramms = Gramms of int<g>
type Inches = Inches of float<inch>

type GadgetName = GadgetName of string

// 5 characters starting with T
type PhoneCode = PhoneCode of string
// 10000 < code < 100000
type TabletCode = TabletCode of int

type GadgetCode =
    | PhoneCode of PhoneCode
    | TabletCode of TabletCode

type Gadget = {
    Code : GadgetCode
    Name : GadgetName
    Weight : Gramms
    ScreenSize : Inches
}

```

---

```fsharp
type UnvalidatedOrder = {
    ...
    ShippingAddress : UnvalidatedAddress
    ...
}

type ValidatedOrder = {
    ...
    ShippingAddress : ValidatedAddress
    ...
}

type AddressValidationService = UnvalidatedAddress -> ValidatedAddress option

type Option<'a> =
    | Some of 'a
    | None
```

---

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

```fsharp
type ValidateOrder = UnvalidatedOrder -> Result<ValidatedOrder,ValidationError list>
and ValidationError = {
    FieldName : string
    ErrorDescription : string
}
```

---

```fsharp
namespace Cart

type Item = ...
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

```fsharp
let placeOrder unvalidatedOrder =
    unvalidatedOrder
    |> validateOrderAdapted
    |> Result.bind priceOrderAdapted
    |> Result.map acknowledgeOrder
    |> Result.map createEvents
```

---

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

---

```fsharp
let checkProductCodeExists unvalidatedOrder =
    ...

let checkAddressExists unvalidatedOrder =
    ...

let validateOrder checkProductCodeExistsFun checkAddressExistsFun unvalidatedService =
    ...

let placeOrder unvalidatedOrder =
    let validateOrder = validateOrder checkProductCodeExists checkAddressExists
    unvlidatedOrder
    |> validatedOrder
    |> ...
    |> ...
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
## Ačiū

Mikhail Smal

https://smal.dev

*)