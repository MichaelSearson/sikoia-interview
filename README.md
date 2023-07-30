# Solution to interview challenge

## Context

See the pdf included in the root of this repository to see the context behind this application.

## Requirements

This solution was build using Visual Studio 2022 and targets .NET 6 so should work pretty much wherever you want to run it. Dependencies have been kept to a minimum in the code but all all restorable via the standard public NuGet feed.

## Assumptions

While developing this I have made a few assumptions that have influence my decisions in terms of business logic / project structure. Happy to discuss the following and any changes I would make if the scenario was to change:

* If there are multiple valid sources of data we will want to return an aggregation - i.e in our unified "Sikoia" model of the data we will strip out the duplicates and have nullable properties that represent data only available from a specific source. I've assumed that we do not want to confuse end users by having multiple "First Name" fields, and would rather be the authority on what "Name" is etc.
    * The logic for aggregation is naive and assumes that if multiple third parties return the same data structure (e.g. name) then we can assume the value will be the same regardless so just picks the first value it finds. In the case of multiple third party specific entries it will just take the first non null value it can find. This is, of course, something that can be extended depending on specific requirements.
* Where we can construct a non-string datatype from the third party JSON we have. E.g. a `DateTime` from `date_established`
* Data that "looks" similar has *not* been assumed to be the same. E.g. `officers` has been assumed to be distinct from `relatedPersons` even though the data is very similar on requests that return data from multiple third parties. Without further requirements I would assume the consumer of the API can determine whether they want to be dealing with one or the other and this prevents unnecessary risk around requirements in one type of data affecting the other if it does turn out they have different meanings/intentions.

## Limitations

The following are some of the known limitations/weaknesses of the solution that have been left either for the sake of simplicity or time:

* Configuration of the custom `HTTPClient` implementations (`ThirdPartyAHttpProvider` and `ThirdPartyBHttpProvider`) has been left at the default. Things like timeout, retry policy etc could all be configured easily enough though in the `ServiceCollectionExtensions` class.
* The parsing logic for the third party API responses is not particularly robust. E.g. I have made assumptions like a non null fulldate we will have non null values for each of its child properties (year/month/day) etc.
    * How much we can trust an endpoint is a matter of discussion, I would assume that ThirdPartyA would be more likely to properly version their endpoints and not break things for consumers, but that's not guaranteed.
    * Having an implementation that tries to parse as much of the response as possible into an internal format so we can try to return as much data as possible in cases where the third party gives us back something unexpected is something I felt too complex for this challenge. It's also something we might not want to do - we may prefer instead to inform our consumers that we can't retrieve the data at the moment and then update our parsing logic to handle the new response explicitly.
* Calling multiple APIs, parsing it all into memory, aggregating the collected data and projecting into to a format that can be exposed to consumers is expensive. Depending on the context building a persisted store of the data and allowing external consumers to read that as the source of truth would be more efficient as we could update this store separately regardless of any requests. The data may be slightly out of data, but this might not matter (again it's context specific). 
* Things like error handling, caching etc are crude and serve to illustrate a point rather than any "real world" example in the interests of time.
* Concerns such as logging have not been implemented in the interest of time.
* Test coverage is low. This is intentional in the interests of time (and to limit the amount of code you have to review). The `Sikoia.Application.UnitTests` project shows an example of the kind of unit tests that should be applied to *most* things in the solution.
    * In addition, in a "real world" scenario I would extend the test coverage to include integration tests that verified the actual logic against stubbed apis mimicking ThirdPartyA and ThirdPartyB. This would allow us to do more comprehensive testing - especially if there were multiple API versions we needed to regression test against. Obviously, we wouldn't want to be reliant on an API we didn't control in these tests.