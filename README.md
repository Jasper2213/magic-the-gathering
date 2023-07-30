# Magic The Gathering
## Description
A .NET 6 solution that contains a Web API, a Web Application, a Minimal API, a GraphQL API, focused on the card game Magic The Gathering. 

## Purpose of each project
### Data Access Library  
Contains Entities, DB Context, Repository classes

### Shared Library  
Contains DTO’s, Mappings, Filters, Extension methods, FluentValidation

### Minimal API  
Serves the end points for posting/update/deleting cards in the deck
- The Minimal API supports
  - All POST/PUT/PATCH/DELETE actions on the data source (MongoDB)

### Web API  
Serves the end points for getting the cards data  
- The Web API supports
  - All READ actions to the database
  - Caching for better reading performance (Redis)
  - Paging/Filtering/Sorting  
      Max. page size is set to 150 (in configuration file)
  - Async/await for action methods and Entity Framework operations
  - Versioning
    - Available versions: 1.1 and 1.5
    - Version 1.5 supports sorting and detail of a card

### Web Application Blazor Server  
- Showing, filtering and sorting cards  
  - Shows a list of the first 150 available cards by default at startup
  - Filtering can be done on these criteria:
    - Set
    - Artist
    - Rarity
    - Card Type (short hand)
    - Card name
    - Card Text
  - Sorting can be done on the name of the card (ascending or descending)
  - Details can be shown for each card (description on hover)

- Adding/deleting cards to/from the deck
  - Clicking on the image of a card in the overview makes it part of the deck.
  - Clicking on the line of a card in the deck list removes it from the deck.
  - Clicking on the amount, next to the card line, adds one to this value
  - Adding a card to the deck is no longer allowed when the limit of 60 cards has
  been reached
  - The deck can be cleared

### GraphQL API  
For querying cards and artists  
- These queries are available:  
  - All Cards  
      - Next to the most common fields, you can also query the related artist  
  - All Artists  
      - Next to id and full name, you can also query the related cards  
      - Filter: limit (show the first ... artists)  
  - Artist  
      - Show one specific artist based on id  
      - Filter: id  

## How it works
[Link to YouTube video that shows all functionality](https://www.youtube.com/watch?v=dE-V-4CEZeE)